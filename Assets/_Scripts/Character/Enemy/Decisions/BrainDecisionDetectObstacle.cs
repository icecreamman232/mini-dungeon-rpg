using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public class BrainDecisionDetectObstacle : BrainDecision
    {
        [SerializeField] private LayerMask m_obstacleMask;
        [SerializeField] private EnemyMovement m_enemyMovement;
        private RaycastHit2D m_raycastHit2D;
        public override bool CheckDecision()
        {
            m_raycastHit2D =
                Physics2D.Raycast(transform.position, m_enemyMovement.MovingDirection, 1.5f, m_obstacleMask);
            return m_raycastHit2D;
        }
    }
}

