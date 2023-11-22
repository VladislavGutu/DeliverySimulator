using System;
using TMPro;
using UnityEngine;

namespace _Assets.OldTruck.Scripts._NotifyManager
{
    public class MoneyInfo : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _moneyText;
        [SerializeField] private TextMeshProUGUI _premiumText;
        
        private void OnEnable()
        {
            // _moneyText.text = MoneyManager.Instance.Currency + "<sprite=1>";
            // _premiumText.text = MoneyManager.Instance.PremiumCurrency + "<sprite=0>";
            
        }
    }
}
