using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class mouselook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;
    public Camera InteractCam;
    public Interactable focus;
    public inventoryUI inventoryui;
   
    
    public bool InvToggle;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        InvUpdate();
       
        float MouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= MouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * MouseX);

        if(Input.GetButtonDown("Interact"))
        {
            //Debug.Log("pressed");
            Ray ray = InteractCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit; 
            if(Physics.Raycast(ray, out hit,100))
         {
           Interactable interactable = hit.collider.GetComponent<Interactable>();
           if(interactable != null )
           {
          //  Debug.Log("tried to interact");
            SetFocus(interactable);
            
           }
        
          }
        }
        
    }
     void InvUpdate()
     {
        InvToggle = !inventoryui.InventoryActive;

     if(InvToggle)
        {
        Cursor.lockState = CursorLockMode.None;
       
    
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
          
        }

     }
    void SetFocus (Interactable newFocus)
    {
        if(newFocus != focus)
        {
            if(focus != null)
            {
                focus.OnDefocused();
            }
            focus = newFocus;
        }
        
        newFocus.OnFocused(transform);

    }
    void RemoveFocus()
    {
        if(focus != null)
        {
          focus.OnDefocused();
        }
        focus = null;
    


    }
}
