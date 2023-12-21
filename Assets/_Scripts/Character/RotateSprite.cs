using JustGame.Scripts.Player;
using UnityEngine;

namespace JustGame.Scripts.Combat
{
    /// <summary>
    /// Rotate sprite following aim direction
    /// </summary>
    public class RotateSprite : MonoBehaviour
    {
        [SerializeField] private Transform m_targetTransform;
        private float m_rotateAngle;
        private PlayerAim m_playerAim;
        
        public void AssignPlayerAimComponent(PlayerAim aimComponent)
        {
            m_playerAim = aimComponent;
        }
        
        public void UpdateRotation(float offsetAngle)
        {
            m_rotateAngle = Mathf.Atan2(m_playerAim.AimDirection.y, m_playerAim.AimDirection.x) * Mathf.Rad2Deg + offsetAngle;
            m_targetTransform.rotation = Quaternion.AngleAxis(m_rotateAngle, Vector3.forward);
        }
    }
}
