using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string username;

    public int totalGradeProgress;
    public int dayProgress;
    public int stageProgress;
    public int mainQuestProgress;

    public bool love;
    public int girlfriendId;

    public bool isEndedGrade;
    public bool isFailed;
    public bool isStudied;
    public bool isFoundHidden;
    public bool isEndedTutorial;

    public int tutorial_progress;

    public int attendCount;
    public int clearCount;
    public bool isMan;
    public int currProfessorIdx;

    public string currentMapName;
    public string prevMapName;
    public int gold;
    public int satisfact;
    public int hungryGazy;

    public List<int> grades = new List<int>();
    public List<HaveItemData> haveConsumeItems = new List<HaveItemData>();
    public List<HaveItemData> haveOtherItems = new List<HaveItemData>();
    public List<HaveItemData> haveBadgeItems = new List<HaveItemData>();
    public List<NPCLike> npcLikes = new List<NPCLike>();
    public List<ProfessorLike> professorLikes = new List<ProfessorLike>();
    public List<int> challengeQuestCounts = new List<int>();

    public List<bool> endingList = new List<bool>();

    public PlayerData()
    {
        // to-do
        // 첫 플레이시 어떻게 초기화할건지 
        totalGradeProgress = 0;
        dayProgress = 0;
        stageProgress = 0;
        mainQuestProgress = 0;

        love = false;
        girlfriendId = -1;

        isEndedGrade = false;
        isFailed = false;
        isEndedTutorial = false;
        isStudied = false;
        isFoundHidden = false;

        tutorial_progress = 0;

        attendCount = 0;
        clearCount = 0;
        isMan = true;

        currentMapName = "House";
        prevMapName = "House";
        currProfessorIdx = 1;

        gold = 5000;
        satisfact = 30;
        hungryGazy = 100;

        haveConsumeItems.Clear();
        haveOtherItems.Clear();
        haveBadgeItems.Clear();

        if (endingList.Count > 0)
        {
            for (int i = 0; i < PlayerDataMgr.playerData_SO.endingList.Count; i++)
            {
                if (endingList[i] == true)
                {
                    endingList = PlayerDataMgr.playerData_SO.endingList;
                    break;
                }
                else if (i == PlayerDataMgr.playerData_SO.endingList.Count - 1)
                {
                    endingList.Clear();
                    for (int j = 0; j < 9; j++)
                    {
                        endingList.Add(false);
                    }
                }
            }
        }
        else
        {
            endingList.Clear();
            for (int j = 0; j < 9; j++)
            {
                endingList.Add(false);
            }
        }


        for (int i = 0; i < 8; i++) grades.Add(0);
        for (int i = 0; i < GenericDataMgr.genericData_SO.FixedChallengeQuestCount; i++) haveBadgeItems.Add(new HaveItemData(GenericDataMgr.genericData_SO.GetItemByCode("BADGE" + i.ToString())));
        for (int i = 0; i < GenericDataMgr.genericData_SO.FixedNPCCount; i++) npcLikes.Add(new NPCLike(10000 + i * 1000));
        for (int j = 0; j < GenericDataMgr.genericData_SO.FixedProfessorCount; j++) professorLikes.Add(new ProfessorLike(99000 - j * 1000));
        for (int k = 0; k < GenericDataMgr.genericData_SO.FixedChallengeQuestCount; k++) challengeQuestCounts.Add(0);
    }
}

[System.Serializable]
public class HaveItemData
{
    public ItemData item;
    public int count;

    public HaveItemData(ItemData itemdata)
    {
        item = itemdata;
        count = 1;
    }
}

[System.Serializable]
public class NPCLike
{
    public int id;
    public int like;
    public NPCLike(int _id, int _like = 0)
    {
        // to-do
        // 첫 플레이시 어떻게 초기화할건지
        id = _id;
        like = _like;
    }

}

[System.Serializable]
public class ProfessorLike
{
    public int id;
    public int like;
    public ProfessorLike(int _id, int _like = 0)
    {
        // to-do
        // 첫 플레이시 어떻게 초기화할건지
        id = _id;
        like = 0;
    }
}

