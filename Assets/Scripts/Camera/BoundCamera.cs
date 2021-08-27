using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoundCamera : MonoBehaviour
{
    protected static BoundCamera instance;
    public static BoundCamera Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BoundCamera>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    [SerializeField] float moveSpeed;

    [SerializeField] private Transform target;
    private Vector3 targetPosition; // 대상의 위치
    private BoxCollider2D bound;

    private Vector3 minBound; // 박스 영역 최소값
    private Vector3 maxBound; // 박스 영역 최대값

    private float halfWidth; // 카메라 반높이값
    private float halfHeight; // 카메라 반너비값

    private Camera theCamera;

    void Awake()
    {
        Player p = FindObjectOfType<Player>();
        if (p != null) target = p.transform.GetComponent<Transform>();

        Bound b = FindObjectOfType<Bound>();
        if (b != null) bound = b.transform.GetComponent<BoxCollider2D>();

        theCamera = GetComponent<Camera>();

        // Rect rt = theCamera.rect;

        // float scale_height = ((float)Screen.width / Screen.height) / ((float)16 / 9);
        // float scale_width = 1f / scale_height;

        // if (scale_height < 1) { rt.height = scale_height; rt.y = (1f - scale_height) / 2f; }
        // else { rt.width = scale_width; rt.x = (1f - scale_width) / 2f; }

        // theCamera.rect = rt;

        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        //SetBound(bound);
    }
    void LateUpdate()
    {
        if (target == null) return;

        if (target.gameObject != null)
        {
            targetPosition.Set(target.position.x
                , target.position.y, this.transform.position.z);

            // Time.dleltaTime : 1초에 실행되는 프레임의 역수
            this.transform.position = Vector3.Lerp(this.transform.position
                , targetPosition, moveSpeed); // 1초에 moveSpeed만큼 이동

            float clampedX = Mathf.Clamp(this.transform.position.x
                , minBound.x + halfWidth, maxBound.x - halfWidth);
            float clampedY = Mathf.Clamp(this.transform.position.y
                , minBound.y + halfHeight, maxBound.y - halfHeight);

            this.transform.position = new Vector3(clampedX, clampedY
                , this.transform.position.z);
        }
    }
    public void SetBound(BoxCollider2D newBound)
    {
        bound = newBound;
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height; // 반너비 구하는 공식
    }
}
