using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryLoaderSlots : IInventoryLoaderSlots
{
    public void LoadSlots(List<GameObject> slots, int numberSlot, GameObject preSlots, GameObject position)
    {
        for (int i = 0; i < numberSlot; i++)
        {
            GameObject slot = GameObject.Instantiate(preSlots, position.transform);
            Slot slotComponent = slot.GetComponent<Slot>();
            slotComponent.slotIndex = i;
            slots.Add(slot);
        }
    }
}
