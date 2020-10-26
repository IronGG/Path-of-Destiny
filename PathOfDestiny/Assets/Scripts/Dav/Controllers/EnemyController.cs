using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions.Must;

public class EnemyController : MonoBehaviour
{

    // zone of detection 
    public float lookRadius = 10f;

    // References
    private Transform target; // target to follow : Player
    private NavMeshAgent agent; // Agent of the ennemy
    private CharacterCombat combat;

    // Start is called before the first frame update
    void Start()
    {

        target = PlayerManager.instance.player.transform;
        
        // make the references
        agent = GetComponent<NavMeshAgent>();

        combat = GetComponent<CharacterCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        // calculate the distance between the player and the ennemy
        float distance = Vector3.Distance(target.position, transform.position);

        // Detection : if the distance between the player and the ennemy is smaller than the detection zone
        if (distance <= lookRadius)
        {
            // ennemy's destination = player position
            agent.SetDestination(target.position);

            // if it's on interaction zone
            if (distance <= agent.stoppingDistance)
            {
                // Get the target's stats (player)
                CharacterStats targetStats = target.GetComponent<CharacterStats>();

                if (targetStats != null)
                {
                    // Attack the target
                    combat.Attack(targetStats);
                }
                
                // Face the target
                FaceTarget();
            }
        }

    }

    void FaceTarget()
    {
        // value : direction to the target
        Vector3 direction = (target.position - transform.position).normalized;

        // rotation direction to : player
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        
        // rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

    }

    // Draw a gizmo of the selected object
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
