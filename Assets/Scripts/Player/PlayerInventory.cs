using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private List<Item> _inventory;
    [SerializeField]
    private int _inventorySize;

    public List<Item> Inventory { get; }
    public List<Item> InventorySize { get; set; }

    public PlayerInventory()
    {
        _inventory = new List<Item>();
    }

    public void AddItem(Item item)
    {
        _inventory.Add(item);
    }
    public bool IsFull()
    {
        return _inventory.Count >= _inventorySize;
    }
}
