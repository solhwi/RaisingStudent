using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MiniGamePopup : MonoBehaviour
{
    [SerializeField] Button popupBtn;
    [SerializeField] Text popupText;

    public void OnClickPopup(bool isClear, string text)
    {
        gameObject.SetActive(!gameObject.activeSelf);
        MiniGameMgr.miniGameMgr.Lock = gameObject.activeSelf;

        popupText.text = text;

        if (isClear) popupBtn.onClick.AddListener(() => OnClickClear());
        else popupBtn.onClick.AddListener(() => OnClickFailed());
    }

    public void OnClickClear()
    {
        MiniGameMgr.miniGameMgr.GameClear();
    }

    public void OnClickFailed()
    {
        MiniGameMgr.miniGameMgr.GameOver();
    }
}