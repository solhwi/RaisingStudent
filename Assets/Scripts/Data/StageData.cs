using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StageDataContainer
{
    public List<StageData> stageDatas = new List<StageData>();

    public StageDataContainer()
    {
        stageDatas.Add(new StageData());
    }
}


[System.Serializable]
public class StageData
{
    public enum StageIdx
    {
        Sleepy,  // 0            총 4회
        TooMuchAssignment, // 1             총 4회
        DDR, // 2           총 4회
        Quiz, // 3              총 4회
        DontUnderstand, // 4             총 4회
        Exam, // 5            총 4회
        Lecture // 6            총 2회
    }

    public int[] stageOrder = new int[26]; // 시간표
    public quizSet[] quizSets = new quizSet[4];
    public lectureSet[] lectureSets = new lectureSet[4];
    public examSet[] examSets = new examSet[2];
}
[System.Serializable]
public class lectureSet // 4 덩어리 필요
{
    public string lectureName;
    public List<string> texts = new List<string>();
}

[System.Serializable]
public class examSet // 2 덩어리
{
    public string examName;
    public List<string> questions = new List<string>(); // 6개
    public List<exampleSet> exampleSets = new List<exampleSet>(); // 6개, 한 덩어리에 4개의 string으로 이루어짐
    public List<int> answers = new List<int>(); // 6개
    public examSet(List<string> _questions, List<exampleSet> _exampleSet, List<int> _answers)
    {
        questions = _questions;
        exampleSets = _exampleSet;
        answers = _answers;
    }
}

[System.Serializable]
public class exampleSet
{
    public string[] examples = new string[4];
    public string answerExample;
}

[System.Serializable]
public class quizSet // 한 덩어리에 퀴즈 6개 + 정답 6개가 될 것임, 한 학기 당 4 덩어리
{
    public string quizName;
    public List<string> questions = new List<string>();
    public List<string> answers = new List<string>();
    public List<bool> boolAnswers = new List<bool>();
    public quizSet(List<string> _questions, List<string> _answers, List<bool> _boolAnswers)
    {
        questions = _questions;
        answers = _answers;
        boolAnswers = _boolAnswers;
    }
}

