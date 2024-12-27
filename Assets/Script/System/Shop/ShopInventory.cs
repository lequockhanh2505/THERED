using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInventory : MonoBehaviour
{
    public List<ItemStats> itemsInShop = new List<ItemStats>();

    public void AddItem(ItemStats item)
    {
        itemsInShop.Add(item);
    }

    public void RemoveItem(ItemStats item)
    {
        itemsInShop.Remove(item);
    }

    public ItemStats GetItemByName(string name)
    {
        return itemsInShop.Find(item => item.itemName == name);
    }
}
