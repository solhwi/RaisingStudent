using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    public GameObject target; // 카메라가 따라갈 대상
    public float moveSpeed; // 카메라 속도
    private Vector3 targetPosition; // 대상의 위치

    public BoxCollider2D bound;

    private Vector3 minBound; // 박스 영역 최소값
    private Vector3 maxBound; // 박스 영역 최대값

    private float halfWidth; // 카메라 반높이값
    private float halfHeight; // 카메라 반너비값

    private Camera theCamera;

    private void Awake()
    {
        if (Instance == null)
        {
            // DontDestroyOnLoad(this.gameObject); // 씬 옮길때 오브젝트 파괴x
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        theCamera = GetComponent<Camera>();
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height; // 반너비 구하는 공식
    }

    void Update()
    {
        if (target.gameObject != null)
        {
            targetPosition.Set(target.transform.position.x
                , target.transform.position.y, this.transform.position.z);

            // Time.dleltaTime : 1초에 실행되는 프레임의 역수
            this.transform.position = Vector3.Lerp(this.transform.position
                , targetPosition, moveSpeed * Time.deltaTime); // 1초에 moveSpeed만큼 이동

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
    }
}
