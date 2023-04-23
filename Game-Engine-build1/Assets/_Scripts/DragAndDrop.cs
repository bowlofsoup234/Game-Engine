using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Item RepresentedItem { get; set; }
    public BackpackSlot CurrentSlot { get; set; }

    public InventorySlot currentSlot {get; set;}

    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;
    private Vector2 originalPosition;

    private BackpackSlot backpackSlot;
    private InventorySlot inventorySlot;
    private CanvasGroup canvasGroup;

    public void SetBackpackSlot(BackpackSlot slot)
    {
        backpackSlot = slot;
    }

    public void SetInventorySlot(InventorySlot slot)
    {
        inventorySlot = slot;
    }
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        originalPosition = rectTransform.anchoredPosition;

        if (CurrentSlot == null)
        {
            CurrentSlot = GetComponentInParent<BackpackSlot>();
        }

        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");

        GameObject hitObject = eventData.pointerCurrentRaycast.gameObject;
        BackpackSlot targetSlot = null;

        if (hitObject != null)
        {
            targetSlot = hitObject.GetComponent<BackpackSlot>() ?? hitObject.GetComponentInParent<BackpackSlot>();
        }

        if (targetSlot != null)
        {
            Debug.Log("Target slot found");

            if (targetSlot != CurrentSlot)
            {
                Debug.Log("Swapping items");
            
                snapper();
            }
        }
        else
        {
           snapper();
        }

        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }


    public void snapper()
    {
    rectTransform.anchoredPosition = originalPosition;
    }
}