using System.Collections;
using System.Collections.Generic;
using NTNumber;
using NTPackage.UI;
using TMPro;
using Unimob.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Unimob.Construction
{
    public class ConstructionUpgrade : PopupUI
    {
        public TextMeshProUGUI TextLevel;
        public TextMeshProUGUI TextName;
        public TextMeshProUGUI TextIncome;
        public TextMeshProUGUI TextCooldown;
        public TextMeshProUGUI TextPrice;
        public Image Icon;
        public TooltipCustom TooltipCustom;

        public NTButtonEffect ButtonUpgrade;

        public ConstructionRender Render;

        public void SetData(ConstructionRender render, Vector2 screenPoint)
        {
            this.TooltipCustom.Show(render, screenPoint);
            this.Render = render;
            this.UpdateData();
        }

        public void UpdateData()
        {
            this.Icon.sprite = ConstructionDataController.Instance.GetIcon(this.Render.Construction.Type);
            this.TextName.text = ConstructionDataController.Instance.GetName(this.Render.Construction.Type);
            this.TextLevel.text = "Level " + this.Render.Construction.Level.ToString();
            this.TextIncome.text = ServiceBigNumber.FormatBigNumber(this.Render.Construction.GetFinalIncome());
            this.TextCooldown.text = this.Render.Construction.GetCooldown().ToString("F0") + "s";

            this.TextPrice.text = ServiceBigNumber.FormatBigNumber(this.Render.Construction.GetUpgradeCost());
            if (MathBigNumber.IsGreaterThanOrEqual(PlayerManager.Instance.GetGold(), this.Render.Construction.GetUpgradeCost()))
            {
                this.TextPrice.color = Color.white;
            }
            else
            {
                this.TextPrice.color = Color.red;
            }
            if(this.Render.Construction.IsLevelMax()){
                this.ButtonUpgrade.UnChose();
                this.TextPrice.color = Color.white;
                this.TextPrice.text = "Max";
            }
            else{
                this.ButtonUpgrade.Chose();
            }
        }

        public void Upgrade()
        {
            if (MathBigNumber.IsGreaterThanOrEqual(PlayerManager.Instance.GetGold(), this.Render.Construction.GetUpgradeCost()))
            {
                PlayerManager.Instance.SpendGold(this.Render.Construction.GetUpgradeCost());
                this.Render.Upgrade();
                this.UpdateData();
            }
        }
    }
}
