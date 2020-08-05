using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class FindClosestPickupCommand : TargetingSystem
{
    public SharedPickup TargetedPickup;
    public override TaskStatus OnUpdate()
    {
        Pickup lastClosest = null;
        float closestDistance = float.MaxValue;
        foreach (Pickup item in ResourceManager.instance.ItemsList)
        {
            float distance = Vector3.Distance(transform.position, item.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                lastClosest = item;
            }
        }
        if (lastClosest)
        {
            TargetedPickup.SetValue(lastClosest);
            Target.SetValue(lastClosest.gameObject);
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
