using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Scripts.Player
{
    public class PlayerAim : MonoBehaviour
    {
        [SerializeField] private Vector2 m_aimDirection;
        private InputManager m_inputManager;

        public Vector2 AimDirection => m_aimDirection;

        private void Start()
        {
            m_inputManager = InputManager.Instance;
        }

        private void Update()
        {
            if (!m_inputManager.IsInputActive) return;

            m_aimDirection = (m_inputManager.GetWorldMousePos() - transform.position).normalized;
        }
    }
}

