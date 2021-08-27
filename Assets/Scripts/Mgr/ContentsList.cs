using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ContentsList : MonoBehaviour
{
    [Header("Set By Finder")]
    [SerializeField] public GameObject Window;
    [SerializeField] public GameObject VendingGame;
    [SerializeField] public GameObject gamemap;
    [SerializeField] public Map map;
    [SerializeField] public Shop shop;
    [SerializeField] public Door door;
    [SerializeField] public VendingGame vendingGame;
    [SerializeField] public CutScene cutScene;

    public string currentMapName;

    void Awake()
    {
        currentMapName = SceneManager.GetActiveScene().name;
        if (currentMapName == "Entrance" || currentMapName == "Tdong1") FindWindow();
        else if (currentMapName == "Tdong3") FindVendingGame();
    }
    public void FindWindow()
    {
        Window = GameObject.Find("Window");

        if (currentMapName == "Entrance")
        {
            map = Window.transform.GetChild(0).GetComponent<Map>();
            shop = Window.transform.GetChild(1).GetComponent<Shop>();
            gamemap = Window.transform.GetChild(2).gameObject;
        }
        else
        {
            map = null;
            shop = Window.transform.GetChild(0).GetComponent<Shop>();
        }

    }

    public void FindVendingGame()
    {
        VendingGame = GameObject.Find("VendingGame");

        if (currentMapName == "Tdong3")
        {
            vendingGame = VendingGame.transform.GetChild(0).GetComponent<VendingGame>();
        }

    }

    public void GoToStage(int ObjId)
    {
        if (ObjId != GenericDataMgr.genericData_SO.Professor[PlayerDataMgr.playerData_SO.currProfessorIdx].objId) return;
        if (PlayerDataMgr.playerData_SO.attendCount >= 2) return;
        if (PlayerDataMgr.playerData_SO.IsWeekend()) return;

        StageData stageData = StageDataMgr.LoadSingleStageData(PlayerDataMgr.playerData_SO.totalGradeProgress);
        currentMapName = "MiniGame" + (stageData.stageOrder[PlayerDataMgr.playerData_SO.stageProgress] + 1).ToString();

        PlayerDataMgr.playerData_SO.stageProgress++; // 스테이지 증가
        PlayerDataMgr.playerData_SO.currProfessorIdx = stageData.stageOrder[PlayerDataMgr.playerData_SO.stageProgress % 26] % 4; // 교수 리셋

        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.chime);
        SceneLoader.Instance.LoadScene(currentMapName);
    }

    public void GoToScene(string sceneName)
    {
        PlayerDataMgr.playerData_SO.prevMapName = PlayerDataMgr.playerData_SO.currentMapName;
        SceneLoader.Instance.LoadScene(sceneName);
    }

}