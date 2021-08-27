using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    [SerializeField] public CanvasGroup graduation; // fade in만 하면 될 듯
    [SerializeField] public CanvasGroup after;
    public GameObject panel;

    public Image graduationImage;
    public Image endingImage;

    public Sprite[] graduation_sprites = new Sprite[3];
    public Sprite[] ending_sprites = new Sprite[6];


    public Text msgText;
    bool isAnim;
    string targetMsg;
    public int CharPerSeconds;
    float interval; //재귀함수에 들어갈 공백시간 값
    int index_str;
    int index_script = 0;
    int index_ending = 0;
    public AudioSource audioSource; //Sound
    private List<List<string>> scripts;

    bool isScriptEnd = false;

    [SerializeField] public GameObject uicanvas;
    [SerializeField] public GameObject boundCamera;


    void Awake()
    {
        GameSet();
        scripts = new List<List<string>>();

        scripts.Add(new List<string>());
        scripts.Add(new List<string>());

        if (PlayerDataMgr.playerData_SO.isFailed)
        {
            if (PlayerDataMgr.playerData_SO.GetItemCountByCode("WORRY") < 20)
            {
                PlayerDataMgr.playerData_SO.endingList[1] = true;
                graduationImage.sprite = graduation_sprites[1]; // 자퇴
                scripts[0].Add("나는 학교와는 맞지 않았나보다.");
                scripts[0].Add("맞지 않다고 판단하기엔 열심히 하지 않은걸까?");
                scripts[0].Add("하지만 이미 나는 그렇게 판단했다.");
                scripts[0].Add("좀 더 나다운 삶의 방식이 더 있을 것이다.");
                scripts[0].Add("후회하지 않는다.");
                scripts[0].Add("후회하지... 않...는다..!");
            }
            else
            {
                PlayerDataMgr.playerData_SO.endingList[2] = true;
                graduationImage.sprite = graduation_sprites[2]; // 퇴학
                scripts[0].Add("으악!");
                scripts[0].Add("고작 학사 경고 두번 맞았다고 퇴학?!");
                scripts[0].Add("안돼 엄마한테 뭐라 그러지?");
                scripts[0].Add("으아악 억울해ㅠㅠㅠ");
                scripts[0].Add("......");
            }
        }
        else
        {
            PlayerDataMgr.playerData_SO.endingList[0] = true;
            graduationImage.sprite = graduation_sprites[0]; // 졸업
            scripts[0].Add("마지막 학기가 끝나고, 나는 무사히 졸업했다.");
            scripts[0].Add("맨날 술만 먹였지만 재밌는 선배들");
            scripts[0].Add("내가 공부할 수 있게 도와준 형누나들");
            scripts[0].Add("이상하지만 순수하고 착한 동기들");
            scripts[0].Add("귀여운 후배들과 함께였어서 대학 생활이 참 행복했다.");
            scripts[0].Add("힘들 때 옆에서 어깨를 토닥여줬던 아빠와");
            scripts[0].Add("성적이 못 나와도 별말 없이 믿어줬던 엄마에게 감사드리고 싶다.");
            scripts[0].Add("눈 코 뜰새 없이 살아가는 와중에도");
            scripts[0].Add("내 옆에 남아준 모든 사람들에게 감사하며");
            scripts[0].Add("대학 생활을 마무리해야겠다.");
            scripts[0].Add("ㅡ");
            scripts[0].Add("이 게임은 홍익대학교 컴퓨터공학과 '박솔휘' 학생을 필두로 제작되었으며,");
            scripts[0].Add("서브 기획에 '이하영',");
            scripts[0].Add("UI 디자인에 '이가은',");
            scripts[0].Add("도트 및 캐릭터 디자인에 '이경주',");
            scripts[0].Add("일러스트 디자인에 '노희진',");
            scripts[0].Add("서브 프로그래머에 '허준혁',");
            scripts[0].Add("사운드에 '이정훈',");
            scripts[0].Add("그리고 등장인물로 기꺼이 출연해준 15명의 동기들과,");
            scripts[0].Add("교수님들께 감사드리고");
            scripts[0].Add("지금까지 컴공생키우기를 플레이해주신");
            scripts[0].Add("여러분들께도 깊은 감사의 말씀드립니다.");
            scripts[0].Add("ㅡ");
            scripts[0].Add("그럼 안녕~!");

        }

        // 0 ~ 24 
        scripts[1].Add("......");

        if (PlayerDataMgr.playerData_SO.GetItemCountByCode("WORRY") > 30)
        {
            if (PlayerDataMgr.playerData_SO.GetTotalGrade() > 3.5) // 대학원
            {
                PlayerDataMgr.playerData_SO.endingList[3] = true;
                endingImage.sprite = ending_sprites[0];
                scripts[1].Add("Ending 1. 나는 대학원생이 되었다.");
            }
            else if (PlayerDataMgr.playerData_SO.GetTotalGrade() > 2.6) // 학원
            {
                PlayerDataMgr.playerData_SO.endingList[5] = true;
                endingImage.sprite = ending_sprites[2];
                scripts[1].Add("Ending 2. 나는 국비지원을 받게 되었다.");
            }
            else // 공무원준비
            {
                PlayerDataMgr.playerData_SO.endingList[7] = true;
                endingImage.sprite = ending_sprites[4];
                scripts[1].Add("Ending 3. 나는 공시생이 되었다.");
            }
        }
        else
        {
            if (PlayerDataMgr.playerData_SO.GetTotalGrade() > 3.5) // 대기업
            {
                PlayerDataMgr.playerData_SO.endingList[4] = true;
                endingImage.sprite = ending_sprites[1];
                scripts[1].Add("Ending 4. 나는 모 대기업의 인턴이 되었다.");
            }
            else if (PlayerDataMgr.playerData_SO.GetTotalGrade() > 2.6) // 중견기업
            {
                PlayerDataMgr.playerData_SO.endingList[6] = true;
                endingImage.sprite = ending_sprites[3];
                scripts[1].Add("Ending 5. 나는 이름 모를 기업의 사원이 되었다.");
            }
            else // 치킨집
            {
                PlayerDataMgr.playerData_SO.endingList[8] = true;
                endingImage.sprite = ending_sprites[5];
                scripts[1].Add("Ending 6. 나는 모 치킨집 사장님이 되었다.");
            }
        }

        PlayerDataMgr.Sync_Cache_To_Persis();
        TempQuestDatasMgr.Sync_Cache_To_Persis();
    }


    void Start()
    {
        StartCoroutine(ProgressVacation(graduation, after));
    }

    public void OnClickPanel()
    {
        if (scripts[index_ending].Count > index_script)
        {
            SetMsg(scripts[index_ending][index_script]);
        }
        else
        {
            isScriptEnd = true;
        }

    }

    public void GameSet()
    {
        Canvas canvas = this.gameObject.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = GameObject.Find("BoundCamera").GetComponent<Camera>();
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

    IEnumerator ProgressVacation(CanvasGroup graduation, CanvasGroup after)
    {
        float timeElapsed = 0f;
        while (timeElapsed < 0.8f)
        {
            graduation.alpha = Mathf.Lerp(0f, 1f, timeElapsed / 0.8f);
            yield return 0;
            timeElapsed += Time.deltaTime;
        }
        graduation.alpha = 1f;

        yield return new WaitForSeconds(3f);

        panel.SetActive(true);

        yield return new WaitUntil(() => isScriptEnd);

        panel.SetActive(false);

        index_ending++;
        isScriptEnd = false;
        index_script = 0;

        yield return new WaitForSeconds(1f);

        if (PlayerDataMgr.playerData_SO.isFailed)
        {
            SceneLoader.Instance.LoadScene("MainPage");
            yield break;
        }

        timeElapsed = 0f;

        while (timeElapsed < 1.5f)
        {
            after.alpha = Mathf.Lerp(0f, 1f, timeElapsed / 1.5f);
            yield return 0;
            timeElapsed += Time.deltaTime;
        }
        after.alpha = 1f;
        graduation.alpha = 0f;

        panel.SetActive(true);

        yield return new WaitUntil(() => isScriptEnd);

        panel.SetActive(false);

        yield return new WaitForSeconds(2f);

        SceneLoader.Instance.LoadScene("MainPage");
    }
}