using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Scripts.Combat
{
    public class RedSlimeGun : MonoBehaviour
    {
        [SerializeField] private ObjectPooler m_objectPooler;
        private Vector2 m_upDirection = Vector2.up;
        private Vector2 m_leftDiagonal = new Vector2(-1f, -1f);
        private Vector2 m_rightDiagonal = new Vector2(1f, -1f);
        
        [ContextMenu("Shoot")]
        public void Shoot()
        {
            var upGO = m_objectPooler.GetPooledGameObject();
            var leftGO = m_objectPooler.GetPooledGameObject();
            var rightGO = m_objectPooler.GetPooledGameObject();

            var upProjectile = upGO.GetComponent<Projectile>();
            var leftProjectile = leftGO.GetComponent<Projectile>();
            var rightProjectile = rightGO.GetComponent<Projectile>();
            
            upProjectile.SpawnProjectile(transform.position, m_upDirection);
            leftProjectile.SpawnProjectile(transform.position, m_leftDiagonal);
            rightProjectile.SpawnProjectile(transform.position, m_rightDiagonal);
        }
    }
}

