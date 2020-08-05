using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class MoveToTargetCommand : Action
{
    public Vector3 Destination;
    public NavMeshAgent agent;
    public SharedGameObject Target;
    public float range;

    public override void OnStart()
    {
        agent.ResetPath();
        Debug.Log($"MoveToTarget Is Target null {Target.Value == null}");
        Vector3 lastClosest = Target.Value.transform.position;
        float closestDistance = float.MaxValue;
        foreach (Vector3 position in GetPositionListAround(Target.Value.transform.position, 2f, 10))
        {
            float distance = Vector3.Distance(transform.position, position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                lastClosest = position;
            }
        }
        if (lastClosest != Destination)
        {
            agent.SetDestination(lastClosest);
            Destination = lastClosest;
        }
    }
    public override TaskStatus OnUpdate()
    {
        if (!Target.Value) return TaskStatus.Failure;
        if (agent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            return TaskStatus.Success;
        }
        else
        {
            Debug.Log(agent.pathStatus);
            Debug.Log(agent.remainingDistance);
            return TaskStatus.Running;
        }
    }
    public static List<Vector3> GetPositionListAround(Vector3 startPosition, float distance, int positionCount)
    {
        List<Vector3> positions = new List<Vector3>();
        for (int i = 0; i < positionCount; i++)
        {
            float angle = i * (360 / positionCount);
            Vector3 direction = Quaternion.Euler(0f, angle, 0f) * Vector3.right;
            positions.Add(startPosition + direction * distance);
        }
        return positions;
    }
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.blue;
    //    foreach (Vector3 position in GetPositionListAround(controller.transform.position, 1, 10))
    //    {
    //        Gizmos.DrawSphere(position, .1f);
    //    }
    //}
}
