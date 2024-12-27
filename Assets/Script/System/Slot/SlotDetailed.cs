using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotDetailed : ISlot
{
    public event Action<ItemStats> OnItemStatsChanged;

    public void UpdateSlot(Slot slot)
    {
        if (slot != null && slot.itemStats != null)
        {
            OnItemStatsChanged?.Invoke(slot.itemStats);
        }
    }
}
