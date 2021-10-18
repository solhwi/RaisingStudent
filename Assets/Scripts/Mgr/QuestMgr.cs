using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum QuestType
{
    main,
    normal,
    repeat
}

public class QuestMgr : MonoBehaviour
{
    [SerializeField] public QuestDebugger questDebugger;

    public QuestData useQuestData = new QuestData();
    public QuestType questType = QuestType.normal;
    public int questContextIndex = 0;

    public void QuestDataUse(NPCdata npcdata, Objdata objdata)
    {
        int talkIndex = GameMgr.dialogMgr.talkIndex;

        if (talkIndex == useQuestData.QuestContext[questContextIndex].TalkContext.Length)
        {
            if (!GiveRewards(npcdata != null ? npcdata.ObjId : objdata.ObjId)) // 여러 이유에 의해 보상 지급이 실패한 경우
            {
                Debug.Log("알 수 없는 오류에 의해 보상 지급에 실패했습니다. 재시도해주세요.");
                UICanvas.Instance.errorPopup.TurnOnErrorPopup();
                GameMgr.dialogMgr.TalkDataUse(null, null, null);
                return;
            } // 보상 지급

            PlayerDataMgr.playerData_SO.UseItemByCode(useQuestData.QuestContext[questContextIndex].ItemForTalk); // 아이템 사용
            if (questContextIndex == useQuestData.QuestContext.Count) SetClear(); // 클리어
            else SetQuestProgress(); // 퀘스트 진도

            GameMgr.dialogMgr.TalkDataUse(null, null, null);
            return;
        }

        GameMgr.dialogMgr.TalkDataUse(npcdata, objdata, useQuestData.QuestContext[questContextIndex].TalkContext);

    }

    public void SetClear()
    {
        questContextIndex = 0;

        int questIndex = questDebugger.questIndex;

        switch (questType)
        {
            case (QuestType.main):
                PlayerDataMgr.playerData_SO.UseHungryGazy(70);
                TempQuestDatasMgr.tempQuestDatas_SO.tempMainQuestDatas[questIndex].isClear = true;
                break;
            case (QuestType.normal):
                TempQuestDatasMgr.tempQuestDatas_SO.tempNormalQuestDatas[questIndex].isClear = true;
                GameMgr.challengeMgr.SetChallengeCount(Challenge.NormalQuestMaster, 1);
                break;
            case (QuestType.repeat):
                TempQuestDatasMgr.tempQuestDatas_SO.tempNormalQuestDatas[questIndex].QuestProgress = 0;
                GameMgr.challengeMgr.SetChallengeCount(Challenge.NormalQuestMaster, 1);
                break;
        }
    }

    public void SetQuestProgress()
    {
        questContextIndex++; // 다음 퀘스트 진행
        TempQuestDatasMgr.Sync_Cache_To_Persis();

        int questIndex = questDebugger.questIndex;

        switch (questType)
        {
            case (QuestType.main):
                TempQuestDatasMgr.tempQuestDatas_SO.tempMainQuestDatas[questIndex].QuestProgress++;
                break;
            case (QuestType.normal):
            case (QuestType.repeat):
                PlayerDataMgr.playerData_SO.UseHungryGazy(15);
                TempQuestDatasMgr.tempQuestDatas_SO.tempNormalQuestDatas[questIndex].QuestProgress++;
                break;
        }

        if (questContextIndex == useQuestData.QuestContext.Count)
        {
            SetClear(); // 클리어
        }
    }
    bool GiveRewards(int id)
    {
        string code = useQuestData.QuestContext[questContextIndex].ItemCodeReward;

        if (code != "" && code != null) // 퀘스트 데이터 작성 시 실수 예방
        {
            if (!PlayerDataMgr.playerData_SO.AddItemByCode(code)) // 아이템 보상
            {
                Debug.Log("인벤토리가 가득 찼거나, 잘못된 형식의 아이템입니다.");
                return false;
            }

        }

        int amount = useQuestData.QuestContext[questContextIndex].GoldReward;

        if (amount != -1 && amount != 0)
        {
            PlayerDataMgr.playerData_SO.AddGold(amount); // 골드 보상
        }

        int like = useQuestData.QuestContext[questContextIndex].LikeReward;

        if (like != -1 && like != 0)
        {
            PlayerDataMgr.playerData_SO.GiveLikePoint(id, like); // 호감도 보상
        }

        return true;
    }
}
