using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    // distance to interact with an object
    public float radius = 3f;

    // to look at the interactable object
    public Transform interactionTransform;
    
    private bool isFocus = false;

    private bool hasInteracted;
    
    private Transform player;
    
    
    

    // Interact with the object
    public virtual void Interact()
    {
        // This method is meant to be overwritten
        // Debug.Log("Interacting with " + transform.name);
    }
    

    private void Update()
    {
        // if the focus is on and haven't already interacted
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);

            if (distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
            
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDeFocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }
    

    private void OnDrawGizmosSelected()
    {
        // if missing interactable transform, put it's own transform
        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }
        
        // visualize the radius
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
