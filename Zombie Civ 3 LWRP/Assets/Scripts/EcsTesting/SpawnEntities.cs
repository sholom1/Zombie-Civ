using Unity.Entities;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;
public class SpawnEntities : SystemBase
{
    public NativeArray<int> amount;
    //private Random random;
    
    protected override void OnStartRunning()
    {
        amount = new NativeArray<int>(1, Allocator.Temp);
        Random random = new Random(64);
        base.OnStartRunning();
        Entities.WithoutBurst().WithStructuralChanges().ForEach((ref PrefabEntityComponent prefabEntityComponent) =>
        {
            EntityManager.Instantiate(prefabEntityComponent.prefabEntity, prefabEntityComponent.amount, Allocator.TempJob);
            amount[0] = prefabEntityComponent.amount;
        }).Run();
        int maxEntities = amount[0];
        Entities.ForEach((ref Translation _translation, ref MoveOrderComponent _moveOrder) =>
        {
            float3 spawnPosition = new float3(random.NextFloat(maxEntities), .5f, random.NextFloat(maxEntities));
            _translation.Value = spawnPosition;
            _moveOrder.TargetPosition = spawnPosition + (random.NextFloat3Direction() * 5f);
        }).Run();
        amount.Dispose();
    }
    protected override void OnStopRunning()
    {
        base.OnStopRunning();
    }

    protected override void OnUpdate()
    {

    }
}
