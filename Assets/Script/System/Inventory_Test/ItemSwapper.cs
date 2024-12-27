using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ItemSwapper : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private GameObject hoverObject; // Hiển thị biểu tượng khi kéo

    [SerializeField]
    private InventoryManager inventoryManager;

    private Slot draggedSlot;

    public void OnBeginDrag(PointerEventData eventData)
    {
        StartDragging(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateDraggingPosition(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        EndDragging(eventData);
    }

    private void StartDragging(PointerEventData eventData)
    {
        draggedSlot = eventData.pointerCurrentRaycast.gameObject?.GetComponent<Slot>();

        if (draggedSlot == null || draggedSlot.itemStats == null)
        {
            Debug.LogError("Dragged slot is null or does not have an item.");
            return;
        }

        if (draggedSlot != null && draggedSlot.itemStats != null)
        {
            hoverObject.SetActive(true);
            hoverObject.GetComponent<Image>().sprite = draggedSlot.icon.sprite;
            hoverObject.GetComponent<RectTransform>().position = eventData.position;
            SetHoverAlpha(0.5f);
        }
    }

    private void UpdateDraggingPosition(PointerEventData eventData)
    {
        hoverObject.GetComponent<RectTransform>().position = eventData.position;
    }

    private void EndDragging(PointerEventData eventData)
    {
        if (draggedSlot == null || draggedSlot.itemStats == null)
        {
            hoverObject.SetActive(false);
            return;
        }

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);

        foreach (var result in raycastResults)
        {
            if (result.gameObject == null) continue;

            Slot targetSlot = result.gameObject?.GetComponent<Slot>();
            if (targetSlot != null)
            {
                SwapSlots(draggedSlot, targetSlot);

                inventoryManager.SaveData("dataInventory.json");

                break;
            }
        }

        hoverObject.SetActive(false);
    }

    private void SetHoverAlpha(float alpha)
    {
        var hoverColor = hoverObject.GetComponent<Image>().color;
        hoverColor.a = alpha;
        hoverObject.GetComponent<Image>().color = hoverColor;
    }

    public void SwapSlots(Slot slotA, Slot slotB)
    {
        if (slotA == null || slotB == null) return;

        SlotData tempData = slotA.GetSlotData();
        slotA.UpdateSlotData(slotB.GetSlotData());
        slotB.UpdateSlotData(tempData);

        slotA.UpdateUI();
        slotB.UpdateUI();
    }
}
