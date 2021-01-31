using UnityEngine;

public class PickUp : Interactable
{
    public Item item;

    public override void Interact()
    {
        Destroy(gameObject);
        Inventory.instance.Add(item);
    }
}
