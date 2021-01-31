using UnityEngine.UI;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    [Header("Up")]
    public Image heroImg;
    public Text nameText;
    public Text guildName;

    [Header("Experience")]
    public Text lv;
    public Text exp;
    public Text expNeeded;

    [Space(2)]
    public GameObject[] buttonsAddingStats;
    [Header("CON")]
    public Text currentCON;
    public Text hp;
    [Header("INT")]
    public Text currentINT;
    public Text mp;
    [Header("STR")]
    public Text currentSTR;
    public Text ap;
    [Header("DEX")]
    public Text currentDEX;
    public Text ac;

    [Header("Extra Attributes")]
    public Text moveSpeed;
    public Text attackSpeed;
    public Text coolDown;
    public Text armor;
    public Text magicResist;
    public Text critickChance;

    [SerializeField] private Stats stats;

    public void OnEnable()
    {
        stats = GameObject.FindWithTag("Player").GetComponent<Stats>();

        nameText.text = "" + stats.heroName;
        if (stats.inGuild)
        {
            guildName.text = "" + stats.guildName;
        }
        else
        {
            guildName.text = " ";
        }

        lv.text = "" + stats.lv;
        exp.text = "" + stats.exp;
        expNeeded.text = "" + stats.expNeeded;

        currentCON.text = "" + stats.CON;
        currentINT.text = "" + stats.INT;
        currentSTR.text = "" + stats.STR;
        currentDEX.text = "" + stats.DEX;

        hp.text = stats.hp + "/" + stats.maxHP;
        mp.text = stats.mp + "/" + stats.maxMP;
        ap.text = stats.minDmg + "-" + stats.maxDmg;
        ac.text = "" + stats.ac;

        moveSpeed.text = "" + stats.moveSpeed;
        attackSpeed.text = "" + stats.attackSpeed;
        coolDown.text = "" + stats.coolDown;
        armor.text = "" + stats.armor;
        magicResist.text = "" + stats.magicResist;
        critickChance.text = "" + stats.critickChance;

        if(stats.notAddedStats > 0)
        {
            for (int i = 0; i < buttonsAddingStats.Length; i++)
            {
                buttonsAddingStats[i].SetActive(true);
            }
        } 
        else
        {
            for (int i = 0; i < buttonsAddingStats.Length; i++)
            {
                buttonsAddingStats[i].SetActive(false);
            }
        }
    }

    public void AddStats(int index)
    {
        switch (index)
        {
            case (0):
                stats.CONBase++;
                break;
            case (1):
                stats.INTBase++;
                break;
            case (2):
                stats.STRBase++;
                break;
            case (3):
                stats.DEXBase++;
                break;
        }
        stats.notAddedStats--;
        stats.UpgradeStats();

        OnEnable();
    }

    public Stats GetStats()
    {
        return stats;
    }
}
