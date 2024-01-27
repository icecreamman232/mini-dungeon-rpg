using System;
using System.Collections;
using JustGame.Scripts.Common;
using JustGame.Scripts.Events;
using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Scripts.LevelElements
{
    public class Coin : Item
    {
        [SerializeField] private SpriteRenderer m_spriteRenderer;
        [SerializeField] private AnimationParameter m_pickUpVFXAnim;
        [SerializeField] private IntEvent m_pickingCoinEvent;
        [SerializeField] private ScriptableEvent<bool> m_playerTravelToNewRoom;
        [SerializeField] private int m_coinValue;

        private bool m_isInProgress;

        private void Start()
        {
            m_playerTravelToNewRoom.AddListener(OnFinishLevel);
        }

        private void OnDestroy()
        {
            m_playerTravelToNewRoom.RemoveListener(OnFinishLevel);
        }

        private void OnFinishLevel(bool isFinish)
        {
            if (!isFinish) return;
            
            //About to destroy the item
            base.OnPicking();
        }
        
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
            m_pickingCoinEvent.Raise(m_coinValue);
            yield return new WaitForSeconds(m_pickUpVFXAnim.Duration);
            base.OnPicking();
            m_isInProgress = false;
        }
        
        
    }
}

