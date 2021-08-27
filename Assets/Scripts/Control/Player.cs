using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rigid;
    public Animator anim;
    public Vector3 dirVec;
    public GameObject scanObject;
    protected static Player instance;
    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Player>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        if (PlayerDataMgr.playerData_SO.isMan) anim.runtimeAnimatorController = Resources.Load("PlayerM") as RuntimeAnimatorController;
        else anim.runtimeAnimatorController = Resources.Load("PlayerW") as RuntimeAnimatorController;

        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    void FixedUpdate() //고정된 주기의 update문
    {
        Scan();
    }

    void SFXWalking()
    {
        SFXMgr.Instance.OverlapPlay_SFX(SFXMgr.SFXName.walk);
    }

    void Scan()
    {
        Debug.DrawRay(rigid.position, dirVec * 1.5f, new Color(0, 1, 0));
        RaycastHit2D rayHit;

        rayHit = Physics2D.Raycast(rigid.position, dirVec, 1.5f, LayerMask.GetMask("NPC"));
        if (rayHit.collider == null) rayHit = Physics2D.Raycast(rigid.position, dirVec, 1.5f, LayerMask.GetMask("Object"));
        if (rayHit.collider == null) scanObject = null;
        else scanObject = rayHit.collider.gameObject;
    }
}
