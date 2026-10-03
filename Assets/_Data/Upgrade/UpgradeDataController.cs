using System;
using System.Collections;
using System.Collections.Generic;
using NTNumber;
using NTPackage;
using NTPackage.EventDispatcher;
using NTPackage.Functions;
using SimpleJSON;
using Unimob.Construction;
using Unimob.Market;
using Unimob.Player;
using UnityEngine;

namespace Unimob.Upgrade
{

    public class UpgradeDataController : NTBehaviour
    {
        [SerializeField]
        private List<UpgradeData> ListUpgradeData;
        [SerializeField]
        private TextAsset UpgradeDataJson;
        [SerializeField]
        private List<Sprite> ListUpgradeIcon;
        
        [SerializeField]
        private NTDictionary<int, UpgradeData> DictionaryUpgradeDataBought;

        public static UpgradeDataController Instance;
        protected override void Awake()
        {
            base.Awake();
            if (UpgradeDataController.Instance != null){
               NTLog.LogError("Only 1 Instance allow");
               return;
             }
            UpgradeDataController.Instance = this;
        }

        #region LoadData
        [NTButton]
        public void LoadData()
        {
            JSONNode jsonNode = JSON.Parse(this.UpgradeDataJson.text);
            this.ListUpgradeData.Clear();
            foreach (JSONNode item in jsonNode)
            {
                UpgradeData upgradeData = new UpgradeData();
                upgradeData.Index = item["Index"].AsInt;
                upgradeData.Name = item["Name"].Value;
                upgradeData.Type = NTFunction.ParseEnumFromString<UpgradeEnum>(item["Type"].Value);
                upgradeData.Value = item["Value"].AsFloat;
                upgradeData.Price_Base = item["Price_Base"].AsDouble;
                upgradeData.Price_Pow = item["Price_Pow"].AsInt;
                upgradeData.Calculate();
                this.ListUpgradeData.Add(upgradeData);
            }
        }
        #endregion

        #region Function

        public void BoughtUpgrade(int index){
            if (this.DictionaryUpgradeDataBought.Contains(index)){
                NTLog.LogError("Upgrade already bought");
                return;
            }
            for(int i = 0; i < this.ListUpgradeData.Count; i++){
                if (this.ListUpgradeData[i].Index == index){
                    if(MathBigNumber.IsGreaterThanOrEqual(PlayerManager.Instance.GetGold(), this.ListUpgradeData[i].Price)){
                        PlayerManager.Instance.SpendGold(this.ListUpgradeData[i].Price);
                        this.DictionaryUpgradeDataBought.Add(index, this.ListUpgradeData[i]);
                        if(this.ListUpgradeData[i].Type == UpgradeEnum.All
                        ||this.ListUpgradeData[i].Type == UpgradeEnum.Wheat
                        ||this.ListUpgradeData[i].Type == UpgradeEnum.Wood
                        ||this.ListUpgradeData[i].Type == UpgradeEnum.Steel
                        ||this.ListUpgradeData[i].Type == UpgradeEnum.Clay
                        ||this.ListUpgradeData[i].Type == UpgradeEnum.All
                        ){
                            this.UpgradeProfit(this.ListUpgradeData[i].Type, this.ListUpgradeData[i].Value);
                        }
                        if(this.ListUpgradeData[i].Type == UpgradeEnum.Customer){
                            this.UpgradeCustomer(this.ListUpgradeData[i].Value);
                        }
                        this.ListUpgradeData.RemoveAt(i);
                        break;
                    }
                    break;
                }
            }

        }

        public void UpgradeCustomer(float value){
            MarketController.Instance.AddCustomerWaitAmount(Mathf.RoundToInt(value));
        }

        public void UpgradeProfit(UpgradeEnum type, float value){
            foreach(Unimob.Construction.Construction item in ConstructionManager.Instance.GetListConstruction()){
                if(type == UpgradeEnum.All){
                    item.UpgradeMultiplierIncome(value);
                    continue;
                }
                if(item.Type == ConstructionType.Wheat && type == UpgradeEnum.Wheat){
                    item.UpgradeMultiplierIncome(value);
                    continue;
                }
                if(item.Type == ConstructionType.Wood && type == UpgradeEnum.Wood){
                    item.UpgradeMultiplierIncome(value);
                    continue;
                }
                if(item.Type == ConstructionType.Steel && type == UpgradeEnum.Steel){
                    item.UpgradeMultiplierIncome(value);
                    continue;
                }
                if(item.Type == ConstructionType.Clay && type == UpgradeEnum.Clay){
                    item.UpgradeMultiplierIncome(value);
                    continue;
                }
            }
            EventListenerManager.Instance.PostEvent(EventCode.ConstructionUpgrade, type);

        }

        #endregion

        #region Get
        public List<UpgradeData> GetListUpgradeDataBought(int amount = 5){
            List<UpgradeData> listUpgradeData = new List<UpgradeData>();
            for(int i = 0; i < amount; i++){
                if(i < this.ListUpgradeData.Count){
                    listUpgradeData.Add(this.ListUpgradeData[i]);
                }
            }

            return listUpgradeData;
        }

        public Sprite GetUpgradeIcon(UpgradeEnum type){
            return this.ListUpgradeIcon[(int)type];
        }

        public string GetTitle(UpgradeEnum type){
            switch(type){
                case UpgradeEnum.Customer:
                    return "Customer";
                case UpgradeEnum.Wheat:
                    return "Wheat";
                case UpgradeEnum.Wood:
                    return "Wood";
                case UpgradeEnum.Steel:
                    return "Steel";
                case UpgradeEnum.Clay:
                    return "Clay";
                case UpgradeEnum.All:
                    return "All";
                default:
                    return "";
            }
        }

        public string GetDescription(UpgradeEnum type, float value){
            switch(type){
                case UpgradeEnum.Customer:
                    return "+ " + Mathf.RoundToInt(value) + " Customer";
                case UpgradeEnum.Wheat:
                    return "x" + value + " Wheat profit";
                case UpgradeEnum.Wood:
                    return "x" + value + " Wood profit";
                case UpgradeEnum.Steel:
                    return "x" + value + " Steel profit";
                case UpgradeEnum.Clay:
                    return "x" + value + " Clay profit";
                case UpgradeEnum.All:
                    return "x" + value + " All profit";
                default:
                    return "";
            }
        }


        #endregion
        
    }
}