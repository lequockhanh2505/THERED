using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int slotIndex = 0;
    public ItemStats itemStats;
    public Image icon;
    public Text quantityText;
    private int quantity;

    public void SetupSlot(ItemStats newItemStats, int initialQuantity)
    {
        itemStats = newItemStats;
        quantity = initialQuantity;
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (itemStats != null)
        {
            icon.sprite = itemStats.icon;
            icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, 1f);
            quantityText.text = quantity.ToString();
            quantityText.color = new Color(quantityText.color.r, quantityText.color.g, quantityText.color.b, quantity > 0 ? 1f : 0f);
        }
        else
        {
            icon.sprite = null;
            icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, 0f);
            quantityText.text = "";
        }
    }

    public SlotData GetSlotData()
    {
        return new SlotData(slotIndex, quantity, itemStats); 
    }

    public void UpdateSlotData(SlotData data)
    {
        slotIndex = data.slotIndex;
        quantity = data.quantity;
        itemStats = data.itemStats;
        UpdateUI();
    }

    public void SetBorder(Sprite newSprite)
    {
        transform.Find("Border").GetComponent<Image>().sprite = newSprite;
    }
}
