using System;
using JustGame.Scripts.Combat;
using JustGame.Scripts.Data;
using JustGame.Scripts.Player;
using UnityEngine;

namespace JustGame.Scripts.Weapons
{
    public enum ThrowingPickAxeState
    {
        READY,
        MOVING_FORWARD,
        MOVING_BACKWARD,
    }
    public class ThrowingPickAxe : MonoBehaviour
    {
        [SerializeField] private ThrowingPickAxeState m_currentState;
        [SerializeField] private LayerMask m_targetMask;
        [SerializeField] private ObstacleHandler m_obstacleHandler;
        [SerializeField] private RangePickAxeDamageHandler m_rangeDamageHandler;
        [SerializeField] private float m_moveSpeed;
        [SerializeField] private float m_movingRange;
        [SerializeField] private float m_minDistanceToPlayer;
        [SerializeField] private RotateSprite m_rotateSprite;
        [SerializeField] private PlayerData m_playerData;

        private Vector2 m_startPos;
        private Vector2 m_movingDirection;
        private float m_traveledDistance;
        private bool m_isInProgress;
        private Transform m_player;
        private GameObject m_pickAxeSpriteGO;
        private PlayerAim m_playerAim;
        
        private readonly float OFFSET_PICKAXE_SPRITE_ANGLE = -45f;
        
        public bool IsInProgress => m_isInProgress;

        public void Initialize(Transform player)
        {
            m_player = player;
            m_playerAim = m_player.GetComponent<PlayerAim>();
            m_pickAxeSpriteGO = transform.GetChild(0).gameObject;
            m_pickAxeSpriteGO.SetActive(false); //Hide the throwing pick axe on start
            m_currentState = ThrowingPickAxeState.READY;
            m_moveSpeed = m_playerData.FlyingSpeed;
            m_movingRange = m_playerData.FlyingRange;
            m_obstacleHandler.OnHitObstacle += OnHittingObstacle;
        }
        
        public void StartMoving(Vector2 direction)
        {
            m_pickAxeSpriteGO.SetActive(true);
            m_currentState = ThrowingPickAxeState.MOVING_FORWARD;
            m_isInProgress = true;
            m_movingDirection = direction;
            m_startPos = m_player.position;
            transform.position = m_startPos;
            m_rangeDamageHandler.ResetDamage();
            m_rangeDamageHandler.EnableKnockBack();
        }

        private void StartMovingBackward()
        {
            m_currentState = ThrowingPickAxeState.MOVING_BACKWARD;
            //Reduce damage when the pickaxe returning to player
            m_rangeDamageHandler.DisableKnockBack();
            m_rangeDamageHandler.SetDamageReduce(m_playerData.RangeDamageReducePercentage);
        }

        public void StopMoving()
        {
            m_pickAxeSpriteGO.SetActive(false);
            m_isInProgress = false;
            m_traveledDistance = 0;
            m_currentState = ThrowingPickAxeState.READY;
        }

        private void OnHittingObstacle()
        {
            if (m_currentState == ThrowingPickAxeState.MOVING_FORWARD)
            {
                StartMovingBackward();
            }
        }

        private float m_angleRot;
        [SerializeField] private float m_angleSpeed;
        [SerializeField] private Transform m_bodySprite;
        private void Update()
        {
            if (!m_isInProgress) return;

            switch (m_currentState)
            {
                case ThrowingPickAxeState.READY:
                    m_angleRot = 0;
                    //We do not update any movement here so we must return
                    return;
                case ThrowingPickAxeState.MOVING_FORWARD:
                    transform.Translate(m_movingDirection * (m_moveSpeed * Time.deltaTime));
                    m_traveledDistance = Vector2.Distance(m_startPos, transform.position);
                    //m_rotateSprite.UpdateRotation(m_playerAim.AimDirection, OFFSET_PICKAXE_SPRITE_ANGLE);
                    if (m_traveledDistance >= m_movingRange)
                    {
                        StartMovingBackward();
                    }
                    break;
                case ThrowingPickAxeState.MOVING_BACKWARD:
                    m_movingDirection = (m_player.position - transform.position).normalized;
                    transform.Translate(m_movingDirection * (m_moveSpeed * Time.deltaTime));
                    //m_rotateSprite.UpdateRotation(m_playerAim.AimDirection * -1, OFFSET_PICKAXE_SPRITE_ANGLE);
                    //We keep moving to wherever player's at
                    var distToPlayer = Vector2.Distance(transform.position, m_player.position);
                    if (distToPlayer <= m_minDistanceToPlayer)
                    {
                        StopMoving();
                    }
                    break;
            }

            m_angleRot += m_angleSpeed * Time.deltaTime;
            if (m_angleRot >= 360)
            {
                m_angleRot = 0;
            }
            m_bodySprite.transform.rotation = Quaternion.AngleAxis(
                m_angleRot * (m_movingDirection.x > 0 ? -1 : 1 )
                , Vector3.forward);
        }

        private void OnDestroy()
        {
            m_obstacleHandler.OnHitObstacle -= OnHittingObstacle;
        }
    }
}

