using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public static InventoryManager Instance;
    [SerializeField]
    public List<GameObject> slots;
    [SerializeField]
    private GameObject slotPrefab;
    [SerializeField]
    private GameObject parentContent;
    [SerializeField]
    private int numberSlot = 20;

    private IItemManager _itemManager;
    private ISlotManager _slotManager;
    private IInventoryLoader _loader;
    private IInventorySaver _saver;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        var path = new UnityDataPathProvider();
        _slotManager = new SlotManager(slots, slotPrefab, parentContent, numberSlot);
        _loader = new InventoryLoader(path);
        _saver = new InventorySaver(path);
        _slotManager.InitializeSlots();
        _slotManager.LoadSlot(_loader);
    }

    private void Start()
    {
        if (slots != null && InventoryUIManager.Instance != null)
        {
            InventoryUIManager.Instance.SelectSlot(slots[0].GetComponent<Slot>());
        }
        else
        {
            Debug.LogWarning("InventoryUIManager.Instance is null or slots is null!");
        }
    }

    public void SaveData(string url)
    {
        _saver.SaveItems(ConvertGameObjectsToSlots(slots), url);
    }

    public List<SlotData> ConvertGameObjectsToSlots(List<GameObject> gameObjects)
    {
        var slots = new List<SlotData>();
        foreach (var gameObject in gameObjects)
        {
            var slot = gameObject.GetComponent<Slot>();
            if (slot != null)
            {
                slots.Add(slot.GetSlotData());
            }
            else
            {
                Debug.LogWarning($"GameObject {gameObject.name} does not have a Slot component.");
            }
        }
        return slots;
    }

    public void LoadInventory()
    {
        _slotManager.LoadSlot(_loader);
    }
}
