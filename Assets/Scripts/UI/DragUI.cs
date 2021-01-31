using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Transform target = default;

    private Vector3 currentMousePos;

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Get touch position on UI
        currentMousePos = Input.mousePosition - target.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Move target UI to mouse position
        Vector3 pos = Input.mousePosition;
        target.position = pos - currentMousePos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }
}
