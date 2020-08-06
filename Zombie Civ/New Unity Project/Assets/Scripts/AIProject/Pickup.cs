using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class Pickup : Interactable
{
    public Item item;
    public override bool Invoke(Character invoker)
    {
        if (invoker.Inventory.Add(item))
        {
            ResourceManager.instance.ItemsList.Remove(this);
            Destroy(gameObject);
            return true;
        }
        return false;
    }
}
public class SharedPickup : SharedVariable<Pickup>
{
    public static implicit operator SharedPickup(Pickup value)
    {
        return new SharedPickup { Value = value };
    }
}