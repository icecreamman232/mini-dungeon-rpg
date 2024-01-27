using JustGame.Scripts.Data;
using JustGame.Scripts.Enemies;
using JustGame.Scripts.Events;
using UnityEngine;

namespace JustGame.Scripts.Levels
{
    public class LevelLayout : MonoBehaviour
    {
        [SerializeField] private BoolEvent m_finishZoneEvent;
        [SerializeField] private LevelLayoutData m_currentData;
        [SerializeField] private EnemyHealth[] m_enemyHealths;
        private int m_enemyNumber;

        public void AssignLevelData(LevelLayoutData data)
        {
            m_currentData = data;
            m_enemyNumber = m_currentData.EnemyData.Length;
        }

        public void LoadLevel()
        {
            m_enemyHealths = new EnemyHealth[m_enemyNumber];
            
            for (int i = 0; i < m_enemyNumber; i++)
            {
                var spawnPos = m_currentData.EnemyData[i].SpawnPosition;
                var enemy = Instantiate(
                    m_currentData.EnemyData[i].Enemy, 
                    spawnPos, 
                    Quaternion.identity,
                    this.transform);
                var enemyHealth = enemy.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    m_enemyHealths[i] = enemyHealth;
                    m_enemyHealths[i].OnDeath += OnTargetKilled;
                }
            }
        }

        public void UnloadLevel()
        {
            m_currentData = null;
            for (int i = 0; i < m_enemyHealths.Length; i++)
            {
                m_enemyHealths[i] = null;
            }

            m_enemyHealths = null;
        }
        
        private void OnTargetKilled()
        {
            m_enemyNumber--;
            if (m_enemyNumber <= 0)
            {
                //TODO:Announce level layout finished
                m_finishZoneEvent.Raise(true);
            }
        }

        // private void OnDestroy()
        // {
        //     for (int i = 0; i < m_enemyHealths.Length; i++)
        //     {
        //         m_enemyHealths[i].OnDeath -= OnTargetKilled;
        //     }
        // }
    }
}

