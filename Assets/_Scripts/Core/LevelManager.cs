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
        [SerializeField] private BoolEvent m_finishZoneEvent;
        
        private void Start()
        {
            m_prevZone = m_currentZone;
            m_currentRoom = Global.MAX_ROOM;
            m_finishZoneEvent.AddListener(OnFinishZone);
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

        private void OnFinishZone(bool isFinished)
        {
            InitializeNextZone();
        }

        private void OnDestroy()
        {
            m_finishZoneEvent.RemoveListener(OnFinishZone);
        }
    }
}

