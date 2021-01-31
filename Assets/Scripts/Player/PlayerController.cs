using Mirror;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private VariableJoystick joystick;

    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float rotationSpeed = 125.0f;
    [SerializeField] private CharacterController thisPlayer = default;
    [SerializeField] private float rotation = default;

    [SerializeField] Text onlineNameText = default;
    [SerializeField] private string onlineName = default;

    [SerializeField] private Stats stats = default;

    [SerializeField] private Animator animator;
    public bool isAttacking = false;
    public int stateMove = -1;

    [SerializeField] private Button buttonAttack = default;
    [SerializeField] private Button buttonDash = default;
    [SerializeField] private bool isPressed = default;

    public Camera cameraPlayer;

    public delegate void Hit();
    public Hit hit;

    public MapManager mapManager;

    [SyncVar]
    private int syncIndex;

    public int indexMap;

    private void Start()
    {
        mapManager = FindObjectOfType<MapManager>();
        mapManager.SetPlayer(this.gameObject, indexMap);

        transform.SetParent(mapManager.maps[indexMap]);

        name = "Player" + UnityEngine.Random.value;
        buttonAttack = GameObject.FindWithTag("AttackButton").GetComponent<Button>();
        buttonDash = GameObject.FindWithTag("DashButton").GetComponent<Button>();

        thisPlayer = gameObject.GetComponent<CharacterController>();
        if (isLocalPlayer)
        {
            buttonAttack.onClick.AddListener(AttackControl);            

            FindObjectOfType<CameraController>().target = this.transform;
            cameraPlayer = Camera.main;

            onlineName = FindObjectOfType<Login>().GetOnlineName();
            onlineNameText.text = onlineName;
            joystick = FindObjectOfType<VariableJoystick>();
        }
        animator = GetComponent<Animator>();

        if (DBManager.name != null)
        {
            stats.heroName = DBManager.name;
            stats.lv = DBManager.lvl;
            stats.exp = DBManager.exp;
            stats.CONBase = DBManager.CON;
            stats.INTBase = DBManager.INE;
            stats.STRBase = DBManager.STR;
            stats.DEXBase = DBManager.DEX;
        }

        stats.UpgradeStats();
    }

    void Update()
    {
        if (!isLocalPlayer) return;
        if(joystick == null)
        {
            joystick = FindObjectOfType<VariableJoystick>();
        }
        if (!isAttacking)
            PlayerControl();

        AttackFinish();

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cameraPlayer.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000.0f))
            {
                if (hit.collider.gameObject.CompareTag("item"))
                {
                    hit.collider.gameObject.GetComponent<PickUp>().Interact();
                }
                if (hit.collider.gameObject.CompareTag("gold"))
                {
                    hit.collider.gameObject.GetComponent<Gold>().Interact();
                }
                if (hit.collider.gameObject.CompareTag("teleport"))
                {
                    Debug.Log("<color= green> TELEPORTED </color>");
                }
            }
        }
        CmdSetParent(indexMap);
    }

    [Command]
    public void CmdSetParent(int index)
    {
        syncIndex = index;
        RpcSetParent(syncIndex);
    }

    [ClientRpc]
    private void RpcSetParent(int index)
    {
        transform.SetParent(mapManager.maps[index]);
    }

    private void PlayerControl()
    {
        float v = joystick.Vertical * speed * Time.deltaTime;
        float h = joystick.Horizontal * rotationSpeed * Time.deltaTime;

        Vector3 pos = transform.up * -9.81f * Time.deltaTime + transform.forward * v;

        rotation += h;
        transform.rotation = Quaternion.Euler(transform.rotation.x, rotation, transform.rotation.z);

        thisPlayer.Move(pos);

        Animations(v);
    }

    private void AttackControl()
    {
        if (!isAttacking && !isPressed)
        {
            isAttacking = true;
            animator.SetBool("Attack", true);
            isPressed = true;
        }
    }

    private void AttackFinish()
    {
        if (animator.GetCurrentAnimatorStateInfo(1).IsName("NormalAttack02_SwordShield") && animator.GetCurrentAnimatorStateInfo(1).normalizedTime >= 0.525f)
        {
            animator.SetBool("Attack", false);
            isAttacking = false;
            isPressed = false;
        } else if(isAttacking && isPressed)
        {
            thisPlayer.Move(transform.forward * speed * Time.deltaTime * 0.25f);
        }
    }

    private void Animations(float move)
    {
        if(move != 0)
        {
            stateMove = 1;
        } else
        {
            stateMove = 0;
        }

        animator.SetInteger("StateMove", stateMove);
    }

    #region GETERS&&SETTERS
    public void SetLvl(int v)
    {
        stats.lv = v;
    }

    public Animator GetAnimator()
    {
        return animator;
    }

    public string GetOnlineName()
    {
        return onlineName;
    }
    public void SetOnlineName(string name)
    {
        onlineName = name;
    }
    #endregion

    #region EnemyFollowPlayer
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Mob"))
        {
            if (other.GetComponent<EnemyController>().GetTarget() == null)
            {
                other.GetComponent<EnemyController>().isFollowing = true;
                if(isLocalPlayer)
                    CmdTarget(other.gameObject, transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Mob") && other.GetComponent<EnemyController>().GetTarget() != null)
        {
            if (other.GetComponent<EnemyController>().GetTarget().name == name)
            {
                other.GetComponent<EnemyController>().isFollowing = false;
                if (isLocalPlayer)
                    CmdTarget(other.gameObject, null);
            }
        }
    }

    [Command]
    void CmdTarget(GameObject target, Transform transform)
    {
        target.GetComponent<EnemyController>().SetSyncTarget(transform);

        NetworkIdentity opponentIdentity = target.GetComponent<NetworkIdentity>();
        TargetDoMagic(opponentIdentity.connectionToClient, transform);
    }

    [TargetRpc]
    public void TargetDoMagic(NetworkConnection target, Transform transform)
    {
        // This will appear on the opponent's client, not the attacking player's
        Debug.Log($"Target = {transform}");
    }




    public void SetSyncIndexMap(int index)
    {
        indexMap = index;
    }
    #endregion
}
