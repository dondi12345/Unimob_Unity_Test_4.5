using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace NTPackage.ScrollAuto
{
    public class ScrollViewAutoBottom : MonoBehaviour
    {
        [Header("References")]
        public ScrollRect ScrollRect;
        public RectTransform Content;

        [Header("Settings")]
        public float BottomThreshold = 0.02f;

        public bool _autoScroll = true;
        private bool _ignoreScrollEvent;
        private Coroutine _scrollCoroutine;

        private void Awake()
        {
            if (ScrollRect == null)
                ScrollRect = GetComponent<ScrollRect>();

            if (Content == null && ScrollRect != null)
                Content = ScrollRect.content;

            ScrollRect.onValueChanged.AddListener(OnScrollValueChanged);
        }

        private void OnDestroy()
        {
            if (ScrollRect != null)
                ScrollRect.onValueChanged.RemoveListener(OnScrollValueChanged);
        }

        private void OnScrollValueChanged(Vector2 value)
        {
            if (_ignoreScrollEvent)
                return;

            // User kéo xuống cuối thì bật lại auto-scroll.
            // User kéo lên thì tắt auto-scroll.
            _autoScroll = IsAtBottom();
        }

        public void NotifyItemAdded()
        {
            // Gọi hàm này sau khi add item mới vào Content.
            if (_autoScroll || IsAtBottom())
            {
                ScrollToBottomNextFrame();
            }
        }

        public void ForceScrollToBottom()
        {
            _autoScroll = true;
            ScrollToBottomNextFrame();
        }

        private void ScrollToBottomNextFrame()
        {
            if (_scrollCoroutine != null)
                StopCoroutine(_scrollCoroutine);

            _scrollCoroutine = StartCoroutine(IEScrollToBottom());
        }

        private IEnumerator IEScrollToBottom()
        {
            // Chờ layout cập nhật sau khi add item
            yield return null;

            Canvas.ForceUpdateCanvases();

            if (Content != null)
                LayoutRebuilder.ForceRebuildLayoutImmediate(Content);

            _ignoreScrollEvent = true;

            ScrollRect.verticalNormalizedPosition = 0f;

            Canvas.ForceUpdateCanvases();

            _ignoreScrollEvent = false;
            _scrollCoroutine = null;
        }

        private bool IsAtBottom()
        {
            if (ScrollRect == null)
                return true;

            if (!ScrollRect.vertical)
                return true;

            if (Content == null || ScrollRect.viewport == null)
                return true;

            // Nếu content chưa cao hơn viewport thì coi như đang ở cuối.
            if (Content.rect.height <= ScrollRect.viewport.rect.height + 1f)
                return true;

            return ScrollRect.verticalNormalizedPosition <= BottomThreshold;
        }
    }
}