using System.Collections.Generic;
using NTPackage.Functions;
using Unimob.Construction;
using Unimob.Customer;
using UnityEngine;

namespace Unimob.Market
{
    public class MarketController : MonoBehaviour
    {
        public Transform DeleveryEnd;
        public Transform CustomerStart;
        public Transform CustomerEnd;
        public float EndRadius = 1f;

        public bool IsInEnd(Vector3 position, Transform end)
        {
            Vector3 offset = position - end.position;
            offset.y = 0f;
            return offset.sqrMagnitude <= this.EndRadius * this.EndRadius;
        }

        public List<Dock> Docks;
        public CustomerRender CustomerRenderPrefab;
        public int CustomerWaitAmount = 1;

        public static MarketController Instance;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            this.CheckDocks();
        }

        public void AddCustomerWaitAmount(int amount)
        {
            this.CustomerWaitAmount += amount;
            this.CheckDocks();
        }

        public void CheckDocks()
        {
            int emptyCount = 0;
            for (int i = 0; i < this.Docks.Count; i++)
            {
                if (this.Docks[i].CustomerRenderRegister == null)
                {
                    emptyCount++;
                }
            }

            int waitingCount = this.Docks.Count - emptyCount;
            while (waitingCount < this.CustomerWaitAmount && emptyCount > 0)
            {
                this.SpawnCustomer(this.GetRandomEmptyDock(emptyCount));
                emptyCount--;
                waitingCount++;
            }
        }

        private Dock GetRandomEmptyDock(int emptyCount)
        {
            int pick = Random.Range(0, emptyCount);
            for (int i = 0; i < this.Docks.Count; i++)
            {
                Dock dock = this.Docks[i];
                if (dock.CustomerRenderRegister != null)
                {
                    continue;
                }
                if (pick == 0)
                {
                    return dock;
                }
                pick--;
            }
            return null;
        }

        private void SpawnCustomer(Dock dock)
        {
            CustomerRender customer = ObjectPoolingManager.Instance.PullObjectFromPooling<CustomerRender>(ObjectPoolingConfig.CustomerRender);
            if (customer == null)
            {
                customer = Instantiate(this.CustomerRenderPrefab);
            }
            customer.transform.SetParent(null);
            customer.transform.SetPositionAndRotation(this.CustomerStart.position, this.CustomerStart.rotation);
            customer.gameObject.SetActive(true);
            customer.name = ObjectPoolingConfig.CustomerRender;
            dock.CustomerRenderRegister = customer;
            customer.GoToDock(dock);
            ConstructionRenderManager.Instance.AddCustomer(customer);
        }
    }
}
