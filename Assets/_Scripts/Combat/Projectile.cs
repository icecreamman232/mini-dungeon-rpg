using UnityEngine;

namespace JustGame.Scripts.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float m_moveSpeed;
        [SerializeField] private Vector2 m_moveDirection;
        [SerializeField] private bool m_destroyOnHit;
        [SerializeField] private DamageHandler m_damageHandler;
        private bool m_isGoing;

        private void Start()
        {
            m_damageHandler.OnHit += OnHitTarget;
        }

        public void SpawnProjectile(Vector2 position, Vector2 direction = default, bool destroyOnHit = true)
        {
            transform.position = position;
            
            m_destroyOnHit = destroyOnHit;
            m_moveDirection = direction;

            m_damageHandler.enabled = true;
            m_isGoing = true;
        }

        private void Movement()
        {
            transform.Translate(m_moveDirection * (m_moveSpeed/10 * Time.deltaTime));
        }

        private void Update()
        {
            if (!m_isGoing) return;

            Movement();
        }

        private void OnHitTarget()
        {
            Debug.Log("Hit target");
            m_isGoing = false;
            if (m_destroyOnHit)
            {
                m_damageHandler.enabled = false;
                this.gameObject.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            m_damageHandler.OnHit -= OnHitTarget;
        }
    }
}

