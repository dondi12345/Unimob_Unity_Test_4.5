using System;
using System.Collections;
using System.Collections.Generic;
using NTFunctions;
using NTPackage.Functions;
using UnityEngine;

namespace NTFunctions
{
    public class VerticalScroll : NTBehaviour
    {
        // holder of all items
        public RectTransform Content;
        //For cacular size board
        public Transform View;
        //Size of each item
        public float SizeX;
        public float SizeY;
        public float TopPadding;
        public float BottomPadding;

        public bool IsDebug = false;
        [NTShowIf("Debug", true)][SerializeField] public int Top = 4;
        [NTShowIf("Debug", true)][SerializeField] public int Bot = 2;
        [NTShowIf("Debug", true)][SerializeField] public int Num = 1;

        [NTShowIf("Debug", true)][SerializeField] public int Max;

        [NTShowIf("Debug", true)][SerializeField] public int amount_x;
        public int amount_y;

        [NTShowIf("Debug", true)][SerializeField] public Vector3 OriginalPos;

        public List<VerticalItem> VerticalItems;

        protected override void OnDisable()
        {
            base.OnDisable();
            this.Content.localPosition = this.OriginalPos;
        }

        public override void LoadComponents()
        {
            base.LoadComponents();
            this.OriginalPos = this.Content.localPosition;
            this.VerticalItems.Clear();
            foreach (Transform item in this.Content)
            {
                if (item.TryGetComponent<VerticalItem>(out VerticalItem verticalItem))
                {
                    this.VerticalItems.Add(verticalItem);
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
                if(this.amount_x < 1) this.amount_x = 1;
                this.amount_y = (int)(View.GetComponent<RectTransform>().sizeDelta.y / this.SizeY);
                if(this.amount_y < 1) this.amount_y = 1;
            }
            for (int i = this.VerticalItems.Count; i < amount_x * amount_y * 2.5f; i++)
            {
                VerticalItem verticalItem = Instantiate(this.VerticalItems[0]);
                verticalItem.transform.SetParent(this.VerticalItems[0].transform.parent);
                verticalItem.transform.localScale = this.VerticalItems[0].transform.localScale;
                this.VerticalItems.Add(verticalItem);
                verticalItem.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
            }
            this.Content.sizeDelta = new Vector2(this.Content.sizeDelta.x, (int)(Mathf.Ceil(this.Max / amount_x) ) * this.SizeY + this.BottomPadding + this.TopPadding);
            foreach (Transform item in this.Content)
            {
                if (index < 0 || index >= this.Max)
                {
                    item.gameObject.SetActive(false);
                }
                else
                {
                    item.GetComponent<VerticalItem>().SetData(index, this);
                    item.gameObject.SetActive(true);
                }
                index++;
            }
        }

        protected override void Update()
        {
            for (int i = 0; i < 5; i++)
            {
                VerticalItem firstVerticalIcon = this.Content.GetChild(0).GetComponent<VerticalItem>();
                VerticalItem lastVerticalIcon = this.Content.GetChild(this.Content.childCount - 1).GetComponent<VerticalItem>();

                float num = (this.Content.localPosition.y + firstVerticalIcon.GetComponent<RectTransform>().localPosition.y) / this.SizeY;
                this.Num = (int)num;
                if (num > Top)
                {
                    if (lastVerticalIcon.Index >= this.Max - 1) return;
                    firstVerticalIcon.SetData(lastVerticalIcon.Index + 1, this);
                    firstVerticalIcon.transform.SetAsLastSibling();
                    continue;
                }

                if (num < Bot)
                {
                    if (firstVerticalIcon.Index <= 0) return;
                    lastVerticalIcon.SetData(firstVerticalIcon.Index - 1, this);
                    lastVerticalIcon.transform.SetAsFirstSibling();
                    continue;
                }
                break;
            }

        }

        public virtual void UpdateData()
        {
            foreach (VerticalItem item in this.VerticalItems)
            {
                item.UpdateData();
            }
        }
    }
}