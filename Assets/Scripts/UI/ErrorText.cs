using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorText : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    void OnEnable()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        StartCoroutine(FadeBackground());
    }

    IEnumerator FadeBackground()
    {
        float timeElapsed = 0f;
        while (timeElapsed < 0.8f)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, timeElapsed / 0.8f);
            yield return 0;
            timeElapsed += Time.deltaTime;
        }
        canvasGroup.alpha = 1f;

        timeElapsed = 0f;

        while (timeElapsed < 0.8f)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, timeElapsed / 0.8f);
            yield return 0;
            timeElapsed += Time.deltaTime;
        }

        canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }
}
