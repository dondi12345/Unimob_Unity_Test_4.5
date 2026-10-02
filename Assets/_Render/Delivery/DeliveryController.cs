using NTPackage.Functions;
using Unimob.Construction;
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

        public void RegisterDelivery(ConstructionRender constructionRender)
        {
            if (constructionRender.DeliveryRenderRegister != null)
            {
                return;
            }

            DeliveryRender delivery = Instantiate(this.DeliveryPrefab, this.DeleveryStart.position, this.DeleveryStart.rotation);
            constructionRender.DeliveryRenderRegister = delivery;
            delivery.GoToConstruction(constructionRender);
        }
    }
}
