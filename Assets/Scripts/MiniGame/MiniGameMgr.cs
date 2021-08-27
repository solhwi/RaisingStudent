using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameMgr : MonoBehaviour
{
    public static MiniGameMgr miniGameMgr;

    [Header("Set By Finder")]
    [SerializeField] public GameObject uicanvas;
    [SerializeField] public GameObject boundCamera;
    [SerializeField] public GameObject player;
    [SerializeField] public bool Lock;
    [SerializeField] public bool isTikToking;


    void Awake()
    {
        miniGameMgr = this;
        GameSet();
    }

    public void GameSet()
    {
        UICanvas.Instance.TurnUI(false);
        boundCamera = GameObject.Find("BoundCamera");
        boundCamera.SetActive(false);
        //player = GameObject.Find("Player");
        //player.SetActive(false);
    }

    public void GameUnset()
    {
        Lock = true;
        UICanvas.Instance.TurnUI(true);
        boundCamera.SetActive(true);
        //player.SetActive(true);
    }

    public void GameOver()
    {
        PlayerDataMgr.playerData_SO.attendCount++;
        if (PlayerDataMgr.playerData_SO.GetWeekProgress() == 4 || PlayerDataMgr.playerData_SO.GetWeekProgress() == 9)
        {
            PlayerDataMgr.playerData_SO.attendCount++;
            PlayerDataMgr.playerData_SO.satisfact -= 5;
        }
        PlayerDataMgr.playerData_SO.satisfact -= 2;

        PlayerDataMgr.Sync_Cache_To_Persis();
        TempQuestDatasMgr.Sync_Cache_To_Persis();

        GameUnset();
        SceneLoader.Instance.LoadScene(GenericDataMgr.genericData_SO.Professor[PlayerDataMgr.playerData_SO.currProfessorIdx].place);
    }

    public void GameClear()
    {

        PlayerDataMgr.playerData_SO.clearCount++;
        PlayerDataMgr.playerData_SO.attendCount++;
        if (PlayerDataMgr.playerData_SO.GetWeekProgress() == 4 || PlayerDataMgr.playerData_SO.GetWeekProgress() == 9)
        {
            PlayerDataMgr.playerData_SO.clearCount++;
            PlayerDataMgr.playerData_SO.attendCount++;
        }

        PlayerDataMgr.Sync_Cache_To_Persis();
        TempQuestDatasMgr.Sync_Cache_To_Persis();

        GameUnset();
        SceneLoader.Instance.LoadScene(GenericDataMgr.genericData_SO.Professor[PlayerDataMgr.playerData_SO.currProfessorIdx].place);
    }

    public void GiveUp()
    {
        GameUnset();
        SceneLoader.Instance.LoadScene("MainTitle");
    }

    public bool IsStop()
    {
        return (Lock || !isTikToking) ? true : false;
    }
}