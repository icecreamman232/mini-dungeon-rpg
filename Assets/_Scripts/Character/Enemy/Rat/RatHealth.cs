using JustGame.Scripts.Enemies;
using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    public class RatHealth : EnemyHealth
    {
        [SerializeField] private SpriteRenderer m_spriteRenderer;
        [SerializeField] private Collider2D m_bodyHitBox;
        
        protected override void Kill()
        {
            OnDeath?.Invoke();
            m_spriteRenderer.enabled = false;
            m_bodyHitBox.enabled = false;
        }
    }
}

