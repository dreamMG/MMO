using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    [SerializeField] private GameObject perfabsItem = default;
    [SerializeField] private Item[] item = default;

    [SerializeField] private GameObject perfabsGold = default;
    [SerializeField] private int gold = default;

    private void Start()
    {
        GetComponent<EnemyController>().onDeath += OnDeath;
    }

    private void OnDeath()
    {
            perfabsItem = Instantiate(perfabsItem, transform.position, Quaternion.identity);
            perfabsItem.GetComponent<PickUp>().item = item[Random.Range(0, item.Length)];

            perfabsGold = Instantiate(perfabsGold, transform.position, Quaternion.identity);
            perfabsGold.GetComponent<Gold>().goldQuantity = gold;
    }   
}
