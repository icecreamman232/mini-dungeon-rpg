using JustGame.Scripts.Combat;
using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Scripts.Enemies
{
    public class GhostAutoGun : MonoBehaviour
    {
        [SerializeField] private ObjectPooler m_objectPooler;

        public void Shoot()
        {
            var leftProjectileGO = m_objectPooler.GetPooledGameObject();
            var rightProjectileGO = m_objectPooler.GetPooledGameObject();

            var leftProjectile = leftProjectileGO.GetComponent<Projectile>();
            var rightProjectile = rightProjectileGO.GetComponent<Projectile>();
            
            leftProjectile.SpawnProjectile(transform.position, Vector2.left);
            rightProjectile.SpawnProjectile(transform.position, Vector2.right);
        }
    }
}

