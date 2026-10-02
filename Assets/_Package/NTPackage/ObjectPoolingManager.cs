using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NTPackage.Functions
{
    public class ObjectPoolingConfig
    {
        public const string ElementalChemicalInputItemUI = "ElementalChemicalInputItemUI";
        public const string BlockElementInputItemUI = "BlockElementInputItemUI";
        public const string ElementChemicalMachineItemUI = "ElementChemicalMachineItemUI";
        public const string BoxMachineItemUI = "BoxMachineItemUI";
        public const string BlockElementItemUI = "BlockElementItemUI";
        public const string BtnChoseElMachineChemicalUI = "BtnChoseElMachineChemicalUI";
        public const string BtnChoseElChemicalGraphUI = "BtnChoseElChemicalGraphUI";
        public const string GraphPointUI = "GraphPointUI";
        public const string TextHorizontalUI = "TextHorizontalUI";
        public const string TextVerticalUI = "TextVerticalUI";
        public const string BetaItemUI = "BetaItemUI";
        public const string PurposeElementUI = "PurposeElementUI";
        public const string PurposeItemUI = "PurposeItemUI";
        public const string AmountItemUI = "AmountItemUI";
        public const string HyperlinkOutletLinkageInputItemUI = "HyperlinkOutletLinkageInputItemUI";
        public const string HyperlinkFeedStageLinkageInputItemUI = "HyperlinkFeedStageLinkageInputItemUI";
        public const string HyperlinkOutletLinkageAQInputItemUI = "HyperlinkOutletLinkageAQInputItemUI";
        public const string HyperlinkOutletLinkageORGInputItemUI = "HyperlinkOutletLinkageORGInputItemUI";
        public const string HyperlinkOutletLinkageDualInputItemUI = "HyperlinkOutletLinkageDualInputItemUI";
        public const string HyperlinkFeedLinkageAqInputItemUI = "HyperlinkFeedLinkageAqInputItemUI";
        public const string HyperlinkFeedLinkageOrgInputItemUI = "HyperlinkFeedLinkageOrgInputItemUI";
        public const string HyperlinkHorizontalAqInputItemUI = "HyperlinkHorizontalAqInputItemUI";
        public const string HyperlinkHorizontalOrgInputItemUI = "HyperlinkHorizontalOrgInputItemUI";
        public const string HyperlinkHorizontalBothInputItemUI = "HyperlinkHorizontalBothInputItemUI";
        public const string HyperlinkFeedLinkageOrgInputItemABUI = "HyperlinkFeedLinkageOrgInputItemABUI";
        public const string HyperlinkFeedLinkageAqInputItemABUI = "HyperlinkFeedLinkageAqInputItemABUI";
        public const string DynamicHInputBetaItemUI = "DynamicHInputBetaItemUI";
        public const string DynamicHInputFeedItemUI = "DynamicHInputFeedItemUI";
        public const string DynamicHInputFeedIChemicaltemUI = "DynamicHInputFeedIChemicaltemUI";
        public const string DynamicHInputThakurItemUI = "DynamicHInputThakurItemUI";
        public const string DynamicHFeedModelItemUI = "DynamicHFeedModelItemUI";
        public const string DynamicHMachineBlockItemUI = "DynamicHMachineBlockItemUI";
        public const string DynamicHFeedModelItemInputValueUI = "DynamicHFeedModelItemInputValueUI";
        public const string StrippingEleInputItemUI = "StrippingEleInputItemUI";
        public const string DynamicHInputKItemUI = "DynamicHInputKItemUI";
        public const string DynamicHKFeedModelItemUI = "DynamicHKFeedModelItemUI";
        public const string BetaItemInputUI = "BetaItemInputUI";
        public const string SeparationTargetSelectIputItemUI = "SeparationTargetSelectIputItemUI";
        public const string FeedBoxElementInputItemUI = "FeedBoxElementInputItemUI";
        public const string RareEarthBoxElementInputItemUI = "RareEarthBoxElementInputItemUI";
    }

    public class ObjectPoolingManager : NTBehaviour
    {
        public NTDictionary<string, List<Transform>> ObjectNTDictionary = new NTDictionary<string, List<Transform>>();
        public Transform Holder;

        public static ObjectPoolingManager Instance;
        protected override void Awake()
        {
            base.Awake();
            if (ObjectPoolingManager.Instance != null)
            {
                Debug.LogWarning("Only 1 instance allow");
                return;
            }
            ObjectPoolingManager.Instance = this;
        }

        public override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadHolder();
        }

        protected void LoadHolder()
        {
            if (Holder != null) return;
            this.Holder = transform.Find("Holder");
        }

        public virtual void PushChildObjectIntoPooling(Transform trans)
        {
            List<Transform> listTrans = new List<Transform>();
            foreach (Transform item in trans)
            {
                listTrans.Add(item);
            }
            for (int i = listTrans.Count - 1; i >= 0; i--)
            {
                this.PushObjectIntoPooling(listTrans[i]);
            }
        }

        public void PushObjectIntoPooling(Transform trans)
        {
            trans.SetParent(this.Holder);
            trans.gameObject.SetActive(false);
            try
            {
                this.ObjectNTDictionary.Get(trans.name).Add(trans);
            }
            catch (System.Exception)
            {
                List<Transform> lsTrans = new List<Transform>();
                lsTrans.Add(trans);
                this.ObjectNTDictionary.Add(trans.name, lsTrans);
            }
        }

        public Transform PullObjectFromPooling(string nameOb)
        {
            try
            {
                Transform trans = this.ObjectNTDictionary.Get(nameOb)[0];
                this.ObjectNTDictionary.Get(nameOb).RemoveAt(0);
                trans.gameObject.SetActive(true);
                return trans;
            }
            catch (System.Exception)
            {
                try
                {
                    Debug.LogWarning
                    (this.ObjectNTDictionary.Get(nameOb).Count);
                }
                catch (System.Exception)
                { }
                return null;
            }
        }
        public T PullObjectFromPooling<T>(string nameOb)
        {
            Transform trans;
            try
            {
                trans = this.ObjectNTDictionary.Get(nameOb)[0];
                this.ObjectNTDictionary.Get(nameOb).RemoveAt(0);
                trans.gameObject.SetActive(true);
                return trans.GetComponent<T>();
            }
            catch (System.Exception)
            {
                return default(T);
            }
        }
    }
}
