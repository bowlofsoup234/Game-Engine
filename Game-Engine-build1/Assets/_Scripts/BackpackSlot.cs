using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class BackpackSlot : MonoBehaviour
{

   public Image icon;
   public Button removeButton;
   Item item;
   public void AddItem(Item newItem)
   {
    item = newItem;
    icon.sprite = item.icon;
    icon.enabled =true;
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
      Backpack.instance.Remove(item);
      
     
   }
   public void UseItem()
   {
      if(item != null)
      {
         Debug.Log("231123");
         item.Use();
      }
   }
}