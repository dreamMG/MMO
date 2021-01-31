using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : Interactable
{
    public int goldQuantity;

    public override void Interact()
    {
        Destroy(gameObject);
        Inventory.instance.AddGold(goldQuantity);
    }
}
