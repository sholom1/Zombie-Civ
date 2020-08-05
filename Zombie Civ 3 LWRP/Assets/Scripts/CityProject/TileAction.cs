using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileAction : ScriptableObject
{
    public new string name;
    private void Awake()
    {
        TimesInvoked = 0;
    }
    public int TimesInvoked;
    public abstract bool Condition(LandTile landTile);

    public abstract bool Trigger(LandTile landTile);
}
