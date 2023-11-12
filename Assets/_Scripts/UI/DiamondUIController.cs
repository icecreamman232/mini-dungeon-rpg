using JustGame.Scripts.Managers;
using TMPro;
using UnityEngine;

namespace JustGame.Scripts.UI
{
    public class DiamondUIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_amountTxt;

        private void Start()
        {
            m_amountTxt.text = "0";
            ResourceManager.Instance.OnChangeDiamondAmount += OnPickingDiamonds;
        }

        private void OnPickingDiamonds(int value)
        {
            m_amountTxt.text = value.ToString();
        }

        private void OnDestroy()
        {
            ResourceManager.Instance.OnChangeDiamondAmount -= OnPickingDiamonds;
        }
    }

}
