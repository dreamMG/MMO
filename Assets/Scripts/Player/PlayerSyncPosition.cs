using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSyncPosition : NetworkBehaviour
{
    [SyncVar]
    private Vector3 syncPos;

    [SyncVar]
    private Quaternion syncRot;

    [SyncVar]
    bool isAttacking;

    [SerializeField] 
    Transform myTransform;

    [SerializeField]
    Animator animator;

    [SerializeField]
    PlayerController player;

    private float timeStartLerping = 2f;
    private float timeToLerp = 350f;
    private Vector3 pos;

    private void Start()
    {
        myTransform = transform;
        animator = GetComponent<Animator>();
        player = GetComponent<PlayerController>();
    }

    void FixedUpdate()
    {
        TransmitPosition();
        LerpPosition();
        Animations();
    }

    void LerpPosition()
    {
        if (!isLocalPlayer)
        {
            float lerpPercentage = (Time.time - timeStartLerping) / timeToLerp;

            myTransform.position = Vector3.SmoothDamp(myTransform.position, syncPos, ref pos, lerpPercentage);
            myTransform.rotation = syncRot;
        }
    }

    void Animations()
    {
        if (Vector3.Distance(syncPos, myTransform.position) > 0.01f)
        {
            animator.SetInteger("StateMove", 1);
        }
        else
        {
            animator.SetInteger("StateMove", 0);
        }
        animator.SetBool("Attack", isAttacking);
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
            CmdSendNameToServer(player.GetOnlineName());
            CmdProvideAnimationToServer(player.isAttacking);
        }
    }

    [Command]
    void CmdProvideAnimationToServer(bool attacking)
    {
        animator.SetBool("Attack", attacking);
        isAttacking = animator.GetBool("Attack");
    }

    [Command]
    void CmdSendNameToServer(string nameToSend)
    {
        RpcSetPlayerName(nameToSend);
    }

    [ClientRpc]
    void RpcSetPlayerName(string name)
    {
        GetComponentInChildren<Text>().text = name;
    }
}
