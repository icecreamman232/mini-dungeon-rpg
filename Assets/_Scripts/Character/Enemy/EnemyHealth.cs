using System;
using System.Collections;
using JustGame.Scripts.Events;
using JustGame.Scripts.Player;
using UnityEngine;

namespace JustGame.Scripts.Enemies
{
    public class EnemyHealth : Health
    {
        [SerializeField] private float m_maxHealth;
        [SerializeField] private float m_curHealth;
        [SerializeField] private float m_invulnerableDuration;
        [SerializeField] private FloatEvent m_healthEvent;
        private bool m_isInvulnerable;

        private void Start()
        {
            m_curHealth = m_maxHealth;
        }

        public override void TakeDamage(float damage, GameObject instigator)
        {
            if (m_isInvulnerable) return;
            
            m_curHealth -= damage;
            
            if (m_curHealth <= 0)
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

