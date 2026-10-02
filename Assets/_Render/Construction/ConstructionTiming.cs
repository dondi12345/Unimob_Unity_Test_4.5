using System.Collections;
using System.Collections.Generic;
using NTPackage.Functions;
using TMPro;
using Unimob.Product;
using UnityEngine;
using UnityEngine.UI;

namespace Unimob.Construction
{

    public class ConstructionTiming : NTBehaviour
    {
        public float Cooldown;
        public float CurrentCooldown;
        public Slider SliderCooldown;

        public TextMeshProUGUI TextCooldown;

        public void SetData(float cooldown){
            this.Cooldown = cooldown;
            this.CurrentCooldown = cooldown;
            this.UpdateText();
        }

        public void SetValue(float value){
            this.CurrentCooldown = value;
            this.UpdateText();
        }

        public void UpdateText(){
            this.TextCooldown.text = this.CurrentCooldown.ToString("F1")+"s";
            this.SliderCooldown.value = this.CurrentCooldown / this.Cooldown;
        }
    }
}