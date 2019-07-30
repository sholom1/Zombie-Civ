using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieCiv.UI;

namespace ZombieCiv.Items
{
    public class ResourcePickup : MonoBehaviour, Interactable
    {
        public Resource.Type ResourceType;
        public float DespawnTime;
        public bool Despawnable;
        public Item Item;
        public int Amount;

        public void Awake()
        {
            switch (ResourceType)
            {
                case Resource.Type.Credits:
                    break;
                case Resource.Type.Wood:
                    Item = new Wood(1f);
                    break;
            }
        }
        public void Pickup(Inventory inventory)
        {
            PointSystem.instance.AddCash(Amount, ResourceType);
            Destroy(gameObject);
        }

        public void Use(Interact player)
        {
            Pickup(player.PlayerInv);
        }

        void Update()
        {
            if (Despawnable)
            {
                DespawnTime -= Time.deltaTime;
                if (DespawnTime <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
