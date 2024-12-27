using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InventoryLoader : IInventoryLoader
{
    private readonly IDataPathProvider _pathProvider;

    public InventoryLoader(IDataPathProvider pathProvider)
    {
        _pathProvider = pathProvider;
    }

    public List<SlotData> LoadItems(string fileName)
    {
        string path = _pathProvider.GetPath(fileName);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            InventoryData inventoryData = JsonUtility.FromJson<InventoryData>(json);
            return inventoryData.items;
        }
        else
        {
            Debug.LogError("JSON file not found at: " + path);
            return new List<SlotData>();
        }
    }
}
