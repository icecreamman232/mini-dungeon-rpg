using System;
using UnityEngine;
using UnityEngine.UI;

namespace JustGame.Scripts.UI
{
    public class HeartIconController : MonoBehaviour
    {
        [SerializeField] private Image m_image;
        [SerializeField] private Color m_hasLifeColor;
        [SerializeField] private Color m_noLifeColor;


        private void Start()
        {
            SetToHasLifeColor();
        }

        public void SetToHasLifeColor()
        {
            m_image.color = m_hasLifeColor;
        }

        public void SetToNoLifeColor()
        {
            m_image.color = m_noLifeColor;
        }
    }
}

