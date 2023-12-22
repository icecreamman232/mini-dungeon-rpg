using System;
using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Scripts.Combat
{
    /// <summary>
    /// Trigger callback on hitting obstacle that specific with a layer mask
    /// </summary>
    public class ObstacleHandler : MonoBehaviour
    {
        [SerializeField] private LayerMask m_obstacleMask;

        public Action OnHitObstacle;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!LayerManager.IsInLayerMask(other.gameObject.layer, m_obstacleMask))
            {
                return;
            }
            OnHitObstacle?.Invoke();
        }
    } 
}

