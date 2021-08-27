using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordNote : MonoBehaviour
{
    [Header("Set In Editor")]
    [SerializeField] Text[] wordNoteTexts = new Text[2];

    int wordCount = 0;

    public void InputWord(string word)
    {
        wordCount++;
        if (wordCount < 10)
            wordNoteTexts[0].text = wordNoteTexts[0].text + word + "\r\n";
        else
            wordNoteTexts[1].text = wordNoteTexts[1].text + word + "\r\n";
    }
}