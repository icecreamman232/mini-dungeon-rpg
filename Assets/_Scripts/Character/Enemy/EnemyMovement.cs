using System;
using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float m_moveSpeed;
        [SerializeField] private Vector2 m_movingDirection;
        private bool m_canMove;
        public Vector2 MovingDirection => m_movingDirection;
        public bool IsMoving => m_canMove;

        public void StartMove()
        {
            m_canMove = true;
        }

        public void StopMove()
        {
            m_canMove = false;
        }

        public void SetDirection(Vector2 newDirection)
        {
            m_movingDirection = newDirection;
        }
        
        private void Update()
        {
            if (!m_canMove) return;
            
            transform.Translate(m_movingDirection * (Time.deltaTime * m_moveSpeed/10));
        }
    }
}

