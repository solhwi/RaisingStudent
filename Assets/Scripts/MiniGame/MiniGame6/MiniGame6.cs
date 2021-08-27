using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame6 : MonoBehaviour
{
    [Header("Set In Editor")]
    [SerializeField] MiniGamePopup miniGamePopup;
    [SerializeField] MiniGameQuestion miniGameQuestion;
    [SerializeField] MiniGameAnswerImage miniGameAnswerImage;
    [SerializeField] QuestionItemText questionItemText;
    [SerializeField] Explanation explanation;
    [SerializeField] GameObject answerPopup;

    [Header("Set Disposable")]
    [SerializeField] Text titleText;
    [SerializeField] Text examText;
    [SerializeField] Text nextText;

    StageData stageData;
    examSet examSets;

    List<string> Questions = new List<string>();
    List<exampleSet> ExampleSets = new List<exampleSet>();
    List<int> Answers = new List<int>();

    int choiceAnswer = 0;
    int questCount = 0;
    int answerCount = 0;

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
            if (stageData.stageOrder[i] == 5)
                count++;

        switch (count)
        {
            case 1:
                examSets = stageData.examSets[0];
                examText.text = "중간고사!";
                break;
            case 0:
                examSets = stageData.examSets[1];
                examText.text = "기말고사!";
                break;
            default:
                Debug.Log("Set_Quiz함수가 스위치문에서 범위를 벗어남");
                examSets = stageData.examSets[0];
                examText.text = "범위 벗어남!";
                break;
        }

        Questions = examSets.questions;

        ExampleSets = examSets.exampleSets;
        Answers = examSets.answers;

        Set_QuestionTexts(0);
    }

    void Set_QuestionTexts(int n)
    {
        miniGameQuestion.Input_Question(Questions[n]);
        questionItemText.Input_QuestionItems(ExampleSets[n].examples);
    }

    public void OnClick_Next()
    {
        if (MiniGameMgr.miniGameMgr.Lock) return;

        questionItemText.selectLock = false;

        if (nextText.text == "다음 문제")
        {
            questionItemText.selectLock = true;
            SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.button);
            nextText.text = "제출하기";
            explanation.gameObject.SetActive(false);
            Next_Quiz();
            return;
        }

        if (choiceAnswer == 0)
        {
            SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.wrong);
            return;
        }
        else if (choiceAnswer == (Answers[questCount] + 1))
        {
            SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.chime);
            answerCount++;
            miniGameAnswerImage.Change_NoAnswerImage(true, questCount);
            answerPopup.SetActive(true);
            MiniGameMgr.miniGameMgr.Lock = true;
            Invoke("Next_Quiz", 1.0f);
        }
        else
        {
            SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.wrong);
            miniGameAnswerImage.Change_NoAnswerImage(false, questCount);
            explanation.Active_Text(ExampleSets[questCount].answerExample);
            nextText.text = "다음 문제";
        }

        choiceAnswer = 0;
    }

    public void OnClick_Choise(int temp)
    {
        if (MiniGameMgr.miniGameMgr.Lock || nextText.text == "다음 문제")
            return;

        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.button);
        questionItemText.Set_TextColor(temp);
        choiceAnswer = temp;
    }

    private void Next_Quiz()
    {
        questCount++;
        MiniGameMgr.miniGameMgr.Lock = false;
        questionItemText.selectLock = false;
        questionItemText.Set_TextColor(100);
        answerPopup.SetActive(false);
        if (questCount == 6)
            if (answerCount >= 4)
            {
                SFXMgr.Instance.OverlapPlay_SFX(SFXMgr.SFXName.win);
                miniGamePopup.OnClickPopup(true, "클리어 성공! (" + answerCount + "/6)");
            }
            else
            {
                SFXMgr.Instance.OverlapPlay_SFX(SFXMgr.SFXName.lose);
                miniGamePopup.OnClickPopup(false, "클리어 실패! (" + answerCount + "/6)");
            }
        else
            Set_QuestionTexts(questCount);
    }
}