using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/Player Data")]
    public class PlayerData : ScriptableObject
    {
        [Header("Pickaxe")]
        [SerializeField] private float m_meleeMinDamage;
        [SerializeField] private float m_meleeMaxDamage;
        [SerializeField] private float m_rangeMinDamage;
        [SerializeField] private float m_rangeMaxDamage;
        [SerializeField] private float m_flyingSpeed;
        [SerializeField] private float m_flyingRange;

        #region GETTER
        
        public float MeleeMinDamage => m_meleeMinDamage;
        public float MeleeMaxDamage => m_meleeMaxDamage;
        public float RangeMinDamage => m_rangeMinDamage;
        public float RangeMaxDamage => m_rangeMaxDamage;
        public float FlyingSpeed => m_flyingSpeed;
        public float FlyingRange => m_flyingRange;

        #endregion
    }
}

