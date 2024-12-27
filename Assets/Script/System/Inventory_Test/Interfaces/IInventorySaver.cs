using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventorySaver 
{
    void SaveItems(List<SlotData> slots ,string fileName);
}
