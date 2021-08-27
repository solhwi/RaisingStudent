using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vacation : MonoBehaviour
{
    [SerializeField] public CanvasGroup canvasGroup; // fade in만 하면 될 듯
    [SerializeField] public CanvasGroup rankPanel;
    public Image vacationImage;
    public Sprite[] vacation_sprites = new Sprite[4];
    public Text rank;
    public string[] contexts = new string[4];
    public Text gradeText;
    public GameObject panel;

    public Text msgText;
    bool isAnim;
    string targetMsg;
    public int CharPerSeconds;
    float interval; //재귀함수에 들어갈 공백시간 값
    int index_str;
    int index_script;
    public AudioSource audioSource; //Sound

    private List<List<string>> scripts;
    private List<string> currScript;

    bool isScriptEnd = false;

    [SerializeField] public GameObject uicanvas;
    [SerializeField] public GameObject boundCamera;

    void Awake()
    {
        GameSet();
        if (PlayerDataMgr.playerData_SO.totalGradeProgress == 4) // 군대
        {
            vacationImage.sprite = vacation_sprites[3];
        }
        else if (PlayerDataMgr.playerData_SO.totalGradeProgress == 8) // 군대
        {
            vacationImage.sprite = vacation_sprites[0];
        }
        else
        {
            int rand = Random.Range(0, 3);
            vacationImage.sprite = vacation_sprites[rand];

            switch (rand)
            {
                case 0: // 프로젝트
                    PlayerDataMgr.playerData_SO.satisfact += 5;
                    PlayerDataMgr.playerData_SO.AddItemByCode("WORRY");
                    PlayerDataMgr.playerData_SO.AddItemByCode("BOOK");
                    break;
                case 1: // 알바
                    PlayerDataMgr.playerData_SO.AddGold(10000);
                    PlayerDataMgr.playerData_SO.AddItemByCode("TISSUE");
                    break;
                case 2: // 놀기
                    PlayerDataMgr.playerData_SO.UseGold(3000);
                    PlayerDataMgr.playerData_SO.AddItemByCode("BEER");
                    PlayerDataMgr.playerData_SO.AddItemByCode("MEDICINE");
                    break;
            }

        }


        // 프로젝트, 알바, 노는거

        scripts = new List<List<string>>();

        scripts.Add(new List<string>());

        scripts[0].Add("짧았지만 길었던 첫 학기가 끝났다.");
        scripts[0].Add("좀 이상하지만 좋은 동기들과");
        scripts[0].Add("착한 선배들을 많이 만났다.");
        scripts[0].Add("고등학교 때 친구들은 잘 지내고 있을까?");
        scripts[0].Add("종강도 했는데 한번 봐야겠다!");

        scripts.Add(new List<string>());

        scripts[1].Add("시험 기간엔 되게 길어보였는데");
        scripts[1].Add("눈 깜짝할 새에 1학년이 끝나버렸다.");
        scripts[1].Add("내년엔 신입생이 들어오는 건가?");
        scripts[1].Add("헐... 내가 헌내기라니");
        scripts[1].Add("내가 헌내기라니!!");

        scripts.Add(new List<string>());

        scripts[2].Add("휴.. 드디어 끝났다.");
        scripts[2].Add("신입생 애들도 좋고 맘에 든다.");
        scripts[2].Add("아냐 한 친구는 조금 무섭기도...");
        scripts[2].Add("종강한 건 너무 좋은데");
        scripts[2].Add("슬슬 과목들이 어려워지는 걸 느낀다.");
        scripts[2].Add("2학기엔 뭐가 더 많던데...");
        scripts[2].Add("아익ㅠㅠ 이제 공부 좀 더 해야겠다.");

        scripts.Add(new List<string>());

        scripts[3].Add("..........");
        scripts[3].Add("......");
        scripts[3].Add("...");
        scripts[3].Add("아 ㅅㅂ...");
        scripts[3].Add("애들아 나 갔다 온다...");

        scripts.Add(new List<string>());

        scripts[4].Add("열심히 했지만 생각보다 성적이 나오지 않았다.");
        scripts[4].Add("새 동아리에 들어서 공부도 하고,");
        scripts[4].Add("친구랑 후배도 많이 만났지만");
        scripts[4].Add("이상하게 신입생 때 같지 않다.");
        scripts[4].Add("에이 모르겠다.");
        scripts[4].Add("이번 방학 땐 뭘 공부하지?!");

        scripts.Add(new List<string>());

        scripts[5].Add("운영체제 과제에 정신이 나갈 뻔 했지만");
        scripts[5].Add("저번 학기보다 성적이 올랐다!");
        scripts[5].Add("교수님들이 짱짱해서 듣기 좋았다.");
        scripts[5].Add("실력이 는 기분이었다.");
        scripts[5].Add("아참, 졸업 프로젝트는 누구랑 하지?");
        scripts[5].Add("으아~");

        scripts.Add(new List<string>());

        scripts[6].Add("짧았지만 길었던 한 학기가 끝났다.");
        scripts[6].Add("졸업 프로젝트도 순조롭다.");
        scripts[6].Add("다음이 마지막 학기라고 생각하니 기분이 이상하다.");
        scripts[6].Add("슬슬 세상에 던져지겠군.");
        scripts[6].Add("나는 어떤 대학생이었을까?");
        scripts[6].Add("이번 방학엔 했던 공부도 돌아보고,");
        scripts[6].Add("못 만나온 사람들을 만나보자.");
        scripts[6].Add("이번 방학도 바쁘겠군!");

        scripts.Add(new List<string>());

        scripts[7].Add("......");
        scripts[7].Add("...");
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

    void Start()
    {
        int curr_progress = PlayerDataMgr.playerData_SO.totalGradeProgress - 1;
        curr_progress = curr_progress >= 0 ? curr_progress : 0;
        int curr_grade = PlayerDataMgr.playerData_SO.grades[curr_progress];
        switch (curr_grade)
        {
            case 0:
                rank.text = "F";
                break;
            case 1:
                rank.text = "D";
                break;
            case 2:
                rank.text = "C";
                break;
            case 3:
                rank.text = "B";
                break;
            case 4:
                rank.text = "A";
                break;
            default:
                rank.text = "C";
                break;
        }
        gradeText.text = contexts[curr_grade];

        currScript = scripts[curr_progress];

        StartCoroutine(ProgressVacation(canvasGroup));
    }

    public void OnClickPanel()
    {
        if (currScript.Count > index_script)
        {
            SetMsg(currScript[index_script]);
        }
        else
        {
            isScriptEnd = true;
        }

    }

    public void SetMsg(string msg)
    {
        if (isAnim)
        {
            msgText.text = targetMsg;
            CancelInvoke(); //재귀함수 정지
            EffectEnd();
        }
        else
        {
            targetMsg = msg;
            EffectStart();
        }
    }

    void EffectStart()
    {
        msgText.text = "";
        index_str = 0;

        interval = 1.0f / CharPerSeconds;
        Debug.Log(interval);

        isAnim = true;
        Invoke("Effecting", interval);//interval만큼 있다가 실행
    }
    void Effecting()
    {
        if (msgText.text == targetMsg)
        {
            EffectEnd();
            return;
        }

        msgText.text += targetMsg[index_str];

        //띄어쓰기와 .이 아닌 char에만 사운드 출력
        if (targetMsg[index_str] != ' ' || targetMsg[index_str] != '.')
        {
            //Sound
            audioSource.Play();
        }

        index_str++;

        Invoke("Effecting", interval);//interval만큼 있다가 실행
    }
    void EffectEnd()
    {
        isAnim = false;
        index_script++;
    }

    IEnumerator ProgressVacation(CanvasGroup fadeIn)
    {
        float timeElapsed = 0f;
        while (timeElapsed < 0.8f)
        {
            fadeIn.alpha = Mathf.Lerp(0f, 1f, timeElapsed / 0.8f);
            yield return 0;
            timeElapsed += Time.deltaTime;
        }
        fadeIn.alpha = 1f;

        yield return new WaitForSeconds(1.5f);

        panel.SetActive(true);

        yield return new WaitUntil(() => isScriptEnd);

        panel.SetActive(false);

        yield return new WaitForSeconds(2f);

        timeElapsed = 0f;

        while (timeElapsed < 1f)
        {
            rankPanel.alpha = Mathf.Lerp(0f, 1f, timeElapsed / 1f);
            yield return 0;
            timeElapsed += Time.deltaTime;
        }
        rankPanel.alpha = 1f;

        yield return new WaitForSeconds(2f);

        if (PlayerDataMgr.playerData_SO.totalGradeProgress == 8)
        {
            GameUnset();
            SceneLoader.Instance.LoadScene("Ending");
        }
        else
        {
            GameUnset();
            SceneLoader.Instance.LoadScene("House");
        }
    }
}
