using NTNumber;
using NTPackage.Functions;
using NTPackage.UI;
using TMPro;
using Unimob.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Unimob.Construction
{
    public class ConstructionUnlock : PopupUI
    {
        public TextMeshProUGUI TextName;
        public TextMeshProUGUI TextPrice;
        public Image Icon;
        public TooltipCustom TooltipCustom;

        public ConstructionRender Render;

        public void SetData(ConstructionRender render, Vector2 screenPoint){
            this.TooltipCustom.Show(render, screenPoint);
            this.Render = render;
            this.Icon.sprite = ConstructionDataController.Instance.GetIcon(render.Construction.Type);
            this.TextName.text = ConstructionDataController.Instance.GetName(render.Construction.Type);
            this.TextPrice.text = ServiceBigNumber.FormatBigNumber(render.Construction.GetBuyCost());

            if(MathBigNumber.IsGreaterThanOrEqual(PlayerManager.Instance.GetGold(), render.Construction.GetBuyCost())){
                this.TextPrice.color = Color.white;
            }else{
                this.TextPrice.color = Color.red;
            }
        }

        public void Unlock(){
            if(MathBigNumber.IsGreaterThanOrEqual(PlayerManager.Instance.GetGold(), this.Render.Construction.GetBuyCost())){
                PlayerManager.Instance.SpendGold(this.Render.Construction.GetBuyCost());
                this.Render.Unlock();
                this.Hide();
            }
        }
    }
}
