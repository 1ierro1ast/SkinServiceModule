using System;
using Codebase.Infrastructure.Services;

namespace Codebase.SkinServiceModule
{
    public interface ISkinService : IService
    {
        public SkinData CurrentSuitSkin { get; }
        public bool NewSkinUnlocked { get; }
        
        public event Action<SkinData> SkinUpdated;
        
        SkinData[] GetSkinDatas(SkinType skinType, SkinData[] skinDatas);
        SkinData[] GetSkinDatas(SkinType skinType);
        SkinData GetSkinData(int id);
        SkinData GetRandomSkinData();
        void SetSkinAvailableForPurchase(int id);
        void UnlockSkin(int id);
        void UnlockSkin(SkinData skinData);
        void DisableNewSkinFlag();
        bool TryBuySkin(int id);
        void SetSkin(SkinData itemTemplate);
    }
}
