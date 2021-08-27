using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TempQuestDatas_SO", fileName = "TempQuestDatas_SO", order = 2)]
public class TempQuestDatas_SO : ScriptableObject
{
    public List<TempQuestData> tempMainQuestDatas = new List<TempQuestData>();
    public List<TempQuestData> tempNormalQuestDatas = new List<TempQuestData>();

    public string GetNormalQuestNameByIdx(int idx) // 퀘스트의 인덱스를 받고 해당 퀘스트 이름을 반환, 프로세스 체크와는 별도
    {
        if (tempNormalQuestDatas.Count <= idx) return null;
        return tempNormalQuestDatas[idx].QuestName;
    }

    public int GetNormalQuestProgressByIdx(int idx) // 노말 퀘스트의 인덱스를 받고 그 진행 상태를 반환, 프로세스 체크와는 별도
    {
        if (tempNormalQuestDatas.Count <= idx) return -1;
        if (tempNormalQuestDatas[idx].isClear == true) return -1;
        return tempNormalQuestDatas[idx].QuestProgress;
    }

    public QuestData GetNormalQuestByIdx(int idx) // 인덱스에 해당하는 일반 퀘스트 반환
    {
        if (CheckCanProcessNormalQuestByIdx(idx)) return QuestDataMgr.LoadSingleNormalQuestData(idx);
        return null;
    }

    public int GetMainQuestProgressByIdx(int idx) // 메인 퀘스트의 인덱스를 받고 그 진행 상태를 반환
    {
        if (CheckCanProcessMainQuestByIdx(idx)) return tempMainQuestDatas[idx].QuestProgress;
        return -1;
    }

    public bool GetMainQuestIsClearByIdx(int idx) => tempMainQuestDatas[idx].isClear;

    public bool CheckCanProcessNormalQuestByIdx(int idx)
    {
        if (idx >= tempNormalQuestDatas.Count)
        {
            Debug.Log("더 이상 퀘스트가 없습니다.");
            return false; // 노말 퀘스트가 더 없는
        }
        if (tempNormalQuestDatas[idx].isClear)
        {
            Debug.Log("이미 퀘스트를 클리어했습니다.");
            return false; // 클리어한 퀘스트인 경우
        }

        if (tempNormalQuestDatas[idx].isRepeat) return true;
        else if (tempNormalQuestDatas[idx].totalGradeProgress != PlayerDataMgr.playerData_SO.totalGradeProgress) return false; // 이번 학기에 수행할 수 있는 퀘스트가 아닌 경우

        return true;
    }

    public bool CheckCanProcessMainQuestByIdx(int idx) // 현재 메인 퀘스트를 진행할 수 있는가?
    {
        if (idx >= tempMainQuestDatas.Count) return false; // 메인 퀘스트가 더 없는
        if (tempMainQuestDatas[idx].isClear) return false; // 이번 주차를 이미 클리어한 경우
        if (!PlayerDataMgr.playerData_SO.IsWeekend()) return false; // 오늘이 주말이 아닌 경우

        return true;
    }

    public int GetQuestIdxById(int id) //오브젝트가 실행 가능한 퀘스트 중 가장 진행 정도가 높은 퀘스트 인덱스 반환
    {
        // obj ID를 받아 objID와 관련된 퀘스트들을 가져온다.
        List<int> tempList = new List<int>();
        int maxIdx = -1;
        int maxProgress = -1;
        int count = 0;

        for (int i = 0; i < tempNormalQuestDatas.Count; i++)
        {
            int questprogress = tempNormalQuestDatas[i].QuestProgress;

            if (tempNormalQuestDatas[i].ObjIds.Count > questprogress &&
            tempNormalQuestDatas[i].ObjIds[questprogress] == id &&
            tempNormalQuestDatas[i].isClear == false &&
            tempNormalQuestDatas[i].totalGradeProgress == PlayerDataMgr.playerData_SO.totalGradeProgress)
            {
                // questprogress가 검사할 questdata의 npc list보다 큰 경우 검사 금지
                // 진행할 수 있는 퀘스트 중에 해당 ObjId가 포함되어있고,
                // 그 퀘스트는 클리어되지 않은 경우
                // 현재 학기에 진행 가능한 퀘스트인 경우
                tempList.Add(i);
                Debug.Log("가용 퀘스트 인덱스:" + i);
                count++;
            }
        }

        if (count == 0) return -1; // 못 찾음

        for (int i = 0; i < tempList.Count; i++)
        {
            if (tempNormalQuestDatas[tempList[i]].QuestProgress > maxProgress)
            {
                maxProgress = tempNormalQuestDatas[tempList[i]].QuestProgress;
                maxIdx = tempList[i];
            }
        }

        return maxIdx;
    }
}
