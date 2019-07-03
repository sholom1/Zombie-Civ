using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageArea : Building{
    public Inventory inventory;
    public StorageHealthManagement HealthManager;
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
