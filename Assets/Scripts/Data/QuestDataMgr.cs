using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class QuestDataMgr
{
    public static QuestData LoadSingleNormalQuestData(int questIdx)
    {
        if (questIdx >= GenericDataMgr.genericData_SO.FixedNormalQuestCount + GenericDataMgr.genericData_SO.FixedRepeatQuestCount) return null;

        string jsonFileName = "questData/NormalQuestData-" + questIdx.ToString();

        TextAsset jsonData = Resources.Load<TextAsset>(jsonFileName);

        Debug.Log("NormalQuestData: SINGLE NORMAL_QUEST_DATA LOAD COMPLETE");
        return JsonUtility.FromJson<QuestData>(jsonData.ToString());
    }

    public static QuestDataContainer LoadSingleMainQuestData()
    {
        string jsonFileName = "questData/MainQuestData";
        TextAsset jsonData = Resources.Load<TextAsset>(jsonFileName);

        Debug.Log("MainQuestData: SINGLE NORMAL_QUEST_DATA LOAD COMPLETE");
        return JsonUtility.FromJson<QuestDataContainer>(jsonData.ToString());
    }

}
