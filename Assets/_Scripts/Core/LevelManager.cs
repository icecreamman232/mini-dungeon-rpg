using JustGame.Scripts.Common;
using JustGame.Scripts.Events;
using UnityEngine;

namespace JustGame.Scripts.Levels
{
    public enum Zone
    {
        GRASSLAND = 0,
        SWAMP = 1,
        JUNGLE  = 2,
        DESSERT = 3,
        OCEAN   = 4,
        VOLCANIC = 5,
        TUNDRA = 6,
        DARK_FOREST = 7,
        CASTLE  = 8,
        HELL = 9,
        LAST_ZONE_INDEX = 9,
    }

    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Zone m_prevZone;
        [SerializeField] private Zone m_currentZone;
        [SerializeField] private int m_currentRoom;
        [SerializeField] private BoolEvent m_finishRoomEvent;
        [SerializeField] private BoolEvent m_finishZoneEvent;
        [SerializeField] private Transform m_roomPivot;
        
        private void Start()
        {
            m_prevZone = m_currentZone;
            m_currentRoom = 0;
            m_finishRoomEvent.AddListener(OnFinishRoom);
        }
        
        private void InitializeNextZone()
        {
            m_prevZone = m_currentZone;
            m_currentZone += 1;
            if ((int)m_currentZone >= 9)
            {
                m_currentZone = Zone.LAST_ZONE_INDEX;
            }
            m_currentRoom = 0;
        }

        private void OnFinishRoom(bool isFinished)
        {
            if (m_currentRoom == 9)
            {
                InitializeNextZone();
            }
            else
            {
                m_currentRoom++;
            }
        }

        private void OnDestroy()
        {
            m_finishRoomEvent.RemoveListener(OnFinishRoom);
        }
    }
}

