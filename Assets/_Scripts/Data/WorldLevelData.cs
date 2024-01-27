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

        public LevelLayoutData[] GetRoomData(int roomIndex)
        {
            switch (roomIndex)
            {
                case 1:
                    return m_room1;
                case 2:
                    return m_room2;
                case 3:
                    return m_room3;
                case 4:
                    return m_room4;
                case 5:
                    return m_room5;
                case 6:
                    return m_room6;
                case 7:
                    return m_room7;
                case 8:
                    return m_room8;
                case 9:
                    return m_room9;
            }

            return null;
        }
    }
}

