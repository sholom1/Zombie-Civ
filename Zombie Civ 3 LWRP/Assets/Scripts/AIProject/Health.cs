using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public UnityEvent OnDeath;
    public float HealthPoints;
    public float HealRate;
    public float MaxHealth;

    public void Update()
    {
        HealthPoints += HealRate * Time.deltaTime;
        if (HealthPoints > MaxHealth)
        {
            HealthPoints = MaxHealth;
        }
    }
    public bool TakeDamage(float amount)
    {
        HealthPoints -= amount;
        if (HealthPoints <= 0)
        {
            OnDeath.Invoke();
            return true;
        }
        return false;
    }
}
