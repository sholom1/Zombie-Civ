using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieCiv.Construction
{
    public class StorageHealth : PlayerHealth
    {
        public StorageArea storageArea;
        public override void ChangeHealth(float health)
        {
            base.ChangeHealth(health);
            storageArea.adjustInventory(currentHealth);
        }
    }
}
