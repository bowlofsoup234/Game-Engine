using UnityEngine;

public class BackpackUI : MonoBehaviour
{
    // Declare public variables for the inventory UI GameObject, and the active state of the inventory
   
    public GameObject backpackUI;
    public GameObject backpackholder;
    public Transform itemsParent;
    public bool InventoryActive = false;

    // Declare private variables for the inventory slots, the inventory itself, and the method to update the UI
    BackpackSlot[] slots;
    Inventory inventory;

    Backpack backpack;

    // Start is called before the first frame update
    void Start()
    {
        // Get a reference to the Inventory instance and subscribe to the OnItemChangedCallback event
        backpack = Backpack.instance;
        backpack.OnItemChangedCallback += UpdateBPUI;

        // Get a reference to the InventorySlot components attached to the items parent transform
        slots = itemsParent.GetComponentsInChildren<BackpackSlot>();

        // Set the initial state of the inventory to inactive
        InventoryActive = false;

        // Call the toggleInv() method to set the UI to the initial state
        toggleInv();
        Debug.Log(slots + "peepeepoopoo");
    }

    void Update(){
            
     if(Input.GetButtonDown("Inventory"))
    {
        toggleInv();
            
        if(InventoryActive == false){
            backpackUI.SetActive(false);
            InventoryActive = true;  
        }    
          
    }



    }
    void toggleInv()
    {

        if(InventoryActive)
        {
           // Debug.Log("bing");
            backpackUI.SetActive(true);
            InventoryActive = false;
         
           
        }
        else
        {
            InventoryActive = true;
           // Debug.Log("bong");
            backpackUI.SetActive(false);
        
        }
    }

    // Method to update the UI with the contents of the inventory
    public void UpdateBPUI()
    {
        // Loop through each inventory slot and either add the corresponding item to the slot or clear it if there is no item
        for(int i = 0; i < slots.Length; i++)
        {
            if(i< backpack.items.Count)
            {
                slots[i].AddItem(backpack.items[i]);
            } 
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}