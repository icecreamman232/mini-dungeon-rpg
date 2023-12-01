using JustGame.Scripts.Enemy;
using UnityEngine;

namespace JustGame.Scripts.Enemies
{
    public class BrainActionsPatrolWithinBounds : BrainAction
    {
        [SerializeField] private bool m_isHorizontal;
        [SerializeField][Min(0)] private float m_leftExtends;
        [SerializeField][Min(0)] private float m_rightExtends;
        [SerializeField] private EnemyMovement m_enemyMovement;
        [SerializeField] private float m_minOffsetToDestination;
        
        private Vector2 m_leftBounds;
        private Vector2 m_rightBounds;
        private Vector2 m_destination;
        private bool m_isMovingToRight;
        
        public override void Initialize(EnemyBrain brain)
        {
            base.Initialize(brain);
            var currentPos = transform.position;
            if (m_isHorizontal)
            {
                m_leftBounds = new Vector2(currentPos.x - m_leftExtends, currentPos.y);
                m_rightBounds = new Vector2(currentPos.x + m_rightExtends, currentPos.y);
            }
            else
            {
                //Up
                m_leftBounds = new Vector2(currentPos.x, currentPos.y + m_leftExtends);
                //Down
                m_rightBounds = new Vector2(currentPos.x, currentPos.y - m_rightExtends);
            }

            m_isMovingToRight = true;
            m_destination = m_rightBounds;
        }

        public override void OnEnterState()
        {
            base.OnEnterState();
            m_enemyMovement.StartMove();
        }

        public override void DoAction()
        {
            if (m_isHorizontal)
            {
                m_enemyMovement.SetDirection(m_isMovingToRight? Vector2.right: Vector2.left);
            }
            else
            {
                m_enemyMovement.SetDirection(m_isMovingToRight? Vector2.down: Vector2.up);
            }
        }

        private void Update()
        {
            if (!m_enemyMovement.IsMoving) return;
            //Try to switch to opposite side when enemy reached the bounds
            float dist = Vector2.Distance(transform.position, m_destination);
            if (dist <= m_minOffsetToDestination)
            {
                m_isMovingToRight = !m_isMovingToRight;
                m_destination = m_isMovingToRight ? m_rightBounds : m_leftBounds;
            }
        }

        public override void OnExitState()
        {
            base.OnExitState();
            m_enemyMovement.SetDirection(Vector2.zero);
            m_enemyMovement.StopMove();
        }

        private void OnDrawGizmos()
        {
            var curPos = transform.position;
            if (!Application.isPlaying)
            {
                if (m_isHorizontal)
                {
                    m_leftBounds = new Vector2(curPos.x - m_leftExtends, curPos.y);
                    m_rightBounds = new Vector2(curPos.x + m_rightExtends, curPos.y);
                }
                else
                {
                    //Up
                    m_leftBounds = new Vector2(curPos.x, curPos.y + m_leftExtends);
                    //Down
                    m_rightBounds = new Vector2(curPos.x, curPos.y - m_rightExtends);
                } 
            }
            
            Gizmos.color = Color.green;
            Gizmos.DrawCube(curPos,Vector3.one * 0.15f);
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(curPos,m_leftBounds);
            Gizmos.DrawLine(curPos,m_rightBounds);
            Gizmos.DrawSphere(m_leftBounds,0.15f);
            Gizmos.DrawSphere(m_rightBounds,0.15f);
        }
    }
}

