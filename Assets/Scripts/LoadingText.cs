using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingText : MonoBehaviour
{
    Text loadingText;
    void OnEnable()
    {
        loadingText = GetComponent<Text>();
        loadingText.text = "Loading";

        StartCoroutine(Loading());
    }

    IEnumerator Loading()
    {
        while (true)
        {
            if (loadingText.text == "Loading...") loadingText.text = "Loading";
            else loadingText.text += ".";
            yield return new WaitForSeconds(0.25f);
        }

    }
}
