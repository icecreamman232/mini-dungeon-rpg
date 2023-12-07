using System;
using System.Collections;
using JustGame.Scripts.Combat;
using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public class EnemyMovement : MonoBehaviour, SlowEffector
    {
        [Header("Base")]
        [SerializeField] private float m_moveSpeed;
        [SerializeField] private Vector2 m_movingDirection;

        [Space] [Header("Effect")] 
        [SerializeField] private bool m_canBeSlow;

        private float m_curSpeed;
        private bool m_canMove;
        public Vector2 MovingDirection => m_movingDirection;
        public bool IsMoving => m_canMove;


        private void Start()
        {
            m_curSpeed = m_moveSpeed;
        }

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
            
            transform.Translate(m_movingDirection * (Time.deltaTime * m_curSpeed/10));
        }

        public void SetOverrideSpeed(float newSpeed, float duration = 0)
        {
            m_curSpeed = newSpeed;
            if (duration == 0) return;
            StartCoroutine(OverrideSpeedRoutine(duration));
        }

        public void SetOverridePercentSpeed(float percent, float duration = 0)
        {
            var newSpeed = m_curSpeed + m_curSpeed * (percent / 100f);
            SetOverrideSpeed(newSpeed, duration);
        }
        
        private IEnumerator OverrideSpeedRoutine(float duration)
        {
            yield return new WaitForSeconds(duration);
            m_curSpeed = m_moveSpeed;
        }
        
        public void TriggerSlow(float slowPercent, float duration)
        {
            if (!m_canBeSlow) return;
            SetOverridePercentSpeed(slowPercent, duration);
        }
    }
}

