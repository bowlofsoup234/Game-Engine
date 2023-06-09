using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    // Declare a static reference to the Inventory instance
    public static Inventory instance;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // If there is already an instance of the Inventory, destroy this instance
        if(instance != null)
        {
            Debug.LogWarning("more than one instance of inv");
            return;
        }

        // Set the static reference to this instance
        instance = this;
    }
    #endregion

    // Declare a delegate to trigger an event when an item is added or removed from the inventory
    public delegate void OnItemChange();
    public OnItemChange OnItemChangedCallback;

    // Define public variables for the maximum number of items that can be stored, the parent object for inventory items, and
    // references to an item and its associated game object that need to be removed from the inventory
    public int space = 20;
    public GameObject parentObject;
    public string itemToRemove;
    public GameObject gameObjectToRemove;
    public Item tmp;
    public Item placeholder;

    // Declare a list to store the items in the inventory
    public List<Item> items = new List<Item>();

    // Method to add an item to the inventory
    public void AddPlaceholders()
    {
    for(int i = 0; i < space; i++)
    {
        items.Add(placeholder);
    }
}

    public bool Add (Item item)
    {
        // If the inventory is already full, log a message and return false
        if(items.Count >= space)
        {
            Debug.Log("not enough room");
            return false;
        }

        // Add the item to the inventory list
        items.Add(item);
        Debug.Log(item + "meow peepee");

        // If there is a method registered for the OnItemChangedCallback event, invoke it
        if(OnItemChangedCallback != null)
            OnItemChangedCallback.Invoke();

        // Return true to indicate that the item was successfully added to the inventory
        return true;
    }

    // Method to remove an item from the inventory
    public void Remove(Item item)
    {
        // Get the name of the item as a string
        itemToRemove = item.ToString();

        // Use a helper method to find the game object reference for the item
        gameObjectToRemove = FindObjectByName(itemToRemove);

        // If the game object reference is not null, deactivate it, remove the item from the inventory list, trigger the
        // OnItemChangedCallback event, remove the parent object association, and destroy the game object
        if (gameObjectToRemove != null)
        {
            gameObjectToRemove.SetActive(true);
            items.Remove(item);

            if (OnItemChangedCallback != null)
            {
                OnItemChangedCallback.Invoke();
            }

            gameObjectToRemove.transform.parent = null;
            Destroy(gameObjectToRemove);
        }
        // If the game object reference is null, log a warning that the item was not found in the inventory
        else
        {
            Debug.LogWarning("No GameObject found with the name: " + itemToRemove);
        }
    }

    // Helper method to find the game object reference for an item by its name
    private GameObject FindObjectByName(string itemToRemove)
    {
        // Get an array of all active and inactive game objects in the scene
        GameObject[] objects = Object.FindObjectsOfType<GameObject>(true);

        // Use LINQ to find the game object that matches the specified name
        GameObject foundObject = objects.FirstOrDefault(obj => obj.name == itemToRemove);

        // Return the found game object reference, or null if no match was found
        return foundObject;
    }
    public void SwapItemsInInventory(int indexA, int indexB)
{
    Debug.Log("Attempting to swap items at indexA: " + indexA + ", indexB: " + indexB);

    if (indexA < 0 || indexA >= items.Count || indexB < 0 || indexB >= items.Count)
    {
        Debug.LogWarning("Index out of range: Unable to swap items in backpack. indexA: " + indexA + ", indexB: " + indexB);
        return;
    }



    tmp = items[indexA];
    items[indexA] = items[indexB];
    items[indexB] = tmp;



    if (OnItemChangedCallback != null)
        OnItemChangedCallback.Invoke();

    Debug.Log("Successfully swapped items at indexA: " + indexA + ", indexB: " + indexB);


}
public void SwapItemsBetweenLists(List<Item> sourceList, List<Item> destinationList, int sourceIndex, int destinationIndex)
{
    Debug.Log("Attempting to swap items from sourceIndex: " + sourceIndex + " to destinationIndex: " + destinationIndex);

    if (sourceIndex < 0 || sourceIndex >= sourceList.Count || destinationIndex < 0 || destinationIndex >= destinationList.Count)
    {
        Debug.LogWarning("Index out of range: Unable to swap items between lists. sourceIndex: " + sourceIndex + ", destinationIndex: " + destinationIndex);
        return;
    }

    Item item = sourceList[sourceIndex];
    sourceList.RemoveAt(sourceIndex);
    destinationList.Insert(destinationIndex, item);

    Debug.Log("Successfully swapped items from sourceIndex: " + sourceIndex + " to destinationIndex: " + destinationIndex);

}
void Start()
{
   AddPlaceholders();
}
}

