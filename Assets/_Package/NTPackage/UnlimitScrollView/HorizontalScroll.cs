using System;
using System.Collections;
using System.Collections.Generic;
using NTFunctions;
using NTPackage.Functions;
using UnityEngine;

namespace NTFunctions
{
    public class HorizontalScroll : NTBehaviour
    {
        // holder of all items
        public RectTransform Content;
        //For cacular size board
        public Transform View;
        //Size of each item
        public float SizeX;
        public float SizeY;
        public float LeftPadding;
        public float RightPadding;
        public bool IsDebug = false;

        [NTShowIf("Debug", true)][SerializeField] public int Left = 4;
        [NTShowIf("Debug", true)][SerializeField] public int Right = 2;
        [NTShowIf("Debug", true)][SerializeField] public int Num = 1;

        [NTShowIf("Debug", true)][SerializeField] public int Max;

        [NTShowIf("Debug", true)][SerializeField] public int amount_x;
        [NTShowIf("Debug", true)][SerializeField] public int amount_y;

        [NTShowIf("Debug", true)][SerializeField] public Vector3 OriginalPos;

        public List<HorizontalItem> HorizontalItems;

        protected override void OnDisable()
        {
            base.OnDisable();
            this.Content.localPosition = this.OriginalPos;
        }

        public override void LoadComponents()
        {
            base.LoadComponents();
            this.OriginalPos = this.Content.localPosition;
            this.HorizontalItems.Clear();
            foreach (Transform item in this.Content)
            {
                if (item.TryGetComponent<HorizontalItem>(out HorizontalItem horizontalItem))
                {
                    this.HorizontalItems.Add(horizontalItem);
                    item.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
                }
            }
        }
        [NTButton]
        public void OnUI(int max)
        {
            this.Content.localPosition = this.OriginalPos;
            this.Max = max;
            int index = 0;
            if (this.View != null)
            {
                this.amount_x = (int)(View.GetComponent<RectTransform>().sizeDelta.x / this.SizeX);
                if(this.amount_x < 1){
                    this.amount_x = (int)(View.GetComponent<RectTransform>().rect.size.x / this.SizeX);
                }
                if(this.amount_x < 1) this.amount_x = 1;
                this.amount_y = (int)(View.GetComponent<RectTransform>().sizeDelta.y / this.SizeY);
                if(this.amount_y < 1) this.amount_y = 1;
            }
            for (int i = this.HorizontalItems.Count; i < amount_x * amount_y * 2.5f; i++)
            {
                HorizontalItem horizontalItem = Instantiate(this.HorizontalItems[0]);
                horizontalItem.transform.SetParent(this.HorizontalItems[0].transform.parent);
                horizontalItem.transform.localScale = this.HorizontalItems[0].transform.localScale;
                this.HorizontalItems.Add(horizontalItem);
                horizontalItem.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
            }
            this.Content.sizeDelta = new Vector2((int)(Mathf.Ceil(this.Max / amount_y) ) * this.SizeX + this.LeftPadding + this.RightPadding, this.Content.sizeDelta.y);
            foreach (Transform item in this.Content)
            {
                if (index < 0 || index >= this.Max)
                {
                    item.gameObject.SetActive(false);
                }
                else
                {
                    item.GetComponent<HorizontalItem>().SetData(index, this);
                    item.gameObject.SetActive(true);
                }
                index++;
            }
        }

        protected override void Update()
        {
            for (int i = 0; i < 5; i++)
            {
                HorizontalItem firstHorizontalIcon = this.Content.GetChild(0).GetComponent<HorizontalItem>();
                HorizontalItem lastHorizontalIcon = this.Content.GetChild(this.Content.childCount - 1).GetComponent<HorizontalItem>();

                float num = -(this.Content.localPosition.x + firstHorizontalIcon.GetComponent<RectTransform>().localPosition.x) / this.SizeX;
                this.Num = (int)num;
                if (num > Left)
                {
                    if (lastHorizontalIcon.Index >= this.Max - 1) return;
                    firstHorizontalIcon.SetData(lastHorizontalIcon.Index + 1, this);
                    firstHorizontalIcon.transform.SetAsLastSibling();
                    continue;
                }

                if (num < Right)
                {
                    if (firstHorizontalIcon.Index <= 0) return;
                    lastHorizontalIcon.SetData(firstHorizontalIcon.Index - 1, this);
                    lastHorizontalIcon.transform.SetAsFirstSibling();
                    continue;
                }
                break;
            }

        }

        public virtual void UpdateData()
        {
            foreach (HorizontalItem item in this.HorizontalItems)
            {
                item.UpdateData();
            }
        }
    }
}