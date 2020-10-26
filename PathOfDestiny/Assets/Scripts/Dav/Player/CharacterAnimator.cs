using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class CharacterAnimator : MonoBehaviour
{
    private const float locomotionAnimationSmoothTime = .1f;
    
    private NavMeshAgent agent;

    private Animator animator;
    
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        animator = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        // get the speed percentage by dividing the speed by the maximal speed
        float speedPercent = agent.velocity.magnitude / agent.speed;
        // set if the player is standing, walking or running. Depends on the speed percent
        animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);
        
    }
}
