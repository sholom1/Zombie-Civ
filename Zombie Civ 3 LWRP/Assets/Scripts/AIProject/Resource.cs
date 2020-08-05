using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class Resource : MonoBehaviour
{
    public Health Health;
    [SerializeField]
    public Loot[] DropPrefabs;
    public int Yield;
    public ResourceType type;

    public Debris[] Debris;
    public void DropResources()
    {
        foreach (Loot loot in DropPrefabs)
        {
            ResourceManager.instance.ItemsList.Add(Instantiate(loot.Drop, transform.position + loot.Position, Quaternion.identity));
        }
        foreach (Debris d in Debris)
        {
            //d.Collider.isTrigger = false;
            d.Rigidbody.useGravity = true;
        }
        ResourceManager.instance.RemoveResource(this);
        Destroy(transform.parent.gameObject, 5);
        Destroy(gameObject);
    }
    public bool Mine(float Damage)
    {
        return Health.TakeDamage(Damage);
    }
    private void OnDrawGizmosSelected()
    {
        foreach (Loot loot in DropPrefabs)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position + loot.Position, .5f);
        }
    }
}
[System.Serializable]
public struct Loot
{
    public Pickup Drop;
    public Vector3 Position;
}
public enum ResourceType
{
    Wood,
    Stone,
    Metal,
    Credits
}
public class SharedResource : SharedVariable<Resource>
{
    public static implicit operator SharedResource(Resource value)
    {
        return new SharedResource { Value = value };
    }
}
