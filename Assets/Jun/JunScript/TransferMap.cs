using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMapName; // 이동할 맵의 이름

    private PlayerManager thePlayer; // 플레이어 정보 받아오기

    void Start()
    {
        // FindObjectOfType<> : 하이어라키에 있는 모든 객체의 <>컴포넌트를 검색, 리턴
        // GetComponent<> : 해당 스크립트 적용 객체 <>컴포넌트 검색, 리턴 (검색범위 차이)
        // thePlayer = FindObjectOfType<PlayerManager>(); // 변수 채우기
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            //thePlayer.currentMapName = transferMapName;
            SceneManager.LoadScene(transferMapName);
        }
    }


}
