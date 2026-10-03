using System;
using System.Collections;
using System.Collections.Generic;
using NTNumber;
using NTPackage.Functions;
using NTPackage.UI;
using TMPro;
using Unimob.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Unimob.Upgrade
{
    public class UpgradeDataItemUI : NTBehaviour
    {
        public Image Icon;
        public TextMeshProUGUI TextTitle;
        public TextMeshProUGUI TextDes;
        public TextMeshProUGUI TextPrice;

        public UpgradeData UpgradeData;

        public Action OnClickUpgrade;

        public void Init(UpgradeData upgradeData, Action onClickUpgrade){
            this.UpgradeData = upgradeData;
            this.Icon.sprite = UpgradeDataController.Instance.GetUpgradeIcon(upgradeData.Type);
            this.TextTitle.text = UpgradeDataController.Instance.GetTitle(upgradeData.Type);
            this.TextDes.text = UpgradeDataController.Instance.GetDescription(upgradeData.Type, upgradeData.Value);
            this.TextPrice.text = ServiceBigNumber.FormatBigNumber(upgradeData.Price);
            this.OnClickUpgrade = onClickUpgrade;
            if(MathBigNumber.IsGreaterThanOrEqual(PlayerManager.Instance.GetGold(), upgradeData.Price)){
                this.TextPrice.color = Color.white;
            }else{
                this.TextPrice.color = Color.red;
            }
        }

        public void _OnclickUpgrade(){
            if(MathBigNumber.IsGreaterThanOrEqual(PlayerManager.Instance.GetGold(), this.UpgradeData.Price)){
                UpgradeDataController.Instance.BoughtUpgrade(this.UpgradeData.Index);
                this.OnClickUpgrade?.Invoke();
            }
        }
    }
}