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

        public void UpdateRotation(Vector2 direction, float offsetAngle)
        {
            m_rotateAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + offsetAngle;
            m_targetTransform.rotation = Quaternion.AngleAxis(m_rotateAngle, Vector3.forward);
        }
    }
}
