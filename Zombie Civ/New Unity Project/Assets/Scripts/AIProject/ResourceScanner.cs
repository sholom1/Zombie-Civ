using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceScanner : Scanner
{
    public ResourceType DesiredResource;
    public GameObject Scan()
    {
        Resource lastClosest = null;
        float closestDistance = float.MaxValue;
        foreach(Resource resource in ResourceManager.instance.ResourceDictionary[DesiredResource])
        {
            float distance = Vector3.Distance(transform.position, resource.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                lastClosest = resource;
            }
        }
        if (lastClosest)
        return lastClosest.gameObject;
        return null;
    }
}
