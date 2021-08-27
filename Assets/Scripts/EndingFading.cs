using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EndingFading : MonoBehaviour, IPointerDownHandler
{
    public MainPage mainPage;
    [SerializeField] public int ending_idx;

    void Start()
    {
        mainPage = FindObjectOfType<MainPage>();
        Image i = GetComponent<Image>();

        if (PlayerDataMgr.playerData_SO.endingList[ending_idx])
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, 1f);
        }
        else
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, 0.2f);
        }
    }

    public void OnPointerDown(PointerEventData e)
    {
        if (PlayerDataMgr.playerData_SO.endingList[ending_idx])
            mainPage.OnClickEnding(ending_idx);
    }

}
