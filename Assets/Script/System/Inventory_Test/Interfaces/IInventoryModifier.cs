using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryModifier
{
    bool AddItem(ItemStats item, List<GameObject> slots, string saveUrl);
    void RemoveItem(ItemStats item, List<GameObject> slots, string saveUrl);
}
