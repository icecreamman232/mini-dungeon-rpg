using DG.Tweening;
using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Scripts.UI
{
    public class ScreenFadeController : Singleton<ScreenFadeController>
    {
        [SerializeField] private float m_fadingDuration;
        [SerializeField] private CanvasGroup m_canvasGroup;

        private bool m_isProcess;
        
        public void FadeOutToBlack()
        {
            if (m_isProcess) return;
            m_canvasGroup.DOFade(1, m_fadingDuration);
        }

        public void FadeInFromBlack()
        {
            if (m_isProcess) return;
            m_canvasGroup.DOFade(0, m_fadingDuration);
        }
    }
}

