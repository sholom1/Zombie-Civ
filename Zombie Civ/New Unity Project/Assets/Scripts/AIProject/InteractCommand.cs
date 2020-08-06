using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class InteractCommand : Action
{
    public SharedGameObject Target;
    public SharedCharacter controller;

    public override TaskStatus OnUpdate()
    {
        if (Target.Value)
        {
            Interactable item = Target.Value.GetComponent<Interactable>();
            if (item)
            {
                if (item.Invoke(controller.Value))
                    return TaskStatus.Success;
            }
            else
            {
                Debug.LogWarning("Somehow the target is not interactable!");
                return TaskStatus.Failure;
            }
        }
        Debug.LogWarning("Target does not exist! This command should not have been executed!");
        return TaskStatus.Failure;
    }
}
