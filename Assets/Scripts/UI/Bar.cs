using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    [SerializeField] private Slider sliderHp;
    [SerializeField] private Slider sliderMp;
    [SerializeField] private Slider sliderExp;
    public Stats stats;

    private void Start()
    {
        sliderHp = GameObject.Find("HpBar").GetComponent<Slider>();
        sliderHp.maxValue = stats.maxHP;
        sliderHp.value = stats.hp;

        sliderMp = GameObject.Find("MpBar").GetComponent<Slider>();
        sliderMp.maxValue = stats.maxMP;
        sliderMp.value = stats.mp;

        sliderExp = GameObject.Find("ExpBar").GetComponent<Slider>();
        sliderExp.maxValue = 100;
        sliderExp.value = stats.exp;

        UpdateExp();
    }

    public void UpdateHp()
    {
        sliderHp.maxValue = stats.maxHP;
        sliderHp.value = stats.hp;
    }
    public void UpdateMp()
    {
        sliderMp.maxValue = stats.maxMP;
        sliderMp.value = stats.mp;
    }
    public void UpdateExp()
    {
        sliderExp.maxValue = stats.expNeeded;
        sliderExp.value = stats.exp;
    }
}