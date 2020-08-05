using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    public Dictionary<int, int> ItemIDAmountPairs = new Dictionary<int, int>();
    public bool Add(Item item)
    {
        if (ItemIDAmountPairs.ContainsKey(item.ID))
        {
            ItemIDAmountPairs[item.ID]++;
            return true;
        }
        else
        {
            ItemIDAmountPairs.Add(item.ID, 1);
            return true;
        }
    }
    public bool IsFull()
    {
        return false;
    }
}
