using System;
using System.Collections;
using System.Collections.Generic;
using NTNumber;
using NTPackage;
using NTPackage.Functions;
using SimpleJSON;
using UnityEngine;

namespace Unimob.Construction
{
    public class ConstructionDataController : NTBehaviour
    {  
        [SerializeField]
        private NTDictionary<ConstructionType, ConstructionData> _constructionDataList;
        [SerializeField]
        private TextAsset _constructionDataJson;

        public static ConstructionDataController Instance;
        protected override void Awake()
        {
            base.Awake();
            if (ConstructionDataController.Instance != null)
            {
                NTLog.LogError("Only 1 Instance allow");
                return;
            }
            ConstructionDataController.Instance = this;
        }

        #region Load Data

        [NTButton]
        public void LoadData()
        {
            _constructionDataList = new NTDictionary<ConstructionType, ConstructionData>();
            JSONNode jsonNode = JSON.Parse(_constructionDataJson.text);
            foreach (JSONNode item in jsonNode.AsArray){
                ConstructionData constructionData = new ConstructionData();
                constructionData.Type = NTFunction.ParseEnumFromString<ConstructionType>(item["Name"].Value);
                constructionData.Name = item["Name"].Value;
                constructionData.LevelMax = item["LevelMax"].AsInt;
                constructionData.Cooldown = item["Cooldown"].AsFloat;
                constructionData.Income_Base = item["Income_Base"].AsDouble;
                constructionData.Income_Pow = item["Income_Pow"].AsInt;
                constructionData.UpgradeCost_Base = item["UpgradeCost_Base"].AsDouble;
                constructionData.UpgradeCost_Pow = item["UpgradeCost_Pow"].AsInt;
                constructionData.IncomeUp_Base = item["IncomeUp_Base"].AsDouble;
                constructionData.IncomeUp_Pow = item["IncomeUp_Pow"].AsInt;
                constructionData.CalculateIncome();
                _constructionDataList.Add(constructionData.Type, constructionData);
            }
            
        }
        #endregion

        #region Get
        public ConstructionData GetConstructionData(ConstructionType type){
            if(_constructionDataList.Contains(type)){
                return _constructionDataList.Get(type);
            }
            NTLog.LogError($"ConstructionData not found: {type}");
            return null;
        }
        public BigNumber GetIncome(ConstructionType type){
            ConstructionData constructionData = _constructionDataList.Get(type);
            if(constructionData == null){
                NTLog.LogError($"ConstructionData not found: {type}");
                return new BigNumber(0);
            }
            return constructionData.Income;
        }

        public double GetUpgradeCostMul(ConstructionType type){
            ConstructionData constructionData = _constructionDataList.Get(type);
            if(constructionData == null){
                NTLog.LogError($"ConstructionData not found: {type}");
                return 1;
            }
            return constructionData.UpgradeCost_Mul;
        }

        public BigNumber GetUpgradeCost(ConstructionType type){
            ConstructionData constructionData = _constructionDataList.Get(type);
            if(constructionData == null){
                NTLog.LogError($"ConstructionData not found: {type}");
                return new BigNumber(0);
            }
            return constructionData.UpgradeCost;
        }

        public BigNumber GetIncomeUp(ConstructionType type){
            ConstructionData constructionData = _constructionDataList.Get(type);
            if(constructionData == null){
                NTLog.LogError($"ConstructionData not found: {type}");
                return new BigNumber(0);
            }
            return constructionData.IncomeUp;
        }

        public BigNumber GetBuyCost(ConstructionType type){
            ConstructionData constructionData = _constructionDataList.Get(type);
            if(constructionData == null){
                NTLog.LogError($"ConstructionData not found: {type}");
                return new BigNumber(0);
            }
            return constructionData.BuyCost;
        }

        #endregion

    }
}
