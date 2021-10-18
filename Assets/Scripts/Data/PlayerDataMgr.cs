using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

// PlayerData를 관리하는 스크립트입니다.
// (2) DataSyn Functions : Persis -> Cache / Cache -> Persis
public class PlayerDataMgr
{
    public static PlayerData_SO playerData_SO = Resources.Load<PlayerData_SO>("PlayerData_SO");

    #region PUBLIC METHODS
    // 첫 플레이시, 플레이어 데이터를 첫플레이에 맞게 초기화합니다.
    public static void Init_PlayerData()
    {
        PlayerData data = new PlayerData();

        string JsonData = JsonUtility.ToJson(data, true);

        string path = GetPathFromSaveFile();
        using (FileStream stream = File.Open(path, FileMode.Create))
        {

            byte[] byteData = Encoding.UTF8.GetBytes(JsonData);

            stream.Write(byteData, 0, byteData.Length);

            stream.Close();

            Sync_Persis_To_Cache();
            Debug.Log("PlayerDataMgr: INIT COMPLETE - " + path);
        }
    }

    public static void Sync_Persis_To_Cache()
    {
        PlayerData playerPersisData;
        string path = GetPathFromSaveFile();
        using (FileStream stream = File.Open(path, FileMode.Open))
        {

            byte[] byteData = new byte[stream.Length];

            stream.Read(byteData, 0, byteData.Length);

            stream.Close();

            string JsonData = Encoding.UTF8.GetString(byteData);

            playerPersisData = JsonUtility.FromJson<PlayerData>(JsonData);

        }

        playerData_SO.haveConsumeItems.Clear();
        playerData_SO.haveOtherItems.Clear();
        playerData_SO.haveBadgeItems.Clear();
        playerData_SO.npcLikes.Clear();
        playerData_SO.professorLikes.Clear();
        playerData_SO.grades.Clear();
        playerData_SO.challengeQuestCounts.Clear();
        playerData_SO.endingList.Clear();

        foreach (int c in playerPersisData.grades) playerData_SO.grades.Add(c);
        foreach (HaveItemData d in playerPersisData.haveConsumeItems)
        {
            ItemData i = GenericDataMgr.genericData_SO.GetItemByCode(d.item.code);

            d.item.sprite = i.sprite;

            playerData_SO.haveConsumeItems.Add(d);
        }
        foreach (HaveItemData d in playerPersisData.haveOtherItems)
        {
            ItemData i = GenericDataMgr.genericData_SO.GetItemByCode(d.item.code);

            d.item.sprite = i.sprite;
            playerData_SO.haveOtherItems.Add(d);
        }
        foreach (HaveItemData d in playerPersisData.haveBadgeItems)
        {
            ItemData i = GenericDataMgr.genericData_SO.GetItemByCode(d.item.code);

            d.item.sprite = i.sprite;
            playerData_SO.haveBadgeItems.Add(d);
        }
        foreach (NPCLike n in playerPersisData.npcLikes) playerData_SO.npcLikes.Add(n);
        foreach (ProfessorLike p in playerPersisData.professorLikes) playerData_SO.professorLikes.Add(p);
        foreach (int i in playerPersisData.challengeQuestCounts) playerData_SO.challengeQuestCounts.Add(i);
        foreach (bool b in playerPersisData.endingList) playerData_SO.endingList.Add(b);

        playerData_SO.totalGradeProgress = playerPersisData.totalGradeProgress;
        playerData_SO.stageProgress = playerPersisData.stageProgress;
        playerData_SO.dayProgress = playerPersisData.dayProgress;
        playerData_SO.mainQuestProgress = playerPersisData.mainQuestProgress;
        playerData_SO.attendCount = playerPersisData.attendCount;
        playerData_SO.clearCount = playerPersisData.clearCount;
        playerData_SO.isMan = playerPersisData.isMan;
        playerData_SO.gold = playerPersisData.gold;
        playerData_SO.satisfact = playerPersisData.satisfact;
        playerData_SO.hungryGazy = playerPersisData.hungryGazy;
        playerData_SO.username = playerPersisData.username;
        playerData_SO.prevMapName = "House";
        playerData_SO.currentMapName = "House";
        playerData_SO.isEndedGrade = playerPersisData.isEndedGrade;
        playerData_SO.isEndedTutorial = playerPersisData.isEndedTutorial;
        playerData_SO.tutorial_progress = playerPersisData.tutorial_progress;
        playerData_SO.love = playerPersisData.love;
        playerData_SO.girlfriendId = playerPersisData.girlfriendId;
        playerData_SO.currProfessorIdx = playerPersisData.currProfessorIdx;
        playerData_SO.isFailed = playerPersisData.isFailed;
        playerData_SO.isStudied = playerPersisData.isStudied;
        playerData_SO.isFoundHidden = playerPersisData.isFoundHidden;

        Debug.Log("PlayerDataMgr: PLAYER_DATA (PERSIS->CACHE) COMPLETE \n " + path);
    }

    public static void Sync_Cache_To_Persis()
    {
        PlayerData playerPersisData = new PlayerData();


        // PlayerData-to-add
        // Player Data에 추가되는 항목은 여기에도 추가하세요. 
        playerPersisData.grades.Clear();
        playerPersisData.npcLikes.Clear();
        playerPersisData.haveConsumeItems.Clear();
        playerPersisData.haveOtherItems.Clear();
        playerPersisData.haveBadgeItems.Clear();
        playerPersisData.professorLikes.Clear();
        playerPersisData.challengeQuestCounts.Clear();
        playerPersisData.endingList.Clear();

        foreach (int c in playerData_SO.grades) playerPersisData.grades.Add(c);
        foreach (HaveItemData d in playerData_SO.haveConsumeItems) playerPersisData.haveConsumeItems.Add(d);
        foreach (HaveItemData d in playerData_SO.haveOtherItems) playerPersisData.haveOtherItems.Add(d);
        foreach (HaveItemData d in playerData_SO.haveBadgeItems) playerPersisData.haveBadgeItems.Add(d);
        foreach (NPCLike n in playerData_SO.npcLikes) playerPersisData.npcLikes.Add(n);
        foreach (ProfessorLike p in playerData_SO.professorLikes) playerPersisData.professorLikes.Add(p);
        foreach (int i in playerData_SO.challengeQuestCounts) playerPersisData.challengeQuestCounts.Add(i);
        foreach (bool b in playerData_SO.endingList) playerPersisData.endingList.Add(b);

        playerPersisData.totalGradeProgress = playerData_SO.totalGradeProgress;
        playerPersisData.stageProgress = playerData_SO.stageProgress;
        playerPersisData.dayProgress = playerData_SO.dayProgress;
        playerPersisData.mainQuestProgress = playerData_SO.mainQuestProgress;
        playerPersisData.attendCount = playerData_SO.attendCount;
        playerPersisData.clearCount = playerData_SO.clearCount;
        playerPersisData.isMan = playerData_SO.isMan;
        playerPersisData.gold = playerData_SO.gold;
        playerPersisData.satisfact = playerData_SO.satisfact;
        playerPersisData.hungryGazy = playerData_SO.hungryGazy;
        playerPersisData.username = playerData_SO.username;
        playerPersisData.prevMapName = "House";
        playerPersisData.currentMapName = "House";
        playerPersisData.isEndedGrade = playerData_SO.isEndedGrade;
        playerPersisData.isEndedTutorial = playerData_SO.isEndedTutorial;
        playerPersisData.tutorial_progress = playerData_SO.tutorial_progress;
        playerPersisData.love = playerData_SO.love;
        playerPersisData.girlfriendId = playerData_SO.girlfriendId;
        playerPersisData.currProfessorIdx = playerData_SO.currProfessorIdx;
        playerPersisData.isFailed = playerData_SO.isFailed;
        playerPersisData.isStudied = playerData_SO.isStudied;
        playerPersisData.isFoundHidden = playerData_SO.isFoundHidden;

        string JsonData = JsonUtility.ToJson(playerPersisData, true);
        string path = GetPathFromSaveFile();
        using (FileStream stream = File.Open(path, FileMode.Create))
        {

            byte[] byteData = Encoding.UTF8.GetBytes(JsonData);

            stream.Write(byteData, 0, byteData.Length);

            stream.Close();

            Debug.Log("PlayerDataMgr: PLAYER_DATA (CACHE->PERSIS) COMPLETE \n " + path);
        }

    }

    // 플레이어데이터가 있는지 없는지. => 보통 Init_PlayerData() 하기전에 검사용
    public static bool isPlayerDataExist()
    {
        if (File.Exists(GetPathFromSaveFile())) return true;
        else return false;
    }

    #endregion


    #region PRIVATE METHODS

    // Helper Function
    private static string GetPathFromSaveFile()
    {
        return Path.Combine(Application.persistentDataPath, "PlayerData.json");
    }

    #endregion
}
