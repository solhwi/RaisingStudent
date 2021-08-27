using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    [SerializeField] public float speed;

    public int VerticalButton; // up down
    public int HorizontalButton; // left, right

    bool temp = true; // 디버깅을 위한 아이

    Player player;
    GameMgr gameMgr;
    bool isHorizonMove;
    float h, v;
    void Awake()
    {
        gameMgr = FindObjectOfType<GameMgr>();
        player = FindObjectOfType<Player>();
        VerticalButton = 0;
        HorizontalButton = 0;
    }

    void Update()
    {
        //PlayerMoveForDebug(); // 디버그를 위한 코드
    }

    void FixedUpdate()
    {
        PlayerMove();
        PlayerAction();
    }
    void PlayerMoveForDebug()
    {
        if (Input.GetKeyDown(KeyCode.Space) && temp)
        {
            temp = false;
            if (player.scanObject != null)
                gameMgr.Talk(player.scanObject);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            temp = true;
        }

        VerticalButton = (int)Input.GetAxisRaw("Vertical");
        HorizontalButton = (int)Input.GetAxisRaw("Horizontal");
    }
    void PlayerMove()
    {
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v); // 수평 이동인가? -> h 이동, 아니면 v 이동
        player.rigid.velocity = moveVec * speed;
    }

    void PlayerAction()
    {
        h = GameMgr.talkMgr.isTalk ? 0 : HorizontalButton;
        v = GameMgr.talkMgr.isTalk ? 0 : VerticalButton;

        bool isHorizontalButtonDown = false;
        bool isVerticalButtonDown = false;
        if (HorizontalButton != 0) isHorizontalButtonDown = true;
        if (VerticalButton != 0) isVerticalButtonDown = true;


        bool hDown = GameMgr.talkMgr.isTalk ? false : isHorizontalButtonDown;
        bool vDown = GameMgr.talkMgr.isTalk ? false : isVerticalButtonDown;
        bool hUp = GameMgr.talkMgr.isTalk ? false : isHorizontalButtonDown;
        bool vUp = GameMgr.talkMgr.isTalk ? false : isVerticalButtonDown;

        // tManager가 talk state이면 bool값 전부 false 처리

        if (hDown)
        {
            isHorizonMove = true;
            vDown = false;
        }
        if (vDown)
        {
            isHorizonMove = false;
            hDown = false;
        }
        if (hUp || vUp) isHorizonMove = h != 0; //수평 이동 중에 수직 이동을 떼었을 때, 멈추는 것을 방지하여 h 변수 체크

        if (player.anim.GetInteger("HorizontalMove") != (int)h) //변동이 있을 때, animator의 변수 세팅하러 진입
        {
            player.anim.SetInteger("HorizontalMove", (int)h);
            player.anim.SetTrigger("isChanged");
        }
        else if (player.anim.GetInteger("VerticalMove") != (int)v)
        {
            player.anim.SetInteger("VerticalMove", (int)v);
            player.anim.SetTrigger("isChanged");
        }

        if (vDown && v == 1) player.dirVec = Vector3.up;
        else if (vDown && v == -1) player.dirVec = Vector3.down;
        else if (hDown && h == -1) player.dirVec = Vector3.left;
        else if (hDown && h == 1) player.dirVec = Vector3.right;
    }

}
