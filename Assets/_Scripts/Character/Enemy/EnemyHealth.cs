using System;
using System.Collections;
using JustGame.Scripts.Managers;
using JustGame.Scripts.Player;
using JustGame.Scripts.UI;
using UnityEngine;

namespace JustGame.Scripts.Enemies
{
    public class EnemyHealth : Health
    {
        [SerializeField] private float m_maxHealth;
        [SerializeField] private float m_curHealth;
        [SerializeField] private float m_invulnerableDuration;
        [SerializeField] private EnemyHealthBarUI m_healthBar;
        private bool m_isInvulnerable;

        public Action OnDeath;
        
        private void Start()
        {
            m_curHealth = m_maxHealth;
            m_healthBar.UpdateHealth(m_curHealth);
        }

        public override void TakeDamage(float damage, GameObject instigator)
        {
            if (m_isInvulnerable) return;
            
            m_curHealth -= damage;
            m_healthBar.UpdateHealth(MathHelpers.Remap(m_curHealth,0,m_maxHealth,0,1));

            if (m_curHealth <= 0)
            {
                Kill();
                return;
            }
            
            StartCoroutine(OnInvulnerable());
        }
        
        protected virtual void Kill()
        {
            //TODO:Kill process here
            OnDeath?.Invoke();
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

