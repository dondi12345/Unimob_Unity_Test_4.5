using System.Collections;
using System.Collections.Generic;
using NTNumber;
using UnityEngine;

namespace Unimob.Construction
{
    public enum ConstructionType{
        Wheat,
        Wood,
        Steel,
        Clay,
    }

    [System.Serializable]
    public class ConstructionData {
        public ConstructionType Type;
        public int LevelMax;
        public string Name;
        public float Cooldown;
        public double Income_Base;
        public int Income_Pow;
        public double UpgradeCost_Base;
        public int UpgradeCost_Pow;
        public double UpgradeCost_Mul;
        public double IncomeUp_Base;
        public int IncomeUp_Pow;
        public double BuyCost_Base;
        public int BuyCost_Pow;
        public int TimeUnlock;

        //Cache
        public BigNumber Income;
        public BigNumber UpgradeCost;
        public BigNumber IncomeUp;
        public BigNumber BuyCost;

        public void CalculateIncome(){
            this.Income = new BigNumber(Income_Base, Income_Pow);
            this.IncomeUp = new BigNumber(IncomeUp_Base, IncomeUp_Pow);
            this.UpgradeCost = new BigNumber(UpgradeCost_Base, UpgradeCost_Pow);
            this.BuyCost = new BigNumber(BuyCost_Base, BuyCost_Pow);
        }
    }
}
