using JustGame.Scripts.Attribute;
using JustGame.Scripts.Combat;
using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Scripts.Player
{
    public class PlayerProjectileWeapon : MonoBehaviour
    {
        [SerializeField] private ObjectPooler m_projectilePooler;
        [SerializeField] private float m_coolDown;
        [SerializeField] [ReadOnly] private Vector2 m_shootDirection;
        [SerializeField] [ReadOnly] private float m_timer;
        
        private InputManager m_inputManager;
        private bool m_isCooldown;
       

        private void Start()
        {
            m_inputManager = InputManager.Instance;
        }

        private void Update()
        {
            UpdateInput();
            Shoot();
            UpdateCooldown();
        }

        private void UpdateInput()
        {
            if (!m_inputManager.IsInputActive) return;

            m_shootDirection = Vector2.zero;
            
            if (m_inputManager.GetKeyClicked(BindingAction.SHOOT_LEFT))
            {
                m_shootDirection.x = -1;
            }
            else if (m_inputManager.GetKeyClicked(BindingAction.SHOOT_RIGHT))
            {
                m_shootDirection.x = 1;
            }
            else if (m_inputManager.GetKeyClicked(BindingAction.SHOOT_UP))
            {
                m_shootDirection.y = 1;
            }
            else if (m_inputManager.GetKeyClicked(BindingAction.SHOOT_DOWN))
            {
                m_shootDirection.y = -1;
            }
        }

        private void Shoot()
        {
            if (m_shootDirection == -Vector2.zero) return;

            if (m_isCooldown) return;

            var projectileGO = m_projectilePooler.GetPooledGameObject();
            if (projectileGO == null) return;
            var projectile = projectileGO.GetComponent<Projectile>();
            if (projectile == null) return;
            projectile.SpawnProjectile(transform.position,m_shootDirection);
            
            m_isCooldown = true;
            m_timer = m_coolDown;
        }

        private void UpdateCooldown()
        {
            if (!m_isCooldown) return;
            m_timer -= Time.deltaTime;
            if (m_timer <= 0)
            {
                m_isCooldown = false;
            }
        }
    }
}

