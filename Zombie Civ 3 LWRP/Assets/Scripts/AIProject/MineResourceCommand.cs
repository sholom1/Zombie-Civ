using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class MineResourceCommand : Action
{
    public SharedResource TargetedResource;
    public float Damage;

    public override TaskStatus OnUpdate()
    {
        if (TargetedResource.Value.Mine(Damage * Time.deltaTime))
            return TaskStatus.Success;
        return TaskStatus.Running;
    }
}
