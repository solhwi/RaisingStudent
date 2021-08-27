using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GenericData_SO", fileName = "GenericData_SO", order = 0)]
public class GenericData_SO : ScriptableObject
{
    public List<ItemData> ConsumeItemList = new List<ItemData>();
    public List<ItemData> OtherItemList = new List<ItemData>();
    public List<ItemData> ChallengeItemList = new List<ItemData>();
    public List<NPC_Generic> NPC = new List<NPC_Generic>();
    public List<Professor_Generic> Professor = new List<Professor_Generic>();

    [SerializeField] public int FixedMainQuestCount = 64;
    [SerializeField] public int FixedNormalQuestCount = 32;
    [SerializeField] public int FixedRepeatQuestCount = 6;
    [SerializeField] public int FixedChallengeQuestCount = 6;
    [SerializeField] public int FixedNPCCount = 15;
    [SerializeField] public int FixedProfessorCount = 4;
    [SerializeField] public int FixedGradeCount = 8;
    [SerializeField] public int FixedInventoryCount = 9;


    public Sprite GetSpriteById(int id, int spriteIdx)
    {
        if (id < 90000) return NPC.Find((n) => n.objId == id).illustImg[spriteIdx];
        else return Professor.Find((p) => p.objId == id).illustImg[spriteIdx];
    }

    public string GetNPCNameById(int id) => NPC.Find((n) => n.objId == id).name_kor;
    public NPC_Generic GetNPCPlaceById(int id) => NPC.Find((n) => n.objId == id);

    public ItemData GetItemByCode(string code)
    {
        if (ConsumeItemList.Find((i) => i.code == code) != null) return ConsumeItemList.Find((i) => i.code == code);
        else if (OtherItemList.Find((i) => i.code == code) != null) return OtherItemList.Find((i) => i.code == code);
        else if (ChallengeItemList.Find((i) => i.code == code) != null) return ChallengeItemList.Find((i) => i.code == code);
        else return null;
    }

    public string GetItemNameByCode(string code)
    {
        if (ConsumeItemList.Find((i) => i.code == code) != null) return ConsumeItemList.Find((i) => i.code == code).name;
        else if (OtherItemList.Find((i) => i.code == code) != null) return OtherItemList.Find((i) => i.code == code).name;
        else if (ChallengeItemList.Find((i) => i.code == code) != null) return ChallengeItemList.Find((i) => i.code == code).name;
        else return null;
    }

    public int GetItemTypebyCode(string code) // 코드를 이용해 아이템 타입 획득
    {
        if (ConsumeItemList.Find((i) => i.code == code) != null) return 0;
        else if (OtherItemList.Find((i) => i.code == code) != null) return 1;
        else if (ChallengeItemList.Find((i) => i.code == code) != null) return 2;
        else return -1;
    }

}
