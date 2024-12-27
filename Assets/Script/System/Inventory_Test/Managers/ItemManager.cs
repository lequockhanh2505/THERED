public class ItemManager : IItemManager
{
    private ISlotManager _slotManager;
    private IInventorySaver _saver;

    public bool AddItem(ItemStats item)
    {
        var availableSlots = _slotManager.GetAvailableSlots();

        if (availableSlots.Count > 0)
        {
            var targetSlot = availableSlots[0];
            targetSlot.itemStats = item;
            targetSlot.UpdateUI();
            return true;
        }
        return false;
    }

    public void RemoveItem(ItemStats item)
    {
        var slots = _slotManager.GetAllSlots();
        foreach (var slotObj in slots)
        {
            Slot slot = slotObj.GetComponent<Slot>();
            if (slot.itemStats == item)
            {
                slot.itemStats = null;
                slot.UpdateUI();
                return;
            }
        }
    }
}
