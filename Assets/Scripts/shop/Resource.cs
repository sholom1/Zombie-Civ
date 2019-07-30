using UnityEngine;
using ZombieCiv.UI;

namespace ZombieCiv.Items
{
    public class Resource : MonoBehaviour
    {

        public Type ResourceType;
        public int Amount;
        public float Health = 100f;
        public GameObject Prefab;
        public Despawnable[] Debris;

        void Start()
        {
            MapGen.Instance.resources.Add(gameObject);
        }

        public void Mine(float damage, PointSystem ps, bool isPlayer)
        {
            Health -= damage;
            if (Health <= 0)
            {
                for (int i = 0; i < Amount; i++)
                {
                    Instantiate(Prefab, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Prefab.transform.rotation);
                }
                foreach (var debris in Debris)
                {
                    debris.Despawn(20);
                }
                MapGen.Instance.resources.Remove(gameObject);
                Destroy(gameObject);
            }
        }

        public enum Type
        {
            Stone,
            ScrapMetal,
            Wood,
            Credits
        }
    }
}
