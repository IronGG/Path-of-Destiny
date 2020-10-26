using System;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    public Stat damage;
    public Stat armor;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        // reduce the damage with the armor
        damage -= armor.GetValue();
        // damage can't go below 0
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        
        // health minus damages taken
        currentHealth -= damage;

        Debug.Log(this.name + " has taken " + damage);
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        // Die in some way
        // This method is meant to be overwritten
    }
}
