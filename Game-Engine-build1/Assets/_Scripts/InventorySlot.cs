using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Add this line

public class InventorySlot : MonoBehaviour, IDropHandler // Add IDropHandler
{
    public Image icon;
    public Button removeButton;
    Item item;

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;

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
        Inventory.instance.Remove(item);
        Debug.Log("oof");
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }

    // Add this method
public void OnDrop(PointerEventData eventData)
{
    Debug.Log("triggered");
    
}
}
