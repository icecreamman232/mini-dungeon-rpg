using System.Collections;
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
        private bool m_isInvulnerable;

        private void Start()
        {
            m_curLife = m_maxLife;
        }

        public override void TakeDamage(float damage, GameObject instigator)
        {
            if (m_isInvulnerable) return;
            
            m_curLife -= 1;
            m_healthEvent.Raise(m_curLife);
            
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
