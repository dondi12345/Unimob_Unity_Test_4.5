using System.Collections;
using System.Collections.Generic;
using NTPackage.Functions;
using UnityEngine;

namespace NTFunctions
{
    public class HorizontalItem : NTBehaviour
    {
        public int Index;
        public HorizontalScroll HorizontalScroll;

        public int X;
        public int Y;
        public int posX;
        public int posY;

        public void SetData(int index, HorizontalScroll horizontalScroll)
        {
            this.Index = index;
            this.HorizontalScroll = horizontalScroll;
            this.GetComponent<RectTransform>().sizeDelta = new Vector2(horizontalScroll.SizeX, horizontalScroll.SizeY);
            int x = this.Index / horizontalScroll.amount_y;
            int y = this.Index % horizontalScroll.amount_y;
            this.X = x;
            this.Y = y;
            this.transform.localPosition = new Vector2(x * horizontalScroll.SizeX + HorizontalScroll.LeftPadding, -y * horizontalScroll.SizeY);
            this.posX = (int)this.transform.localPosition.x;
            this.posY = (int)(this.transform.localPosition.y);
            this.UpdateData();
        }

        public virtual void UpdateData()
        {

        }
    }
}