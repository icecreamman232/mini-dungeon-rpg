using System;
using JustGame.Scripts.Attribute;
using JustGame.Scripts.LevelElements;
using Unity.VisualScripting;
using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/Loot Data")]
    public class LootContainer : ScriptableObject
    {
        public LootData[] LootList;
    }
    
    [Serializable]
    public class LootData
    {
        public Item Item;
        public int Amount;
        public float Weight;
        [SerializeField][ReadOnly] private float m_lowerPercent;
        [SerializeField][ReadOnly] private float m_upperPercent;

        public float LowerPercent
        {
            get => m_lowerPercent;
            set => m_lowerPercent = value;
        }

        public float UpperPercent
        {
            get => m_upperPercent;
            set => m_upperPercent = value;
        }
    }
}

