using System.Collections;
using JustGame.Scripts.Common;
using JustGame.Scripts.Managers;
using JustGame.Scripts.Player;
using UnityEngine;

namespace JustGame.Scripts.Enemies
{
    public class SpiderWeb : MonoBehaviour
    {
        [SerializeField] private float m_lifeCycle;
        [SerializeField] private float m_percentSpeedReduce;
        [SerializeField] private float m_speedReduceDuration;
        [SerializeField] private bool m_isActive;
        [SerializeField] private LayerMask m_targetMask;
        [SerializeField] private AnimationParameter m_triggerAnim;
        
        public void Spawn(Vector2 position)
        {
            transform.position = position;
            m_isActive = true;
            m_triggerAnim.SetTrigger();
            StartCoroutine(SelfDestroy());
        }

        private IEnumerator SelfDestroy()
        {
            yield return new WaitForSeconds(m_lifeCycle);
            Destroy(this.gameObject);
            m_isActive = false;
        }
        
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (LayerManager.IsInLayerMask(other.gameObject.layer, m_targetMask)
                && m_isActive)
            {
                TriggerWeb(other);
            }
        }

        private void TriggerWeb(Collider2D target)
        {
            var movement = target.gameObject.GetComponentInParent<PlayerMovement>();
            movement.SetOverridePercentSpeed(-m_percentSpeedReduce, m_speedReduceDuration);
            Destroy(this.gameObject);
        }
    }
}

