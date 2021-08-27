using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame1 : MonoBehaviour
{
    [SerializeField] MiniGameSlider miniGameSlider;
    [SerializeField] MiniGamePopup miniGamePopup;
    [SerializeField] TimeSlider timeSlider;
    [SerializeField] public int getTime;

    void Awake()
    {
        timeSlider.SettingTime(10);
    }

    void Update()
    {
        if (timeSlider.currTime <= 0.1 && !MiniGameMgr.miniGameMgr.isTikToking && !MiniGameMgr.miniGameMgr.Lock) // 클리어
        {
            MiniGameMgr.miniGameMgr.Lock = true;
            SFXMgr.Instance.OverlapPlay_SFX(SFXMgr.SFXName.win);
            miniGamePopup.OnClickPopup(true, "클리어 성공!"); // 미니게임 팝업에게 결과 산출 양도
        }

        else if (miniGameSlider.currMana == 0 && MiniGameMgr.miniGameMgr.isTikToking && !MiniGameMgr.miniGameMgr.Lock) // 비클리어
        {
            MiniGameMgr.miniGameMgr.Lock = true;
            SFXMgr.Instance.OverlapPlay_SFX(SFXMgr.SFXName.lose);
            miniGamePopup.OnClickPopup(false, "클리어 실패!");
        }
    }
}