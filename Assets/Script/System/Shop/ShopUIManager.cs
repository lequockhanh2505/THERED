using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopUIManager : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public GameObject confirmUI;
    public Slot objPoint;

    public Sprite spriteHold;
    public Sprite spritePre;

    public static event Action<Slot> OnItemConfirm;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GetObjectInPoint(eventData).GetComponent<Slot>())
        {
            SlotSelect(GetObjectInPoint(eventData).GetComponent<Slot>());
        }
    }

    private void SlotSelect(Slot slot)
    {
        Slot prePoint = objPoint;
        Slot currentPoint = slot;
        if (prePoint != currentPoint)
        {
            currentPoint.SetBorder(spriteHold);
            objPoint = currentPoint;
            prePoint.SetBorder(spritePre);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GetObjectInPoint(eventData).GetComponent<Slot>())
        {
            confirmUI.SetActive(true);
            OnItemConfirm?.Invoke(GetObjectInPoint(eventData).GetComponent<Slot>());
        }
    }

    private GameObject GetObjectInPoint(PointerEventData eventData)
    {
        return eventData.pointerCurrentRaycast.gameObject;
    }
}
