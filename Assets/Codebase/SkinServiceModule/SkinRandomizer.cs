using Codebase.Infrastructure.Services;
using UnityEngine;

namespace Codebase.SkinServiceModule
{
    public class SkinRandomizer : MonoBehaviour
    {
        private ISkinService _skinService;
        private SkinLoader _skinLoader;

        private void Awake()
        {
            _skinService = AllServices.Container.Single<ISkinService>();
            _skinLoader = GetComponent<SkinLoader>();
            Load();
        }

        public void Load()
        {
            if(_skinService == null) _skinService = AllServices.Container.Single<ISkinService>();
            if (_skinLoader == null) return;
            var skinData = _skinService.GetRandomSkinData();
            _skinLoader.LoadOutfit(skinData);
        }
    }
}
