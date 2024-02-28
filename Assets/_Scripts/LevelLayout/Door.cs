using System.Collections;
using JustGame.Scripts.Common;
using JustGame.Scripts.Events;
using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Scripts.Levels
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private AnimationParameter m_openAnim;
        [SerializeField] private BoolEvent m_finishRoomEvent;
        [SerializeField] private BoolEvent m_playerTravelToNewRoomEvent;
        [SerializeField] private bool m_isLocked;
        
        [ContextMenu("Unlock")]
        public void Unlock()
        {
            m_isLocked = false;
            this.gameObject.layer = LayerManager.Interactible;
        }

        [ContextMenu("Lock")]
        public void Lock()
        {
            m_isLocked = true;
            this.gameObject.layer = LayerManager.Obstacle;
        }
        
        private void Start()
        {
            Lock();
            m_finishRoomEvent.AddListener(OnFinishLevel);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerManager.PlayerLayer
                && other.CompareTag("Player")
                && !m_isLocked)
            {
                StartCoroutine(PlayerTravelToNewRoom());
            }
        }

        private void OnFinishLevel(bool isFinished)
        {
            m_openAnim.SetBool(true);
            Unlock();
        }
        
        private IEnumerator PlayerTravelToNewRoom()
        {
            m_playerTravelToNewRoomEvent.Raise(true);
            yield return null;
        }

        private void OnDestroy()
        {
            m_finishRoomEvent.RemoveListener(OnFinishLevel);
        }
    }
}

