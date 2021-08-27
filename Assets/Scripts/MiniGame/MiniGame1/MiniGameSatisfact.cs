using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameSatisfact : MonoBehaviour
{
    [SerializeField] Image satisfact;
    [SerializeField] Image emotion;
    [SerializeField] Sprite awaken;
    [SerializeField] Sprite drowsy;
    [SerializeField] Sprite sleeping;
    [SerializeField] GameObject sleepingState;

    void Update()
    {
        Set_Slider(satisfact.fillAmount);
    }

    public void Set_Slider(float value)
    {
        if (value >= 0.7f)
            emotion.sprite = awaken;
        else if (value >= 0.3f)
        {
            emotion.sprite = drowsy;
            sleepingState.gameObject.SetActive(false);
        }
        else
        {
            emotion.sprite = sleeping;
            sleepingState.gameObject.SetActive(true);
        }
    }

}