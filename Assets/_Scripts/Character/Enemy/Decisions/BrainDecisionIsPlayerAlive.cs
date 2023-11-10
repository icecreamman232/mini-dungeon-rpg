using JustGame.Scripts.Runtime;
using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public class BrainDecisionIsPlayerAlive : BrainDecision
    {
        [SerializeField] private GlobalRuntimeVariables m_globalRuntime;
        public override bool CheckDecision()
        {
            if (m_globalRuntime.PlayerHealth == null) return false;
            return !m_globalRuntime.PlayerHealth.IsDead;
        }
    }
}

