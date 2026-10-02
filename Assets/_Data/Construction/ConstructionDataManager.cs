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
    public class ConstructionDataManager : NTBehaviour
    {

        [SerializeField]
        private NTDictionary<ConstructionType, Construction> _constructionList;

        public static ConstructionDataManager Instance;
        protected override void Awake()
        {
            base.Awake();
            if (ConstructionDataManager.Instance != null)
            {
                NTLog.LogError("Only 1 Instance allow");
                return;
            }
            ConstructionDataManager.Instance = this;
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
                Construction construction = new Construction();
                construction.Init(constructionType);
                this._constructionList.Add(constructionType, construction);
            }
        }

        #endregion

        #region Get

        #endregion

    }
}
