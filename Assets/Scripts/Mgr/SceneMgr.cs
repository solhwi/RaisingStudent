using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SceneMgr : MonoBehaviour
{
    [SerializeField] public GameObject uicanvas;

    public void ReadyForLoad(string sceneName)
    {
        if (sceneName == "Entrance" || sceneName == "Tdong1")
        {
            GameMgr.contentsMgr.contentsList.currentMapName = sceneName;
            GameMgr.contentsMgr.contentsList.FindWindow();
        }

        if (sceneName == "Tdong3")
        {
            GameMgr.contentsMgr.contentsList.currentMapName = sceneName;
            GameMgr.contentsMgr.contentsList.FindVendingGame();
        }

        UICanvas.Instance.cutScene.GetComponent<CanvasGroup>().alpha = 0f;
        UICanvas.Instance.cutScene.gameObject.SetActive(false);

        if (sceneName == "MainPage" || sceneName == "Prologue" || sceneName == "Ending" || sceneName == "Vacation" || sceneName.Contains("MiniGame"))
        {
            UICanvas.Instance.TurnUI(false);
            Player.Instance.gameObject.SetActive(false);
        }
        else
        {
            UICanvas.Instance.TurnUI(true);
            Player.Instance.gameObject.SetActive(true);
        }



        GameMgr.contentsMgr.FreeLock(); // 토크 락 해제
        PlayerDataMgr.playerData_SO.currentMapName = SceneManager.GetActiveScene().name;

        if (sceneName == "House")
        {
            UICanvas.Instance.tutorial.gameObject.SetActive(!PlayerDataMgr.playerData_SO.isEndedTutorial);
            PlayerDataMgr.playerData_SO.prevMapName = "House";
        }

        if (PlayerDataMgr.playerData_SO.hungryGazy == 0)
        {
            UICanvas.Instance.errorText.gameObject.GetComponent<Text>().text = "피로도가 없습니다.";
            UICanvas.Instance.errorText.gameObject.SetActive(true);
        }
    }

}