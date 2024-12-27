using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollbarManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Scrollbar scrollbar;
    public ScrollRect scrollRect;
    void Start()
    {
        scrollbar = GetComponentInChildren<Scrollbar>();

        if (scrollbar != null)
        {
            StartCoroutine(SetScrollbarValue());
        }
    }

    IEnumerator SetScrollbarValue()
    {
        yield return null;
        scrollbar.value = 1f;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //scrollRect.enabled = false;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        //scrollRect.enabled = true;
    }

    public void OnPointerOn(PointerEventData eventData)
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }

}
