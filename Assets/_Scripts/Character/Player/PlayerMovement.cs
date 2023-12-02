using System.Collections;
using JustGame.Scripts.Managers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using Vector2 = UnityEngine.Vector2;

namespace JustGame.Scripts.Player
{
    public enum FacingDirection
    {
        LEFT,
        RIGHT,
        UP,
        DOWN,
    }
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float m_moveSpeed;
        [SerializeField] private Vector2 m_moveDirection;
        [SerializeField] private Vector2 m_lastDirection;
        [SerializeField] private LayerMask m_obstacleMask;
        [SerializeField] private Animator m_animator;

        private float m_curSpeed;
        private InputManager m_input;
        private int m_runningAnim = Animator.StringToHash("bool_IsRun");

        public Vector2 MovingDirection => m_moveDirection;
        public Vector2 LastDirection => m_lastDirection;

        private void Start()
        {
            m_input = InputManager.Instance;
            m_curSpeed = m_moveSpeed;
        }

        public void SetOverrideSpeed(float newSpeed, float duration = 0)
        {
            m_curSpeed = newSpeed;
            if (duration == 0) return;
            StartCoroutine(OverrideSpeedRoutine(duration));
        }

        public void SetOverridePercentSpeed(float percent, float duration = 0)
        {
            var newSpeed = m_curSpeed + m_curSpeed * (percent / 100f);
            SetOverrideSpeed(newSpeed, duration);
        }

        private IEnumerator OverrideSpeedRoutine(float duration)
        {
            yield return new WaitForSeconds(duration);
            m_curSpeed = m_moveSpeed;
        }
        

        private void Update()
        {
           HandleInput();
           UpdateMovement();
           UpdateAnimator();
        }

        private void HandleInput()
        {
            if (!m_input.IsInputActive)
            {
                m_moveDirection = Vector2.zero;
                return;
            }
            
            if (m_input.GetKeyDown(BindingAction.MOVE_LEFT))
            {
                m_moveDirection.x = -1;
            }
            else if (m_input.GetKeyDown(BindingAction.MOVE_RIGHT))
            {
                m_moveDirection.x = 1;
            }
            else
            {
                m_moveDirection.x = 0;
            }
            
            if (m_input.GetKeyDown(BindingAction.MOVE_UP))
            {
                m_moveDirection.y = 1;
            }
            else if (m_input.GetKeyDown(BindingAction.MOVE_DOWN))
            {
                m_moveDirection.y = -1;
            }
            else
            {
                m_moveDirection.y = 0;
            }
        }

        private bool CheckObstacle()
        {
            var currentPos = transform.position;
            bool result = false;
        
            if (m_moveDirection.x != 0 && m_moveDirection.y != 0)
            {
                result = Physics2D.Raycast(currentPos, m_moveDirection, 0.5f + 0.3f, m_obstacleMask);
            }
            else
            {
                result = Physics2D.Raycast(currentPos, m_moveDirection, 0.5f, m_obstacleMask);
            }

            return result;
        }
        
        private void UpdateMovement()
        {
            if (CheckObstacle())
            {
                m_moveDirection = Vector2.zero;
                return;
            }
            transform.Translate(m_moveDirection * (m_curSpeed/10 * Time.deltaTime));
            if (m_moveDirection != Vector2.zero)
            {
                m_lastDirection = m_moveDirection;
            }
        }
        
        private void UpdateAnimator()
        {
            m_animator.SetBool(m_runningAnim, m_moveDirection != Vector2.zero);
        }
    }
}

