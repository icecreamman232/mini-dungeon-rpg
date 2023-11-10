using UnityEngine;

namespace JustGame.Scripts.Data
{
    public enum LayoutType
    {
        LayoutSquare,
    }
    [CreateAssetMenu(menuName = "JustGame/Data/Enemy Layout")]
    public class EnemyLayoutData : ScriptableObject
    {
        public LayoutType LayoutType;
        public GameObject EnemyPrefab;
    }
}

