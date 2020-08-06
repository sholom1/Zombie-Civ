using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class BuildingDictionary : ScriptableObject
{
    [SerializeField]
    private List<BuildingPrefab> Buildings = new List<BuildingPrefab>();
    private Dictionary<string, BuildingPrefab> BuildingPairs = new Dictionary<string, BuildingPrefab>();

    public void LoadBuildings()
    {
        foreach (BuildingPrefab building in Buildings)
            BuildingPairs.Add(building.name, building);
    }
    public bool TryGetBuilding(string name, out BuildingPrefab building)
    {
        return BuildingPairs.TryGetValue(name, out building);
    }
    public List<BuildingPrefab> GetAllLoadedBuildings()
    {
        return Buildings;
    }
}

