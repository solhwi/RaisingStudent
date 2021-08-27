using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSlider : MonoBehaviour
{
    public float currTime = 0;
    bool first = false;
    Slider slTimer;

    void Start()
    {
        slTimer = GetComponent<Slider>();
        slTimer.maxValue = currTime;
        StartTikTok();
    }
    void Update()
    {
        if (MiniGameMgr.miniGameMgr.Lock) StopTikTok();
        else if (MiniGameMgr.miniGameMgr.isTikToking) TikToking();
    }
    void TikToking()
    {
        if (slTimer.value < 0.1f) StopTikTok();
        currTime -= Time.deltaTime;
        SetTimeSlider(currTime);
    }
    public void SetTimeSlider(float time)
    {
        slTimer.value -= Time.deltaTime;
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
        if (first)
            slTimer.value = currTime;
        else
            first = true;
    }
}