using JustGame.Scripts.Combat;
using JustGame.Scripts.Managers;
using JustGame.Scripts.Runtime;
using UnityEngine;

namespace JustGame.Scripts.Enemies
{
    public class VioletBatProjectileWeapon : MonoBehaviour
    {
        [SerializeField] private ObjectPooler m_objectPooler;
        [SerializeField] private GlobalRuntimeVariables m_runtimeVariables;
        
        public void Shoot()
        {
            var shootingDirection = (m_runtimeVariables.PlayerTransform.position - transform.position).normalized;
            
            var projectileGO = m_objectPooler.GetPooledGameObject();
            var projectile = projectileGO.GetComponent<Projectile>();
            projectile.SpawnProjectile(transform.position, shootingDirection);
        }
    }
}

