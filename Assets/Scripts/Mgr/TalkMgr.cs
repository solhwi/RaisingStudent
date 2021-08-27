using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkMgr : MonoBehaviour
{
    public bool isBonusTalk = false;
    public bool isTalk = false;

    public string[] talkData;
    public Objdata objdata;
    public NPCdata npcdata;

    public void NormalTalk(NPCdata npcdata, Objdata objdata)
    {
        isBonusTalk = false;

        this.npcdata = npcdata;
        this.objdata = objdata;

        if (npcdata != null) talkData = npcdata.TalkContexts;
        else talkData = objdata.TalkContexts;

        GameMgr.dialogMgr.TalkDataUse(npcdata, objdata, talkData);
    }

    public void BonusTalk(NPCdata npcdata)
    {
        isBonusTalk = true;

        this.npcdata = npcdata;
        this.objdata = null;

        talkData = npcdata.TalkContexts;

        GameMgr.dialogMgr.TalkDataUse(npcdata, objdata, talkData);
    }

    public void TalkEnded()
    {
        if (npcdata != null) // npc인 경우 추가적인 행위
        {
            if (npcdata.GiftTalkContexts.Length == GameMgr.dialogMgr.talkIndex && isBonusTalk) // 선물하기 시 아이템 사용
            {
                PlayerDataMgr.playerData_SO.UseItemByCode(npcdata.FavoriteItemCode);
            }
        }

        if (objdata != null && objdata.HasContents)
        {
            GameMgr.contentsMgr.GetContents(objdata.ObjId);
        }
        else if (npcdata != null && npcdata.HasContents)
        {
            GameMgr.contentsMgr.GetContents(npcdata.ObjId);
        }

        TalkExit();
    }

    public void TalkExit()
    {
        objdata = null;
        npcdata = null;
        isTalk = false;
        isBonusTalk = false;
    }
}