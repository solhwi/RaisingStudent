using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class TempQuestDatasMgr
{
    public static TempQuestDatas_SO tempQuestDatas_SO = Resources.Load<TempQuestDatas_SO>("TempQuestDatas_SO");

    #region PUBLIC METHODS
    public static void Init_TempQuestData()
    {
        TempQuestDatas data = new TempQuestDatas();

        string JsonData = JsonUtility.ToJson(data, true);

        string path = GetPathFromSaveFile();
        using (FileStream stream = File.Open(path, FileMode.Create))
        {

            byte[] byteData = Encoding.UTF8.GetBytes(JsonData);

            stream.Write(byteData, 0, byteData.Length);

            stream.Close();

            Sync_Persis_To_Cache();
            Debug.Log("TempQuestDataMgr: INIT COMPLETE - " + path);
        }
    }

    public static void Sync_Persis_To_Cache()
    {
        TempQuestDatas QuestPersisData;
        string path = GetPathFromSaveFile();
        using (FileStream stream = File.Open(path, FileMode.Open))
        {

            byte[] byteData = new byte[stream.Length];

            stream.Read(byteData, 0, byteData.Length);

            stream.Close();

            string JsonData = Encoding.UTF8.GetString(byteData);

            QuestPersisData = JsonUtility.FromJson<TempQuestDatas>(JsonData);

        }
        //들어갈 cache 초기화

        tempQuestDatas_SO.tempMainQuestDatas.Clear();
        tempQuestDatas_SO.tempNormalQuestDatas.Clear();
        
        TempQuestDatas tmp = new TempQuestDatas(); 

        for (int i = 0; i < QuestPersisData.tempNormalQuestDatas.Count; i++)
        {
            QuestPersisData.tempNormalQuestDatas[i].ObjIds = tmp.tempNormalQuestDatas[i].ObjIds;
            tempQuestDatas_SO.tempNormalQuestDatas.Add(QuestPersisData.tempNormalQuestDatas[i]);
        }

        for(int i=0; i< QuestPersisData.tempMainQuestDatas.Count; i++)
        {
            QuestPersisData.tempMainQuestDatas[i].ObjIds = tmp.tempMainQuestDatas[i].ObjIds;
            tempQuestDatas_SO.tempMainQuestDatas.Add(QuestPersisData.tempMainQuestDatas[i]);
        }

        foreach (TempQuestData t in QuestPersisData.tempNormalQuestDatas)
        {
           
        }

        foreach (TempQuestData t in QuestPersisData.tempMainQuestDatas)
        {
            //tempQuestDatas_SO.tempMainQuestDatas.Add(t);
        }

        Debug.Log("QuestDataMgr: PLAYER_DATA (PERSIS->CACHE) COMPLETE \n " + path);
    }

    public static void Sync_Cache_To_Persis()
    {
        TempQuestDatas QuestPersisData = new TempQuestDatas();

        QuestPersisData.tempMainQuestDatas.Clear();
        QuestPersisData.tempNormalQuestDatas.Clear();

        foreach (TempQuestData n in tempQuestDatas_SO.tempMainQuestDatas)
        {
            QuestPersisData.tempMainQuestDatas.Add(n);
        }
        foreach (TempQuestData n in tempQuestDatas_SO.tempNormalQuestDatas)
        {
            QuestPersisData.tempNormalQuestDatas.Add(n);
        }

        string JsonData = JsonUtility.ToJson(QuestPersisData, true);
        string path = GetPathFromSaveFile();
        using (FileStream stream = File.Open(path, FileMode.Create))
        {

            byte[] byteData = Encoding.UTF8.GetBytes(JsonData);

            stream.Write(byteData, 0, byteData.Length);

            stream.Close();

            Debug.Log("TempQuestDataMgr: PLAYER_DATA (CACHE->PERSIS) COMPLETE \n " + path);
        }

    }

    #endregion


    #region PRIVATE METHODS

    // Helper Function
    private static string GetPathFromSaveFile()
    {
        return Path.Combine(Application.persistentDataPath, "TempQuestData.json");
    }

    #endregion
}
