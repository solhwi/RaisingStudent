
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuestDebugger : MonoBehaviour
{
    public int questIndex;

    public bool NormalQuestListDebug(int ObjId) // 노말 퀘스트 진행 가능?
    {
        questIndex = TempQuestDatasMgr.tempQuestDatas_SO.GetQuestIdxById(ObjId); // Obj에 관련된 퀘스트 중 가장 진행이 많이 퀘스트

        if (questIndex == -1) // obj가 진행할 퀘스트가 없는 경우 리턴
        {
            Debug.Log("진행할 수 있는 퀘스트가 없음");
            return false;
        }

        GameMgr.questMgr.useQuestData = QuestDataMgr.LoadSingleNormalQuestData(questIndex); // 일반 퀘스트 데이터
        GameMgr.questMgr.questContextIndex = TempQuestDatasMgr.tempQuestDatas_SO.GetNormalQuestProgressByIdx(questIndex); // 현재 진행 정도

        string itemCodeForTalk = GameMgr.questMgr.useQuestData.QuestContext[GameMgr.questMgr.questContextIndex].ItemForTalk;

        if (itemCodeForTalk != "" && PlayerDataMgr.playerData_SO.GetItemCountByCode(itemCodeForTalk) < 0)
        {
            Debug.Log("퀘스트 진행에 필요한 아이템을 가지고 있지 않음.");
            return false;
        }

        if (TempQuestDatasMgr.tempQuestDatas_SO.tempNormalQuestDatas[questIndex].isRepeat == true) GameMgr.questMgr.questType = QuestType.repeat;
        else GameMgr.questMgr.questType = QuestType.normal;

        return true;
    }
    public bool MainQuestListDebug(int ObjId) // 메인 퀘스트 진행 가능?
    {
        questIndex = PlayerDataMgr.playerData_SO.mainQuestProgress; // 현재 메인 퀘스트 진행 정도

        int questProgress = TempQuestDatasMgr.tempQuestDatas_SO.GetMainQuestProgressByIdx(questIndex);

        if (questProgress == -1) return false;

        if (TempQuestDatasMgr.tempQuestDatas_SO.tempMainQuestDatas[questIndex].ObjIds[questProgress] != ObjId)
        {
            Debug.Log("진행할 수 있는 퀘스트가 없음");
            return false;
        }

        GameMgr.questMgr.useQuestData = QuestDataMgr.LoadSingleMainQuestData().mainQuestDatas[questIndex];
        GameMgr.questMgr.questContextIndex = TempQuestDatasMgr.tempQuestDatas_SO.GetMainQuestProgressByIdx(questIndex);

        string itemCodeForTalk = GameMgr.questMgr.useQuestData.QuestContext[GameMgr.questMgr.questContextIndex].ItemForTalk;

        if (itemCodeForTalk != "" && PlayerDataMgr.playerData_SO.GetItemCountByCode(itemCodeForTalk) < 0)
        {
            Debug.Log("퀘스트 진행에 필요한 아이템을 가지고 있지 않음.");
            return false;
        }

        GameMgr.questMgr.questType = QuestType.main;

        return true;
    }
}
