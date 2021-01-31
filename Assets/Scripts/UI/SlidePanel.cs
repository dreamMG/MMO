using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class SlidePanel : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Transform target = default;

    private Vector2 startPos;
    private Vector3 pos;
    [SerializeField] private Vector3 positionOnScreen = default;

    [SerializeField]private RectTransform rectTransform = default;

    public void OnDrag(PointerEventData eventData)
    {
        //Drag object form -25.0f to 25.0f
        pos = new Vector3(Input.mousePosition.x - startPos.x, target.position.y, 0)
        {
            x = Mathf.Clamp(Input.mousePosition.x - startPos.x, Screen.width - 102.5f, Screen.width + 77.5f)
        };
        target.position = pos;

        positionOnScreen = new Vector3(Screen.width - pos.x, Screen.height - pos.y);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Get position of touch
        startPos = Input.mousePosition - target.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Active the options panel
        if (positionOnScreen.x >= -12f)
        {
            rectTransform.anchoredPosition = new Vector2(-75, rectTransform.anchoredPosition.y);
        }
        else if (positionOnScreen.x < -12f)
        {
            rectTransform.anchoredPosition = new Vector2(55, rectTransform.anchoredPosition.y);
        }
    }
}
