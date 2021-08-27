using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    protected static SceneLoader instance;
    public static SceneLoader Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SceneLoader>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    [Header("Set In Editor")]
    [SerializeField] private CanvasGroup sceneLoaderCanvasGroup;
    [SerializeField] private CutScene cutScene;
    [SerializeField] private Image progressBar;
    [SerializeField] private GameObject Background;

    [Header("Set In Runtime")]
    SceneMgr sceneMgr;
    string loadSceneName;
    [SerializeField] public GameObject uicanvas;

    void Awake()
    {
        if (cutScene != null) cutScene.gameObject.SetActive(false);

        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainPage") UICanvas.Instance.TurnUI(false);
    }

    public void LoadScene(string sceneName)
    {
        Canvas canvas = this.transform.GetChild(0).gameObject.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = GameObject.Find("BoundCamera").GetComponent<Camera>();
        UICanvas.Instance.TurnUI(false);


        gameObject.SetActive(true);
        Background.SetActive(true);

        SceneManager.sceneLoaded += LoadSceneEnd;
        loadSceneName = sceneName;
        StartCoroutine(Load(sceneName));
    }
    private IEnumerator Load(string sceneName)
    {
        progressBar.fillAmount = 0f;
        yield return StartCoroutine(Fade(true));
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;
        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;
            timer += Time.unscaledDeltaTime;
            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                if (progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
                if (progressBar.fillAmount == 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
    private void LoadSceneEnd(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == loadSceneName) // 로딩이 끝나면
        {

            StartCoroutine(Fade(false));
            SceneManager.sceneLoaded -= LoadSceneEnd;
        }
    }
    private IEnumerator Fade(bool isFadeIn)
    {
        float timer = 0f;
        while (timer <= 1f)
        {
            yield return null;
            timer += Time.unscaledDeltaTime * 2f;
            sceneLoaderCanvasGroup.alpha = Mathf.Lerp(isFadeIn ? 0 : 1, isFadeIn ? 1 : 0, timer);
        }
        if (!isFadeIn)
        {
            GameMgr.sceneMgr.ReadyForLoad(loadSceneName);
            gameObject.SetActive(false);
        }
    }
}