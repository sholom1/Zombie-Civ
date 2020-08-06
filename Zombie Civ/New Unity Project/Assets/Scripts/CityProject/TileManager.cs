using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileManager : MonoBehaviour
{
    public static TileManager instance;
    public LayerMask TileLayer;
    public LandTile SelectedTile;
    public new Camera camera;
    public BuildingDictionary buildingDictionary;
    public TileDicitonary tileDicitonary;
    public GameObject ActionsMenu;
    public Button ActionButtonPrefab;
    public List<Button> InstantiatedActions;
    public BuildMenuManager BuildMenu;
    public int X, Y;
    public bool CanSelect = true;
    public Dictionary<Vector3Int, LandTile> Map = new Dictionary<Vector3Int, LandTile>();
    public float timer;
    public float timerOG;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        buildingDictionary.LoadBuildings();
        tileDicitonary.LoadBuildings();
        GenerateMap();
    }

    public void Update()
    {
        if ((timer -= Time.deltaTime) <= 0)
        {
            foreach (LandTile tile in Map.Values)
            {
                if (tile.Owned)
                {
                    tile.IncreaseResources();
                }
            }
            timer = timerOG;
        }
        if (CanSelect && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, TileLayer))
            {
                LandTile tile = hit.collider.GetComponent<LandTile>();
                if (tile && SelectedTile != tile)
                {
                    SelectedTile = tile;
                    StopAllCoroutines();
                    StartCoroutine(CheckIfActionConditionsHaveChanged());
                    //if (buildingDictionary.TryGetBuilding("Forestry", out BuildingPrefab building))
                    //{
                    //    Debug.Log("Built building");
                    //    SelectedTile.BuildBuilding(building);
                    //    SelectedTile.CurrentBuilding = building.Name;
                    //}
                }
            }
        }
    }
    public void GenerateMap()
    {
        for (int x = 0; x < X; x++)
        {
            for (int y = 0; y < Y; y++)
            {
                List<LandTile> tiles = tileDicitonary.GetAllLoadedTiles();
                Map.Add(new Vector3Int(x, 0, y), Instantiate(tiles[Random.Range(0, tiles.Count)], new Vector3(x * 10, 0, y * 10), Quaternion.identity, gameObject.transform));
            }
        }
    }
    public IEnumerator CheckIfActionConditionsHaveChanged()
    {
        while (true)
        {
            UpdateActions();
            yield return new WaitForSeconds(1f);
        }
    }
    public void UpdateActions()
    {
        foreach (Button b in InstantiatedActions)
            Destroy(b.gameObject);
        InstantiatedActions.Clear();
        foreach (TileAction action in SelectedTile.AvailiableActions)
        {
            Button newButton = Instantiate(ActionButtonPrefab, ActionsMenu.transform);
            newButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = action.name;
            if (action.Condition(SelectedTile))
                newButton.onClick.AddListener(() => action.Trigger(SelectedTile));
            else
                newButton.interactable = false;
            InstantiatedActions.Add(newButton);
        }
    }
    public bool TryGetActiveTile(int x, int y, out LandTile tile)
    {
        return Map.TryGetValue(new Vector3Int(x, 0, y), out tile);
    }
    public List<LandTile> GetNeighbors(LandTile tile)
    {
        List<LandTile> returnList = new List<LandTile>();
        Vector3Int tilePos = Vector3Int.RoundToInt(tile.transform.position);
        for (int x = -1; x <= 1; x++)
        {
            for (int z = -1; z <=1; z++)
            {
                if (x == 0 && z == 0)
                    continue;
                if (Map.TryGetValue(new Vector3Int(tilePos.x + x * 10, 0, tilePos.z + z * 10), out LandTile neighbor))
                {
                    returnList.Add(neighbor);
                }
            }
        }
        return returnList;
    }
    public static float GetDistance(LandTile a, LandTile b)
    {
        float distX = Mathf.Abs(a.transform.position.x - b.transform.position.y);
        float distZ = Mathf.Abs(a.transform.position.z - b.transform.position.z);
        if (distX > distZ)
            return 14 * distZ + 10 * distX;
        return 14 * distX + 10 * distZ;
    }
}
