using NTPackage.Functions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NTPackage.UI
{
[RequireComponent(typeof(TMP_InputField))]
public class InputFieldScrollForwarder : NTBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler,
    IScrollHandler
{
    public ScrollRect scrollRect;

    private bool forwardDrag;

    protected override void Awake()
    {
        base.Awake();
        if (scrollRect == null)
            scrollRect = GetComponentInParent<ScrollRect>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (scrollRect == null)
            return;

        // Forward vertical drag to ScrollRect.
        forwardDrag =
            Mathf.Abs(eventData.delta.y) > Mathf.Abs(eventData.delta.x);

        if (forwardDrag)
            scrollRect.OnBeginDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (forwardDrag && scrollRect != null)
            scrollRect.OnDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (forwardDrag && scrollRect != null)
            scrollRect.OnEndDrag(eventData);

        forwardDrag = false;
    }

    public void OnScroll(PointerEventData eventData)
    {
        if (scrollRect != null)
            scrollRect.OnScroll(eventData);
    }
}
}