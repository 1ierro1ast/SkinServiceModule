using System;
using System.Collections.Generic;
using System.Linq;
using Codebase.Infrastructure.Services.AssetManagement;
using Codebase.Infrastructure.Services.DataStorage;
using Codebase.Infrastructure.Services.SaveLoad;
using Random = UnityEngine.Random;

namespace Codebase.SkinServiceModule
{
    public class SkinService : ISkinService
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IGameVariables _gameVariables;
        private readonly ISaveLoadService _saveLoadService;
        private SkinData[] _skinDatas;
        private Dictionary<int, SkinData> _skinTable;
        
        private SkinData _currentSuitSkin;

        private bool _newSkinUnlocked;

        public event Action<SkinData> SkinUpdated;
        
        public SkinData CurrentSuitSkin => _currentSuitSkin;

        public bool NewSkinUnlocked => _newSkinUnlocked;

        public SkinService(IAssetProvider assetProvider, IGameVariables gameVariables, ISaveLoadService saveLoadService)
        {
            _assetProvider = assetProvider;
            _gameVariables = gameVariables;
            _saveLoadService = saveLoadService;
            
            LoadSkinDatas();
            LoadCurrentSkins();
        }

        private void LoadSkinDatas()
        {
            var skinDatas = _assetProvider.GetAllScriptableObjects<SkinData>(AssetPath.SkinDatasPath);
            
            _skinDatas = GetSkinDatas(0, skinDatas);

            _skinTable = new Dictionary<int, SkinData>();
            
            foreach (var skinData in skinDatas)
            {
                var sd = LoadSkinData(skinData);
                _skinTable.Add(skinData.Id, sd);
            }
        }

        private void LoadCurrentSkins()
        {
            _currentSuitSkin = GetSkinData(_saveLoadService.LoadInt("CurrentSuitSkinId", 0));
        }

        private void SaveSkinData(SkinData skinData)
        {
            _saveLoadService.SaveBool($"isAvailableForPurchase_{skinData.Name}_{skinData.Id}", skinData.IsAvailableForPurchase);
            _saveLoadService.SaveBool($"isUnlocked_{skinData.Name}_{skinData.Id}", skinData.IsUnlocked);
        }

        private SkinData LoadSkinData(SkinData skinData)
        {
            skinData.IsAvailableForPurchase =
                _saveLoadService.LoadBool($"isAvailableForPurchase_{skinData.Name}_{skinData.Id}", skinData.IsAvailableForPurchase);
            skinData.IsUnlocked =
                _saveLoadService.LoadBool($"isUnlocked_{skinData.Name}_{skinData.Id}", skinData.IsUnlocked);
            return skinData;
        }

        public SkinData[] GetSkinDatas(SkinType skinType, SkinData[] skinDatas)
        {
            return skinDatas.Where(item => item.SkinType == skinType).ToArray();
        }
        
        public SkinData[] GetSkinDatas(SkinType skinType)
        {
            switch (skinType)
            {
                case 0:
                    return _skinDatas;
            }

            return _skinDatas;
        }

        public SkinData GetRandomSkinData()
        {
            return _skinTable[Random.Range(0, _skinTable.Count-1)];
        }

        public void SetSkinAvailableForPurchase(int id)
        {
            var skinData = GetSkinData(id);
            skinData.IsAvailableForPurchase = true;
            _newSkinUnlocked = true;
            SaveSkinData(skinData);
        }

        public void UnlockSkin(int id)
        {
            SkinData skinData = GetSkinData(id);
            skinData.IsAvailableForPurchase = true;
            skinData.IsUnlocked = true;
            _newSkinUnlocked = true;
            SaveSkinData(skinData);
        }
        
        public void UnlockSkin(SkinData skinData)
        {
            skinData.IsAvailableForPurchase = true;
            skinData.IsUnlocked = true;
            _newSkinUnlocked = true;
            SaveSkinData(skinData);
        }

        public void DisableNewSkinFlag()
        {
            _newSkinUnlocked = false;
        }

        public bool TryBuySkin(int id)
        {
            SkinData skinData = GetSkinData(id);
            if (_gameVariables.TrySpendCoins(skinData.ScoreCost))
            {
                UnlockSkin(skinData);
                return true;
            }

            return false;
        }

        public void SetSkin(SkinData itemTemplate)
        {
            switch (itemTemplate.SkinType)
            {
                case 0:
                    _currentSuitSkin = itemTemplate;
                    _saveLoadService.SaveInt("CurrentSuitSkinId", _currentSuitSkin.Id);
                    break;
            }
            SkinUpdated?.Invoke(itemTemplate);
        }

        public SkinData GetSkinData(int id) => _skinTable[id];
    }
}