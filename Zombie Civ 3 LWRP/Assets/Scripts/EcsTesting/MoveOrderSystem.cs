using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Collections;
using Unity.Mathematics;

public class MoveOrderSystem : SystemBase
{
    

    protected override void OnUpdate()
    {
        float _deltaTime = Time.DeltaTime;
        Entities.ForEach((ref Translation translation, ref LocalToWorld position,
            ref MoveOrderComponent moveOrder) =>
        {
            translation.Value += (position.Position - moveOrder.TargetPosition) * moveOrder.Speed * _deltaTime;
        }).Schedule();
    }
}
public struct MoveOrderJob : IJobParallelFor
{
    public void Execute(int index)
    {
        throw new System.NotImplementedException();
    }
}
