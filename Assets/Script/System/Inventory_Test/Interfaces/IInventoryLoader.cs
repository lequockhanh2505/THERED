using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryLoader {
    List<SlotData> LoadItems(string url);
}
