using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> slots;
    [SerializeField]
    private GameObject slotPrefab;
    [SerializeField]
    private GameObject content;
    [SerializeField]
    private List<ItemStats> shopItems;
    private PlayerStats _playerStats;

    private ISlotManager _slotManager;
    private IShopLoader _shopLoader;

    private void Awake()
    {
        _playerStats = FindObjectOfType<PlayerStats>();
        _slotManager = new SlotManager(slots, slotPrefab, content, shopItems.Count);
        _shopLoader = new ShopLoader(_slotManager, shopItems);
        _slotManager.InitializeSlots();
        _shopLoader.LoadShop();
    }
}
