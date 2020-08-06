using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPrefab : MonoBehaviour
{
    public new string name;
    public TileResources DailyResources;
    public TileResources StockPiledResources;
    public TileResources MaxStockPile;
    public List<TileAction> ActionsToAdd;
    public List<TileAction> ActionsToRemove;
}
