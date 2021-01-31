using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BarEnemy : MonoBehaviour
{
    [SerializeField]private Slider slider = default;
    [SerializeField]private StatsEnemy stats = default;

    private void Start()
    {
        slider.maxValue = stats.maxHP;
        slider.value = stats.hp;
    }

    public void UpdateHP()
    {
        slider.maxValue = stats.maxHP;
        slider.value = stats.hp;
    }
}
