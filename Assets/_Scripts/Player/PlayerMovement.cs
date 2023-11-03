using JustGame.Scripts.Managers;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace JustGame.Scripts.Player
{
    public enum FacingDirecttion
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
        [SerializeField] private Animator m_animator;
        private InputManager m_input;

        private int m_runningAnim = Animator.StringToHash("bool_IsRun");

        private void Start()
        {
            m_input = InputManager.Instance;
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

        private void UpdateMovement()
        {
            transform.Translate(m_moveDirection * (m_moveSpeed/10 * Time.deltaTime));
        }

        
        private void UpdateAnimator()
        {
            m_animator.SetBool(m_runningAnim, m_moveDirection != Vector2.zero);
        }
    }
}

