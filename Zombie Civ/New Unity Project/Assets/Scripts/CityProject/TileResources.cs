using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class TileResources
{
    public int Wood;
    public int Stone;
    public int Metal;
    public int Credits;
    public int Food;
    public static TileResources operator +(TileResources a, TileResources b)
    {
        return new TileResources
        {
            Credits = a.Credits + b.Credits,
            Stone = a.Stone + b.Stone,
            Metal = a.Metal + b.Metal,
            Wood = a.Wood + b.Wood,
            Food = a.Food + b.Food
        };
    }
    public static TileResources operator -(TileResources a, TileResources b)
    {
        return new TileResources
        {
            Credits = a.Credits - b.Credits,
            Stone = a.Stone - b.Stone,
            Metal = a.Metal - b.Metal,
            Wood = a.Wood - b.Wood,
            Food = a.Food - b.Food
        };
    }
}
