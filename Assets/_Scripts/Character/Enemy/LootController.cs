using JustGame.Scripts.Data;
using UnityEngine;

namespace JustGame.Scripts.Enemies
{
    public class LootController : MonoBehaviour
    {
        [SerializeField] private LootContainer m_lootData;
        private void Start()
        {
            ComputeDropWeight();
        }

        private void ComputeDropWeight()
        {
            float m_curPercent = 0;
            float m_nextPercent;
            float totalWeight = 0;

            for (int i = 0; i < m_lootData.LootList.Length; i++)
            {
                totalWeight += m_lootData.LootList[i].Weight;
            }
            
            for (int i = 0; i < m_lootData.LootList.Length; i++)
            {
                m_nextPercent = m_lootData.LootList[i].Weight/totalWeight * 100f + m_curPercent;
                m_lootData.LootList[i].LowerPercent = m_curPercent;
                m_lootData.LootList[i].UpperPercent = m_nextPercent;
                
                m_curPercent = m_nextPercent;
            }
        }
    }
}

