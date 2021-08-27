using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    // Button closeButton;
    // Button buttonT;
    public bool isMapOpen = false;

    // void Awake()
    // {
    //     closeButton = transform.Find("Close").GetComponent<Button>();
    //     buttonT = transform.GetChild(1).Find("T").GetComponent<Button>();

    //     closeButton.onClick.AddListener(() => OnClickMap());
    //     buttonT.onClick.AddListener(() => OnClickT());
    // }
    public void OnClickMap()
    {
        isMapOpen = !isMapOpen;
        if (!isMapOpen) SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.button);
        gameObject.SetActive(isMapOpen);
    }
    public void OnClickT()
    {
        isMapOpen = false;
        gameObject.SetActive(isMapOpen);
        PlayerDataMgr.playerData_SO.prevMapName = PlayerDataMgr.playerData_SO.currentMapName;
        SceneLoader.Instance.LoadScene("Tdong1"); // T동 1층으로
    }
}