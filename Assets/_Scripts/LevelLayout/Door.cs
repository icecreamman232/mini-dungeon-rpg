using System.Collections;
using JustGame.Scripts.Common;
using JustGame.Scripts.Events;
using JustGame.Scripts.Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace JustGame.Scripts.Levels
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private AnimationParameter m_openAnim;
        [SerializeField] private BoolEvent m_finishRoomEvent;
        [SerializeField] private BoolEvent m_playerTravelToNewRoomEvent;

        private void Start()
        {
            m_finishRoomEvent.AddListener(OnFinishLevel);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer != LayerManager.PlayerLayer) return;
            
            StartCoroutine(PlayerTravelToNewRoom());
        }

        private void OnFinishLevel(bool isFinished)
        {
            m_openAnim.SetBool(true);
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

