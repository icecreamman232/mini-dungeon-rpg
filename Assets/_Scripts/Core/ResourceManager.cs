using System;
using JustGame.Scripts.Events;
using UnityEngine;

namespace JustGame.Scripts.Managers
{
    public class ResourceManager : Singleton<ResourceManager>
    {
        [SerializeField] private int m_coins;
        [SerializeField] private int m_diamonds;
        [SerializeField] private IntEvent m_coinPickingEvent;
        [SerializeField] private IntEvent m_diamondPickingEvent;

        public Action<int> OnChangeCoinAmount;
        public Action<int> OnChangeDiamondAmount;

        private void Start()
        {
            m_coinPickingEvent.AddListener(AfterPickingCoin);
            m_diamondPickingEvent.AddListener(AfterPickingDiamond);
        }

        private void AfterPickingCoin(int value)
        {
            m_coins += value;
            OnChangeCoinAmount?.Invoke(m_coins);
        }

        private void AfterPickingDiamond(int value)
        {
            m_diamonds += value;
            OnChangeDiamondAmount?.Invoke(m_diamonds);
        }

    }
}

