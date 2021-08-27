using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GenericData
{
    public List<ItemData> ConsumeItemList = new List<ItemData>();
    public List<ItemData> OtherItemList = new List<ItemData>();
    public List<ItemData> ChallengeItemList = new List<ItemData>();
    public List<NPC_Generic> NPC = new List<NPC_Generic>();
    public List<Professor_Generic> Professor = new List<Professor_Generic>();
}

[System.Serializable]
public class ItemData
{
    public Sprite sprite;
    public string code;
    public string name;
    public string description;
    public int price;
    public int healAmount;
}


[System.Serializable]
public class NPC_Generic
{
    public int objId;
    public Sprite[] illustImg;
    public string name_kor;
    public string place;
    public bool isMan;
}

[System.Serializable]
public class Professor_Generic
{
    public int objId;
    public Sprite[] illustImg;
    public string name_kor;
    public string place;
    public int minigame;

}