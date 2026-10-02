using NTNumber;
using NTPackage.Functions;
using NTPackage.UI;
using TMPro;
using UnityEngine;

namespace Unimob.Construction
{
    public class TooltipCustom : NTBehaviour
    {

        public RectTransform Board;
        public RectTransform CanvasRect;

        public void Show(ConstructionRender render, Vector2 screenPoint)
        {
            this.gameObject.SetActive(true);
            this.Place(screenPoint);
        }

        public void Place(Vector2 screenPoint)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(this.CanvasRect, screenPoint, null, out Vector2 canvasLocal);

            Vector2 canvasSize = this.CanvasRect.rect.size;
            Vector2 anchored = canvasLocal + Vector2.Scale(canvasSize, this.CanvasRect.pivot) - canvasSize * 0.5f;

            Vector2 size = this.Board.rect.size;
            Vector2 pivot = this.Board.pivot;
            float halfX = canvasSize.x * 0.5f;
            float halfY = canvasSize.y * 0.5f;

            float top = anchored.y + size.y * (1f - pivot.y);
            if (top > halfY)
            {
                anchored.y -= top - halfY;
            }

            float bottom = anchored.y - size.y * pivot.y;
            if (bottom < -halfY)
            {
                anchored.y += -halfY - bottom;
            }

            float right = anchored.x + size.x * (1f - pivot.x);
            if (right > halfX)
            {
                anchored.x -= right - halfX;
            }

            float left = anchored.x - size.x * pivot.x;
            if (left < -halfX)
            {
                anchored.x += -halfX - left;
            }

            this.Board.anchoredPosition = anchored;
        }
    }
}
