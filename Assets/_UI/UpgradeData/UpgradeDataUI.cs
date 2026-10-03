using System.Collections;
using System.Collections.Generic;
using NTPackage.Functions;
using NTPackage.UI;
using UnityEngine;

namespace Unimob.Upgrade
{
    public class UpgradeDataUI : PopupUI
    {
        public List<UpgradeDataItemUI> UpgradeDataItems;
        public UpgradeDataItemUI UpgradeDataItemPrefab;
        public Transform Holder;

        public override void UpdateData(object data = null)
        {
            base.UpdateData(data);
            this.Clear();
            List<UpgradeData> listUpgradeData = UpgradeDataController.Instance.GetListUpgradeDataBought();
            for(int i = 0; i < listUpgradeData.Count; i++){
                UpgradeDataItemUI upgradeDataItem = ObjectPoolingManager.Instance.PullObjectFromPooling<UpgradeDataItemUI>(ObjectPoolingConfig.UpgradeDataItem);
                if(upgradeDataItem == null) upgradeDataItem = Instantiate(this.UpgradeDataItemPrefab);
                upgradeDataItem.transform.SetParent(this.Holder);
                NTFunction.ResetPosition(upgradeDataItem.transform);
                upgradeDataItem.gameObject.SetActive(true);
                upgradeDataItem.transform.name = ObjectPoolingConfig.UpgradeDataItem;
                upgradeDataItem.Init(listUpgradeData[i], ()=>{
                    this.UpdateData();
                });
                this.UpgradeDataItems.Add(upgradeDataItem);
            }
        }

        public void Clear(){
            ObjectPoolingManager.Instance.PushChildObjectIntoPooling(this.Holder);
            this.UpgradeDataItems.Clear();
        }
    }
}