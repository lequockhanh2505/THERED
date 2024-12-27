//using System.Collections.Generic;
//using System.IO;
//using UnityEngine;

//public class InventoryDataHandler : MonoBehaviour
//{
//    public static InventoryDataHandler Instance;

//    private void Awake()
//    {
//        Instance = this;
//    }

//    public List<SlotData> LoadInventoryData()
//    {
//        string path = Path.Combine(Application.streamingAssetsPath, "dataInventory.json");
//        if (File.Exists(path))
//        {
//            string json = File.ReadAllText(path);
//            InventoryData inventoryData = JsonUtility.FromJson<InventoryData>(json);
//            return inventoryData.items;
//        }
//        else
//        {
//            Debug.LogError("JSON file not found at: " + path);
//            return new List<SlotData>();
//        }
//    }

//    public void SaveInventoryData(List<Slot> slots)
//    {
//        string path = Path.Combine(Application.streamingAssetsPath, "dataInventory.json");

//        List<SlotData> slotDataList = new List<SlotData>();
//        foreach (var slot in slots)
//        {
//            SlotData slotData = slot.GetSlotData(); // Call GetSlotData on Slot objects, not SlotData
//            slotDataList.Add(slotData);
//        }

//        InventoryData inventoryData = new InventoryData { items = slotDataList };
//        string json = JsonUtility.ToJson(inventoryData, true);
//        File.WriteAllText(path, json);
//    }
//}
