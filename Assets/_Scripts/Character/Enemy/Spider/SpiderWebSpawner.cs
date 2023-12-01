using System;
using System.Collections;
using System.Collections.Generic;
using JustGame.Scripts.Combat;
using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Scripts.Enemies
{
    /// <summary>
    /// Spawn spider web at current position
    /// </summary>
    public class SpiderWebSpawner : MonoBehaviour
    {
        [SerializeField] private ObjectPooler m_objectPooler;
        [SerializeField] private Projectile m_projectile;

        private void Start()
        {
            m_projectile.OnDestroyAction += SpawnWeb;
        }

        public void SpawnWeb()
        {
            var webGO = m_objectPooler.GetPooledGameObject();
            var web = webGO.GetComponentInChildren<SpiderWeb>();
            web.Spawn(transform.position);
        }

        private void OnDestroy()
        {
            m_projectile.OnDestroyAction -= SpawnWeb;
        }
    }
}

