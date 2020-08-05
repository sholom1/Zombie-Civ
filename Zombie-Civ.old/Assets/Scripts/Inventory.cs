using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    private Dictionary<Item, int> StoredItems;
	public float currentSpace;
	public int Space;

    private void Start()
    {
        StoredItems = new Dictionary<Item, int>();
        currentSpace = Space;
    }
    public bool AddItems(Item item, int amount)
    {
        if (currentSpace - amount * item.Mass < 0) return false;
        if (StoredItems.TryGetValue(item, out int value))
        {
            currentSpace -= amount * item.Mass;
            StoredItems[item] += amount;
            Debug.Log("Increment Item count");
        }
        else
        {
            StoredItems.Add(item, amount);
            currentSpace -= amount * item.Mass;
            Debug.Log("Add new Item");
        }
        return true;
    }
    public bool Contains(Item item, int amount)
    {
        if (StoredItems.TryGetValue(item, out int value))
            if (value >= amount) return true;
        return false;
    }
    public bool Remove(Item itemToRemove, int amount){
        if (StoredItems.TryGetValue(itemToRemove, out int value) && value - amount >= 0)
        {
            currentSpace += amount * itemToRemove.Mass;
            StoredItems[itemToRemove] -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }
	public static bool TransferItems(Inventory transferringInventory, Inventory receivingInventory, Item item, int amount){

        if (transferringInventory.Remove(item, amount))
        {
            if (receivingInventory.AddItems(item, amount)) return true;
            transferringInventory.AddItems(item, amount);
        }
        return false;
	}
    public bool TryGetValue(Item key, out int amount)
    {
        return StoredItems.TryGetValue(key, out amount);
    }
}
