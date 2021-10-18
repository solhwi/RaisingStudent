using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MiniGame2 : MonoBehaviour
{
    [Header("Set In Editor")]
    [SerializeField] public MiniPlayer miniPlayer;
    [SerializeField] public MiniGamePopup miniGamePopup;
    [SerializeField] public TimeSlider timeSlider;
    [SerializeField] GameObject[] spawnItem = new GameObject[3]; // 생성되는 아이템
    [SerializeField] Sprite[] computerSprite = new Sprite[2];


    [Header("Set In Runtime")]
    [SerializeField] float InstantiateTime; // 생성 주기
    [SerializeField] GameObject[] spawnPoint = new GameObject[5];
    [SerializeField] GameObject spawnedPrefab;

    [SerializeField] public int getTime;

    void Awake()
    {
        StageCheck();
        timeSlider.SettingTime(getTime);
        for (int i = 0; i < 5; i++)
        {
            spawnPoint[i] = GameObject.Find("SpawnPoint" + i.ToString());
        }
    }

    void StageCheck()
    {
        switch (PlayerDataMgr.playerData_SO.totalGradeProgress)
        {
            case 0: getTime = 10; InstantiateTime = 0.21f; break;
            case 1: getTime = 12; InstantiateTime = 0.2f; break;
            case 2: getTime = 12; InstantiateTime = 0.19f; break;
            case 3: getTime = 14; InstantiateTime = 0.18f; break;
            case 4: getTime = 14; InstantiateTime = 0.18f; break;
            case 5: getTime = 16; InstantiateTime = 0.17f; break;
            case 6: getTime = 16; InstantiateTime = 0.17f; break;
            case 7: getTime = 16; InstantiateTime = 0.16f; break;
            default:
                Debug.Log("StageCheck 스위치문에서 범위를 벗어남");
                getTime = 15;
                break;
        }

        if (PlayerDataMgr.playerData_SO.totalGradeProgress == 0 && PlayerDataMgr.playerData_SO.dayProgress < 2)
        {
            InstantiateTime = 0.3f;
        }
    }

    void Update()
    {
        if (timeSlider.currTime <= 0.1f && MiniGameMgr.miniGameMgr.isTikToking && !MiniGameMgr.miniGameMgr.Lock)
        {
            SFXMgr.Instance.OverlapPlay_SFX(SFXMgr.SFXName.win);
            miniGamePopup.OnClickPopup(true, "클리어 성공!");
        }
        else if (miniPlayer.hitCount <= 0 && !MiniGameMgr.miniGameMgr.Lock)
        {
            SFXMgr.Instance.OverlapPlay_SFX(SFXMgr.SFXName.lose);
            miniGamePopup.OnClickPopup(false, "클리어 실패!");
        }
    }

    void Start()
    {
        //MiniGameMgr.miniGameMgr.boundCamera.SetActive(false);
        StartCoroutine(GenerateAssignment());
    }

    public IEnumerator GenerateAssignment()
    {
        while (true)
        {
            if (!MiniGameMgr.miniGameMgr.IsStop()) Generate();
            yield return new WaitForSeconds(InstantiateTime);
        }
    }
    public void Generate()
    {
        int temp = new System.Random().Next(0, 5);
        StartCoroutine(Computer_Sprite(temp));
        spawnedPrefab = Instantiate(spawnItem[new System.Random().Next(0, 3)], spawnPoint[temp].transform);

    }

    public IEnumerator Computer_Sprite(int temp)
    {
        spawnPoint[temp].gameObject.GetComponent<SpriteRenderer>().sprite = computerSprite[1];
        while (true)
        {
            yield return new WaitForSeconds(1);
            spawnPoint[temp].gameObject.GetComponent<SpriteRenderer>().sprite = computerSprite[0];
        }
    }
}