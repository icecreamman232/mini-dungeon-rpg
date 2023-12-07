using System.Collections;
using JustGame.Scripts.Data;
using JustGame.Scripts.Events;
using UnityEngine;

namespace JustGame.Scripts.Player
{
    public class PlayerHealth : Health
    {
        [SerializeField] private int m_maxLife;
        [SerializeField] private int m_curLife;
        [SerializeField] private float m_invulnerableDuration;
        [SerializeField] private IntEvent m_healthEvent;
        [SerializeField] private ScreenShakeEvent m_screenShakeEvent;
        [SerializeField] private ShakeProfile m_takeDmgShakeProfile;
        
        private bool m_isInvulnerable;
        private bool m_isDead;
        public bool IsDead => m_isDead;
        
        private void Start()
        {
            m_curLife = m_maxLife;
            m_isDead = false;
        }

        public override void TakeDamage(float damage, GameObject instigator)
        {
            if (m_isInvulnerable) return;
            
            m_curLife -= 1;
            m_healthEvent.Raise(m_curLife);
            m_screenShakeEvent.DoShake(m_takeDmgShakeProfile);
            
            if (m_curLife <= 0)
            {
                Kill();
                return;
            }
            
            StartCoroutine(OnInvulnerable());
        }

        private void Kill()
        {
            //TODO:Kill process here
            m_isDead = true;
            this.gameObject.SetActive(false);
        }
        
        private IEnumerator OnInvulnerable()
        {
            if (m_isInvulnerable)
            {
                yield break;
            }

            m_isInvulnerable = true;

            yield return new WaitForSeconds(m_invulnerableDuration);
            
            m_isInvulnerable = false;
        }
    }
}
