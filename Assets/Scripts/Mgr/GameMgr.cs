using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour
{
    protected static GameMgr instance;
    public static GameMgr Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameMgr>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    public static QuestMgr questMgr;
    public static TalkMgr talkMgr;
    public static ContentsMgr contentsMgr;
    public static ChallengeMgr challengeMgr;
    public static DialogMgr dialogMgr;
    public static SceneMgr sceneMgr;
    public static BGMMgr bgmMgr;
    public static SFXMgr sfxMgr;

    NPCdata npcdata;
    Objdata objdata;

    void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        questMgr = FindObjectOfType<QuestMgr>();
        talkMgr = FindObjectOfType<TalkMgr>();
        contentsMgr = FindObjectOfType<ContentsMgr>();
        challengeMgr = FindObjectOfType<ChallengeMgr>();
        dialogMgr = FindObjectOfType<DialogMgr>();
        sceneMgr = FindObjectOfType<SceneMgr>();
        bgmMgr = FindObjectOfType<BGMMgr>();
        sfxMgr = FindObjectOfType<SFXMgr>();
    }

    public void Talk(GameObject scanObj)
    {
        if (contentsMgr.ContentLock) return; // 토크 난타 방지

        if (dialogMgr.typeEffect.isAnim) // 현재 npc가 말을 하는 중일 경우 리턴
        {
            dialogMgr.typeEffect.SetMsg("");
            return;
        }

        if (scanObj.layer == LayerMask.NameToLayer("NPC"))
        {
            npcdata = scanObj.GetComponent<NPCdata>();
            objdata = null;
        }
        else if (scanObj.layer == LayerMask.NameToLayer("Object"))
        {
            npcdata = null;
            objdata = scanObj.GetComponent<Objdata>();
        }

        DataUse();
    }

    void DataUse()
    {
        PlayerDataMgr.Sync_Cache_To_Persis();
        TempQuestDatasMgr.Sync_Cache_To_Persis();

        if (PlayerDataMgr.playerData_SO.hungryGazy <= 0)
        {
            talkMgr.NormalTalk(npcdata, objdata);
            return;
        }

        if (!PlayerDataMgr.playerData_SO.isEndedTutorial)
        {
            UICanvas.Instance.errorText.gameObject.GetComponent<Text>().text = "튜토리얼이 끝나지 않았습니다.";
            UICanvas.Instance.errorText.gameObject.SetActive(true);
            talkMgr.NormalTalk(npcdata, objdata);
            return;
        }

        if (questMgr.questDebugger.MainQuestListDebug(npcdata != null ? npcdata.ObjId : objdata.ObjId))
            questMgr.QuestDataUse(npcdata, objdata);

        else if (questMgr.questDebugger.NormalQuestListDebug(npcdata != null ? npcdata.ObjId : objdata.ObjId))
        {
            if (PlayerDataMgr.playerData_SO.GetDayOfWeek() == 2 && !TempQuestDatasMgr.tempQuestDatas_SO.GetMainQuestIsClearByIdx(PlayerDataMgr.playerData_SO.mainQuestProgress))
            {
                UICanvas.Instance.errorText.gameObject.GetComponent<Text>().text = "메인 퀘스트를 먼저 클리어해야 합니다.";
                UICanvas.Instance.errorText.gameObject.SetActive(true);
                talkMgr.NormalTalk(npcdata, objdata);
            }
            else
            {
                questMgr.QuestDataUse(npcdata, objdata);
            }
        }
        else
        {
            if (talkMgr.isBonusTalk && npcdata != null) talkMgr.BonusTalk(npcdata);
            else talkMgr.NormalTalk(npcdata, objdata);
        }
    }
}
