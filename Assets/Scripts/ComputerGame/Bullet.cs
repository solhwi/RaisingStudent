using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    [Header("Set In Editor")]
    [SerializeField] GameObject[] spawnItem = new GameObject[5];


    [Header("Set In Runtime")]
    [SerializeField] float instantiateTime; // 생성 주기
    [SerializeField] GameObject[] spawnPoint = new GameObject[5];



}
