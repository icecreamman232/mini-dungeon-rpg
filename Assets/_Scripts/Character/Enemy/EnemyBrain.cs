using System;
using System.Collections.Generic;
using JustGame.Scripts.Enemies;
using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public class EnemyBrain : MonoBehaviour
    {
        public List<BrainState> States;
        public Transform Target;
        public bool BrainActive;
        public BrainState CurrentState;
        public float TimeInState;
        
        private EnemyHealth m_enemyHealth;
        
        private void Start()
        {
            m_enemyHealth = GetComponentInParent<EnemyHealth>();
            m_enemyHealth.OnDeath += OnDeath;
            foreach (var state in States)
            {
                state.Initialize(this);
            }

            CurrentState = States[0];
            CurrentState.EnterState();
        }

        private void OnDeath()
        {
            if (BrainActive)
            {
                BrainActive = false;
                ResetBrain();
            }
        }

        public void ResetBrain()
        {
            CurrentState = States[0];
        }
        
        private void Update()
        {
            if (!BrainActive) return;

            if (CurrentState == null) return;
            
            CurrentState.DoActions();
            
            if (!BrainActive) return;
            
            CurrentState.CheckTransitions();

            TimeInState += Time.deltaTime;
        }

        public void TransitionToState(string stateName)
        {
            if (CurrentState == null)
            {
                CurrentState = FindState(stateName);
                if (CurrentState != null)
                {
                    CurrentState.EnterState();
                }

                return;
            }

            if (stateName != CurrentState.StateName)
            {
                CurrentState.ExitState();
                TimeInState = 0;
                CurrentState = FindState(stateName);
                if (CurrentState != null)
                {
                    CurrentState.EnterState();
                }
            }
        }

        public BrainState FindState(string stateName)
        {
            foreach (var state in States)
            {
                if (state.StateName == stateName)
                {
                    return state;
                }
            }

            if (stateName != "")
            {
                Debug.LogError($"State name {stateName} not found!");
            }

            return null;
        }
        
        public BrainAction[] GetAttachedActions()
        {
            var actions = GetComponentsInChildren<BrainAction>();
            return actions;
        }

        public BrainDecision[] GetAttachedDecisions()
        {
            var decisions = GetComponentsInChildren<BrainDecision>();
            return decisions;
        }

        private void OnDestroy()
        {
            m_enemyHealth.OnDeath -= OnDeath;
        }
    }
}
