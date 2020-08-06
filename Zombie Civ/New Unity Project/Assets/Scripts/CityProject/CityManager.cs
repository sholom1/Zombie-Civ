using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityManager : MonoBehaviour
{
    public Camera camera;
    public LayerMask TileLayer;
    #region singleton
    public static CityManager instance;
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
    }
    #endregion
    public int citizens;
    public InputDictionary inputDictionary;
    public LineRenderer pathLine;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StopAllCoroutines();
            StartCoroutine(RecordPath());
        }
    }
    public IEnumerator RecordPath()
    {
        TileManager.instance.CanSelect = false;
        LandTile start = null, end = null;
        while (!Input.GetKeyDown(inputDictionary.Cancel) || end != null)
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, TileLayer)) {
                LandTile hitTile = hit.collider.GetComponent<LandTile>();
                if (!start)
                {
                    start = hitTile;
                }
                else if (!start.Equals(hitTile))
                {
                    Vector3[] positions = CalculatePath(start, hitTile).Positions.ToArray();
                    pathLine.SetPositions(positions);
                }
            }
            yield return null;
        }
        TileManager.instance.CanSelect = true;
    }
    public Route CalculatePath(LandTile start, LandTile end)
    {
        List<RouteTile> openSet = new List<RouteTile>();
        HashSet<RouteTile> closedSet = new HashSet<RouteTile>();
        Route path = new Route();

        openSet.Add(new RouteTile(start, end));

        while (openSet.Count > 0)
        {
            RouteTile currentTile = openSet[0];
            for (int i = 0; i < openSet.Count; i++)
            {

                if (openSet[i].fCost < currentTile.fCost || openSet[i].fCost == currentTile.fCost && openSet[i].hCost < currentTile.hCost)
                {
                    currentTile = openSet[i];
                }
            }
            openSet.Remove(currentTile);
            closedSet.Add(currentTile);

            if (currentTile.tile == end)
            {
                while (currentTile.Parent != null)
                {
                    path.Positions.Add(currentTile.tile.transform.position);
                    currentTile = currentTile.Parent;
                }
                break;
            }

            foreach (LandTile neighbor in TileManager.instance.GetNeighbors(currentTile.tile))
            {
                RouteTile tile = new RouteTile(currentTile, neighbor, end);
                if (tile.gCost < TileManager.GetDistance(start, neighbor) || !openSet.Contains(tile))
                {
                    openSet.Add(tile);
                }
            }
        }
        return path;
    }
}
public class Route
{
    public List<Vector3> Positions;
    public Route()
    {
        Positions = new List<Vector3>();
    }
}
class RouteTile
{
    public float gCost;
    public float hCost;
    public LandTile tile;
    public RouteTile Parent;
    public float fCost { get { return gCost + hCost; } }
    public RouteTile(RouteTile parent, LandTile myTile, LandTile goal)
    {
        tile = myTile;
        Parent = parent;
        gCost = parent.fCost + TileManager.GetDistance(myTile, parent.tile);
        hCost = TileManager.GetDistance(myTile, goal);
    }
    public RouteTile(LandTile myTile, LandTile goal)
    {
        tile = myTile;
        gCost = 0;
        hCost = TileManager.GetDistance(myTile, goal);
    }
    public override bool Equals(object obj)
    {
        if (obj is RouteTile)
        {
            return ((RouteTile)obj).tile == tile;
        }
        else
            return false;
    }
}
