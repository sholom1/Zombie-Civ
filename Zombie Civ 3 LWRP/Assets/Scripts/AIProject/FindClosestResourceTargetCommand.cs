using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class FindClosestResourceTargetCommand : TargetingSystem
{
    public ResourceType DesiredResource;
    public SharedResource TargetedResource;
    public override TaskStatus OnUpdate()
    {
        Resource lastClosest = null;
        float closestDistance = float.MaxValue;
        foreach (Resource resource in ResourceManager.instance.ResourceDictionary[DesiredResource])
        {
            float distance = Vector3.Distance(transform.position, resource.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                lastClosest = resource;
            }
        }
        if (lastClosest)
        {
            TargetedResource.SetValue(lastClosest);
            Target.SetValue(lastClosest.gameObject);
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
