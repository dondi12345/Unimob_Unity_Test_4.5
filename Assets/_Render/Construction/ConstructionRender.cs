using System.Collections;
using System.Collections.Generic;
using NTPackage.Functions;
using NTPackage.UI;
using Rubik.VFX;
using Unimob.Delivery;
using Unimob.Product;
using UnityEngine;
using UnityEngine.EventSystems;

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
        public ConstructionRenderSkin ConstructionRenderSkin;
        public ConstructionRenderTitle ConstructionRenderTitle;
        public Transform HarvestPoint;
        public DeliveryRender DeliveryRenderRegister;


        public float Cooldown;

        public void Init()
        {
            this.Construction = ConstructionManager.Instance.GetConstruction(this.Type);
            this.State = ConstructionState.Lock;
            this.ConstructionTiming.gameObject.SetActive(false);
            this.ConstructionRenderSkin.Init();
            this.ConstructionRenderTitle.Init();
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

        public void UpdateData()
        {
            this.ConstructionRenderTitle.UpdateData();
        }

        public void UpdateState()
        {
            switch (this.State)
            {
                case ConstructionState.Lock:
                    break;
                case ConstructionState.Unlocking:
                    this.DoneUnlocking();
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
            this.Construction.Bought();
            this.Cooldown = ConstructionDataController.Instance.GetTimeUnlock(this.Type);
            this.State = ConstructionState.Unlocking;
            this.ConstructionTiming.gameObject.SetActive(true);
            this.ConstructionTiming.SetData(this.Cooldown);
            this.ConstructionRenderSkin.OpenBox();
        }

        public void DoneUnlocking()
        {
            this.ConstructionRenderSkin.Unlock();
            this.ConstructionRenderTitle.DoneUnlocking();
            this.Processing();
            VFXGameEntity vfx = VFXGameManager.Instance.InstantiateFX(VFXGameConfig.ConstructionUnlockFX, this.transform.position, Quaternion.identity);
            vfx.SetMoveTo(this.transform, 0.5f);
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
            ConstructionRenderManager.Instance.OnCompleted(this);
        }

        [NTButton]
        public void Harvesting()
        {
            this.State = ConstructionState.Harvesting;
            this.Cooldown = ConstructionConfig.CooldownHarvesting;
            this.ConstructionTiming.gameObject.SetActive(true);
            this.ConstructionTiming.SetData(this.Cooldown);
        }

        public void Upgrade()
        {
            this.Construction.Upgrade();
            this.UpdateData();
        }

        public void OnPopup()
        {
            if (this.State == ConstructionState.Unlocking)
            {
                return;
            }
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(this.transform.position);
            if (this.State == ConstructionState.Lock)
            {
                PopupManager.Instance.OnUI(PopupCode.ConstructionUnlock, null, popup =>
                {
                    ConstructionUnlock constructionUnlock = popup as ConstructionUnlock;
                    constructionUnlock.SetData(this, screenPoint);
                });
                return;
            }

            PopupManager.Instance.OnUI(PopupCode.ConstructionUpgrade, null, popup =>
            {
                ConstructionUpgrade constructionUpgrade = popup as ConstructionUpgrade;
                constructionUpgrade.SetData(this, screenPoint);
            });

        }
        #endregion


    }
}
