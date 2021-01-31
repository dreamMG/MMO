using Mirror;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : NetworkBehaviour
{
    public GameObject perfab;

    public float width, lenght;
    public int quantity;

    public bool canRespawn = true;
    public float timeToRespawn = 2;

    private float timer;

    public List<GameObject> spawned = new List<GameObject>();

    [Client]
    private void Update()
    {
        if (!canRespawn) return;

        timer += Time.deltaTime;

        if (quantity > spawned.Count && timer > timeToRespawn)
        {
            CmdSpawn(transform.position + new Vector3(Random.Range(-width / 2, width / 2), 1, Random.Range(-lenght / 2, lenght / 2)));
            timer = 0;
        }

        if (timer > timeToRespawn)
        {
            DeleteOnList();
            timer = 0;
        }

       
    }

    [ServerCallback]
    void CmdSpawn(Vector3 location)
    {
        var obj = Instantiate(perfab, location, Quaternion.identity);
        //obj.transform.SetParent(transform);
        spawned.Add(obj);
        NetworkServer.Spawn(obj);
    }

    void DeleteOnList()
    {
        for (int i = 0; i < spawned.Count; i++)
        {
            if(spawned[i] == null)
            {
                spawned.RemoveAt(i);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, 1, lenght));
    }
}
