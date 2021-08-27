using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TempQuestDatas
{
    public List<TempQuestData> tempMainQuestDatas = new List<TempQuestData>();
    public List<TempQuestData> tempNormalQuestDatas = new List<TempQuestData>();

    public TempQuestDatas()
    {
        QuestDataContainer QuestDataContainer = QuestDataMgr.LoadSingleMainQuestData(); // 메인 퀘스트 데이터를 끌어 와서

        int idx = 0;

        for (idx = 0; idx < QuestDataContainer.mainQuestDatas.Count; idx++) // 메인 퀘스트 데이터와 일치하도록 순서대로 세팅함
        {
            QuestData mainQuestData = QuestDataContainer.mainQuestDatas[idx];

            List<int> tempidlist = new List<int>();
            for (int j = 0; j < mainQuestData.QuestContext.Count; j++) tempidlist.Add(mainQuestData.QuestContext[j].ObjId);

            tempMainQuestDatas.Add(new TempQuestData(mainQuestData.QuestId, mainQuestData.QuestName, tempidlist, 0));
        }

        idx = 0;

        while (true)
        {
            QuestData normalQuestData = QuestDataMgr.LoadSingleNormalQuestData(idx);
            if (normalQuestData == null) break; // 못 가져올 때까지 가져옴

            List<int> tempidlist = new List<int>();
            for (int j = 0; j < normalQuestData.QuestContext.Count; j++) tempidlist.Add(normalQuestData.QuestContext[j].ObjId);

            // 40번 퀘스트까진 일반 퀘스트임, 그 뒤는 반복 퀘스트
            if (idx < 4) tempNormalQuestDatas.Add(new TempQuestData(normalQuestData.QuestId, normalQuestData.QuestName, tempidlist, 0));
            else if (idx < 8) tempNormalQuestDatas.Add(new TempQuestData(normalQuestData.QuestId, normalQuestData.QuestName, tempidlist, 1));
            else if (idx < 12) tempNormalQuestDatas.Add(new TempQuestData(normalQuestData.QuestId, normalQuestData.QuestName, tempidlist, 2));
            else if (idx < 16) tempNormalQuestDatas.Add(new TempQuestData(normalQuestData.QuestId, normalQuestData.QuestName, tempidlist, 3));
            else if (idx < 20) tempNormalQuestDatas.Add(new TempQuestData(normalQuestData.QuestId, normalQuestData.QuestName, tempidlist, 4));
            else if (idx < 24) tempNormalQuestDatas.Add(new TempQuestData(normalQuestData.QuestId, normalQuestData.QuestName, tempidlist, 5));
            else if (idx < 28) tempNormalQuestDatas.Add(new TempQuestData(normalQuestData.QuestId, normalQuestData.QuestName, tempidlist, 6));
            else if (idx < 32) tempNormalQuestDatas.Add(new TempQuestData(normalQuestData.QuestId, normalQuestData.QuestName, tempidlist, 7));
            else tempNormalQuestDatas.Add(new TempQuestData(normalQuestData.QuestId, normalQuestData.QuestName, tempidlist, 0, true));
            idx++;
        }

    }
}

[System.Serializable]
public class TempQuestData
{
    public int QuestId;
    public string QuestName;
    public List<int> ObjIds;
    public bool isClear; // 알아서 체크될 것
    public int QuestProgress; // 알아서 올라갈 것
    public int totalGradeProgress; // 메인 퀘스트의 경우 안 써도 됨
    public bool isRepeat; // 반복 퀘스트에만 체크

    public TempQuestData(int _QuestId, string _QuestName, List<int> _ObjIds, int _totalGradeProgress, bool _isRepeat = false)
    {
        QuestId = _QuestId;
        QuestName = _QuestName;
        ObjIds = _ObjIds;
        isClear = false;
        QuestProgress = 0;
        totalGradeProgress = _totalGradeProgress;
        isRepeat = _isRepeat;
    }

}

