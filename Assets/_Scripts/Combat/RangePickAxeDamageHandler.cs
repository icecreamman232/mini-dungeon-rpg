using JustGame.Scripts.Data;
using UnityEngine;

namespace JustGame.Scripts.Combat
{
    public class RangePickAxeDamageHandler : DamageHandler
    {
        [SerializeField] private PlayerData m_playerData;
        private void Start()
        {
            ResetDamage();
        }

        public void SetDamageReduce(float percent)
        {
            m_minDamage = m_minDamage * percent / 100f;
            m_maxDamage = m_maxDamage * percent / 100f;
        }

        public void EnableKnockBack()
        {
            m_isPreventKnockBack = false;
        }
        
        public void DisableKnockBack()
        {
            m_isPreventKnockBack = true;
        }
        
        public void ResetDamage()
        {
            m_minDamage = m_playerData.RangeMinDamage;
            m_maxDamage = m_playerData.RangeMaxDamage;
        }
    } 
}

