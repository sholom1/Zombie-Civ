using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePathAction : TileAction
{
    public override bool Condition(LandTile landTile)
    {
        return true;
    }

    public override bool Trigger(LandTile landTile)
    {
        throw new System.NotImplementedException();
    }
}
