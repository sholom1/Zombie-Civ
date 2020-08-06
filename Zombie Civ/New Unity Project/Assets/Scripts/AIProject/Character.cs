using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public abstract class Character : MonoBehaviour
{
    public Health Health;
    public Inventory Inventory;
}
public class SharedCharacter : SharedVariable<Character>
{
    public static implicit operator SharedCharacter(Character value)
    {
        return new SharedCharacter { Value = value };
    }
}
