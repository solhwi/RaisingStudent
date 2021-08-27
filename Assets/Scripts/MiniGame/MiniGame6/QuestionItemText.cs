using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionItemText : MonoBehaviour
{
    [Header("Set In Editor")]
    [SerializeField] Text[] questionTexts = new Text[4];
    Color basicColor;

    public bool selectLock = false;
    public int selectedNum = 100;

    void Awake()
    {
        basicColor = questionTexts[0].color;
    }

    public void Input_QuestionItems(string[] questionItems)
    {
        for (int i = 0; i < 4; i++)
            questionTexts[i].text = (i + 1) + ") " + questionItems[i];
    }

    public void Set_TextColor(int num)
    {
        num--;
        if (selectedNum == num || selectLock)
            return;

        if (num == 99)
        {
            questionTexts[selectedNum].color = basicColor;
            selectedNum = 100;
            return;
        }

        questionTexts[num].color = Color.red;
        if (selectedNum != 100)
            questionTexts[selectedNum].color = basicColor;

        selectedNum = num;
    }

}