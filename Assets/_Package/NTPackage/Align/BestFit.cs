using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NTFunctions
{
    public class BestFit : AlignFit
    {

        public RectTransform Main;
        public RectTransform BaseOn;

        public float top = 0;
        public float bot = 0;
        public float right = 0;
        public float left = 0;

        public bool IsFollow = false;

        protected override void Update()
        {
            if (this.BaseOn == null) return;
            if (this.Main == null) this.Main = transform.GetComponent<RectTransform>();
            Vector2 size = new Vector2(this.BaseOn.rect.width + right + left, this.BaseOn.rect.height + top + bot);
            if (this.Main.sizeDelta != size) this.FitSelf();
            if(this.IsFollow){
                this.Main.position = this.BaseOn.position;
            }
        }

        public override void FitSelf()
        {
            this.Main.sizeDelta = new Vector2(this.BaseOn.rect.width + right + left, this.BaseOn.rect.height + top + bot);
            base.FitSelf();
        }
    }
}