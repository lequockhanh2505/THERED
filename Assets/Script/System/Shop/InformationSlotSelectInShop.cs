using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationSlotSelectInShop : MonoBehaviour, ISlotSelect
{
    [SerializeField] public InventoryManager inventoryManager;
    [SerializeField] private ItemManager itemManager;
    [SerializeField] private PlayerStats playerStats;

    [SerializeField] Image icon;
    [SerializeField] TMP_Text nameItem;
    [SerializeField] TMP_Text descriptionItem;

    private IItemManager _itemManager;

    private Slot slotCurrent;

    void OnEnable()
    {
        ShopUIManager.OnItemConfirm += SlotSelect;
    }

    void OnDisable()
    {
        ShopUIManager.OnItemConfirm -= SlotSelect;
    }

    private void Awake()
    {
        if (inventoryManager == null)
        {
            Debug.LogWarning("InventoryManager is null");
            inventoryManager = FindObjectOfType<InventoryManager>();
        }

        _itemManager = new ItemManager();
    }

    public void BuyItem()
    {
        if (GetItemInInventory() == null)
        {
            Debug.Log("Inventory Full");
            return;
        }
        else
        {
            if (playerStats.Coin > slotCurrent.itemStats.price)
            {
                playerStats.Coin -= slotCurrent.itemStats.price;

                _itemManager.AddItem(slotCurrent.itemStats);

                GetItemInInventory().GetComponent<Slot>().SetupSlot(slotCurrent.itemStats, 1);
                inventoryManager.SaveData("dataInventory.json");
            }
        }
    }

    public GameObject GetItemInInventory()
    {
        if(inventoryManager.slots.Count == 0) { return null; }

        foreach (GameObject slot in inventoryManager.slots)
        {
            if (slot.GetComponent<Slot>().itemStats == null)
            {
                return slot;
            }
        }
        return null;
    }

    public void SlotSelect(Slot slot)
    {
        slotCurrent = slot;

        if (slot != null && slot.itemStats != null)
        {
            UpdateUI(slot.itemStats);
        }
        else
        {
            Debug.LogError("Invalid slot or item.");
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
