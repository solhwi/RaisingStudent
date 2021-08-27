using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkPopup : Talk
{
    public void OnClickYes()
    {
        GameMgr.talkMgr.isBonusTalk = true;
        gameObject.SetActive(false);

        player = FindObjectOfType<Player>();

        if (player.scanObject != null)
            GameMgr.Instance.Talk(player.scanObject);
    }

    public void OnClickNo()
    {
        GameMgr.talkMgr.TalkExit();
        gameObject.SetActive(false);
    }

}