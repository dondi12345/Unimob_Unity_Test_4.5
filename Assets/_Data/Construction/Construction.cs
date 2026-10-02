using System.Collections;
using System.Collections.Generic;
using NTNumber;
using UnityEngine;

namespace Unimob.Construction
{
    [System.Serializable]
    public class Construction
    {
        public ConstructionType Type;
        public int Level; // Level 0 is not bought

        //Data
        public ConstructionData ConstructionData;

        //Cache
        public BigNumber Income;
        public BigNumber UpgradeCost;
        public BigNumber FinalIncome;
        public BigNumber Offline;

        public float MultiplierIncome;
        
        #region Function

        public void Init(ConstructionType type){
            this.Type = type;
            this.Level = 0;
            this.ConstructionData = ConstructionDataController.Instance.GetConstructionData(type);
            this.Income = ConstructionDataController.Instance.GetIncome(type);
            this.UpgradeCost = ConstructionDataController.Instance.GetUpgradeCost(type);
            this.Offline = ConstructionDataController.Instance.GetOffline(type);
            this.FinalIncome = this.Income;
            this.MultiplierIncome = 1;
        }

        public void Bought(){
            this.Level = 1;
        }

        public void Upgrade(){
            this.Level++;
            this.Income = MathBigNumber.Add(this.Income, ConstructionDataController.Instance.GetIncomeUp(this.Type));
            this.UpgradeCost = MathBigNumber.Multiply(this.UpgradeCost, ConstructionDataController.Instance.GetUpgradeCostMul(this.Type));
            this.FinalIncome = MathBigNumber.Multiply(this.Income, this.MultiplierIncome);
            this.Offline = MathBigNumber.Add(this.Offline, ConstructionDataController.Instance.GetOfflineUp(this.Type));
        }

        #endregion

        #region Get
        public BigNumber GetFinalIncome(){
            return this.FinalIncome;
        }
        public BigNumber GetUpgradeCost(){
            return this.UpgradeCost;
        }

        public BigNumber GetBuyCost(){
            return ConstructionDataController.Instance.GetBuyCost(this.Type);
        }

        public BigNumber GetOffline(){
            return this.Offline;
        }

        public bool IsLevelMax(){
            return this.Level >= this.ConstructionData.LevelMax;
        }

        public float GetCooldown(){
            return this.ConstructionData.Cooldown;
        }
        #endregion
    }
}