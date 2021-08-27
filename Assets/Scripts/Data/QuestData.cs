using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestDataContainer
{
    public List<QuestData> mainQuestDatas = new List<QuestData>();

    public QuestDataContainer()
    {
        mainQuestDatas.Add(new QuestData());
    }
}

[System.Serializable]
public class QuestData
{
    // quest에 대한 정보를 담고 있음, quest의 이름과 quest와 관련된 npc
    public int QuestId;
    public string QuestName;
    public string QuestDescription;
    public List<TalkData> QuestContext;
}

[System.Serializable]
public class TalkData
{
    public int ObjId;
    public string ObjName;
    public string ItemForTalk; // 말을 걸기 위해 필요한 아이템
    public string[] TalkContext;
    public string ItemCodeReward;
    public int GoldReward;
    public int LikeReward;
}

