using System;
using JustGame.Scripts.Enemy;
using JustGame.Scripts.Managers;
using JustGame.Scripts.Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JustGame.Scripts.Combat
{
    public class DamageHandler : MonoBehaviour
    {
        [Header("Damage")]
        [SerializeField] protected LayerMask m_targetMask;
        [SerializeField] protected float m_minDamage;
        [SerializeField] protected float m_maxDamage;
        [Header("KnockBack")] 
        [SerializeField] protected float m_knockBackForce;
        [SerializeField] protected float m_knockBackDuration;

        public Action OnHit;
        
        protected float GetDamage()
        {
            return Random.Range(m_minDamage, m_maxDamage + 1);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!LayerManager.IsInLayerMask(other.gameObject.layer, m_targetMask)) return;
            DealDamage(other);
        }

        protected void DealDamage(Collider2D target)
        {
            OnHit?.Invoke();
            var health = target.GetComponentInParent<Health>();
            if (health != null)
            {
                health.TakeDamage(GetDamage(), this.gameObject);

                if (target.gameObject.layer == LayerManager.EnemyLayer)
                {
                    //Enemy movement should be on parent GO.
                    //The collider is on the body GO, hence we query the parent
                    var enemyMovement = target.gameObject.GetComponentInParent<EnemyMovement>();
                    if (enemyMovement == null)
                    {
                        Debug.LogError($"Enemy Movement not found on {target.gameObject.name}");
                        return;
                    }
                    var knockBackDir = (target.transform.position - transform.position).normalized;
                    enemyMovement.ApplyKnockBack(knockBackDir, m_knockBackForce, m_knockBackDuration);
                }
            }
        }
    }
}

