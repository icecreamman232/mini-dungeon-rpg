using JustGame.Scripts.Runtime;
using UnityEngine;

namespace JustGame.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Camera m_camera;
        [SerializeField] private GlobalRuntimeVariables m_runtimeVariables;

        private void Awake()
        {
            m_runtimeVariables.SetCamera(m_camera);
        }
    }
}
