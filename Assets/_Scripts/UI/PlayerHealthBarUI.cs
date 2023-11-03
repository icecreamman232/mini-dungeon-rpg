using System;
using System.Collections.Generic;
using JustGame.Scripts.Events;
using UnityEngine;

namespace JustGame.Scripts.UI
{
    public class PlayerHealthBarUI : MonoBehaviour
    {
        [SerializeField] private IntEvent m_healthEvent;
        [SerializeField] private List<GameObject> m_lifeIconList;

        private void Start()
        {
            m_healthEvent.AddListener(OnPlayerUpdateHealth);
        }

        private void OnPlayerUpdateHealth(int currentLife)
        {
            m_lifeIconList[currentLife + 1].SetActive(false);
        }

        private void OnDestroy()
        {
            m_healthEvent.RemoveListener(OnPlayerUpdateHealth);
        }
    }
}

