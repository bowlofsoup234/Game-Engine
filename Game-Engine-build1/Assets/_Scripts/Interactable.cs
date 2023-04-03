using UnityEngine;

public class Interactable : MonoBehaviour
{
    // Declare public variables for the interaction radius and focus status
    public float radius = 3f;
    public bool isFocus = false;
    Transform player;

    // Method for interacting with the object (to be overridden by child classes)
    public virtual void Interact()
    {
        // Log a message indicating the object has been interacted with
        Debug.Log("interacting with " + transform.name);
    }

    // Update is called once per frame
    void update()
    {
        // If the object is in focus, log a message (this may not work due to incorrect capitalization)
        if (isFocus)
        {
            Debug.Log("focused triggered");
        }
    }

    // Method to set the object as in focus when the player is within range
    public void OnFocused (Transform playerTransform )
    {
        isFocus = true;
        player = playerTransform;
        Debug.Log("item spotted");

        // Check if the player is within range of the object, and if so, call the Interact() method
        float distance = Vector3.Distance(player.position, transform.position);
        if(distance <= radius)
        {
            Interact();
        }
    }

    // Method to set the object as out of focus when the player moves out of range
    public void OnDefocused()
    {
        isFocus = false;
        player = null;
    }

    // Draw a yellow sphere around the object to represent the interaction radius in the Unity editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,radius);
    }
}
