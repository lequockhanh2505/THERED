using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopLoader : IShopLoader
{
    ISlotManager _slotManager;
    List<ItemStats> items;

    public ShopLoader(ISlotManager slotManager, List<ItemStats> items)
    {
        _slotManager = slotManager;
        this.items = items;
    }
    public void LoadShop()
    {
        var slots = _slotManager.GetAllSlots();
        for (int i = 0; i < items.Count && i < slots.Count; i++)
        {
            Slot slot = slots[i].GetComponent<Slot>();
            slot.SetupSlot(items[i], 1);
        }
    }
}
