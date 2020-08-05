using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class TileDicitonary : ScriptableObject
{
    [SerializeField]
    private List<LandTile> TilePrefabs = new List<LandTile>();
    private Dictionary<string, LandTile> TilePairs = new Dictionary<string, LandTile>();

    public void LoadBuildings()
    {
        foreach (LandTile Tile in TilePrefabs)
            TilePairs.Add(Tile.name, Tile);
    }
    public bool TryGetTile(string name, out LandTile tile)
    {
        return TilePairs.TryGetValue(name, out tile);
    }
    public List<LandTile> GetAllLoadedTiles()
    {
        return TilePrefabs;
    }
}
