using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/World Data")]
    public class WorldLevelData : ScriptableObject
    {
        [SerializeField] private LevelLayoutData[] m_room1;
        [SerializeField] private LevelLayoutData[] m_room2;
        [SerializeField] private LevelLayoutData[] m_room3;
        [SerializeField] private LevelLayoutData[] m_room4;
        [SerializeField] private LevelLayoutData[] m_room5;
        [SerializeField] private LevelLayoutData[] m_room6;
        [SerializeField] private LevelLayoutData[] m_room7;
        [SerializeField] private LevelLayoutData[] m_room8;
        [SerializeField] private LevelLayoutData[] m_room9;

        public LevelLayoutData[] GetRoom1Data => m_room1;
        public LevelLayoutData[] GetRoom2Data => m_room2;
        public LevelLayoutData[] GetRoom3Data => m_room3;
        public LevelLayoutData[] GetRoom4Data => m_room4;
        public LevelLayoutData[] GetRoom5Data => m_room5;
        public LevelLayoutData[] GetRoom6Data => m_room6;
        public LevelLayoutData[] GetRoom7Data => m_room7;
        public LevelLayoutData[] GetRoom8Data => m_room8;
        public LevelLayoutData[] GetRoom9Data => m_room9;
    }
}

