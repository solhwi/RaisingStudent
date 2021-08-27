using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attend : MonoBehaviour
{
    string str;

    void Awake()
    {
        str = "아직 듣지 않은 수업이 있어요! [ ";
    }

    void Update()
    {
        if (PlayerDataMgr.playerData_SO.GetDayOfWeek() == 2)
        {
            if (!TempQuestDatasMgr.tempQuestDatas_SO.GetMainQuestIsClearByIdx(PlayerDataMgr.playerData_SO.mainQuestProgress))
            {
                gameObject.GetComponent<Text>().text = "아직 메인 퀘스트가 클리어되지 않았어요!";
                gameObject.GetComponent<Text>().color = new Color(1f, 0f, 0f, 1f);
                gameObject.GetComponent<CanvasGroup>().alpha = 1f;
            }
            else
            {
                gameObject.GetComponent<Text>().text = "모든 퀘스트를 클리어했어요! 이제 자도 돼요!";
                gameObject.GetComponent<Text>().color = new Color(0f, 0f, 1f, 1f);
                gameObject.GetComponent<CanvasGroup>().alpha = 1f;
            }
        }
        else
        {
            if (PlayerDataMgr.playerData_SO.attendCount < 2)
            {
                gameObject.GetComponent<Text>().text = str + GenericDataMgr.genericData_SO.Professor[PlayerDataMgr.playerData_SO.currProfessorIdx].name_kor + "," +
                GenericDataMgr.genericData_SO.Professor[PlayerDataMgr.playerData_SO.currProfessorIdx].place + " ]";
                gameObject.GetComponent<Text>().color = new Color(1f, 0f, 0f, 1f);
                gameObject.GetComponent<CanvasGroup>().alpha = 1f;
            }
            else
            {
                gameObject.GetComponent<Text>().text = "모든 수업을 들었어요! 이제 자도 돼요!";
                gameObject.GetComponent<Text>().color = new Color(0f, 0f, 1f, 1f);
                gameObject.GetComponent<CanvasGroup>().alpha = 1f;
            }
        }




    }
}
