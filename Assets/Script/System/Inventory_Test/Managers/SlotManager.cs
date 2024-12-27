using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : ISlotManager
{
    private readonly List<GameObject> slots = new List<GameObject>();
    private readonly GameObject slotPrefab;
    private readonly GameObject parentContent;
    private readonly int numberSlot;
    public SlotManager(List<GameObject> slots, GameObject slotPrefab, GameObject parentContent, int numberSlot)
    {
        this.slots = slots;
        this.slotPrefab = slotPrefab;
        this.parentContent = parentContent;
        this.numberSlot = numberSlot;
    }

    public void InitializeSlots()
    {
        slots.Clear();
        for (int i = 0; i < numberSlot; i++)
        {
            GameObject slot = GameObject.Instantiate(slotPrefab, parentContent.transform);
            slots.Add(slot);
        }
    }

    public void LoadSlot(IInventoryLoader _loader)
    {
        List<SlotData> loadedItems = _loader.LoadItems("dataInventory.json");
        if (loadedItems == null || loadedItems.Count == 0)
        {
            Debug.LogWarning("No items loaded from inventory.");
            return;
        }
        List<Slot> availableSlots = GetAvailableSlots();
        for (int i = 0; i < loadedItems.Count; i++)
        {
            if (i < availableSlots.Count)
            {
                Slot slot = availableSlots[i];
                slot.SetupSlot(loadedItems[i].itemStats, loadedItems[i].quantity);
                slot.UpdateUI();
            }
            else
            {
                Debug.LogWarning("Not enough available slots for all items.");
                break;
            }
        }
    }

    public List<GameObject> GetAllSlots()
    {
        return slots;
    }

    public List<Slot> GetAvailableSlots()
    {
        var availableSlots = new List<Slot>();
        foreach (var slotObj in slots)
        {
            Slot slot = slotObj.GetComponent<Slot>();
            if (slot.itemStats == null)
                availableSlots.Add(slot);
        }
        return availableSlots;
    }

    //public void SaveInventory(string saveFilePath)
    //{
    //    var pathProvider = new UnityDataPathProvider();
    //    _saver = new InventorySaver(pathProvider);

    //    var slotData = new List<SlotData>();

    //    foreach (var slotObject in slots)
    //    {
    //        Slot slot = slotObject.GetComponent<Slot>();
    //        if (slot != null)
    //        {
    //            SlotData data = slot.GetSlotData();
    //            slotData.Add(data);
    //        }
    //        else
    //        {
    //            Debug.LogError("Slot component missing on slot object: " + slotObject.name);
    //        }
    //    }

    //    _saver.SaveItems(slotData, saveFilePath);
    //}
}
