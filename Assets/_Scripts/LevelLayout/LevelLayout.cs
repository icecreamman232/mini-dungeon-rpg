using JustGame.Scripts.Enemies;
using UnityEngine;

namespace JustGame.Scripts.Levels
{
    public class LevelLayout : MonoBehaviour
    {
        [SerializeField] private EnemyHealth[] m_enemyHealths;
        private int m_enemyNumber;
        
        private void Start()
        {
            m_enemyNumber = m_enemyHealths.Length;
            for (int i = 0; i < m_enemyHealths.Length; i++)
            {
                m_enemyHealths[i].OnDeath += OnTargetKilled;
            }
        }

        private void OnTargetKilled()
        {
            m_enemyNumber--;
            if (m_enemyNumber <= 0)
            {
                //TODO:Announce level layout finished
            }
        }

        private void OnDestroy()
        {
            for (int i = 0; i < m_enemyHealths.Length; i++)
            {
                m_enemyHealths[i].OnDeath -= OnTargetKilled;
            }
        }
    } 
}

