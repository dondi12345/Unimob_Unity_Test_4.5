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
    public class ConstructionManager : NTBehaviour
    {

        [SerializeField]
        private NTDictionary<ConstructionType, Construction> _constructionList;

        public static ConstructionManager Instance;
        protected override void Awake()
        {
            base.Awake();
            if (ConstructionManager.Instance != null)
            {
                NTLog.LogError("Only 1 Instance allow");
                return;
            }
            ConstructionManager.Instance = this;
        }

        #region Load Data

        public void Init()
        {
            List<ConstructionType> listConstructionType = new List<ConstructionType>{
                ConstructionType.Wheat,
                ConstructionType.Wood,
                ConstructionType.Steel,
                ConstructionType.Clay,
            };
            this._constructionList = new NTDictionary<ConstructionType, Construction>();
            foreach (ConstructionType constructionType in listConstructionType){
                ConstructionType type = constructionType;
                Construction construction = new Construction();
                construction.Init(type);
                this._constructionList.Add(type, construction);
            }
        }

        #endregion

        #region Get
        public Construction GetConstruction(ConstructionType type){
            Construction construction = this._constructionList.Get(type);
            if(construction == null){
                NTLog.LogError($"Construction not found: {type}");
                return null;
            }
            return construction;
        }

        public List<Construction> GetListConstruction(){
            List<Construction> listConstruction = new List<Construction>();
            foreach (ConstructionType type in this._constructionList.GetKeys()){
                listConstruction.Add(this._constructionList.Get(type));
            }
            return listConstruction;
        }
        

        #endregion

    }
}
