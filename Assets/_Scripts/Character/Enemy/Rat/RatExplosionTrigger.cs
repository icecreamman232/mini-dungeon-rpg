using System.Collections;
using JustGame.Scripts.Common;
using JustGame.Scripts.Enemies;
using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public class RatExplosionTrigger : MonoBehaviour
    {
        [SerializeField] private EnemyHealth m_enemyHealth;
        [SerializeField] private AnimationParameter m_explodeAnim;
        [SerializeField] private Collider2D m_explodeHitBox;

        private void Start()
        {
            m_enemyHealth.OnDeath += OnTriggerExplosion;
        }

        private void OnTriggerExplosion()
        {
            StartCoroutine(ExplodeRoutine());
        }

        private IEnumerator ExplodeRoutine()
        {
            m_explodeHitBox.enabled = true;
            m_explodeAnim.SetTrigger();
            yield return new WaitForSeconds(m_explodeAnim.Duration);
            m_explodeHitBox.enabled = false;
        }

        private void OnDestroy()
        {
            StopCoroutine(ExplodeRoutine());
            m_enemyHealth.OnDeath -= OnTriggerExplosion;
        }
    }
 
}
