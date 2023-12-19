using System;
using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/Cheat Data")]
    public class CheatData : ScriptableObject
    {
        [SerializeField] private bool m_isCheatEnable;
        [SerializeField] private bool m_invulnerable;

        public bool IsCheatEnable => m_isCheatEnable;
        public bool Invulnerable => m_invulnerable;

        private void OnEnable()
        {
            //TODO:Add RELEASE_BUILD script symbol to build process
            #if UNITY_EDITOR || !RELEASE_BUILD
                m_isCheatEnable = true;
            #else 
                m_isCheatEnable = false;
            #endif
        }

        private void OnDestroy()
        {
            m_isCheatEnable = false;
        }
    } 
}

