using JustGame.Scripts.Common;
using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Scripts.Enemies
{
    public class SpiderWeb : MonoBehaviour
    {
        [SerializeField] private bool m_isActive;
        [SerializeField] private LayerMask m_targetMask;
        [SerializeField] private AnimationParameter m_triggerAnim;
        
        public void Spawn(Vector2 position)
        {
            transform.position = position;
            m_isActive = true;
            m_triggerAnim.SetTrigger();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (LayerManager.IsInLayerMask(other.gameObject.layer, m_targetMask)
                && m_isActive)
            {
                TriggerWeb();
            }
        }

        private void TriggerWeb()
        {
            this.gameObject.SetActive(false);
        }
    }
}

