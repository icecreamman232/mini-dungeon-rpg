using System;
using JustGame.Scripts.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JustGame.Scripts.Enemies
{
    public class LootController : MonoBehaviour
    {
        [SerializeField] private float m_dropRange;
        [SerializeField] private LootContainer m_lootData;
        [SerializeField] private EnemyHealth m_enemyHealth;
        
        private void Start()
        {
            ComputeDropWeight();
            m_enemyHealth.OnDeath += DropLoot;
        }

        private void ComputeDropWeight()
        {
            float m_curPercent = 0;
            float m_nextPercent;
            float totalWeight = 0;

            for (int i = 0; i < m_lootData.LootList.Length; i++)
            {
                totalWeight += m_lootData.LootList[i].Weight;
            }
            
            for (int i = 0; i < m_lootData.LootList.Length; i++)
            {
                m_nextPercent = m_lootData.LootList[i].Weight/totalWeight * 100f + m_curPercent;
                m_lootData.LootList[i].LowerPercent = m_curPercent;
                m_lootData.LootList[i].UpperPercent = m_nextPercent;
                
                m_curPercent = m_nextPercent;
            }
        }

        private void DropLoot()
        {
            Vector2 spawnPos;
            for (int i = 0; i < m_lootData.LootList.Length; i++)
            {
                for (int j = 0; j < m_lootData.LootList[i].Amount; j++)
                {
                    spawnPos = Random.insideUnitCircle * m_dropRange + (Vector2)transform.position;
                    Instantiate(m_lootData.LootList[i].Item, spawnPos, Quaternion.identity);
                }
            }
        }

        private void OnDestroy()
        {
            m_enemyHealth.OnDeath -= DropLoot;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position,m_dropRange);
        }
    }
}

