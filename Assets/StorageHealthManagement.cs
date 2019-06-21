using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageHealthManagement : PlayerHealth {
    public StorageArea storageArea;
    public override void ChangeHealth(float health)
    {
        base.ChangeHealth(health);
        storageArea.adjustInventory(currentHealth);
    }
}
