using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeText : MonoBehaviour
{
    public float currTime = -1;

    void Start()
    {
        StartTikTok();
    }
    void Update()
    {
        if (MiniGameMgr.miniGameMgr.Lock) StopTikTok();
        else if (MiniGameMgr.miniGameMgr.isTikToking) TikToking();
    }
    void TikToking()
    {
        if (currTime < 0.1f) StopTikTok();
        currTime -= Time.deltaTime;
        SetTimeText(currTime);
    }
    public void SetTimeText(float time)
    {
        int minten = (int)((time / 600f) % 10);
        int minone = (int)((time / 60f) % 10);
        int sec = (int)(time % 60f);
        string secStr = sec < 10 ? "0" + sec : sec.ToString();
        string minStr = minten > 0 ? "0" + minone : minten.ToString() + minone.ToString();

        GetComponent<Text>().text = minStr + ":" + secStr;
    }
    public void StartTikTok()
    {
        MiniGameMgr.miniGameMgr.isTikToking = true;
    }
    public void StopTikTok()
    {
        MiniGameMgr.miniGameMgr.isTikToking = false;
    }
    public void SettingTime(float _second)
    {
        currTime = _second;
    }
}