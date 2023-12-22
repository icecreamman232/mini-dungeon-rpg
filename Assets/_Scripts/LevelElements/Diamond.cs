using System.Collections;
using JustGame.Scripts.Common;
using JustGame.Scripts.Events;
using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Scripts.LevelElements
{
    public class Diamond : Item
    {
        [SerializeField] private SpriteRenderer m_spriteRenderer;
        [SerializeField] private AnimationParameter m_pickUpVFXAnim;
        [SerializeField] private IntEvent m_pickingDiamondEvent;
        [SerializeField] private int m_diamondValue;
        
        private bool m_isInProgress;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerManager.PlayerLayer
                && other.CompareTag("Player"))
            {
                OnPicking();
            }
        }

        protected override void OnPicking()
        {
            if (m_isInProgress) return;
            StartCoroutine(PickingRoutine());
        }
        
        private IEnumerator PickingRoutine()
        {
            m_isInProgress = true;
            m_spriteRenderer.enabled = false;
            m_pickUpVFXAnim.SetTrigger();
            m_pickingDiamondEvent.Raise(m_diamondValue);
            yield return new WaitForSeconds(m_pickUpVFXAnim.Duration);
            base.OnPicking();
            m_isInProgress = false;
        }
    }
}


