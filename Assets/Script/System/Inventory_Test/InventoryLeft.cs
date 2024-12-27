using System.Collections.Generic;
using UnityEngine;

public class InventoryLeft : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;
    public static InventoryLeft Instance;
    

    private void Awake()
    {

        

        Instance = this;
    }

    void Start()
    {
        
    }
}
