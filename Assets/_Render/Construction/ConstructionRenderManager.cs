using System.Collections;
using System.Collections.Generic;
using NTPackage.Functions;
using Unimob.Product;
using UnityEngine;

namespace Unimob.Construction
{

    public class ConstructionRenderManager : NTBehaviour
    {
        public List<ConstructionRender> ListConstructionRender;

        public static ConstructionRenderManager Instance;
        protected override void Awake()
        {
            base.Awake();
            if (ConstructionRenderManager.Instance != null)
            {
                NTLog.LogError("Only 1 Instance allow");
                return;
            }
            ConstructionRenderManager.Instance = this;
        }

        #region Load Data
        public void Init(){
            foreach(ConstructionRender constructionRender in this.ListConstructionRender){
                constructionRender.Init();
            }
        }
        #endregion
    }
}