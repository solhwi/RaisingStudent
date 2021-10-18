using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame5 : MonoBehaviour
{
    [Header("Set In Editor")]
    [SerializeField] MiniGamePopup miniGamePopup;
    [SerializeField] MiniGamePause miniGamePause;
    [SerializeField] TimeSlider timeSlider;
    [SerializeField] WordSlots wordSlots;

    [Header("Set In Runtime")]
    public Button[] wordButton = new Button[12];

    int getTime = 0;
    int deleteCount = 0;
    int currClearCount = 0;
    int firstNum = 0;
    int[,] wordNum = new int[8, 6] {
        {0,0,0,0,0,0}, {0,1,1,3,3,2}, {0,1,3,3,2,2}, {0,1,3,1,3,2},
        {1,2,0,3,0,0}, {2,1,0,4,5,4}, {5,2,1,4,0,5}, {2,4,5,4,3,5},
     };

    int clearCount = 3;
    float increaseTime = 10f;

    bool firstSelect = false;
    bool Stop = false;

    void Update()
    {
        if (!MiniGameMgr.miniGameMgr.isTikToking && timeSlider.currTime <= 0.1f && !MiniGameMgr.miniGameMgr.Lock)
        {
            SFXMgr.Instance.OverlapPlay_SFX(SFXMgr.SFXName.lose);
            miniGamePopup.OnClickPopup(false, "클리어 실패!");
        }
        if (miniGamePause.transform.Find("Pause").gameObject.activeSelf && !Stop)
        {
            Stop = true;
            wordSlots.Hiding_Words(true);
        }

        if (Stop && !miniGamePause.transform.Find("Pause").gameObject.activeSelf)
        {
            Stop = false;
            wordSlots.Hiding_Words(false);
        }
    }

    void Awake()
    {
        StageCheck();
        timeSlider.SettingTime(getTime);

        for (int i = 0; i < 12; i++)
        {
            int temp = i;
            wordButton[temp] = transform.Find("WordSlots").GetChild(temp).gameObject.GetComponent<Button>();
            wordButton[temp].onClick.AddListener(() => OnClick_Word(temp));
        }
    }

    void StageCheck()
    {
        switch (PlayerDataMgr.playerData_SO.totalGradeProgress)
        {
            case 0: getTime = 15; clearCount = 2; increaseTime = 10f; break;
            case 1: getTime = 14; clearCount = 3; increaseTime = 10f; break;
            case 2: getTime = 13; clearCount = 3; increaseTime = 10f; break;
            case 3: getTime = 12; clearCount = 4; increaseTime = 10f; break;
            case 4: getTime = 11; clearCount = 4; increaseTime = 9f; break;
            case 5: getTime = 10; clearCount = 5; increaseTime = 9f; break;
            case 6: getTime = 9; clearCount = 5; increaseTime = 9f; break;
            case 7: getTime = 10; clearCount = 6; increaseTime = 9f; break;
            default:
                Debug.Log("StageCheck 스위치문에서 범위를 벗어남");
                getTime = 21;
                break;
        }
        if (PlayerDataMgr.playerData_SO.totalGradeProgress == 0 && PlayerDataMgr.playerData_SO.dayProgress < 2)
        {
            getTime = 25;
        }
    }

    public void OnClick_Word(int index)
    {
        if (MiniGameMgr.miniGameMgr.Lock || wordSlots.SlotText_NullCheck(index))
            return;

        if (!firstSelect) // 처음 선택 여부
        {
            SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.button);
            firstSelect = true;
            firstNum = index;
            wordSlots.Select_WordSlot(firstNum, true);
            return;
        }

        firstSelect = false;

        if (firstNum == index) // 같은 버튼 다시 클릭
        {
            SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.button);
            wordSlots.Select_WordSlot(firstNum, false);
            return;
        }

        if (wordSlots.Compare_Words(firstNum, index)) // 다른 버튼 클릭
        {
            deleteCount++;
        }
        else
        {
            timeSlider.SettingTime(timeSlider.currTime - 2f);
            return;
        }

        if (deleteCount >= 6) // 단어를 다 맞췄는가?
        {
            currClearCount++;
            if (currClearCount == clearCount) // 클리어
            {
                SFXMgr.Instance.OverlapPlay_SFX(SFXMgr.SFXName.win);
                miniGamePopup.OnClickPopup(true, "클리어 성공!");
                return;
            }

            timeSlider.SettingTime(increaseTime);
            wordSlots.Create_words(wordNum[PlayerDataMgr.playerData_SO.totalGradeProgress, currClearCount]);
            deleteCount = 0;
        }
    }
}
