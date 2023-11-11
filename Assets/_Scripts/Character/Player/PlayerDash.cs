using System.Collections;
using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Scripts.Player
{
    public class PlayerDash : MonoBehaviour
    {
        [SerializeField] private PlayerMovement m_playerMovement;
        [SerializeField] private BoxCollider2D m_collider2D;
        [SerializeField] private float m_dashDistance;
        [SerializeField] private float m_dashSpeed;
        [SerializeField] private LayerMask m_obstacleMask;
        private InputManager m_inputManager;
        
        
        private bool m_isDashing;
        private void Start()
        {
            m_inputManager = InputManager.Instance;
        }

        private void Update()
        {
            if (!m_inputManager.IsInputActive) return;

            if (m_inputManager.GetKeyClicked(BindingAction.DASH))
            {
                StartCoroutine(DashRoutine());
            }
        }

        private IEnumerator DashRoutine()
        {
            if (m_isDashing)
            {
                yield break;
            }

            m_isDashing = true;
            //Make player invincible while dashing
            m_collider2D.enabled = false;
            
            var initPos = transform.position;
            var destination = (Vector2)initPos + 
                    (m_playerMovement.MovingDirection != Vector2.zero
                        ? m_playerMovement.MovingDirection 
                        : m_playerMovement.LastDirection)
                    * m_dashDistance;

            bool isHitObstacle = false;
            float distanceTraveled = 0;
            Vector2 direction = Vector2.zero;
            Vector2 currentPos;
            
            while (distanceTraveled < m_dashDistance && m_isDashing && !isHitObstacle)
            {
                currentPos = transform.position;
                direction = (destination - (Vector2)currentPos).normalized;
                isHitObstacle = Physics2D.Raycast(currentPos, direction, 1f, m_obstacleMask);
                if (isHitObstacle)
                {
                    yield return null;
                }
                transform.position = Vector2.MoveTowards(currentPos, destination, Time.deltaTime * m_dashSpeed);
                distanceTraveled = Vector2.Distance(initPos, currentPos);
                yield return null;
            }

            StopDash();
        }

        private void StopDash()
        {
            m_isDashing = false;
            m_collider2D.enabled = true;
            StopCoroutine(DashRoutine());
        }
    }
}

