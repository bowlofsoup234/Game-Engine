using UnityEngine;

public class inventoryUI : MonoBehaviour
{
    // Declare public variables for the inventory UI GameObject, and the active state of the inventory
    public GameObject InventoryUI;
    public Transform itemsParent;
    public bool InventoryActive = false;

    // Declare private variables for the inventory slots, the inventory itself, and the method to update the UI
    InventorySlot[] slots;
    Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        // Get a reference to the Inventory instance and subscribe to the OnItemChangedCallback event
        inventory = Inventory.instance;
        inventory.OnItemChangedCallback += UpdateUI;

        // Get a reference to the InventorySlot components attached to the items parent transform
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        // Set the initial state of the inventory to inactive
        InventoryActive = false;

        // Call the toggleInv() method to set the UI to the initial state
        toggleInv();
        Debug.Log(slots);
    }

    // Update is called once per frame
    void Update()
    {
        // If the Inventory button is pressed, toggle the active state of the inventory
        if(Input.GetButtonDown("Inventory"))
        {
            toggleInv();
        }
    }
        
    // Method to toggle the active state of the inventory UI
    void toggleInv()
    {
        Debug.Log(InventoryActive);

        // If the inventory is already active, set it to inactive and vice versa
        if(InventoryActive)
        {
            Debug.Log("bing");
            InventoryUI.SetActive(true);
            InventoryActive = false;
        }
        else
        {
            InventoryActive = true;
            Debug.Log("bong");
            InventoryUI.SetActive(false);
        }
    }

    // Method to update the UI with the contents of the inventory
    public void UpdateUI()
    {
        // Loop through each inventory slot and either add the corresponding item to the slot or clear it if there is no item
        for(int i = 0; i < slots.Length; i++)
        {
            if(i< inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            } 
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
