using UnityEngine;

namespace JustGame.Scripts.Weapons
{
    public enum ThrowingPickAxeState
    {
        READY,
        MOVING_FORWARD,
        MOVING_BACKWARD,
    }
    public class ThrowingPickAxe : MonoBehaviour
    {
        [SerializeField] private ThrowingPickAxeState m_currentState;
        [SerializeField] private LayerMask m_targetMask;
        [SerializeField] private float m_moveSpeed;
        [SerializeField] private float m_movingRange;

        private Vector2 m_startPos;
        private Vector2 m_movingDirection;
        private float m_traveledDistance;
        private bool m_isInProgress;
        private Transform m_player;
        
        public bool IsInProgress => m_isInProgress;

        public void Initialize(Transform player)
        {
            m_player = player;
            m_currentState = ThrowingPickAxeState.READY;
        }
        
        public void StartMoving(Vector2 direction)
        {
            m_currentState = ThrowingPickAxeState.MOVING_FORWARD;
            m_isInProgress = true;
            m_movingDirection = direction;
            m_startPos = m_player.position;
            transform.position = m_startPos;
        }

        private void StartMovingBackward()
        {
            m_currentState = ThrowingPickAxeState.MOVING_BACKWARD;
            
            //Set current direction to opposite side
            m_movingDirection.x *= -1;
            m_movingDirection.y *= -1;
            
            m_startPos = transform.position;
            m_traveledDistance = 0;
        }

        public void StopMoving()
        {
            m_isInProgress = false;
            m_traveledDistance = 0;
            m_currentState = ThrowingPickAxeState.READY;
        }

        private void Update()
        {
            if (!m_isInProgress) return;

            switch (m_currentState)
            {
                case ThrowingPickAxeState.READY:
                    //We do not update any movement here so we must return
                    return;
                case ThrowingPickAxeState.MOVING_FORWARD:
                    if (m_traveledDistance >= m_movingRange)
                    {
                        StartMovingBackward();
                    }
                    break;
                case ThrowingPickAxeState.MOVING_BACKWARD:
                    if (m_traveledDistance >= m_movingRange)
                    {
                        StopMoving();
                    }
                    break;
            }
            
            transform.Translate(m_movingDirection * (m_moveSpeed * Time.deltaTime));
            m_traveledDistance = Vector2.Distance(m_startPos, transform.position);
        }
    }
}

