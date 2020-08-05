using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableManager : MonoBehaviour
{
    public int objectswithhealthnumber = 0;
    public GameObject[] objectsWithHealth;
    public List<GameObject> thingswithhealth = new List<GameObject>();
    #region singleton
    public static DestructableManager instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }
    #endregion
    // Use this for initialization
    void Start()
    {
    }
    //adds to the list of things with health
    public void addMe(GameObject enterMe)
    {
        thingswithhealth.Add(enterMe);
        objectswithhealthnumber++;
    }
    //removes from the list of things with health
    public void removeMe(GameObject RemoveMe)
    {
        thingswithhealth.Remove(RemoveMe);
        objectswithhealthnumber--;
    }
}
