using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : Item
{
    public Wood(float mass) : base(mass)
    {
    }
    public override bool Equals(object obj)
    {
        if (obj != null && obj is Wood && ((Item)obj).Mass == Mass)
            return true;
        return false;
    }
}
