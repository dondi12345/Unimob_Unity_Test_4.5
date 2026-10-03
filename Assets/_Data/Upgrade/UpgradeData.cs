using System.Collections;
using System.Collections.Generic;
using NTNumber;
using Unimob.Construction;
using UnityEngine;

namespace Unimob.Upgrade
{

    public enum UpgradeEnum
    {
        Customer,
        Wheat,
        Wood,
        Steel,
        Clay,
        All,
    }

    [System.Serializable]
    public class UpgradeData
    {
        public int Index;
        public string Name;
        public UpgradeEnum Type;
        public float Value;
        public double Price_Base;
        public int Price_Pow;

        //Calculate
        public BigNumber Price;
        public void Calculate()
        {
            this.Price = new BigNumber(this.Price_Base, this.Price_Pow);
        }
    }
}
