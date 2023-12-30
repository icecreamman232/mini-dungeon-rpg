using System.Collections;
using JustGame.Scripts.Data;
using JustGame.Scripts.Events;
using JustGame.Scripts.Managers;
using JustGame.Scripts.UI;
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
        [Header("References")]
        [SerializeField] private Zone m_prevZone;
        [SerializeField] private Zone m_currentZone;
        [SerializeField] private int m_currentRoom;
        [SerializeField] private BoolEvent m_finishRoomEvent;
        [SerializeField] private BoolEvent m_finishZoneEvent;
        [SerializeField] private BoolEvent m_playerTravelToNewRoomEvent;
        [SerializeField] private GameObject m_playerPrefab;
        [SerializeField] private Transform m_playerTransform;
        [SerializeField] private Transform m_spawnPoint;
        [SerializeField] private Transform m_roomPivot;
        [SerializeField] private GameObject m_roomLayout;
        [SerializeField] private GameObject m_enemyLayout;
        [SerializeField] private CameraFollowing m_cameraFollowing;

        [Space] [Header("Test Mode")] 
        [SerializeField] private bool m_isTestMode;
        [SerializeField] private RoomLayoutData m_testModeRoomLayout;
        [SerializeField] private EnemyLayoutData m_testModeEnemyLayout;
        [Space] 
        [Header("Castle Layout")] 
        [SerializeField] private RoomLayoutData[] m_castleRoomLayouts;
        [SerializeField] private EnemyLayoutData[] m_castleEnemyLayouts;
        
        private void Start()
        {
            m_prevZone = m_currentZone;
            m_currentRoom = 0;
            m_playerTravelToNewRoomEvent.AddListener(OnFinishRoom);

            FirstTimeLoad();
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
                StartCoroutine(OnChangeRoom());
            }
        }

        private void FirstTimeLoad()
        {
            InputManager.Instance.IsInputActive = false;
            var player = Instantiate(m_playerPrefab, m_spawnPoint.position, Quaternion.identity);
            m_playerTransform = player.transform;
            m_cameraFollowing.SetCameraPosition(m_playerTransform.position);
            m_cameraFollowing.SetTarget(m_playerTransform);
            
            var roomLayout = GetRoomLayout();
            var enemyLayoutPrefab = GetEnemyLayout(roomLayout.LayoutType);
            m_roomLayout = Instantiate(roomLayout.RoomPrefab, Vector3.zero, Quaternion.identity, m_roomPivot);
            m_enemyLayout = Instantiate(enemyLayoutPrefab, Vector3.zero, Quaternion.identity, m_roomPivot);
            InputManager.Instance.IsInputActive = true;
        }
        
        
        private IEnumerator OnChangeRoom()
        {
            InputManager.Instance.IsInputActive = false;
            ScreenFadeController.Instance.FadeOutToBlack();
            yield return new WaitForSeconds(0.5f);
            m_playerTransform.position = m_spawnPoint.position;
            m_cameraFollowing.SetCameraPosition(m_playerTransform.position);
            Destroy(m_roomLayout);
            Destroy(m_enemyLayout);
            var roomLayout = GetRoomLayout();
            var enemyLayoutPrefab = GetEnemyLayout(roomLayout.LayoutType);
            m_roomLayout = Instantiate(roomLayout.RoomPrefab, Vector3.zero, Quaternion.identity, m_roomPivot);
            m_enemyLayout = Instantiate(enemyLayoutPrefab, Vector3.zero, Quaternion.identity, m_roomPivot);
            
            m_currentRoom++;
            
            ScreenFadeController.Instance.FadeInFromBlack();
            yield return new WaitForSeconds(0.5f);
            InputManager.Instance.IsInputActive = true;
        }

        private RoomLayoutData GetRoomLayout()
        {
            if (m_isTestMode)
            {
                return m_testModeRoomLayout;
            }
            var rand = Random.Range(0, m_castleRoomLayouts.Length);
            return m_castleRoomLayouts[rand];
        }

        private GameObject GetEnemyLayout(LayoutType type)
        {
            if (m_isTestMode)
            {
                return m_testModeEnemyLayout.EnemyPrefab;
            }
            
            int index = Random.Range(0, m_castleEnemyLayouts.Length);
            EnemyLayoutData layout = m_castleEnemyLayouts[index];
            while (layout.LayoutType != type)
            {
                index = Random.Range(0, m_castleEnemyLayouts.Length);
                layout = m_castleEnemyLayouts[index];
            }

            return layout.EnemyPrefab;
        }
        
        
        private void OnDestroy()
        {
            m_finishRoomEvent.RemoveListener(OnFinishRoom);
        }
    }
}

