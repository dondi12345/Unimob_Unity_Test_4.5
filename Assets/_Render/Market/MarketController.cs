using System.Collections.Generic;
using NTPackage.Functions;
using Unimob.Customer;
using UnityEngine;

namespace Unimob.Market
{
    public class MarketController : MonoBehaviour
    {
        public Transform DeleveryEnd;
        public Transform CustomerStart;
        public Transform CustomerEnd;

        public List<Dock> Docks;
        public CustomerRender CustomerRenderPrefab;

        private void Start()
        {
            this.CheckDocks();
        }

        public void CheckDocks()
        {
            for (int i = 0; i < this.Docks.Count; i++)
            {
                Dock dock = this.Docks[i];
                if (dock.CustomerRenderRegister == null)
                {
                    this.SpawnCustomer(dock);
                }
            }
        }

        private void SpawnCustomer(Dock dock)
        {
            CustomerRender customer = ObjectPoolingManager.Instance.PullObjectFromPooling<CustomerRender>(ObjectPoolingConfig.CustomerRender);
            if (customer == null)
            {
                customer = Instantiate(this.CustomerRenderPrefab);
            }
            customer.transform.SetPositionAndRotation(this.CustomerStart.position, this.CustomerStart.rotation);
            customer.gameObject.SetActive(true);
            dock.CustomerRenderRegister = customer;
            customer.GoToDock(dock);
        }
    }
}
