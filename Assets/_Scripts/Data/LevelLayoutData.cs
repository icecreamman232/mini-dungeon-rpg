using System;
using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/Level Layout Data")]
    public class LevelLayoutData : ScriptableObject
    {
        public EnemyData[] EnemyData;
    }
    
    [Serializable]
    public class EnemyData
    {
        public Vector3 SpawnPosition;
        public GameObject Enemy;

        public EnemyData()
        {
            
        }
    }
}

