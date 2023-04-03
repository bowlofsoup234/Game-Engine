using System.Collections.Generic;
using UnityEngine;

public class GameObjectArray : MonoBehaviour
{
    // Declare the game object array
    private GameObject[] objects;

    void Start()
    {
        // Add and activate the tag for all child objects, including inactive ones
        foreach (Transform child in transform)
        {
            child.gameObject.tag = "ItemInInventory";
            child.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        // Find all game objects with the "ItemInInventory" tag
        GameObject[] inventoryItems = GameObject.FindGameObjectsWithTag("ItemInInventory");

        // Create the game object array with the size of the list
        objects = new GameObject[inventoryItems.Length];

        // Loop over the list and add the game objects to the array
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            objects[i] = inventoryItems[i];

            // Check if the game object was found
            if (objects[i] == null)
            {
                Debug.LogError("Could not find game object with tag ItemInInventory");
            }
        }

        // Print the names of the game objects in the array
        for (int i = 0; i < objects.Length; i++)
        {
        //    Debug.Log(objects[i].name);
        }
    }
}