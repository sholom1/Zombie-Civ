using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public List<Resource> SceneResources = new List<Resource>();
    public Dictionary<ResourceType, List<Resource>> ResourceDictionary = new Dictionary<ResourceType, List<Resource>>();
    public static ResourceManager instance;
    public List<Pickup> ItemsList = new List<Pickup>();

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(this);
    }
    private void Start()
    {
        foreach(Resource resource in SceneResources)
        {
            AddResource(resource);
        }
    }
    public void AddResource(Resource resource)
    {
        if (ResourceDictionary.TryGetValue(resource.type, out List<Resource> value))
        {
            value.Add(resource);
        }
        else
        {
            ResourceDictionary.Add(resource.type, new List<Resource> { resource });
        }
    }
    public void RemoveResource(Resource resource)
    {
        if(ResourceDictionary.TryGetValue(resource.type, out List<Resource> value))
        {
            value.Remove(resource);
        }
        else
        {
            Debug.LogWarning($"Resource Manager is missing a resource of type {resource.type.ToString()}, @ {resource.transform.position}");
        }
    }
}
