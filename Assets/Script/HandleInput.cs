using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleInput : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;

    [SerializeField]
    GameObject Inventory;

    [SerializeField]
    GameObject Shop;

    bool turnOnInventory = false;

    bool turnOnShop = false;
    private bool canOpenUIShop = false;

    void OnEnable()
    {
        Gipsy.OnArea += showUI;
    }

    void OnDisable()
    {
        Gipsy.OnArea -= showUI;
    }

    void showUI(bool state)
    {
        canOpenUIShop = state;
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !turnOnInventory)
        {
            turnOnInventory = true;
            Inventory.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.I) && turnOnInventory)
        {
            turnOnInventory = false;
            Inventory.SetActive(false);
        }

        if (canOpenUIShop && Input.GetKeyDown(KeyCode.F))
        {
            if (!turnOnShop)
            {
                turnOnShop = true;
                Shop.SetActive(true);
            }
            else
            {
                turnOnShop = false;
                Shop.SetActive(false);
            }
        }
    }
}
