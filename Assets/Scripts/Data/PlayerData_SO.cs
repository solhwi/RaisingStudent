using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerData_SO", fileName = "PlayerData_SO", order = 1)]
public class PlayerData_SO : ScriptableObject
{
    public string username;
    public bool isStudied;
    public bool isFoundHidden;

    public int totalGradeProgress; // 총 8 학기
    public int dayProgress; // 한 학기에 30일
    public int stageProgress; // 한 학기에 26 스테이지
    public int mainQuestProgress; // 메인 퀘스트 0 ~ 64
    public bool isEndedGrade;
    public bool isFailed;
    public bool love;
    public int girlfriendId;
    public bool isEndedTutorial;
    public int tutorial_progress;

    public int attendCount; // 오늘 출석헀는가? 침대에 누울떄 이 값이 false면 해당 학기 교수님과의 호감도가 대폭 하락
    public int clearCount; // 오늘자 미니게임을 클리어했는가? 클리어했다면 호감도가 상승, 클리어를 못했다면 소폭 하락   
    public bool isMan; // 캐릭터 성별
    public int currProfessorIdx;

    public string currentMapName; // 현재 맵 위치, save n load에 사용됨
    public string prevMapName; // 맵 위치 이동에 사용됨
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

    public int GetDayOfWeek() => dayProgress % 3;
    public int GetWeekProgress() => dayProgress / 3;
    public bool IsWeekend() => GetDayOfWeek() >= 2 ? true : false;

    public void GetSatisfact(int amount) => satisfact = satisfact + amount <= 100 ? satisfact + amount : 100;
    public void UseHungryGazy(int amount) => hungryGazy -= hungryGazy - amount > 0 ? amount : hungryGazy;
    public void AddGold(int amount)
    {
        gold += amount;
        if (GameMgr.challengeMgr.CountForClears[(int)Challenge.YoungNRich] < gold)
        {
            GameMgr.challengeMgr.ChallengeQuestClear(Challenge.YoungNRich);
        }
        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.coin);
    }
    public void UseGold(int amount)
    {
        if (gold >= amount) gold -= amount;
        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.coin);
    }
    public void GiveLikePoint(int id, int amount) // 호감도 올리기
    {
        NPCLike npclike = npcLikes.Find((n) => n.id == id);
        if (npclike != null)
        {
            npclike.like += amount;

            if (npclike.like > GameMgr.challengeMgr.CountForClears[(int)Challenge.Love])
            {
                if (girlfriendId == -1)
                {
                    girlfriendId = id;
                    PlayerDataMgr.Sync_Cache_To_Persis();
                    TempQuestDatasMgr.Sync_Cache_To_Persis();
                }
            }
        }
        if (professorLikes.Find((p) => p.id == id) != null) professorLikes.Find((p) => p.id == id).like += amount;

    }

    public int GetItemCountByCode(string code) // 내가 아이템을 몇 개 가지고 있는 지 알고 싶을 때
    {
        if (haveConsumeItems.Find((i) => i.item.code == code) != null) return haveConsumeItems.Find((i) => i.item.code == code).count;
        else if (haveOtherItems.Find((i) => i.item.code == code) != null) return haveOtherItems.Find((i) => i.item.code == code).count;
        else if (haveBadgeItems.Find((i) => i.item.code == code) != null) return haveBadgeItems.Find((i) => i.item.code == code).count;
        else return -1;
    }

    public int GetItemIdxbyCode(string code) // 코드를 이용해 아이템 획득 시 사용
    {
        for (int i = 0; i < haveConsumeItems.Count; i++)
            if (haveConsumeItems[i].item.code == code) return i;
        for (int i = 0; i < haveOtherItems.Count; i++)
            if (haveOtherItems[i].item.code == code) return i;
        for (int i = 0; i < haveBadgeItems.Count; i++)
            if (haveBadgeItems[i].item.code == code) return i;

        return -1;
    }


    public bool AddItemByCode(string code) // 코드를 이용한 아이템 획득
    {
        int idx = GetItemIdxbyCode(code);

        switch (GenericDataMgr.genericData_SO.GetItemTypebyCode(code))
        {
            case 0:
                if (haveConsumeItems.Count >= GenericDataMgr.genericData_SO.FixedInventoryCount) return false; // 인벤토리 full

                if (0 < GetItemCountByCode(code)) haveConsumeItems[idx].count++;
                else haveConsumeItems.Add(new HaveItemData(GenericDataMgr.genericData_SO.GetItemByCode(code)));
                return true;
            case 1:
                if (haveOtherItems.Count >= GenericDataMgr.genericData_SO.FixedInventoryCount) return false; // 인벤토리 full

                if (0 < GetItemCountByCode(code)) haveOtherItems[idx].count++;
                else haveOtherItems.Add(new HaveItemData(GenericDataMgr.genericData_SO.GetItemByCode(code)));
                return true;
            case 2:
                if (0 < GetItemCountByCode(code)) haveBadgeItems[idx].count++;
                else haveBadgeItems.Add(new HaveItemData(GenericDataMgr.genericData_SO.GetItemByCode(code)));
                return true;
            default: // 잘못된 형식의 아이템
                return false;
        }
    }

    public void UseItemByCode(string code) // 코드를 이용한 아이템 사용
    {
        if (code == null || code == "") return;

        int idx = GetItemIdxbyCode(code);

        switch (GenericDataMgr.genericData_SO.GetItemTypebyCode(code))
        {
            case 0:
                hungryGazy += haveConsumeItems[idx].item.healAmount;
                if (code == "CAKE") GameMgr.challengeMgr.SetChallengeCount(Challenge.PigKing, 1); // 업적
                if (1 < GetItemCountByCode(code)) haveConsumeItems[idx].count--;
                else if (GetItemCountByCode(code) == 1) haveConsumeItems.RemoveAt(idx);

                break;
            case 1:
                if (1 < GetItemCountByCode(code)) haveOtherItems[idx].count--;
                else if (GetItemCountByCode(code) == 1) haveOtherItems.RemoveAt(idx);
                break;
            case 2:
                if (1 < GetItemCountByCode(code)) haveBadgeItems[idx].count--;
                else if (GetItemCountByCode(code) == 1) haveBadgeItems.RemoveAt(idx);
                break;
            default:
                break;
        }
    }

    public bool SetNextDay()
    {
        if (!IsWeekend()) // 평일
        {
            if (!CheckTodayGrade()) return false; // 출석 안 한 경우

            switch (GetWeekProgress())
            {
                case 1: case 3: case 6: case 8: dayProgress++; break; // 퀴즈
                case 4: case 9: dayProgress += 2; break; // 중간, 기말
                default: break;
            }
        }
        else // 주말
        {
            if (TempQuestDatasMgr.tempQuestDatas_SO.GetMainQuestIsClearByIdx(mainQuestProgress)) mainQuestProgress++;
            else return false;
        }

        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.alarm);
        DayReward();
        return true;
    }

    void DayReward()
    {
        dayProgress++; // 하루 증가

        attendCount = 0;
        clearCount = 0;
        hungryGazy = 100;
        isStudied = false;

        PlayerDataMgr.Sync_Cache_To_Persis();
        TempQuestDatasMgr.Sync_Cache_To_Persis();

        if (dayProgress >= 28)
        {
            isEndedGrade = true;
            SetForVacation();
        }
    }

    void SetForVacation()
    {
        isFoundHidden = false;

        int grade = 0;

        for (int i = 0; i < professorLikes.Count; i++) grade += professorLikes[i].like; // 교수님 호감도로 성적 산출 준비

        if (grade > 24) grades[totalGradeProgress] = 4;
        else if (grade > 17) grades[totalGradeProgress] = 3;
        else if (grade > 12) grades[totalGradeProgress] = 2;
        else grades[totalGradeProgress] = 1;

        GetSatisfact(grade / 4); // 만족도 비례 상승

        for (int i = 0; i < professorLikes.Count; i++) professorLikes[i].like = 0; // 교수님 호감도로 성적 산출 준비

        totalGradeProgress++;
        dayProgress = 0;
        stageProgress = 0;

        PlayerDataMgr.Sync_Cache_To_Persis();
        TempQuestDatasMgr.Sync_Cache_To_Persis();
    }

    public bool CheckTodayGrade()
    {
        if (attendCount < 2) return false;

        int temp = 0;

        if (clearCount >= 2) temp = 2;
        else if (clearCount == 1) temp = 1;
        else temp = 0;

        GiveLikePoint(GenericDataMgr.genericData_SO.Professor[currProfessorIdx].objId, temp);
        GetSatisfact(temp);

        return true;
    }

    public string GetSubjectNameByProgress(int progress)
    {
        switch (progress)
        {
            case 0: return "파이썬";
            case 1: return "C언어";
            case 2: return "C++";
            case 3: return "자료구조론";
            case 4: return "알고리즘";
            case 5: return "운영체제";
            case 6: return "데이터베이스";
            case 7: return "컴퓨터네트워크";
            default: return null;
        }
    }
    public string GetMapname()
    {
        switch (currentMapName)
        {
            case "House": return "집";
            case "HallWay1": return "집 복도 1층";
            case "HallWay2": return "집 복도 2층";
            case "HouseFront": return "집 앞";
            case "Entrance": return "홍문관 입구";
            case "Tdong1": return "T동 1층";
            case "Tdong3": return "T동 3층";
            case "Tdong5": return "T동 5층";
            case "Tdong6": return "T동 6층";
            case "Tdong7": return "T동 7층";
            default: return "알 수 없는 위치";
        }
    }
    public string GetDate()
    {
        string Date = null;

        switch (totalGradeProgress)
        {
            case 0: Date = "1-1"; break;
            case 1: Date = "1-2"; break;
            case 2: Date = "2-1"; break;
            case 3: Date = "2-2"; break;
            case 4: Date = "3-1"; break;
            case 5: Date = "3-2"; break;
            case 6: Date = "4-1"; break;
            case 7: Date = "4-2"; break;
            default: break;
        }
        Date = Date + " " + (GetWeekProgress() + 1).ToString() + "주차 ";
        switch (GetDayOfWeek())
        {
            case 0: Date += "월"; break;
            case 1: Date += "목"; break;
            case 2: Date += "토"; break;
            default: break;
        }

        return Date;
    }

    public string GetSemester()
    {
        string semester;

        semester = (((int)totalGradeProgress / 2) + 1).ToString();
        semester += "학년 ";

        semester += (((int)totalGradeProgress % 2) + 1).ToString();
        semester += "학기";

        return semester;
    }

    public float GetTotalGrade()
    {
        float total = 0;
        foreach (int i in grades)
        {
            total += i;
        }
        return total / 8;
    }

}

