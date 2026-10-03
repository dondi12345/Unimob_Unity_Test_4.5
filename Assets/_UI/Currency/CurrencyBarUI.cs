using System.Collections;
using System.Collections.Generic;
using NTNumber;
using NTPackage.Functions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Unimob.Player
{
    public class CurrencyBarUI : NTBehaviour
    {
        public PlayerCurrency Currency;
        public Image Icon;
        public TextMeshProUGUI Amount;

        protected override void Start()
        {
            base.Start();
            this.Icon.sprite = PlayerManager.Instance.GetIconCurrency(this.Currency);
            this.Amount.text = ServiceBigNumber.FormatBigNumber(PlayerManager.Instance.GetCurrency(this.Currency));
        }

        protected override void Update()
        {
            base.Update();
            this.Amount.text = ServiceBigNumber.FormatBigNumber(PlayerManager.Instance.GetCurrency(this.Currency));
        }
    }
}