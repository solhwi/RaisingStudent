using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MovingObject
{
    // MovingObject

    // public float speed;
    // public int walkCount;
    // protected int currentWalkCount;

    // protected Vector3 vector;

    // public BoxCollider2D boxCollider;
    // public LayerMask layerMask; // 통과 가능 판단
    // public Animator animator;

    static public PlayerManager instance; // 공유 변수
    public string currentMapName; // transferMapName 변수 값 저장
    public float runSpeed;
    private float applyRunSpeed;
    private bool applyRunFlag = false; // 달리기 여부 판단

    private bool canMove = true;


    void Start()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject); // 씬 옮길때 오브젝트 파괴x
            boxCollider = GetComponent<BoxCollider2D>();
            animator = GetComponent<Animator>();
            instance = this;
        }
        else // (instance != null)
        {
            Destroy(this.gameObject);
        }

    }
    IEnumerator MoveCoroutine()
    {
        while (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift)) // 뛰기
            {
                applyRunSpeed = runSpeed;
                applyRunFlag = true;
            }
            else
            {
                applyRunSpeed = 0;
                applyRunFlag = false;
            }

            // vector에 x,y,z값을 전달
            vector.Set(Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"), transform.position.z);

            if (vector.x != 0)
            {
                vector.y = 0;
            }

            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            RaycastHit2D hit;
            Vector2 start = transform.position;
            Vector2 end = start + new Vector2(vector.x * speed * walkCount
                                            , vector.y * speed * walkCount);

            boxCollider.enabled = false;
            hit = Physics2D.Linecast(start, end, layerMask); // 본인 충돌 방지
            boxCollider.enabled = true;

            if (hit.transform != null)
            {
                break; // 벽이 있다면, 이동x
            }

            animator.SetBool("Walking", true); // standing -> walking

            while (currentWalkCount < walkCount)
            {
                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0);
                }
                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * (speed + applyRunSpeed), 0);
                }
                if (applyRunFlag)
                {
                    currentWalkCount++;
                }
                currentWalkCount++;
                yield return new WaitForSeconds(0.01f);
            }
            currentWalkCount = 0;
        }
        animator.SetBool("Walking", false); // walking -> standing

        canMove = true;
    }

    void Update()
    {
        if (canMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false; // 움직일 수 없는 상태로 진입
                StartCoroutine(MoveCoroutine());
            }
        }
    }
}
