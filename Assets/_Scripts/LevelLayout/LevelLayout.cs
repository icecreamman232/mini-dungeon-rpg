using JustGame.Scripts.Data;
using JustGame.Scripts.Enemies;
using JustGame.Scripts.Events;
using UnityEditor;
using UnityEngine;

namespace JustGame.Scripts.Levels
{
    public class LevelLayout : MonoBehaviour
    {
        [SerializeField] private BoolEvent m_finishZoneEvent;
        [SerializeField] private string m_levelName;
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
                m_finishZoneEvent.Raise(true);
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

