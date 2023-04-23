using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BackpackSlot : MonoBehaviour, IDropHandler
{
    public Image icon;
    public Button removeButton;
    public string itemName;
    public GameObject parentObject;
    Item item;
   

    public void AddItem(Item newItem)
{
    item = newItem;
    icon.sprite = item.icon;
    icon.enabled = true;
  
    if (newItem.name == "placeholder")
    {
        removeButton.interactable = false; // Do not enable the remove button if the item name is "placeholder"
    }
    else
    {
        removeButton.interactable = true;
    }

    itemName = newItem.name;
    

    DragAndDrop dragAndDrop = GetComponent<DragAndDrop>();
    if (dragAndDrop != null)
    {
        dragAndDrop.RepresentedItem = newItem;
        dragAndDrop.CurrentSlot = this;
        dragAndDrop.GetComponent<Image>().sprite = newItem.icon;
        dragAndDrop.GetComponent<Image>().enabled = true;

        dragAndDrop.SetBackpackSlot(this);
    }
}


    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        Backpack.instance.Remove(item);
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }


public void OnDrop(PointerEventData eventData)
{
    Debug.Log("Item dropped from " + eventData.pointerDrag.GetComponent<DragAndDrop>().CurrentSlot + " to " + this);

    DragAndDrop dragAndDrop = eventData.pointerDrag.GetComponent<DragAndDrop>();
    Inventory inventory = Inventory.instance;
    Backpack backpack = Backpack.instance;
    if (dragAndDrop != null && backpack != null) 
    {
        if(eventData.pointerDrag.GetComponent<DragAndDrop>().CurrentSlot = null )
        { 
            int currentIndex = dragAndDrop.currentSlot.transform.GetSiblingIndex();
            int targetIndex = transform.GetSiblingIndex();
            Debug.Log("dropped from BP slot");
            inventory.SwapItemsBetweenLists(backpack.items, inventory.items, currentIndex, targetIndex);
            inventory.OnItemChangedCallback.Invoke();
            backpack.OnItemChangedCallback.Invoke();
            

        }
        else 

        {
            int currentIndex = dragAndDrop.CurrentSlot.transform.GetSiblingIndex();
            int targetIndex = transform.GetSiblingIndex(); 
            Debug.Log("dropped from INV slot");
            backpack.SwapItemsInBackpack(currentIndex, targetIndex);
        }
    }
}
}
