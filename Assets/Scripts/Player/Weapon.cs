using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Weapon : NetworkBehaviour
{
    [SerializeField] private Stats stats;

    public float noramlized = 0.4f;

    [SerializeField] private GameObject effectHit = default;
    [SerializeField] private GameObject floatingText = default;
    [SerializeField] private GameObject lvlUp = default;

    private void Start()
    {
        stats = GameObject.FindWithTag("Player").GetComponent<Stats>();
    }

    private void OnTriggerEnter(Collider other)
    {   
        if(other.tag == "Enemy")
        {
            EnemyController enemy = other.gameObject.GetComponentInParent<EnemyController>();
            if (GetComponent<PlayerController>().isAttacking) {
                int dmg = Random.Range(stats.minDmg, stats.maxDmg) + 1;

                CmdMakeDmg(enemy.gameObject, dmg);

                SpawnHitText(other.transform.position, floatingText, dmg);
                SpawnDecal(other.transform.position, effectHit);
            }

            if (enemy.stats.hp <= 0)
            {
                enemy.onDeath.Invoke();
                Destroy(enemy.gameObject);
                Exp(other);
            }
        }
    }
    public int health;

    [Command]
    void CmdMakeDmg(GameObject target, int damage)
    {
        target.GetComponent<EnemyController>().stats.hp -= damage;
        target.GetComponent<EnemyController>().SetSyncHp(target.GetComponent<EnemyController>().GetSyncHp() - damage);

        target.GetComponentInChildren<BarEnemy>().UpdateHP();

        NetworkIdentity opponentIdentity = target.GetComponent<NetworkIdentity>();
        TargetDoDmg(opponentIdentity.connectionToClient, damage);
    }

    [TargetRpc]
    public void TargetDoDmg(NetworkConnection target, int damage)
    {
        //Debug.Log($"Damage = {damage}");
    }

    void Exp(Collider other)
    {
        stats.exp += other.GetComponent<StatsEnemy>().getingExp;

        if(stats.exp >= stats.expNeeded)
        {
            stats.lv++;
            stats.notAddedStats++;
            SpawnDecal(transform.position, lvlUp);

            stats.UpgradeStats();
            stats.exp = 0;
        }
        GetComponent<Bar>().UpdateExp();
        stats.SaveStats();
    }

    void SpawnDecal(Vector3 point, GameObject prefab)
    {
        GameObject spawnedDecal = GameObject.Instantiate(prefab, point, Quaternion.LookRotation(point.normalized));
        Destroy(spawnedDecal, 1);
    }
    void SpawnHitText(Vector3 point, GameObject prefab, int dmg)
    {
        GameObject spawnedDecal = GameObject.Instantiate(prefab, point, Quaternion.LookRotation(point.normalized));
        spawnedDecal.GetComponentInChildren<Text>().text = "-" + dmg;
        Destroy(spawnedDecal, 1);
    }
}
