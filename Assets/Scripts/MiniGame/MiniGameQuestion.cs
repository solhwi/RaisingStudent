using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameQuestion : MonoBehaviour
{
    [SerializeField] Text questionText;
    [SerializeField] Text questNumText;

    public void Input_Question(string _Question)
    {
        int tempNum = int.Parse(questNumText.text);
        questNumText.text = (tempNum + 1).ToString();
        questionText.text = _Question;
    }
}