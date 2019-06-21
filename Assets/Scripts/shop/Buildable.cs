using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Buildable : MonoBehaviour
{
    [Header("Building Info..")]
    //Building Specs
    public Cost Price;
    public GameObject obj;
    public BuildingType WhatIsThis;
    [Space(1)]
    //While Building management
    [Header("While Building Management..")]
    public float buildTime;
    public bool canRotate;
    public GameObject construction;
    public List<Collider> colliders = new List<Collider>();

    void Start()
    {
    }
    /// <summary>
    /// Starts Construction
    /// </summary>
    public void BuildStart()
    {
        GameObject newBuilding = Instantiate(obj, transform.position, transform.rotation);
        Building newBuildingScript = newBuilding.GetComponent<Building>();
        if (newBuildingScript != null)
        {
            StartCoroutine(newBuildingScript.BuildStart(buildTime));
        }
    }
    //if our Gameobject is within another then add their colliders to this list
    void OnTriggerEnter(Collider c)
    {
        //Debug.Log (c.tag.ToString ());
        if (c.tag == "building")
        {
            colliders.Add(c);
        }
    }
    
    //once out remove them
    void OnTriggerExit(Collider c)
    {
        //Debug.Log (c.tag.ToString ());
        if (c.tag == "building")
        {
            colliders.Remove(c);
        }
    }
    public enum BuildingType
    {
        Nothing,
        Post,
        Wall,
        House,
        TownCenter,
        Farm,
        Factory,
        ResourceStorage
    }
}