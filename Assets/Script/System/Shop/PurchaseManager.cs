using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseManager
{
    private ItemManager _itemManager;
    private PlayerStats _playerStats;

    public PurchaseManager(ItemManager itemManager, PlayerStats playerStats)
    {
        _itemManager = itemManager;
        _playerStats = playerStats;
    }

    public bool TryBuyItem(ItemStats item)
    {
        if (item == null)
        {
            Debug.LogError("Item is null. Cannot proceed with purchase.");
            return false;
        }

        if (_playerStats.Coin >= item.price)
        {
            Debug.Log(_playerStats);
            if (_itemManager.AddItem(item))
            {
                _playerStats.Coin -= item.price;
                Debug.Log("Item purchased successfully!");
                return true;
            }
            else
            {
                Debug.Log("Inventory is full. Purchase failed.");
                return false;
            }
        }
        else
        {
            Debug.Log("Not enough coins to buy the item.");
            return false;
        }
    }
}
