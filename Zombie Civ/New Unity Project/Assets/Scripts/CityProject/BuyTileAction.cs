using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "TileActions/Buy Tile")]
public class BuyTileAction : TileAction
{
    public Wall BasicWall;
    public override bool Condition(LandTile landTile)
    {
        return !landTile.Owned;
    }

    public override bool Trigger(LandTile landTile)
    {
        LandTile neighbor;
        //Tile Above
        if (TileManager.instance.TryGetActiveTile((int)landTile.transform.position.x, (int)landTile.transform.position.y + 10, out neighbor))
        {
            if(neighbor.walls[2] == null)
            {
                Wall newWall = Instantiate(BasicWall,
                                            new Vector3Int((int)(landTile.transform.position.x), 1, (int)(landTile.transform.position.y + 5)),
                                            Quaternion.identity,
                                            CityManager.instance.transform);
                neighbor.walls[2] = newWall;
                landTile.walls[0] = newWall;
            }
        }
        if (TileManager.instance.TryGetActiveTile((int)landTile.transform.position.x + 10, (int)landTile.transform.position.y, out neighbor))
        {
            if (neighbor.walls[3] == null)
            {
                Wall newWall = Instantiate(BasicWall,
                                            new Vector3Int((int)(landTile.transform.position.x + 5), 1, (int)(landTile.transform.position.y)),
                                            Quaternion.identity,
                                            CityManager.instance.transform);
                neighbor.walls[3] = newWall;
                landTile.walls[1] = newWall;
            }
        }
        if (TileManager.instance.TryGetActiveTile((int)landTile.transform.position.x, (int)landTile.transform.position.y - 10, out neighbor))
        {
            if (neighbor.walls[0] == null)
            {
                Wall newWall = Instantiate(BasicWall,
                                            new Vector3Int((int)(landTile.transform.position.x), 1, (int)(landTile.transform.position.y - 5)),
                                            Quaternion.identity,
                                            CityManager.instance.transform);
                neighbor.walls[0] = newWall;
                landTile.walls[2] = newWall;
            }
        }
        if (TileManager.instance.TryGetActiveTile((int)landTile.transform.position.x - 10, (int)landTile.transform.position.y, out neighbor))
        {
            if (neighbor.walls[1] == null)
            {
                Wall newWall = Instantiate(BasicWall,
                                            new Vector3Int((int)(landTile.transform.position.x - 5), 1, (int)(landTile.transform.position.y)),
                                            Quaternion.identity,
                                            CityManager.instance.transform);
                neighbor.walls[1] = newWall;
                landTile.walls[3] = newWall;
            }
        }
        return landTile.Owned = true;
    }
}
