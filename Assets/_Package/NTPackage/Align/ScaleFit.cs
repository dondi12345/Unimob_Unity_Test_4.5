using UnityEngine;

namespace NTFunctions
{
    public class ScaleFit : MonoBehaviour
    {
        public RectTransform Main;
        public RectTransform BaseOn;
        public Vector2 OriginalSize;
        // Update is called once per frame
        void Update()
        {
            Vector2 size = this.BaseOn.rect.size;
            float scaleX = size.x / this.OriginalSize.x;
            float scaleY = size.y / this.OriginalSize.y;
            float scale = Mathf.Min(scaleX, scaleY);
            this.Main.localScale = new Vector3(scale, scale, 1);
        }
    }
}