using System;
using System.Collections;
using JustGame.Scripts.Combat;
using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public enum EnemyMovementState
    {
        STOP,
        MOVING,
        KNOCK_BACK,
    }
    public class EnemyMovement : MonoBehaviour, SlowEffector
    {
        [Header("Base")] 
        [SerializeField] private EnemyMovementState m_curState;
        [SerializeField] private float m_moveSpeed;
        [SerializeField] private Vector2 m_movingDirection;

        [Space] [Header("KnockBack")] 
        [SerializeField] private bool m_immuneKnockBack;
        [SerializeField] private float m_deceleration;

        [Space] [Header("Effect")] 
        [SerializeField] private bool m_canBeSlow;

        private float m_curSpeed;
        private bool m_canMove;
        public Vector2 MovingDirection => m_movingDirection;
        public bool IsMoving => m_canMove;


        private void Start()
        {
            m_curSpeed = m_moveSpeed;
            m_curState = EnemyMovementState.STOP;
        }

        public void StartMove()
        {
            //While being knockback,
            //we wont update direction to avoid breaking knockback routine
            if (m_curState == EnemyMovementState.KNOCK_BACK) return;
            m_canMove = true;
            m_curState = EnemyMovementState.MOVING;
        }

        public void StopMove()
        {
            //While being knockback,
            //we wont update direction to avoid breaking knockback routine
            if (m_curState == EnemyMovementState.KNOCK_BACK) return;
            m_canMove = false;
            m_curState = EnemyMovementState.STOP;
        }

        public void SetDirection(Vector2 newDirection)
        {
            //While being knockback,
            //we wont update direction to avoid breaking knockback routine
            if (m_curState == EnemyMovementState.KNOCK_BACK) return;
            m_movingDirection = newDirection;
        }
        
        private void Update()
        {
            if (!m_canMove) return;
            ComputeSpeed();
            transform.Translate(m_movingDirection * (Time.deltaTime * m_curSpeed/10));
        }
        
        /// <summary>
        /// Base on current state to compute proper speed
        /// </summary>
        private void ComputeSpeed()
        {
            switch (m_curState)
            {
                case EnemyMovementState.STOP:
                    break;
                case EnemyMovementState.MOVING:
                    break;
                case EnemyMovementState.KNOCK_BACK:
                    if (m_curSpeed > 0)
                    {
                        m_curSpeed -= m_deceleration * Time.deltaTime;
                        m_curSpeed = Mathf.Clamp(m_curSpeed, 0, m_moveSpeed);
                    }
                    break;
            }
        }

        public void ApplyKnockBack(Vector2 direction, float force, float duration)
        {
            if (m_immuneKnockBack) return;
            if (!gameObject.activeSelf) return;

            StartCoroutine(KnockBackRoutine(direction, force, duration));
        }

        private IEnumerator KnockBackRoutine(Vector2 direction, float force, float duration)
        {
            if (m_curState == EnemyMovementState.KNOCK_BACK)
            {
                yield break;
            }
            Debug.Log("Enter knockback routine");
            var prevDir = m_movingDirection;
            var prevState = m_curState;
            var prevSpeed = m_curSpeed;

            m_movingDirection = direction;
            m_curState = EnemyMovementState.KNOCK_BACK;
            m_curSpeed = force;
            float timer = 0;
            
            while (timer < duration)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            m_movingDirection = prevDir;
            m_curState = prevState;
            m_curSpeed = prevSpeed;
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

