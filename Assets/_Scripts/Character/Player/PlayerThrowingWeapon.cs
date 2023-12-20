using System;
using System.Collections;
using JustGame.Scripts.Attribute;
using JustGame.Scripts.Managers;
using JustGame.Scripts.Player;
using UnityEngine;

namespace JustGame.Scripts.Weapons
{
    public class PlayerThrowingWeapon : MonoBehaviour
    {
        [SerializeField] private PlayerAim m_playerAim;
        [SerializeField] private PlayerMeleeWeapon m_meleeWeapon;
        [SerializeField] private GameObject m_throwingPickAxePrefab;

        [SerializeField][ReadOnly] private ThrowingPickAxe m_pickAxe;
        private bool m_atkInprogress;
        private InputManager m_input;

        public bool IsInProgress => m_atkInprogress;

        private void Start()
        {
            m_input = InputManager.Instance;
            var pickAxeGO = Instantiate(m_throwingPickAxePrefab, null);
            if (pickAxeGO == null)
            {
                Debug.LogError("Unable to instantiate throwing pick axe");
                return;
            }

            m_pickAxe = pickAxeGO.GetComponent<ThrowingPickAxe>();
            
            //Should get highest level parent transform.
            //As PlayerAim component is put on parent game object, we should use it here
            m_pickAxe.Initialize(m_playerAim.transform);
        }

        private void Update()
        {
            if (!m_input.IsInputActive) return;

            if (m_input.GetRightClick())
            {
                StartThrowing();
            }
        }

        private void StartThrowing()
        {
            if (m_meleeWeapon.IsInProgress) return;
            if (m_atkInprogress) return;
            
            StartCoroutine(AttackRoutine());
        }

        private IEnumerator AttackRoutine()
        {
            if (m_atkInprogress)
            {
                yield break;
            }

            m_atkInprogress = true;
            m_pickAxe.StartMoving(m_playerAim.AimDirection);

            yield return new WaitUntil(() => !m_pickAxe.IsInProgress);
            
            m_atkInprogress = false;
        }
    }
}

