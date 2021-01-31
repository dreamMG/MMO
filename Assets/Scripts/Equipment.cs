using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Equipment : Slot
{
    public TypeSlot type;

    [SerializeField] private StatsUI statsUI = default;
    [SerializeField] private Stats stats = default;

    private void Start()
    {
        stats = GameObject.FindWithTag("Player").GetComponent<Stats>();

        if (item != null)
            SetIteam();
    }

    public void SetIteam()
    {
        stats.CONItem += item.CON;
        stats.INTItem += item.INE;
        stats.STRItem += item.STR;
        stats.DEXItem += item.DEX;

        Inventory.instance.Remove(item);
        stats.UpgradeStats();
        statsUI.OnEnable();
    }

    public void BackToInventory(Item item)
    {
        stats.CONItem -= item.CON;
        stats.INTItem -= item.INE;
        stats.STRItem -= item.STR;
        stats.DEXItem -= item.DEX;

        Inventory.instance.Add(item);
        stats.UpgradeStats();
        statsUI.OnEnable();
    }
}
