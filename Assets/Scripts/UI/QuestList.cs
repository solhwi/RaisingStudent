using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestList : MonoBehaviour
{
    [Header("Set In Editor")]
    [SerializeField] public QuestDescription questDescription;


    [Header("Set In Runtime")]
    [HideInInspector] QuestDataContainer mainQuests;
    [HideInInspector] QuestData mainQuestData;
    [HideInInspector] QuestData normalQuestData;

    [Header("Fixed Data")]
    public int normalQuestCount = 38;
    public int repeatQuestCount = 6;
    public int interactiveQuestCount = 11;

    void Awake()
    {
        // 메인퀘스트 버튼 세팅
        Button tempbutton = transform.Find("ScrollArea").GetChild(0).GetChild(0).GetComponent<Button>();
        tempbutton.onClick.AddListener(() => questDescription.SetMainQuestDescription());


        // 일반퀘스트 버튼 세팅
        for (int i = 1; i <= interactiveQuestCount - repeatQuestCount; i++)
        {

            int tempInt = i;
            tempbutton = transform.Find("ScrollArea").GetChild(0).GetChild(tempInt).GetComponent<Button>();
            tempbutton.onClick.AddListener(() => questDescription.SetNormalQuestDescripton(tempInt - 1));
        }

        // 반복퀘스트 버튼 세팅s
        for (int i = 0; i < repeatQuestCount; i++)
        {
            int tempInt = i;
            tempbutton = transform.Find("ScrollArea").GetChild(0).GetChild(interactiveQuestCount - 1 - tempInt).GetComponent<Button>();
            tempbutton.onClick.AddListener(() => questDescription.SetNormalQuestDescripton(normalQuestCount - 1 - tempInt, true));
        }
    }

    public void UpdateQuestList()
    {
        UpdateMainQuest();
        UpdateNormalQuest();
        UpdateRepeatQuest();
        questDescription.SetQuestForDescription(mainQuests, mainQuestData, normalQuestData);
    }

    void UpdateMainQuest()
    {
        Text tempText = transform.Find("ScrollArea").GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<Text>();

        if (TempQuestDatasMgr.tempQuestDatas_SO.CheckCanProcessMainQuestByIdx(PlayerDataMgr.playerData_SO.mainQuestProgress))
        {
            mainQuests = QuestDataMgr.LoadSingleMainQuestData();
            mainQuestData = mainQuests.mainQuestDatas[PlayerDataMgr.playerData_SO.mainQuestProgress];

            tempText.text = mainQuestData.QuestName; // 퀘스트 리스트에 퀘스트 이름 갱신
        }
        else
        {
            tempText.text = "메인 퀘스트는 주말에 갱신됩니다.";
        }
    }

    void UpdateNormalQuest()
    {
        for (int i = 1; i < interactiveQuestCount - repeatQuestCount; i++) // 일반 퀘스트 4개
        {
            int idx = (i - 1) + PlayerDataMgr.playerData_SO.totalGradeProgress * 4;

            string QuestState = "진행 중";

            if (TempQuestDatasMgr.tempQuestDatas_SO.GetNormalQuestProgressByIdx(idx) == 0) QuestState = "미해결";
            if (TempQuestDatasMgr.tempQuestDatas_SO.tempNormalQuestDatas[idx].isClear) QuestState = "해결";

            Text tempText = transform.Find("ScrollArea").GetChild(0).GetChild(i).GetChild(0).GetComponent<Text>();
            tempText.text = TempQuestDatasMgr.tempQuestDatas_SO.GetNormalQuestNameByIdx(idx) + "( " + QuestState + " )";
        }
    }
    void UpdateRepeatQuest()
    {
        for (int i = 0; i < repeatQuestCount; i++)
        {
            Text tempText = transform.Find("ScrollArea").GetChild(0).GetChild(interactiveQuestCount - 1 - i).GetChild(0).GetComponent<Text>();
            tempText.text = TempQuestDatasMgr.tempQuestDatas_SO.GetNormalQuestNameByIdx(normalQuestCount - 1 - i) + "( 반복 가능 )";
        }
    }
}
