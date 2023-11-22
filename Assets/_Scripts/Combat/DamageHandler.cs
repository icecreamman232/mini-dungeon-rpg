using System;
using JustGame.Scripts.Managers;
using JustGame.Scripts.Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JustGame.Scripts.Combat
{
    public class DamageHandler : MonoBehaviour
    {
        [SerializeField] private float m_minDamage;
        [SerializeField] private float m_maxDamage;
        [SerializeField] private LayerMask m_targetMask;

        public Action OnHit;
        
        private float GetDamage()
        {
            return Random.Range(m_minDamage, m_maxDamage + 1);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!LayerManager.IsInLayerMask(other.gameObject.layer, m_targetMask)) return;
            DealDamage(other);
        }

        private void DealDamage(Collider2D other)
        {
            OnHit?.Invoke();
            var health = other.GetComponentInParent<Health>();
            if (health != null)
            {
                health.TakeDamage(GetDamage(), this.gameObject);
            }
        }
    }
}

