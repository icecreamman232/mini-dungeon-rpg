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
            var destination = (Vector2)initPos + m_playerMovement.MovingDirection * m_dashDistance;
            float distanceTraveled = 0;
            while (distanceTraveled < m_dashDistance && m_isDashing)
            {
                transform.position = Vector2.MoveTowards(transform.position, destination, Time.deltaTime * m_dashSpeed);
                distanceTraveled = Vector2.Distance(initPos, transform.position);
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

