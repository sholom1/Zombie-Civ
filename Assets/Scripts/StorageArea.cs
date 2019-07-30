using UnityEngine;

namespace ZombieCiv.Construction
{
    public class StorageArea : Building
    {
        public Inventory inventory;
        public StorageHealth HealthManager;
        private float OGhealth;

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            OGhealth = HealthManager.currentHealth;
        }
        public void adjustInventory(float newHealth)
        {
            inventory.Space = Mathf.RoundToInt(inventory.Space * newHealth / OGhealth);
        }
    }
}
