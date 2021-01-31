using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Is more than one inventory!");
            return;
        }

        instance = this;
    }
    #endregion

    public List<Item> items = new List<Item>();

    public int space = 20;

    [SerializeField] private int gold;

    public bool Add(Item item)
    {
        if (items.Count >= space)
        {
            Debug.Log("Not enough room.");
            return false;
        }
        items.Add(item);
        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);
    }

    public void AddGold(int goldQuantity)
    {
        gold += goldQuantity;
    }

    public int GetGold()
    {
        return gold;
    }
}
