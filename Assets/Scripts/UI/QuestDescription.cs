using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestDescription : MonoBehaviour
{
    [Header("Set In Editor")]
    [SerializeField] public GameObject Description;
    [SerializeField] public Text questName;
    [SerializeField] public Text questDescription;
    [SerializeField] public Text questProgress;
    [SerializeField] public Text nextNPCname;
    [SerializeField] public Text questRewards;
    [SerializeField] public Text questItemForTalk;


    [Header("Set In Runtime")]
    [HideInInspector] QuestDataContainer mainQuests;
    [HideInInspector] QuestData mainQuestData;
    [HideInInspector] QuestData normalQuestData;

    public void SetQuestForDescription(QuestDataContainer mainQuests, QuestData mainQuestData, QuestData normalQuestData)
    {
        this.mainQuests = mainQuests;
        this.mainQuestData = mainQuestData;
        this.normalQuestData = normalQuestData;
    }

    public void SetMainQuestDescription()
    {
        int idx = PlayerDataMgr.playerData_SO.mainQuestProgress;

        if (!TempQuestDatasMgr.tempQuestDatas_SO.CheckCanProcessMainQuestByIdx(idx)) return;

        TalkData talkData = mainQuestData.QuestContext[TempQuestDatasMgr.tempQuestDatas_SO.GetMainQuestProgressByIdx(idx)];

        Description.SetActive(true);

        SetDescription(mainQuestData.QuestName, mainQuestData.QuestDescription,
        talkData.ObjName, GenericDataMgr.genericData_SO.GetNPCPlaceById(talkData.ObjId),
        TempQuestDatasMgr.tempQuestDatas_SO.GetMainQuestProgressByIdx(idx), mainQuestData.QuestContext.Count,
        GenericDataMgr.genericData_SO.GetItemNameByCode(talkData.ItemCodeReward),
        talkData.GoldReward, GenericDataMgr.genericData_SO.GetItemNameByCode(talkData.ItemForTalk)
        );
    }

    public void SetNormalQuestDescripton(int idx, bool isRepeat = false)
    {
        if (!isRepeat) idx += 4 * PlayerDataMgr.playerData_SO.totalGradeProgress; // idx 0이면 0

        normalQuestData = TempQuestDatasMgr.tempQuestDatas_SO.GetNormalQuestByIdx(idx);
        if (normalQuestData == null) return;

        int tempIdx;
        if ((tempIdx = TempQuestDatasMgr.tempQuestDatas_SO.GetNormalQuestProgressByIdx(idx)) == -1) return;

        TalkData talkData = normalQuestData.QuestContext[tempIdx];

        Description.SetActive(true);

        SetDescription(normalQuestData.QuestName, normalQuestData.QuestDescription,
        talkData.ObjName, GenericDataMgr.genericData_SO.GetNPCPlaceById(talkData.ObjId),
        TempQuestDatasMgr.tempQuestDatas_SO.GetNormalQuestProgressByIdx(idx), normalQuestData.QuestContext.Count,
        GenericDataMgr.genericData_SO.GetItemNameByCode(talkData.ItemCodeReward),
        talkData.GoldReward, GenericDataMgr.genericData_SO.GetItemNameByCode(talkData.ItemForTalk)
        );

    }

    public void SetDescription(string questname, string questdescription, string npcname, NPC_Generic npc, int nowProgress,
    int totalProgress, string itemName, int gold, string itemNameForTalk)
    {
        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.button);
        questName.text = questname;
        questDescription.text = questdescription;
        nextNPCname.text = npc != null ? $"{npcname} ( {npc.place} )" : $"{npcname}";
        questProgress.text = (nowProgress + 1) + " / " + (totalProgress + 1);
        if (itemName == null) itemName = "?";
        questRewards.text = $"골드 {gold} / {itemName}";
        questItemForTalk.text = $"챙겨가야 할 것?   {itemNameForTalk}";
    }

}
