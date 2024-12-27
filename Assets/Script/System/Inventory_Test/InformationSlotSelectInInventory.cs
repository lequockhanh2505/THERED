using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationSlotSelectInInventory : MonoBehaviour, ISlotSelect
{
    [SerializeField]
    private Image icon;
    [SerializeField]
    private TMP_Text nameItem;
    [SerializeField]
    private TMP_Text descriptionItem;

    void OnEnable()
    {
        InventoryUIManager.OnSlotSelect += SlotSelect;
    }

    void OnDisable()
    {
        InventoryUIManager.OnSlotSelect -= SlotSelect;
    }

    public void SlotSelect(Slot slot)
    {
        if (slot != null && slot.itemStats != null)
        {
            UpdateUI(slot.itemStats);
        }
        else
        {
            ClearUI();
        }
    }

    public void UpdateUI(ItemStats itemStats)
    {
        icon.sprite = itemStats.icon;
        icon.color = SetAlpha(itemStats.icon != null ? 1f : 0f);

        nameItem.SetText(itemStats.itemName);

        descriptionItem.SetText(itemStats.description);
    }

    public void ClearUI()
    {
        icon.sprite = null;
        icon.color = SetAlpha(0f);

        nameItem.SetText("");

        descriptionItem.SetText("");
    }

    public Color SetAlpha(float alpha)
    {
        return new Color(icon.color.r, icon.color.g, icon.color.b, alpha);
    }
}
