using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainPage : MonoBehaviour
{
    [SerializeField] GameObject minPage;
    [SerializeField] GameObject maxPage;
    [SerializeField] GameObject makers;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] InputField input;
    [SerializeField] Text output;

    [SerializeField] GameObject endings;
    [SerializeField] GameObject endingClose;
    [SerializeField] GameObject ending_GameObject;
    [SerializeField] GameObject ending_close_panel;
    [SerializeField] Image endingImage;
    [SerializeField] Text endingText;
    [SerializeField] List<Sprite> ending_sprites;
    public List<string> description = new List<string>();

    [SerializeField] GameObject beginnerPage;
    [SerializeField] public GameObject uicanvas;
    [SerializeField] public Text beginnerText;

    void Start()
    {
        if (PlayerDataMgr.isPlayerDataExist())
        {
            PlayerDataMgr.Sync_Persis_To_Cache();
        }
        GameSet();
    }

    public void GameSet()
    {
        Canvas canvas = this.gameObject.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = GameObject.Find("BoundCamera").GetComponent<Camera>();
    }

    public void GameUnset()
    {
        PlayerDataMgr.Sync_Cache_To_Persis();
        TempQuestDatasMgr.Sync_Cache_To_Persis();
    }


    public void OnClickEnding(int idx)
    {
        endingImage.sprite = ending_sprites[idx];
        endingText.text = description[idx];

        ending_GameObject.SetActive(true);
        ending_close_panel.SetActive(true);
    }

    public void CheckCommand()
    {
        switch (input.text)
        {
            // ABC(); ABC() abc;
            case "RESET();":
            case "Reset();":
            case "reset();":
            case "RESET()":
            case "Reset()":
            case "reset()":
            case "RESET;":
            case "Reset;":
            case "reset;":
            case "RESET":
            case "Reset":
            case "reset":
                OnClickStart(true);
                break;
            case "RUN();":
            case "Run();":
            case "run();":
            case "RUN()":
            case "Run()":
            case "run()":
            case "RUN;":
            case "Run;":
            case "run;":
            case "RUN":
            case "Run":
            case "run":
                OnClickStart(false);
                break;
            case "EXIT();":
            case "Exit();":
            case "exit();":
            case "EXIT()":
            case "Exit()":
            case "exit()":
            case "EXIT;":
            case "Exit;":
            case "exit;":
            case "EXIT":
            case "Exit":
            case "exit":
                OnClickExit();
                break;
            case "MADE();":
            case "Made();":
            case "made();":
            case "MADE()":
            case "Made()":
            case "made()":
            case "MADE;":
            case "Made;":
            case "made;":
            case "MADE":
            case "Made":
            case "made":
                makers.SetActive(true);
                break;
            case "ENDING();":
            case "Ending();":
            case "ending();":
            case "ENDING()":
            case "Ending()":
            case "ending()":
            case "ENDING;":
            case "Ending;":
            case "ending;":
            case "ENDING":
            case "Ending":
            case "ending":
                endings.SetActive(true);
                endingClose.SetActive(true);
                break;
            default:
                input.text = "";
                output.text = "";
                break;
        }
    }

    public void OnClickMakers() => makers.SetActive(true);
    public void OnClickEndings()
    {
        endings.SetActive(true);
        endingClose.SetActive(true);
    }

    public void TransformMode()
    {
        beginnerPage.SetActive(!beginnerPage.activeSelf);
        beginnerText.text = beginnerPage.activeSelf ? "전공자 모드로 변경" : "비전공자 모드로 변경";
    }

    public void OnClickStart(bool b)
    {
        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.button);

        if (b)
        {
            PlayerDataMgr.Init_PlayerData();
            TempQuestDatasMgr.Init_TempQuestData();
            StartCoroutine(TurnOnPage(true));
        }
        else if (!PlayerDataMgr.isPlayerDataExist())
        {
            PlayerDataMgr.Init_PlayerData();
            TempQuestDatasMgr.Init_TempQuestData();
            StartCoroutine(TurnOnPage(true));
        }
        else
        {
            PlayerDataMgr.Sync_Persis_To_Cache();
            TempQuestDatasMgr.Sync_Persis_To_Cache();
            StartCoroutine(TurnOnPage(false));
        }
    }

    public void OnClickExit()
    {
        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.button);
        Application.Quit();
    }

    public IEnumerator TurnOnPage(bool b)
    {
        minPage.SetActive(true);
        yield return new WaitForSeconds(1f);


        maxPage.SetActive(true);
        yield return new WaitForSeconds(1.5f);


        canvasGroup.gameObject.SetActive(true);
        StartCoroutine(FadeBackground(canvasGroup, b));
    }

    IEnumerator FadeBackground(CanvasGroup fadeIn, bool b)
    {
        float timeElapsed = 0f;
        while (timeElapsed < 0.8f)
        {
            fadeIn.alpha = Mathf.Lerp(0f, 1f, timeElapsed / 0.8f);
            yield return 0;
            timeElapsed += Time.deltaTime;
        }
        fadeIn.alpha = 1f;

        yield return new WaitForSeconds(1f);

        timeElapsed = 0f;

        while (timeElapsed < 0.8f)
        {
            fadeIn.alpha = Mathf.Lerp(1f, 0f, timeElapsed / 0.8f);
            yield return 0;
            timeElapsed += Time.deltaTime;
        }
        fadeIn.alpha = 0f;
        canvasGroup.gameObject.SetActive(false);

        GameUnset();
        if (b) SceneLoader.Instance.LoadScene("Prologue");
        else SceneLoader.Instance.LoadScene("House");
    }

}
