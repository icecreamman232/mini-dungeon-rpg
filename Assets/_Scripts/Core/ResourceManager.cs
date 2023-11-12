using JustGame.Scripts.Events;
using UnityEngine;

namespace JustGame.Scripts.Managers
{
    public class ResourceManager : MonoBehaviour
    {
        [SerializeField] private int m_coins;
        [SerializeField] private int m_diamonds;
        [SerializeField] private IntEvent m_coinPickingEvent;
        [SerializeField] private IntEvent m_diamondPickingEvent;

        private void Start()
        {
            m_coinPickingEvent.AddListener(AfterPickingCoin);
            m_diamondPickingEvent.AddListener(AfterPickingDiamond);
        }

        private void AfterPickingCoin(int value)
        {
            m_coins += value;
        }

        private void AfterPickingDiamond(int value)
        {
            m_diamonds += value;
        }

    }
}

