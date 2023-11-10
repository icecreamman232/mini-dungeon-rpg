using JustGame.Scripts.Runtime;
using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    /// <summary>
    /// Set player as brain target
    /// </summary>
    public class BrainActionSetTargetPlayer : BrainAction
    {
        [SerializeField] private GlobalRuntimeVariables m_globalRuntime;
        
        public override void DoAction()
        {
            if (m_globalRuntime.PlayerTransform == null)
            {
                return;
            }
            m_brain.Target = m_globalRuntime.PlayerTransform;
        }
    }
}

