using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Dash : MonoBehaviour
{
    public static Dash instance;

    public GameObject dashRange;

    private Vector3 start;
    static Vector3 delta;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject())
        {
            start = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            delta = start - Input.mousePosition;
        }
    }

    public void DoDash()
    {
        //Debug.Log(delta);
        dashRange.SetActive(false);
    }

    public void ShowRange()
    {
        dashRange.SetActive(true);
    }
}
