using UnityEngine;
using UnityEngine.Serialization;

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

        #region GETTER
        
        public float MeleeMinDamage => m_meleeMinDamage;
        public float MeleeMaxDamage => m_meleeMaxDamage;
        public float RangeMinDamage => m_rangeMinDamage;
        public float RangeMaxDamage => m_rangeMaxDamage;
        
        #endregion
    }
}

