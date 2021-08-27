using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum Challenge
{
    PigKing, // 돼지왕
    HiddenRoadFinder, // 숨은 길찾기 마스터
    NormalQuestMaster, // 일반 퀘스트 마스터
    StudyBug, // 공부벌레
    Love, // 사랑꾼
    YoungNRich // 영앤리치
}

public class ChallengeMgr : MonoBehaviour
{
    [Header("Set In Editor")]
    [SerializeField] public List<int> CountForClears = new List<int>();
    [SerializeField] public List<bool> badgeLocks = new List<bool>();

    void Start()
    {
        SetChallengeCount(Challenge.PigKing);
        SetChallengeCount(Challenge.HiddenRoadFinder);
        SetChallengeCount(Challenge.NormalQuestMaster);
        SetChallengeCount(Challenge.StudyBug);
        SetChallengeCount(Challenge.Love);
        SetChallengeCount(Challenge.YoungNRich);
    }

    public void ChallengeQuestClear(Challenge id)
    {
        int idx = (int)id;

        if (idx >= badgeLocks.Count) return;
        //if (badgeLocks[idx] == true) return; // 한번 지급된 뱃지라면 리턴

        switch (id)
        { // 인덱스에 해당하는 보상 지급하기
            case Challenge.PigKing:
                PlayerDataMgr.playerData_SO.haveBadgeItems[0].item.name = "돼지왕";
                break;
            case Challenge.HiddenRoadFinder:
                PlayerDataMgr.playerData_SO.haveBadgeItems[1].item.name = "길찾기마스터";
                break;
            case Challenge.NormalQuestMaster:
                PlayerDataMgr.playerData_SO.haveBadgeItems[2].item.name = "봉사왕";
                break;
            case Challenge.StudyBug:
                PlayerDataMgr.playerData_SO.haveBadgeItems[3].item.name = "공부벌레";
                break;
            case Challenge.Love:
                PlayerDataMgr.playerData_SO.haveBadgeItems[4].item.name = "사랑꾼";
                PlayerDataMgr.playerData_SO.haveBadgeItems[4].item.description = $"내 마음을 담아... 사랑합니다 ({GenericDataMgr.genericData_SO.GetNPCNameById(PlayerDataMgr.playerData_SO.girlfriendId)})"; ;
                break;
            case Challenge.YoungNRich:
                PlayerDataMgr.playerData_SO.haveBadgeItems[5].item.name = "영앤리치";
                break;
            default:
                break;
        }

        badgeLocks[idx] = true; // 클리어 시 해당 업적 lock
        PlayerDataMgr.playerData_SO.challengeQuestCounts[idx] = 100000;
        PlayerDataMgr.Sync_Cache_To_Persis();
    }

    public void SetChallengeCount(Challenge id, int amount = 0)
    {
        int idx = (int)id;
        int countForClear = CountForClears[idx];

        PlayerDataMgr.playerData_SO.challengeQuestCounts[idx] += amount; // 해당하는 플레이어의 챌린지 카운트를 올리기

        if (countForClear <= PlayerDataMgr.playerData_SO.challengeQuestCounts[idx]) // 챌린지 클리어 조건보다 많거나 같아지면,
        {
            ChallengeQuestClear(id); // 해당하는 업적 클리어
        }
    }
}