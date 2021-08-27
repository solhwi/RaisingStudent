using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MiniGamePause : MonoBehaviour
{
    [SerializeField] GameObject pause;
    public void OnClickPause()
    {
        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.button);
        pause.gameObject.SetActive(!pause.gameObject.activeSelf);
        MiniGameMgr.miniGameMgr.isTikToking = !pause.gameObject.activeSelf;
    }

    public void OnClickMain()
    {
        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.button);
        pause.gameObject.SetActive(!pause.gameObject.activeSelf);
        MiniGameMgr.miniGameMgr.isTikToking = !pause.gameObject.activeSelf;
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