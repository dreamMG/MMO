using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
   public void Hide(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
    public void Show(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }
}
