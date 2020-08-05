using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "TileActions/Open Build UI")]
public class OpenBuildUITileAction : TileAction
{
    public override bool Condition(LandTile landTile)
    {
        if (landTile.Owned)
            TileManager.instance.BuildMenu.UpdateButtonsForSelectedTile(landTile);
        return landTile.Owned;
    }

    public override bool Trigger(LandTile landTile)
    {
        if (TileManager.instance)
        {
            TileManager.instance.BuildMenu.BuildingButtonsContainer.SetActive(true);
            return true;
        }
        return false;
    }
}
