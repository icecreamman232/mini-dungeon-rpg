using System.Collections;
using JustGame.Scripts.Common;
using JustGame.Scripts.Managers;
using JustGame.Scripts.Player;
using UnityEngine;

namespace JustGame.Scripts.Weapons
{
    public class PlayerMeleeWeapon : MonoBehaviour
    {
        [SerializeField] private PlayerAim m_playerAim;
        [SerializeField] private PlayerThrowingWeapon m_throwingWeapon;
        [SerializeField] private Transform m_damageArea;
        [SerializeField] private BoxCollider2D m_damageAreaCollider;
        [SerializeField] private float m_atkDuration;
        [SerializeField] private AnimationParameter m_atkAnim;
        [SerializeField] private float m_offsetAngle;

        private bool m_atkInProgress;
        private float m_rotateAngle;
        private InputManager m_input;

        public bool IsInProgress => m_atkInProgress;

        private void Start()
        {
            m_input = InputManager.Instance;
        }

        private void Update()
        {
            if (!m_input.IsInputActive) return;
            
            m_rotateAngle = Mathf.Atan2(m_playerAim.AimDirection.y, m_playerAim.AimDirection.x) * Mathf.Rad2Deg + m_offsetAngle;
            m_damageArea.rotation = Quaternion.AngleAxis(m_rotateAngle,Vector3.forward);

            if (m_input.GetLeftClick())
            {
                WeaponStart();
            }
        }

        public void WeaponStart()
        {
            if (m_throwingWeapon.IsInProgress) return;
            if (m_atkInProgress) return;
            
            StartCoroutine(AttackRoutine());
        }

        private IEnumerator AttackRoutine()
        {
            if (m_atkInProgress)
            {
                yield break;
            }
            m_atkInProgress = true;
            m_damageAreaCollider.enabled = true;
            m_atkAnim.SetBool(true);
            yield return new WaitForSeconds(m_atkDuration);
            m_atkInProgress = false;
            m_damageAreaCollider.enabled = false;
            m_atkAnim.SetBool(false);
        }
        
        public void WeaponStop()
        {
            m_atkAnim.SetBool(false);
            m_atkInProgress = false;
            m_damageAreaCollider.enabled = false;
            StopCoroutine(AttackRoutine());
        }
        
        
    }
}
