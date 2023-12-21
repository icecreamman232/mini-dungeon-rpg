using JustGame.Scripts.Data;
using UnityEngine;

namespace JustGame.Scripts.Combat
{
    public class MeleePickAxeDamageHandler : DamageHandler
    {
        [SerializeField] private PlayerData m_playerData;
        private void Start()
        {
            m_minDamage = m_playerData.MeleeMinDamage;
            m_maxDamage = m_playerData.MeleeMaxDamage;
        }
    }
}

