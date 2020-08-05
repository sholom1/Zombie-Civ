using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CompareTwoTargetsCommand : Conditional
{
    public SharedGameObject LeftTarget;
    public SharedGameObject RightTarget;
    public SharedGameObject Result;

    public override TaskStatus OnUpdate()
    {
        float d1 = Vector3.Distance(transform.position, LeftTarget.Value.transform.position);
        float d2 = Vector3.Distance(transform.position, RightTarget.Value.transform.position);
        Debug.Log($"Is {d1} <= {d2}. Is {LeftTarget.Value == null}, {RightTarget.Value == null}");
        if (d1 <= d2)
        {
            Result.SetValue(LeftTarget.Value);
            Debug.Log(LeftTarget.Value == null);
            Debug.Log(Result.Value == null);
            LeftTarget.SetValue(null);
            RightTarget.SetValue(null);
            return TaskStatus.Success;
        }
        else
        {
            Result.SetValue(RightTarget.Value);
            Debug.Log(RightTarget.Value == null);
            Debug.Log(Result.Value == null);
            LeftTarget.SetValue(null);
            RightTarget.SetValue(null);
            return TaskStatus.Failure;
        }
    }
}
