using UnityEngine;
using UnityEngine.UI;

namespace JustGame.Scripts.UI
{
    public class EnemyHealthBarUI : MonoBehaviour
    {
        [SerializeField] private Image m_healthBar;
        
        public void UpdateHealth(float curValue)
        {
            m_healthBar.fillAmount = curValue;
            if (m_healthBar.fillAmount <= 0)
            {
                this.gameObject.SetActive(false);
            }
        }
    } 
}

