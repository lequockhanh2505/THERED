[System.Serializable]
public class SlotData
{
    public int slotIndex;
    public int quantity;
    public ItemStats itemStats;

    public SlotData()
    {

    }
    public SlotData(int slotIndex, int quantity, ItemStats itemStats)
    {
        this.slotIndex = slotIndex;
        this.quantity = quantity;
        this.itemStats = itemStats;
    }
}
