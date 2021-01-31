using UnityEngine;

public enum TypeSlot
{
    Weapon,
    Armor,
    Helmet,
    Shield,
    Boots
}


[CreateAssetMenu(fileName = "Items", menuName ="Item")]
public class Item : ScriptableObject
{
    public string nameItem;
    public Sprite imageItem;

    public TypeSlot slot;

    public int CON;
    public int INE;
    public int STR;
    public int DEX;

    public int lv;

    [TextArea]
    public string desription;


}