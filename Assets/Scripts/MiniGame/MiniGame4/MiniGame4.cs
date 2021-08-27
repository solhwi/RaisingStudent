using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MiniGame4 : MonoBehaviour
{
    [Header("Set In Editor")]
    [SerializeField] MiniGamePopup miniGamePopup;
    [SerializeField] MiniGameAnswerImage miniGameAnswerImage;
    [SerializeField] MiniGameQuestion question;
    [SerializeField] MiniGame4Popup miniGame4Popup;
    [SerializeField] Text titleText;

    StageData stageData;
    quizSet quizSets;

    List<string> questions = new List<string>();
    List<string> answers = new List<string>();
    List<bool> boolAnswers = new List<bool>();

    int answerCount = 0;
    int questCount = 0;

    void Awake()
    {
        titleText.text = PlayerDataMgr.playerData_SO.GetSubjectNameByProgress(PlayerDataMgr.playerData_SO.totalGradeProgress);
        stageData = StageDataMgr.LoadSingleStageData(PlayerDataMgr.playerData_SO.totalGradeProgress);
        Set_Quiz(PlayerDataMgr.playerData_SO.stageProgress);
    }

    void Set_Quiz(int _quizOrder)
    {
        int count = 0;
        for (int i = _quizOrder + 1; i < stageData.stageOrder.Length; i++)
            if (stageData.stageOrder[i] == 3)
                count++;

        switch (count)
        {
            case 0: quizSets = stageData.quizSets[3]; break;
            case 1: quizSets = stageData.quizSets[2]; break;
            case 2: quizSets = stageData.quizSets[1]; break;
            case 3: quizSets = stageData.quizSets[0]; break;
            default:
                Debug.Log("Set_Quiz함수가 스위치문에서 범위를 벗어남");
                quizSets = stageData.quizSets[0]; break;
        }

        questions = quizSets.questions;
        answers = quizSets.answers;
        boolAnswers = quizSets.boolAnswers;

        question.Input_Question(questions[0]);
    }

    public void Select_Question(bool choose)
    {
        if (miniGame4Popup.gameObject.activeSelf || MiniGameMgr.miniGameMgr.Lock)
            return;

        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.button);

        if (choose == boolAnswers[questCount])
        {
            answerCount++;
            miniGameAnswerImage.Change_NoAnswerImage(true, questCount);
            miniGame4Popup.Set_Popup(true, answers[questCount]);
        }
        else
        {
            miniGameAnswerImage.Change_NoAnswerImage(false, questCount);
            miniGame4Popup.Set_Popup(false, answers[questCount]);
        }
        miniGame4Popup.gameObject.SetActive(true);
    }

    public void OnClick_Check()
    {
        questCount++;
        if (questCount == 6)
            if (answerCount >= 4)
            {
                SFXMgr.Instance.OverlapPlay_SFX(SFXMgr.SFXName.win);
                miniGamePopup.OnClickPopup(true, "클리어 성공! (" + answerCount + "/6)");
            }
            else
            {
                SFXMgr.Instance.OverlapPlay_SFX(SFXMgr.SFXName.lose);
                miniGamePopup.OnClickPopup(false, "클리어 실패!(" + answerCount + "/6)");
            }
        else
            question.Input_Question(questions[questCount]);

        miniGame4Popup.gameObject.SetActive(false);
    }
}
