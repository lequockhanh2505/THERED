using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InventorySaver : IInventorySaver
{
    private readonly IDataPathProvider _pathProvider;
    public InventorySaver(IDataPathProvider pathProvider)
    {
        _pathProvider = pathProvider;
    }
    public void SaveItems(List<SlotData> slots, string fileName)
    {
        string path = _pathProvider.GetPath(fileName);

        List<SlotData> slotDataList = new List<SlotData>();
        foreach (var slotObject in slots)
        {
            if (slotObject != null)
            {
                slotDataList.Add(slotObject);
            }
        }

        InventoryData inventoryData = new InventoryData { items = slotDataList };
        string json = JsonUtility.ToJson(inventoryData, true);
        File.WriteAllText(path, json);
    }
}
