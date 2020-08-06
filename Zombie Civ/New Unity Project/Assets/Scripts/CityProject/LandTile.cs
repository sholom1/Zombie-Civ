using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LandTile : MonoBehaviour
{

    public bool Owned;

    public TileResources DailyResources;

    public TileResources StockPiledResources;

    public TileResources MaxStockPile;

    public string CurrentBuilding;
    [SerializeField]
    private BuildingPrefab Graphics;

    public List<TileAction> AvailiableActions;
    public UnityEvent OnSelected;
    public UnityEvent OnDeselect;

    public float EntryCost;

    public Wall[] walls = new Wall[4];

    public void BuildBuilding(BuildingPrefab building)
    {
        if (CurrentBuilding != null && TileManager.instance.buildingDictionary.TryGetBuilding(CurrentBuilding, out BuildingPrefab existingBuilding))
        {
            DailyResources -= existingBuilding.DailyResources;
            //StockPiledResources -= existingBuilding.StockPiledResources;
            MaxStockPile -= existingBuilding.MaxStockPile;
        }
        CurrentBuilding = building.name;
        DailyResources += building.DailyResources;
        //StockPiledResources += building.StockPiledResources;
        MaxStockPile += building.MaxStockPile;
        Destroy(Graphics.gameObject);
        Graphics = Instantiate(building, transform);
    }
    public void IncreaseResources()
    {
        StockPiledResources.Wood = Mathf.Clamp(StockPiledResources.Wood + DailyResources.Wood, 0, MaxStockPile.Wood);
        StockPiledResources.Stone = Mathf.Clamp(StockPiledResources.Stone + DailyResources.Stone, 0, MaxStockPile.Stone);
        StockPiledResources.Metal = Mathf.Clamp(StockPiledResources.Metal + DailyResources.Metal, 0, MaxStockPile.Metal);
        StockPiledResources.Credits = Mathf.Clamp(StockPiledResources.Credits + DailyResources.Credits, 0, MaxStockPile.Credits);
        StockPiledResources.Food = Mathf.Clamp(StockPiledResources.Food + DailyResources.Food, 0, MaxStockPile.Food);
    }
    public void Select()
    {
        OnSelected.Invoke();
    }
    public void DeSelect()
    {
        OnDeselect.Invoke();
    }
    public override bool Equals(object other)
    {
        if (other is LandTile)
        {
            return ((LandTile)other).transform.position == transform.position;
        }
        return false;
    }
}
