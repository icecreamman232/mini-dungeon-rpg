using JustGame.Scripts.Runtime;
using UnityEngine;

namespace JustGame.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GlobalRuntimeVariables m_runtimeVariables;

        private void Start()
        {
            m_runtimeVariables.SetPlayer(this.transform);
        }
    }
}

