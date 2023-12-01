using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace JustGame.Scripts.Combat
{
    public class Projectile : MonoBehaviour
    {
        [Header("Basic")]
        [SerializeField] private float m_moveSpeed;
        [SerializeField] private Vector2 m_moveDirection;
        [Space]
        [Header("Damage")]
        [SerializeField] private bool m_destroyOnHit;
        [SerializeField] private bool m_destroyAtMaxRange;
        [SerializeField] private float m_maxRange;
        [SerializeField] private DamageHandler m_damageHandler;
        [Space]
        [Header("Rotation")] 
        //Rotate projectile body at shooting direction
        [SerializeField] private bool m_IsRotateToDirection;
        [SerializeField] private float m_offsetAngle;
        //If this is empty then it will automatically grab parent transform
        [SerializeField] private Transform m_transformToRotate;

        private Vector2 m_initPos;
        private bool m_isGoing;

        public Action OnDestroyAction;

        private void Start()
        {
            m_damageHandler.OnHit += OnHitTarget;
            if (m_transformToRotate == null)
            {
                m_transformToRotate = transform;
            }
        }

        public void SpawnProjectile(Vector2 position, Vector2 direction = default, bool destroyOnHit = true)
        {
            m_initPos = position;
            transform.position = position;
            
            m_destroyOnHit = destroyOnHit;
            m_moveDirection = direction;
            
            if (m_IsRotateToDirection)
            {
                var angle = Mathf.Atan2(m_moveDirection.y, m_moveDirection.x) * Mathf.Rad2Deg + m_offsetAngle;
                m_transformToRotate.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }

            m_damageHandler.enabled = true;
            m_isGoing = true;
        }

        private void Movement()
        {
            if (m_destroyAtMaxRange)
            {
                var curRange = Vector2.Distance(m_initPos, transform.position);
                if (curRange >= m_maxRange)
                {
                    DestroyProjectile();
                }
            }
            transform.Translate(m_moveDirection * (m_moveSpeed/10 * Time.deltaTime));
        }

        private void Update()
        {
            if (!m_isGoing) return;

            Movement();
        }

        private void OnHitTarget()
        {
            m_isGoing = false;
            if (m_destroyOnHit)
            {
                DestroyProjectile();
            }
        }

        /// <summary>
        /// This doesnt mean to destroy whole game object but will disable and set it inactive instead
        /// </summary>
        private void DestroyProjectile()
        {
            m_damageHandler.enabled = false;
            this.gameObject.SetActive(false);
        }
        
        private void OnDisable()
        {
            //Even though the name is OnDestroyAction
            //but it means to be called when projectile finished their life cycle
            OnDestroyAction?.Invoke();
        }

        private void OnDestroy()
        {
            m_damageHandler.OnHit -= OnHitTarget;
        }
    }
}

