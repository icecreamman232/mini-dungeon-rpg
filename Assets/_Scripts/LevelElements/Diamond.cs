using JustGame.Scripts.Events;
using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Scripts.LevelElements
{
    public class Diamond : Item
    {
        [SerializeField] private IntEvent m_pickingDiamondEvent;
        [SerializeField] private int m_diamondValue;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer != LayerManager.PlayerLayer) return;

            OnPicking();
        }

        protected override void OnPicking()
        {
            m_pickingDiamondEvent.Raise(m_diamondValue);
            base.OnPicking();
        }
    }
}


