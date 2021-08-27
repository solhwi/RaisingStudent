using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prologue : MonoBehaviour
{

    [Header("Set in Editor")]
    [SerializeField] CanvasGroup[] scene = new CanvasGroup[10];

    [Header("Set in Runtime")]
    private int processIndex = 0;
    private float fadeTime = 2f;
    bool Lock = false;
    bool isInvoking = false;

    [SerializeField] public GameObject uicanvas;
    [SerializeField] public GameObject boundCamera;

    void Start()
    {
        GameSet();
        PrologueProgress();
    }

    public void GameSet()
    {
        Canvas canvas = this.gameObject.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = GameObject.Find("BoundCamera").GetComponent<Camera>();
    }
    public void GameUnset()
    {

    }

    private void PrologueProgress()
    {
        if (processIndex < scene.Length - 1)
        {
            StartCoroutine(FadeBackground(scene[processIndex + 1], scene[processIndex]));
            processIndex++;
        }
        else
        {
            if (!Lock)
            {
                Lock = true;
                GameUnset();
                SceneLoader.Instance.LoadScene("House");
            }
        }
    }

    IEnumerator FadeBackground(CanvasGroup fadeIn, CanvasGroup fadeOut)
    {
        float timeElapsed = 0f;
        while (timeElapsed < fadeTime)
        {
            fadeIn.alpha = Mathf.Lerp(0f, 1f, timeElapsed / fadeTime);
            yield return 0;
            timeElapsed += Time.deltaTime;
        }
        fadeIn.alpha = 1f;
        fadeOut.alpha = 0f;

        isInvoking = false;
    }

    public void OnClickNextPanel()
    {
        if (!isInvoking)
        {
            isInvoking = true;
            PrologueProgress();
        }
        else
        {
            return;
        }

    }

}
