using System;
using JustGame.Scripts.Enemy;
using UnityEngine;

namespace JustGame.Scripts.Enemies
{
    public class BrainDecisionsDetectInRange : BrainDecision
    {
        [SerializeField] private float m_range;
        [SerializeField] private LayerMask m_targetMask;
        
        public override bool CheckDecision()
        {
            var result = Physics2D.OverlapCircle(transform.position, m_range, m_targetMask);
            return result != null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, m_range);
        }
    }
}

