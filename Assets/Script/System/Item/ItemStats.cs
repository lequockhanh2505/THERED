using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ItemStats/Item") ]
public class ItemStats : ScriptableObject
{
    public enum ItemType
    {
        Weapon,
        Armor,
        Consumable,
        Misc,
        Coin,

    }

    public string itemName;
    public int price;
    public Sprite icon;
    public string description;
    public ItemType itemType;

    public bool isCanStack;

}
