using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryUIManager : MonoBehaviour, IPointerClickHandler
{
    public static InventoryUIManager Instance;
    public Slot objPoint;

    public Sprite spriteHold;
    public Sprite spritePre;

    public static event Action<Slot> OnSlotSelect;

    private void Awake()
    {
        Instance = this;
    }

    public void SelectSlot(Slot slot)
    {
        
        Slot prePoint = objPoint;
        Slot currentPoint = slot;
        if (prePoint != currentPoint)
        {
            currentPoint.SetBorder(spriteHold);
            objPoint = currentPoint;
            if (prePoint != null)
            {
                prePoint.SetBorder(spritePre);
            }
            OnSlotSelect?.Invoke(slot);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>() != null)
        {
            SelectSlot(eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>());
        }
    }
}
