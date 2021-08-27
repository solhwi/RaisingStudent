using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBox : MonoBehaviour
{
    public static RandomBox instance;
    // Update is called once per frame
    void Awake()
    {
        instance = this;
        if (PlayerDataMgr.playerData_SO.dayProgress != 2 && PlayerDataMgr.playerData_SO.dayProgress != 8 && PlayerDataMgr.playerData_SO.dayProgress != 15)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    public void OnClickBox()
    {
        PlayerDataMgr.playerData_SO.AddItemByCode(GenericDataMgr.genericData_SO.ConsumeItemList[Random.Range(0, 5)].code);
        gameObject.SetActive(false);
    }
}
