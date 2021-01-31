using Mirror;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [Header("Basic info")]
    public string heroName;
    public string guildName;
    public bool inGuild;

    [Header("Exp")]
    [GrayOut]
    public int lv;
    public int exp;
    public int expNeeded;

    [Header("Stats")]
    public int CON;
    public int INT;
    public int STR;
    public int DEX;

    [Header("Statsbase")]
    public int CONBase;
    public int INTBase;
    public int STRBase;
    public int DEXBase;

    [Header("StatsFromitem")]
    public int CONItem;
    public int INTItem;
    public int STRItem;
    public int DEXItem;

    [SerializeField]
    [Header("Status")]
    public int hp;
    public int mp;
    [GrayOut]
    public int maxHP;
    [GrayOut]
    public int maxMP;
    [GrayOut]
    public int minDmg;
    [GrayOut]
    public int maxDmg;
    [GrayOut]
    public int ac;

    [Header("Attributes")]
    public int moveSpeed;
    public int armor;
    public int attackSpeed;
    public int magicResist;
    public int coolDown;
    public int critickChance;

    [Header("ADD STATS")]
    public int notAddedStats = 0;

    [SerializeField] private Bar bar;

    private void Start()
    {
        UpgradeStats();
    }

    public void UpgradeStats()
    {
        CON = CONBase + CONItem;
        INT = INTBase + INTItem;
        STR = STRBase + STRItem;
        DEX = DEXBase + DEXItem;

        //HP FORM CON
        maxHP = CON * 20;
        //MANA
        maxMP = INT * 10;
        //DMG
        minDmg = (int)(STR * 0.8f);
        maxDmg = (int)(STR * 1.4f);
        //AC
        ac = (int)(DEX * 1.6f);

        if (mp > maxMP)
        {
            mp = maxMP;
        }
        if(hp > maxHP)
        {
            hp = maxHP;
        }

        expNeeded = (int) Mathf.Pow(lv, 3) * 1000;
    }

    public void SaveStats()
    {
        DBManager.InitDBManager(heroName, lv, exp, CONBase, INTBase, STRBase, DEXBase);
        MyNetworkManager.instance.CallSaveData();
    }
}
