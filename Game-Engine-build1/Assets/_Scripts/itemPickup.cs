
using UnityEngine;

public class itemPickup : Interactable // Inherits from Interactable script
{
// Public variables
public GameObject parentObject; // The object that contains the item to be picked up
public GameObject deleteObject; // The object to be deleted after the item has been picked up
public Inventory inventory; // The player's inventory
public gun gun; // The gun object
public Item item; // The item that will be picked up
public GameObject child; // The child object that will be placed in the inventory
public Transform Invholder; // The transform of the inventory holder
public Transform holder; // The transform of the item holder
public bool isEquipped = false; // Boolean that tracks whether the item is currently equipped or not
public bool Droping = true; // Boolean that tracks whether the item can be dropped or not
private Rigidbody rb; // Rigidbody component

  
// Called when the player interacts with the item
public override void Interact()
{
    base.Interact(); // Calls Interact function from Interactable script
    Pickup(); // Calls Pickup function
}

// Picks up the item and adds it to the player's inventory
void Pickup()
{
    bool wasPickedup = Inventory.instance.Add(item); // Adds item to the inventory and returns true if successful
    Debug.Log("Picking up item " + item.name); 
    if (wasPickedup)
    {
        Debug.Log("Sending child");
        child = parentObject;
        Debug.Log(child);
        // If there are no child objects in the inventory, make the parent object active and assign it to the inventory holder
        if (Invholder.transform.childCount == 0)
        {
            parentObject.gameObject.SetActive(true);
            Assigner(Invholder);
        }
        // If there are child objects in the inventory, make the parent object inactive
        else
        {
            parentObject.gameObject.SetActive(false);
            Debug.Log("Set inactive");
        }
        Assigner(Invholder); // Calls the Assigner function to assign the child object to the inventory holder
    }
}

// Assigns the child object to the inventory holder
public void Assigner(Transform Invholder)
{
    Debug.Log(child);
    child.transform.SetParent(Invholder);
    child.transform.SetPositionAndRotation(Invholder.position, Invholder.rotation);
}

// Toggles the Rigidbody component on or off
void ToggleRigidbody(bool on)
{
    rb = parentObject.GetComponent<Rigidbody>();
    if (rb != null)
    {
        rb.isKinematic = !on;
        rb.useGravity = on;
    }
}

// Called every frame
void Update()
{
    // Check if the item has a parent object and update isEquipped accordingly
    if (transform.parent == null)
    {
        isEquipped = false;
        ToggleRigidbody(true); // Turn on Rigidbody component
    }
    else
    {
        isEquipped = true;
        ToggleRigidbody(false); // Turn off Rigidbody component
    }
}
    
}