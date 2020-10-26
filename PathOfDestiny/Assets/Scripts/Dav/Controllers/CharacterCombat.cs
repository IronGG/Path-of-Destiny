using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    public float attackSpeed = 1f;
    private float attackCooldown = 0f;

    public float attackDelay = 0.6f;

    public event System.Action OnAttack;
    
    private CharacterStats myStats;

    private void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    private void Update()
    {
        // initiate the cooldown
        attackCooldown -= Time.deltaTime;
    }

    public void Attack(CharacterStats targetStats)
    {
        // verifies the cooldown
        if (attackCooldown <= 0)
        {
            // target takes damages
            StartCoroutine(DoDamage(targetStats,attackDelay));

            if (OnAttack != null)
            {
                OnAttack();
            }
            
            // reset the cooldown
            attackCooldown = 1 / attackSpeed;
        }
    }

    IEnumerator DoDamage(CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);
        
        stats.TakeDamage(myStats.damage.GetValue());
    }
}
