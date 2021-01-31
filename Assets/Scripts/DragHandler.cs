using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Profiling;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerDownHandler, IPointerUpHandler
{
    public static Item itemDragged;

    public Transform startParent;
    public Slot currentSlot;

    private Canvas canvas;
    private GraphicRaycaster graphicRaycaster;

    public RectTransform invPanel;

    [SerializeField]private Equipment startEq = default;

    private void Start()
    {
        invPanel = FindObjectOfType<InventoryUI>().transform as RectTransform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.parent.TryGetComponent<Equipment>(out startEq);
        if (!canvas)
        {
            canvas = GetComponentInParent<Canvas>();
            graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
        }
        // Change parent of our item to the canvas.
        transform.SetParent(canvas.transform, true);
        // And set it as last child to be rendered on top of UI.
        transform.SetAsLastSibling();

        itemDragged = currentSlot.GetComponent<Slot>().item;
        currentSlot.toogleTip.gameObject.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // On end we need to test if we can drop item into new slot.
        var results = new List<RaycastResult>();
        graphicRaycaster.Raycast(eventData, results);
        // Check all hits.
        foreach (var hit in results)
        {
            // If we found slot.
            var slot = hit.gameObject.GetComponent<Slot>();
            var eq = hit.gameObject.GetComponent<Equipment>();

            if (eq)
            {
                if (eq.item == null && eq.transform.childCount <= 1 && eq.type == itemDragged.slot)
                {
                    currentSlot.item = null;
                    currentSlot = slot;
                    currentSlot.item = itemDragged;

                    eq.SetIteam();
                }
                break;
            }

            if (slot)
            {
                // We should check if we can place ourselves​ there.
                if (slot.item == null && slot.transform.childCount <= 1)
                {
                    // Swapping references.
                    currentSlot.item = null;
                    currentSlot = slot;
                    currentSlot.item = itemDragged;
                    if (startEq != null)
                    {
                        startEq.BackToInventory(itemDragged);
                    }
                }
                    // In either cases we should break check loop.
                    break;
            }
        }
        // Changing parent back to slot.
        transform.SetParent(currentSlot.transform);
        // And centering item position.
        transform.localPosition = Vector3.zero;
        startEq = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition))
        {
            Remove();
        }
    }

    public void Remove()
    {
        Inventory.instance.Remove(itemDragged);
        Destroy(gameObject);
        currentSlot.item = null;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        currentSlot.toogleTip.gameObject.SetActive(true);
        currentSlot.toogleTip.transform.position = Input.mousePosition + new Vector3(150*2,0);
        currentSlot.toogleTip.GetComponent<ToogleTip>().UpdateToolTip(currentSlot.item);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        currentSlot.toogleTip.gameObject.SetActive(false);
    }
}
