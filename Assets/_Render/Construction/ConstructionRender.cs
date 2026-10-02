using System.Collections;
using System.Collections.Generic;
using NTPackage.Functions;
using Unimob.Product;
using UnityEngine;

namespace Unimob.Construction
{

    public class ConstructionRender : NTBehaviour
    {
        public ConstructionType Type;
        public ConstructionState State;

        public Construction Construction;

        public List<Transform> ListProductPosition;
        public List<ProductSkin> ListProduct;
        public ProductSkin ProductSkinPrefab;

        public ConstructionTiming ConstructionTiming;

        public Transform TransBox;
        public Transform TransProduct;

        public float Cooldown;

        public void Init(Construction construction)
        {
            this.Construction = construction;
            this.State = ConstructionState.Lock;
            this.ConstructionTiming.gameObject.SetActive(false);
            this.TransBox.gameObject.SetActive(true);
            this.TransProduct.gameObject.SetActive(false);
        }

        protected override void Update()
        {
            base.Update();
            if (this.Cooldown > 0)
            {
                this.Cooldown -= Time.deltaTime;
                this.ConstructionTiming.SetValue(this.Cooldown);
                if (this.State == ConstructionState.Processing)
                {
                    this.ProcessingUpdate();
                }
                if (this.Cooldown <= 0)
                {
                    this.ConstructionTiming.gameObject.SetActive(false);
                    this.UpdateState();
                }
            }
        }


        #region Function

        public void UpdateState()
        {
            switch (this.State)
            {
                case ConstructionState.Lock:
                    break;
                case ConstructionState.Unlocking:
                    this.Processing();
                    break;
                case ConstructionState.Processing:
                    this.Completed();
                    break;
                case ConstructionState.Harvesting:
                    this.Processing();
                    break;
            }
        }

        [NTButton]
        public void Unlock()
        {
            this.Cooldown = ConstructionDataController.Instance.GetTimeUnlock(this.Type);
            this.State = ConstructionState.Unlocking;
            this.ConstructionTiming.gameObject.SetActive(true);
            this.ConstructionTiming.SetData(this.Cooldown);
            this.TransBox.gameObject.SetActive(false);
            this.TransProduct.gameObject.SetActive(true);
        }

        public void Processing()
        {
            this.Cooldown = ConstructionDataController.Instance.GetCooldownProcessing(this.Type);
            this.State = ConstructionState.Processing;
            this._currentProductCount = 0;
            this.ListProduct.Clear();
            // this.ConstructionTiming.gameObject.SetActive(false);
            // this.ConstructionTiming.SetData(this.Cooldown);
        }

        int _currentProductCount = 0;
        public void ProcessingUpdate()
        {
            int countProduct = this.ListProductPosition.Count;
            float cooldownProcessing = ConstructionDataController.Instance.GetCooldownProcessing(this.Type);
            if ((cooldownProcessing - this.Cooldown) / cooldownProcessing > ((float)(_currentProductCount + 1) / countProduct))
            {
                if (this._currentProductCount < countProduct)
                {
                    this._currentProductCount++;
                    this.CreateProduct();
                }
            }
        }

        public void CreateProduct()
        {
            ProductSkin productSkin = ObjectPoolingManager.Instance.PullObjectFromPooling<ProductSkin>(ObjectPoolingConfig.ProductSkin);
            if (productSkin == null)
            {
                productSkin = Instantiate(this.ProductSkinPrefab);
            }
            productSkin.transform.SetParent(this.ListProductPosition[this._currentProductCount - 1]);
            NTFunction.ResetPosition(productSkin.transform);
            productSkin.gameObject.SetActive(true);
            this.ListProduct.Add(productSkin);
        }

        public void Completed()
        {
            this.State = ConstructionState.Completed;
            this.Cooldown = 0;
            this.ConstructionTiming.gameObject.SetActive(false);
        }

        [NTButton]
        public void Harvesting()
        {
            this.State = ConstructionState.Harvesting;
            this.Cooldown = ConstructionConfig.CooldownHarvesting;
            this.ConstructionTiming.gameObject.SetActive(true);
            this.ConstructionTiming.SetData(this.Cooldown);
        }
        #endregion


    }
}
