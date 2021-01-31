using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToogleTip : MonoBehaviour
{
    public Text nameText;

    public Text CON;
    public Text INE;
    public Text STR;
    public Text DEX;

    public Text desription;

    public void UpdateToolTip(Item item)
    {
        nameText.text = item.nameItem;

        CON.text = "CON: " + item.CON;
        INE.text = "INT: " + item.INE;
        STR.text = "STR: " + item.STR;
        DEX.text = "DEX: " + item.DEX;

        desription.text = "" + item.desription;
    }
}
