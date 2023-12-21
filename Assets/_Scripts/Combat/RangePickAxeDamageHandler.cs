using JustGame.Scripts.Data;
using UnityEngine;

namespace JustGame.Scripts.Combat
{
    public class RangePickAxeDamageHandler : DamageHandler
    {
        [SerializeField] private PlayerData m_playerData;
        private void Start()
        {
            m_minDamage = m_playerData.RangeMinDamage;
            m_maxDamage = m_playerData.RangeMaxDamage;
        }
    } 
}

