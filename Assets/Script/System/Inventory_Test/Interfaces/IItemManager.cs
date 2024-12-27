using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemManager 
{
    bool AddItem(ItemStats item);
    void RemoveItem(ItemStats item);
}
