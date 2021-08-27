using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Explanation : MonoBehaviour
{
    [SerializeField] Text explanationText;

    public void Active_Text(string str)
    {
        explanationText.text = str;
        gameObject.SetActive(true);
    }

}