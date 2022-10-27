using Codebase.Infrastructure.Services;
using UnityEngine;

namespace Codebase.SkinServiceModule
{
    public class PlayerSkinLoader : SkinLoader
    {
        private SkinnedMeshRenderer _bodyMeshRenderer;

        private ISkinService _skinService;

        private void Awake()
        {
            _bodyMeshRenderer = _renderer;
            _skinService = AllServices.Container.Single<ISkinService>();

            LoadSkins();

            _skinService.SkinUpdated += SkinService_OnSkinUpdated;
        }

        private void LoadSkins()
        {
            LoadSuitSkin();
        }

        private void LoadSuitSkin()
        {
            _bodyMeshRenderer.sharedMesh = _skinService.CurrentSuitSkin.Mesh;
            _bodyMeshRenderer.materials = _skinService.CurrentSuitSkin.Materials;
        }

        private void OnDestroy()
        {
            _skinService.SkinUpdated -= SkinService_OnSkinUpdated;
        }

        private void SkinService_OnSkinUpdated(SkinData skinData)
        {
            LoadSuitSkin();
        }
    }
}