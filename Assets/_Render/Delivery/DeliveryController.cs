using NTPackage.Functions;
using Unimob.Construction;
using Unimob.Customer;
using Unimob.Customer;
using UnityEngine;

namespace Unimob.Delivery
{
    public class DeliveryController : NTBehaviour
    {
        public static DeliveryController Instance;

        public Transform DeleveryStart;
        public DeliveryRender DeliveryPrefab;

        protected override void Awake()
        {
            base.Awake();
            if (Instance != null)
            {
                NTLog.LogError("Only 1 Instance allow");
                return;
            }
            Instance = this;
        }

        public void RegisterDelivery(ConstructionRender constructionRender, CustomerRender customerRender)
        {
            if (constructionRender.DeliveryRenderRegister != null)
            {
                return;
            }

            DeliveryRender delivery = ObjectPoolingManager.Instance.PullObjectFromPooling<DeliveryRender>(ObjectPoolingConfig.DeliveryRender);
            if (delivery == null)
            {
                delivery = Instantiate(this.DeliveryPrefab);
            }
            delivery.name = ObjectPoolingConfig.DeliveryRender;
            delivery.transform.SetParent(null);
            delivery.transform.SetPositionAndRotation(this.DeleveryStart.position, this.DeleveryStart.rotation);
            delivery.gameObject.SetActive(true);
            constructionRender.DeliveryRenderRegister = delivery;
            delivery.GoToConstruction(constructionRender, customerRender);
        }
    }
}
