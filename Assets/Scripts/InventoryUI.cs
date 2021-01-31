using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject perfabsItem = default;

    public GameObject parent;
    public List<GameObject> slots;

    public Inventory inventory;

    public Text gold;

    private void Awake()
    {
        inventory = Inventory.instance;
        slots = new List<GameObject>(inventory.space);

        Transform[] slotsArr = parent.GetComponentsInChildren<Transform>();
        for (int i = 1; i < slotsArr.Length; i++)
        {
            slots.Add(slotsArr[i].gameObject);
        }

        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].GetComponent<Slot>().index = i;
        }

        SetItem();
    }

    private void OnEnable()
    {
        SetItem();
    }

    public void SetItem()
    {
        if (inventory.items.Count > 0)
        {
            for (int i = 0; i < inventory.items.Count; i++)
            {
                if (inventory.items[i] != null && slots[i].GetComponent<Slot>().item == null)
                {
                    GameObject item = Instantiate(perfabsItem, slots[i].transform);
                    item.GetComponentInParent<Slot>().item = inventory.items[i];
                    item.GetComponent<Image>().sprite = inventory.items[i].imageItem;
                    item.GetComponent<DragHandler>().currentSlot = item.GetComponentInParent<Slot>();
                }
            }
        }

        gold.text = "GOLD: " + inventory.GetGold();
    }

    public void RemoveItem(int index)
    {
            Destroy(slots[index]);
    }
}
