using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject Menu;

    public void OnClickPause()
    {
        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.button);
        Menu.SetActive(!Menu.activeSelf);
    }

    public void OnClickContinue()
    {
        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.button);
    }

    public void OnClickSave()
    {
        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.button);
        PlayerDataMgr.Sync_Persis_To_Cache();
        TempQuestDatasMgr.Sync_Persis_To_Cache();
        Menu.SetActive(!Menu.activeSelf);
        SceneLoader.Instance.LoadScene("MainPage");
    }

    public void OnClickExit()
    {
        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.button);
        //PlayerDataMgr.Init_PlayerData();
        //TempQuestDatasMgr.Init_TempQuestData();
        Application.Quit();
    }

}