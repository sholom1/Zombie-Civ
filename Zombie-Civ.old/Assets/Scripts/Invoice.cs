using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invoice
{
    public List<Item> items = new List<Item>();
    public bool allitems;
    public List<Inventory> providingInventorys = new List<Inventory>();
    public List<Receipt> receipts = new List<Receipt>();
    public Invoice(List<Item> items1, bool allitems1, List<Inventory> pi, List<Receipt> r)
    {
        items = items1;
        allitems = allitems1;
        providingInventorys = pi;
        receipts = r;
    }
    public Invoice(bool _allItems)
    {
        allitems = _allItems;
    }
    public override string ToString()
    {
        string returnString = "";
        foreach (Item item in items)
        {
            returnString += item.ToString() + ", ";
        }
        return returnString;
    }
}
