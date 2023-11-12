using JustGame.Scripts.Events;
using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Scripts.LevelElements
{
    public class Coin : Item
    {
        [SerializeField] private IntEvent m_pickingCoinEvent;
        [SerializeField] private int m_coinValue;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer != LayerManager.PlayerLayer) return;

            OnPicking();
        }

        protected override void OnPicking()
        {
            m_pickingCoinEvent.Raise(m_coinValue);
            base.OnPicking();
        }
    }
}

