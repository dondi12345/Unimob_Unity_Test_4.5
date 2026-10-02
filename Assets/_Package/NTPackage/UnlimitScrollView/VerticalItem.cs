using System.Collections;
using System.Collections.Generic;
using NTPackage.Functions;
using UnityEngine;

namespace NTFunctions
{
    public class VerticalItem : NTBehaviour
    {
        public int Index;
        public VerticalScroll VerticalScroll;

        public int X;
        public int Y;
        public int posX;
        public int posY;

        public void SetData(int index, VerticalScroll verticalScroll)
        {
            this.Index = index;
            this.VerticalScroll = verticalScroll;
            this.GetComponent<RectTransform>().sizeDelta = new Vector2(verticalScroll.SizeX, verticalScroll.SizeY);
            int x = this.Index % verticalScroll.amount_x;
            int y = this.Index / verticalScroll.amount_x;
            this.X = x;
            this.Y = y;
            this.transform.localPosition = new Vector2(x * verticalScroll.SizeX, -y * verticalScroll.SizeY - VerticalScroll.TopPadding);
            this.posX = (int)this.transform.localPosition.x;
            this.posY = (int)(this.transform.localPosition.y);
            this.UpdateData();
        }

        public virtual void UpdateData()
        {

        }
    }
}