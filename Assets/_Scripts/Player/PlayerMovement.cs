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
        [SerializeField] private FacingDirecttion m_facingDirecttion;
        [SerializeField] private LayerMask m_obstacleMask;
        [SerializeField] private SpriteRenderer m_spriteRenderer;
        [SerializeField] private Animator m_animator;
        private InputManager m_input;

        private int m_boolIsLeftAnim = Animator.StringToHash("IsLeft");
        private int m_intDirectionX = Animator.StringToHash("Direction-X");
        private int m_intDirectionY = Animator.StringToHash("Direction-Y");

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

            m_moveDirection = Vector2.zero;
            
            if (m_input.GetKeyDown(BindingAction.MOVE_LEFT))
            {
                m_moveDirection.x = -1;
                m_facingDirecttion = FacingDirecttion.LEFT;
            }
            else if (m_input.GetKeyDown(BindingAction.MOVE_RIGHT))
            {
                m_moveDirection.x = 1;
                m_facingDirecttion = FacingDirecttion.RIGHT;
            }
            else if (m_input.GetKeyDown(BindingAction.MOVE_UP))
            {
                m_moveDirection.y = 1;
                m_facingDirecttion = FacingDirecttion.UP;
            }
            else if (m_input.GetKeyDown(BindingAction.MOVE_DOWN))
            {
                m_moveDirection.y = -1;
                m_facingDirecttion = FacingDirecttion.DOWN;
            }
        }

        private void UpdateMovement()
        {
            transform.Translate(m_moveDirection * (m_moveSpeed * Time.deltaTime));
        }

        private void CheckObstacle()
        {
            
        }
        
        private void UpdateAnimator()
        {
            m_spriteRenderer.flipX = m_moveDirection.x > 0;
            m_animator.SetBool(m_boolIsLeftAnim, m_moveDirection.x <= -1);
            m_animator.SetInteger(m_intDirectionX, (int)m_moveDirection.x);
            m_animator.SetInteger(m_intDirectionY, (int)m_moveDirection.y);
            m_spriteRenderer.flipX = m_facingDirecttion == FacingDirecttion.RIGHT;
        }
    }
}

