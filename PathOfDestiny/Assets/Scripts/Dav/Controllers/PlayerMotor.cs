using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// to be sure to have a component
[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    private Transform target; // Target to follow
    
    private NavMeshAgent agent; // reference to our agent
    
    
    // Start is called before the first frame update
    void Start()
    {
        // reference to the NavMeshAgent
        agent = GetComponent<NavMeshAgent>();
        
    }

    private void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
            FaceTarget();
        }
    }

    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }

    public void FollowTarget(Interactable newTarget)
    {
        agent.stoppingDistance = newTarget.radius * .8f;
        agent.updateRotation = false;

        target = newTarget.interactionTransform;
    }

    public void StopFollowTarget()
    {
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;
        
        target = null;
    }

    void FaceTarget()
    {
        // set a direction to the target
        Vector3 direction = (target.position - transform.position).normalized;
        
        // rotate to the direction and avoid to move the y axis
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));

        // make the rotation smoothly
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    
}
