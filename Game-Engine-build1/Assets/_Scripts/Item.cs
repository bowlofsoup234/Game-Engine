
using UnityEngine;

[CreateAssetMenu(fileName ="new Item",menuName ="Inventory/item")]
public class Item : ScriptableObject


{

  
   new public string name = "New Item";
   public Sprite icon = null;


   public virtual void Use()
   {
      Debug.Log("Using "+ name);
   }
}
