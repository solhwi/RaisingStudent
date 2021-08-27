using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MiniGame7 : MonoBehaviour
{
    [Header("Set In Editor")]
    [SerializeField] MiniGamePopup miniGamePopup;
    [SerializeField] Text text;
    [SerializeField] Text titleText;
    [SerializeField] Text contentText;

    StageData stageData;
    lectureSet lectureSets;

    string lectureName;
    bool lectureLock = false;
    int lectureCount = 0;
    int lectureLen = 0;

    void Awake()
    {
        stageData = StageDataMgr.LoadSingleStageData(PlayerDataMgr.playerData_SO.totalGradeProgress);
        text.text = PlayerDataMgr.playerData_SO.GetSemester();
        Set_Lecture(PlayerDataMgr.playerData_SO.stageProgress);
    }

    void Set_Lecture(int _lectureOrder)
    {
        int count = 0;
        for (int i = 0; i < _lectureOrder; i++)
            if (stageData.stageOrder[i] == 6)
                count++;

        switch (count)
        {
            case 1:
                lectureSets = stageData.lectureSets[0];
                lectureName = stageData.quizSets[0].quizName;
                break;
            case 2:
                lectureSets = stageData.lectureSets[1];
                lectureName = stageData.quizSets[1].quizName;
                break;
            case 3:
                lectureSets = stageData.lectureSets[2];
                lectureName = stageData.quizSets[2].quizName;
                break;
            case 4:
                lectureSets = stageData.lectureSets[3];
                lectureName = stageData.quizSets[3].quizName;
                break;
            default:
                Debug.Log("Set_Lecture함수가 스위치문에서 범위를 벗어남");
                lectureSets = stageData.lectureSets[0];
                lectureName = stageData.quizSets[0].quizName;
                break;
        }

        titleText.text = lectureName;
        lectureLen = lectureSets.texts.Count;

        OnClick_Content();
    }

    public void OnClick_Content()
    {
        if (MiniGameMgr.miniGameMgr.Lock || lectureLock)
            return;

        if (lectureCount == lectureLen)
        {
            SFXMgr.Instance.OverlapPlay_SFX(SFXMgr.SFXName.win);
            miniGamePopup.OnClickPopup(true, "수강 완료!!");
            return;
        }

        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.button);
        contentText.text = lectureSets.texts[lectureCount];
        lectureCount++;

        lectureLock = true;
        Invoke("LectureLock", 1f);
    }

    public void LectureLock()
    {
        lectureLock = false;
    }

    public void OnClick_Skip()
    {
        if (MiniGameMgr.miniGameMgr.Lock)
            return;

        SFXMgr.Instance.OverlapPlay_SFX(SFXMgr.SFXName.lose);
        contentText.text = lectureSets.texts[lectureLen - 1];
        miniGamePopup.OnClickPopup(false, "스킵! ( 추가점수 X )");
    }
}