using UnityEngine;

namespace Codebase.SkinServiceModule
{
    [CreateAssetMenu(fileName = "SkinData", menuName = "SkinData", order = 51)]
    [ExecuteInEditMode]
    public class SkinData : ScriptableObject
    {
        [SerializeField] private int _id;
        [SerializeField] private int scoreCost;
        [SerializeField] private string _name;
        [SerializeField] private Sprite _iconUnlocked;
        [SerializeField] private Sprite _iconLocked;
        [SerializeField] private Mesh _mesh;
        [SerializeField] private Material[] _materials;
    
        [SerializeField] private GameObject _prefab;
        [SerializeField] private SkinType _skinType;
        [SerializeField] private SkinPurchaseType skinPurchaseType;
        [SerializeField] private bool _isAvailableForPurchase;
        [SerializeField] private bool isUnlocked;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public int ScoreCost
        {
            get => scoreCost;
            set => scoreCost = value;
        }

        public string Name
        {
            get => _name;
            set
            {
                name = value;
                _name = value;
            }
        }

        public Sprite IconUnlocked
        {
            get => _iconUnlocked;
            set => _iconUnlocked = value;
        }

        public Sprite IconLocked
        {
            get => _iconLocked;
            set => _iconLocked = value;
        }

        public Mesh Mesh
        {
            get => _mesh;
            set => _mesh = value;
        }

        public Material[] Materials
        {
            get => _materials;
            set => _materials = value;
        }

        public SkinType SkinType
        {
            get => _skinType;
            set => _skinType = value;
        }

        public SkinPurchaseType SkinPurchaseType
        {
            get => skinPurchaseType;
            set => skinPurchaseType = value;
        }

        public bool IsAvailableForPurchase
        {
            get => _isAvailableForPurchase;
            set => _isAvailableForPurchase = value;
        }

        public bool IsUnlocked
        {
            get => isUnlocked; 
            set => isUnlocked = value;
        }

        public GameObject Prefab => _prefab;
    }

    public enum SkinPurchaseType
    {
        Money,
        AdReward,
        Vip
    }

    public enum SkinType
    {
        SuperHero = 0,
        Other = 1
    }
}