using System.Collections;
using System.Collections.Generic;
using NTPackage.Functions;
using Unimob.Customer;
using Unimob.Delivery;
using Unimob.Product;
using UnityEngine;

namespace Unimob.Construction
{

    public class ConstructionRenderManager : NTBehaviour
    {
        public List<ConstructionRender> ListConstructionRender;
        public List<CustomerRender> ListWaitingCustomer;

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

        public void OnCompleted(ConstructionRender constructionRender){
            if(!this.ListConstructionRender.Contains(constructionRender)){
                return;
            }
            this.CheckCompleted();
        }

        public void AddCustomer(CustomerRender customerRender){
            this.ListWaitingCustomer.Add(customerRender);
            this.CheckCompleted();
        }

        public void CheckCompleted(){
            for (int i = 0; i < this.ListConstructionRender.Count; i++){
                if (this.ListWaitingCustomer.Count == 0){
                    return;
                }
                ConstructionRender constructionRender = this.ListConstructionRender[i];
                if (constructionRender.State == ConstructionState.Completed && constructionRender.DeliveryRenderRegister == null){
                    CustomerRender customerRender = this.ListWaitingCustomer[0];
                    this.ListWaitingCustomer.RemoveAt(0);
                    DeliveryController.Instance.RegisterDelivery(constructionRender, customerRender);
                }
            }
        }
        #endregion
    }
}