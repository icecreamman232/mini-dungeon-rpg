using JustGame.Scripts.Player;
using UnityEngine;

namespace JustGame.Scripts.Runtime
{
    [CreateAssetMenu(menuName = "JustGame/Runtime/Global variables")]
    public class GlobalRuntimeVariables : ScriptableObject
    {
        [SerializeField] private Camera m_mainCamera;
        [SerializeField] private Transform m_player;
        [SerializeField] private PlayerHealth m_playerHealth;
        
        public Camera MainCamera => m_mainCamera;

        public Transform PlayerTransform => m_player;
        public PlayerHealth PlayerHealth => m_playerHealth;
        
        public void SetCamera(Camera camera)
        {
            m_mainCamera = camera;
        }

        public void SetPlayer(Transform player)
        {
            m_player = player;
            m_playerHealth = player.GetComponent<PlayerHealth>();
        }
    }
}

