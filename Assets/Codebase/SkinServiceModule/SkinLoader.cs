using UnityEngine;

namespace Codebase.SkinServiceModule
{
    public class SkinLoader : MonoBehaviour
    {
        [SerializeField] protected Animator _animator;
        [SerializeField] protected SkinnedMeshRenderer _renderer;

        public Animator Animator => _animator;

        public void LoadOutfit(SkinData skinData)
        {
            _renderer.sharedMesh = skinData.Mesh;
            _renderer.materials = skinData.Materials;
        }
    }
}