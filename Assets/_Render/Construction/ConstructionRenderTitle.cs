using System.Collections;
using System.Collections.Generic;
using NTNumber;
using NTPackage.Functions;
using NTPackage.UI;
using TMPro;
using Unimob.Product;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Unimob.Construction
{

    public class ConstructionRenderTitle : NTBehaviour
    {
        public ConstructionRender ConstructionRender;

        public TextMeshProUGUI TextPrice;
        public Image Icon;
        public TextMeshProUGUI TextOffline;

        public void Init(){
            gameObject.SetActive(false);
        }

        public void DoneUnlocking(){
            this.gameObject.SetActive(true);
            this.UpdateData();
        }

        public void UpdateData(){
            this.TextPrice.text = ServiceBigNumber.FormatBigNumber(this.ConstructionRender.Construction.GetFinalIncome());
            this.Icon.sprite = ConstructionDataController.Instance.GetIcon(this.ConstructionRender.Construction.Type);
            this.TextOffline.text = ServiceBigNumber.FormatBigNumber(this.ConstructionRender.Construction.GetOffline()) + "/min";
        }
    }
}
