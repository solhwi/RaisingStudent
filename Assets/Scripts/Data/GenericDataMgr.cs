using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class GenericDataMgr
{
    public static GenericData_SO genericData_SO = Resources.Load<GenericData_SO>("GenericData_SO");

    public static string GetPathFromSaveFile()
    {
        return Path.Combine(Application.persistentDataPath, "genericData.json");
    }

    public static void Sync_Persis_To_Cache()
    {
        TextAsset jsonData = Resources.Load<TextAsset>("genericData");
        GenericData genericPersisData = JsonUtility.FromJson<GenericData>(jsonData.ToString());

        genericData_SO.NPC.Clear();
        genericData_SO.Professor.Clear();
        genericData_SO.ConsumeItemList.Clear();
        genericData_SO.OtherItemList.Clear();
        genericData_SO.ChallengeItemList.Clear();

        foreach (NPC_Generic n in genericPersisData.NPC) genericData_SO.NPC.Add(n);
        foreach (Professor_Generic p in genericPersisData.Professor) genericData_SO.Professor.Add(p);
        foreach (ItemData d in genericPersisData.ConsumeItemList) genericData_SO.ConsumeItemList.Add(d);
        foreach (ItemData d in genericPersisData.ConsumeItemList) genericData_SO.OtherItemList.Add(d);
        foreach (ItemData d in genericPersisData.ChallengeItemList) genericData_SO.ChallengeItemList.Add(d);


        Debug.Log("GenericDataMgr: GENERIC_DATA (PERSIS->CACHE) COMPLETE");

    }

    public static void Sync_Cache_To_Persis()
    {
        GenericData genericPersisData = new GenericData();

        genericPersisData.NPC.Clear();
        genericPersisData.Professor.Clear();
        genericPersisData.ConsumeItemList.Clear();
        genericPersisData.OtherItemList.Clear();
        genericPersisData.ChallengeItemList.Clear();

        foreach (NPC_Generic n in genericData_SO.NPC) genericPersisData.NPC.Add(n);
        foreach (Professor_Generic p in genericData_SO.Professor) genericPersisData.Professor.Add(p);
        foreach (ItemData d in genericData_SO.ConsumeItemList) genericPersisData.ConsumeItemList.Add(d);
        foreach (ItemData d in genericData_SO.ConsumeItemList) genericPersisData.OtherItemList.Add(d);
        foreach (ItemData d in genericData_SO.ChallengeItemList) genericPersisData.ChallengeItemList.Add(d);

        string JsonData = JsonUtility.ToJson(genericPersisData, true);
        string path = GetPathFromSaveFile();
        using (FileStream stream = File.Open(path, FileMode.Create))
        {

            byte[] byteData = Encoding.UTF8.GetBytes(JsonData);

            stream.Write(byteData, 0, byteData.Length);

            stream.Close();

            Debug.Log("GenericDataMgr: GENERIC_DATA (CACHE->PERSIS) COMPLETE \n" + path);
        }
    }

}
