using System.Collections;
using System.Collections.Generic;
using NTPackage.Functions;
using Unimob.Construction;
using Unimob.Player;
using Unimob.Upgrade;
using UnityEngine;

namespace Unimob.DataCenter
{
    public class DataCenterController : NTBehaviour
    {
        public static DataCenterController Instance;
        protected override void Awake()
        {
            base.Awake();
            if (DataCenterController.Instance != null){
               NTLog.LogError("Only 1 Instance allow");
               return;
             }
            DataCenterController.Instance = this;
        }

        protected override void Start(){
            base.Start();
            this.LoadData();
            this.Init();
        }

        #region Load
        public void LoadData(){
            ConstructionDataController.Instance.LoadData();
            UpgradeDataController.Instance.LoadData();
        }
        public void Init(){
            PlayerManager.Instance.Init();
            ConstructionManager.Instance.Init();

            // Render
            ConstructionRenderManager.Instance.Init();
        }
        #endregion
    }
}
