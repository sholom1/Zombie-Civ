using Unity.Entities;
using Unity.Mathematics;
[GenerateAuthoringComponent]
public struct MoveOrderComponent : IComponentData
{
    public float3 TargetPosition;
    public float Speed;
}
