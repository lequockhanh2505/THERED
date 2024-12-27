using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryModifier : IInventoryModifier
{
    private readonly IInventorySaver _saver;

    public InventoryModifier(IInventorySaver saver)
    {
        _saver = saver;
    }

    public bool AddItem(ItemStats item, List<GameObject> slots, string saveUrl)
    {
        foreach (var slotObj in slots)
        {
            Slot slot = slotObj.GetComponent<Slot>();
            if (slot.itemStats == null)
            {
                slot.SetupSlot(item, 1);
                return true;
            }
        }
        Debug.Log("Thêm vật phẩm thất bại: Inventory đầy.");
        return false;
    }


    public void RemoveItem(ItemStats item, List<GameObject> slots, string saveUrl)
    {
        throw new NotImplementedException();
    }

    public List<SlotData> ConvertObjToSlot(List<GameObject> objSlots)
    {
        List<SlotData> slotList = new List<SlotData>();
        foreach (GameObject slotObj in objSlots)
        {
            SlotData slotComponent = slotObj.GetComponent<SlotData>();
            if (slotComponent != null)
            {
                slotList.Add(slotComponent);
            }
        }
        return slotList;
    }
}
