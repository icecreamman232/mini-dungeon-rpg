using UnityEngine;

namespace JustGame.Scripts.Runtime
{
    [CreateAssetMenu(menuName = "JustGame/Runtime/Global variables")]
    public class GlobalRuntimeVariables : ScriptableObject
    {
        [SerializeField] private Camera m_mainCamera;

        public Camera MainCamera => m_mainCamera;
        
        public void SetCamera(Camera camera)
        {
            m_mainCamera = camera;
        }
    }
}

