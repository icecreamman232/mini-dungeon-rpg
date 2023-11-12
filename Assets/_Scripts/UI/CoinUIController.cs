using JustGame.Scripts.Managers;
using TMPro;
using UnityEngine;

namespace JustGame.Scripts.UI
{
    public class CoinUIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_amountTxt;

        private void Start()
        {
            m_amountTxt.text = "0";
            ResourceManager.Instance.OnChangeCoinAmount += OnPickingCoin;
        }

        private void OnPickingCoin(int value)
        {
            m_amountTxt.text = value.ToString();
        }

        private void OnDestroy()
        {
            ResourceManager.Instance.OnChangeCoinAmount -= OnPickingCoin;
        }
    }
}
