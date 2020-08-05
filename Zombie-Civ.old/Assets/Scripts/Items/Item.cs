using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Item {
	
	public float Mass;
    public Item(float mass)
    {
        Mass = mass;
    }
    public override bool Equals(object obj)
    {
        if (obj != null && obj is Item && ((Item)obj).Mass == Mass) 
            return true;
        return false;
    }
    public override int GetHashCode()
    {
        return Tuple.Create(Mass, GetType()).GetHashCode();
    }
}
