using Mirror;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : NetworkBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform target;
    
    public Text nameText;
    public StatsEnemy stats;

    [SyncVar]
    private Vector3 syncPos;
    [SyncVar]
    private Quaternion syncRot;
    [SyncVar]
    private Transform syncTarget;
    [SyncVar]
    private int syncHp;

    [SerializeField]
    Transform myTransform;

    public delegate void OnHit();
    public OnHit onHit;

    public delegate void OnDeath();
    public OnDeath onDeath;

    public bool isFollowing = false;

    private void Start()
    {
        myTransform = transform;
        syncPos = transform.position;
        stats = GetComponentInChildren<StatsEnemy>();
        stats.UpgradeStats();

        agent = GetComponent<NavMeshAgent>();
        syncTarget = null;
        nameText.text = stats.heroName;

        syncHp = stats.hp;
    }
    void FixedUpdate()
    {
        Position(); 
        TransmitPosition();

        stats.hp = syncHp;
        target = syncTarget;
        GetComponentInChildren<BarEnemy>().UpdateHP();

        if (isFollowing)
        {
            FollowPlayer();
        }
    }

    public void FollowPlayer()
    {
        if (target != null)
        {
            if (Vector3.Distance(transform.position, target.position) > 2.5f)
            {
                agent.isStopped = false;
                agent.SetDestination(target.position);
            }
            else
            {
                agent.isStopped = true;
            }
        }
    }

    void Position()
    {
        if (!isLocalPlayer)
        {
            agent.destination = syncPos;
            myTransform.rotation = syncRot;
        }
    }

    [Command]
    void CmdProvidePositionToServer(Vector3 pos, Quaternion rot)
    {
        syncPos = pos;
        syncRot = rot;
    }

    [ClientCallback]
    void TransmitPosition()
    {
        if (isLocalPlayer)
        {
            CmdProvidePositionToServer(myTransform.position, myTransform.rotation);
        }
    }

    public void SetSyncHp(int syncHp)
    {
        this.syncHp = syncHp;
    }

    public int GetSyncHp()
    {
        return syncHp;
    }

    public void SetSyncTarget(Transform syncTarget)
    {
        this.syncTarget = syncTarget;
    }

    public Transform GetSyncTarget()
    {
        return syncTarget;
    }
    public Transform GetTarget()
    {
        return target;
    }
}
