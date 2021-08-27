using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public static Tutorial Instance;

    [SerializeField] public GameObject textPanel;
    [SerializeField] public GameObject touchPanel;
    [SerializeField] public Text msgText;
    [SerializeField] public GameObject pause;
    [SerializeField] public GameObject Q_I;
    [SerializeField] public GameObject joystick;
    [SerializeField] public GameObject talkButton;
    [SerializeField] public GameObject hungry;
    [SerializeField] public GameObject satisfact;
    [SerializeField] public GameObject popup;

    /* 스크립트 관련 */

    List<List<string>> tutorial_scripts = new List<List<string>>();
    List<string> currScript = new List<string>();

    int str_idx; // 하나의 string 내부 char를 가리키는 index
    int script_idx; // 현재 스크립트의 어느 대사를 치고 있는가?
    int total_script_idx; // 여러 스크립트 중 어느 스크립트를 쓸 것인가?

    /* 메시지 출력 관련 */

    bool isAnim;
    string targetMsg;
    public int CharPerSeconds;
    float interval; //재귀함수에 들어갈 공백시간 값
    public AudioSource audioSource; //Sound


    void Awake()
    {
        Instance = this;
        total_script_idx = 0;

        tutorial_scripts.Add(new List<string>());
        // PART 1 : 튜토리얼 진행 여부
        tutorial_scripts[0].Add("치지직ㅡ 들려?");
        tutorial_scripts[0].Add("좋아 들리는군.");
        tutorial_scripts[0].Add("이번에 새로 들어온 신입생이지?");
        tutorial_scripts[0].Add("반가워 나는 김민준이라고 해.");
        tutorial_scripts[0].Add("네가 어리버리할 거 같아서 도와주러 왔어.");
        tutorial_scripts[0].Add("어때? 튜토리얼을 들을래?");

        tutorial_scripts.Add(new List<string>());
        // PART 1 : 이동과 말 걸기
        tutorial_scripts[1].Add("좋아. 기본부터 착실히 알려주지.");
        tutorial_scripts[1].Add("아래에 빨갛게 보이는 건 조이스틱이야."); // 1
        tutorial_scripts[1].Add("보면 알겠지만 누르면 움직일 수 있어."); // 2
        tutorial_scripts[1].Add("이건 대화 버튼"); // 3
        tutorial_scripts[1].Add("사물이나 사람을 보고 누르면 말을 걸 수 있어."); // 4
        tutorial_scripts[1].Add("쉽지?");
        tutorial_scripts[1].Add("한번 해볼까?");
        tutorial_scripts[1].Add("나머지는 나를 찾아오면 알려줄게.");
        tutorial_scripts[1].Add("나는 홍문관 앞에 있어.");
        tutorial_scripts[1].Add("포탈에 말을 걸면 이동할 수 있으니 천천히 와.");
        tutorial_scripts[1].Add("잘 모르겠으면 집 앞에 지도가 있으니 한번 보고!");

        tutorial_scripts.Add(new List<string>());
        // PART 1 : UI들
        tutorial_scripts[2].Add("오 잘 왔군!");
        tutorial_scripts[2].Add("얼빵한 줄 알았더니 그래도 곧잘하는 걸");
        tutorial_scripts[2].Add("위에 빨갛게 보이는 건 학교생활만족도야."); // 2
        tutorial_scripts[2].Add("이 게이지에 따라 방학이나 졸업 때 뭘 하는 지가 달라져."); // 3
        tutorial_scripts[2].Add("아, 0이 돼버리면 게임이 끝나버리니까 조심해."); // 4
        tutorial_scripts[2].Add("만족도는 열람실에서 과제를 하거나"); // 5
        tutorial_scripts[2].Add("수업을 열심히 들으면 올라가니까 공부 열심히 하구"); // 6

        tutorial_scripts[2].Add("이건 피로도야."); // 7
        tutorial_scripts[2].Add("퀘스트를 하면 떨어져."); // 8
        tutorial_scripts[2].Add("0이 되면 퀘스트를 진행할 수 없어."); // 9
        tutorial_scripts[2].Add("커피 같은 걸 먹으면 오르긴 하니까"); // 10
        tutorial_scripts[2].Add("급하면 편의점을 애용해봐."); // 11

        tutorial_scripts[2].Add("이건 인벤토리랑 퀘스트 창이니까 한번씩 열어보고."); // 12
        tutorial_scripts[2].Add("일반 퀘스트는 매 학기마다 갱신돼."); // 13
        tutorial_scripts[2].Add("퀘스트가 많이 없으니까 아껴 하도록 해~"); // 14

        tutorial_scripts[2].Add("이제 수업을 한번 들으러 가볼까?");

        tutorial_scripts[2].Add("여길 누르면 시간표랑 다양한 메뉴가 나오니까"); // 16
        tutorial_scripts[2].Add("한번 본 다음에 교수님을 잘 찾아서 수업을 들어보자."); // 17
        tutorial_scripts[2].Add("평일엔 두 개의 수업을 들어야 하고,"); // 14
        tutorial_scripts[2].Add("주말엔 매주 갱신되는 메인 퀘스트를 클리어해야해."); // 14
        tutorial_scripts[2].Add("다 해야 잠을 잘 수 있어."); // 14
        tutorial_scripts[2].Add("퀴즈나 시험 주엔 시간이 조금 빨리 가니까 참고하구."); // 14
        tutorial_scripts[2].Add("조금 빡센가? 하하"); // 14
        tutorial_scripts[2].Add("하지만 컴공생인걸! ^^"); // 14
        tutorial_scripts[2].Add("교수님들은 T동 5층과 7층 강의실에 있어.");
        tutorial_scripts[2].Add("내 뒤의 홍문관에 말을 걸면 T동 1층으로 이동할 수 있으니까 참고해.");
        tutorial_scripts[2].Add("아, 홍문관 옆엔 게임 전체 지도도 있어! 한번 보도록!");
        tutorial_scripts[2].Add("그럼 즐거운 학교 생활 해보자!");
        tutorial_scripts[2].Add("졸업까지 파이팅!");
    }

    void OnEnable()
    {
        if (!PlayerDataMgr.playerData_SO.isEndedTutorial)
            StartCoroutine(TutorialProgress());
    }

    IEnumerator TutorialProgress()
    {
        yield return new WaitUntil(() => PlayerDataMgr.playerData_SO.tutorial_progress == 0);

        textPanel.SetActive(true);
        touchPanel.SetActive(true);

        // 튜토리얼을 들을래? 
        // yes 클릭 시 progress가 1이 됨 // no 클릭 시 튜토리얼이 그냥 꺼지고 isEnd가 저장됨

        yield return new WaitUntil(() => PlayerDataMgr.playerData_SO.tutorial_progress == 1);

        textPanel.SetActive(true);
        touchPanel.SetActive(true);
        OnClickPanel();
        // 대사를 좀 더 치면서 알려줌
        // 이 때 이동과 말 걸기를 알려준다.
        // 민준이에게 찾아오라고 함
        // 대사가 끝나면 튜토리얼이 꺼지며, 민준이에게 대사를 걸 때까지 퀘스트를 진행할 수 없으며, 교수에게 말을 걸 수 없다.
        // 민준이에게 말을 걸면 progress가 2가 된다.

        yield return new WaitUntil(() => PlayerDataMgr.playerData_SO.tutorial_progress == 2);
        textPanel.SetActive(true);
        touchPanel.SetActive(true);
        OnClickPanel();

        // 이 때 만족도와 배고픔 게이지, 퀘스트 / 인벤토리를 알려주고, 마지막에 pause로 시간표를 확인하라 한다.
        // 수업을 들으라고 한다.
        // 민준이가 응원해주며 마무리된다.

        //PlayerDataMgr.Sync_Cache_To_Persis();
        //gameObject.SetActive(false);
    }

    public void OnClickPanel()
    {
        currScript = tutorial_scripts[total_script_idx];

        if (currScript.Count > script_idx)
        {
            // 여기서 각 스크립트에 따라 빨간 거 띄워야 한다.
            if (total_script_idx == 1)
            {
                joystick.SetActive(script_idx == 1 || script_idx == 2);
                talkButton.SetActive(script_idx == 3 || script_idx == 4);
            }
            else if (total_script_idx == 2)
            {
                satisfact.SetActive(script_idx == 2 || script_idx == 3 || script_idx == 4 || script_idx == 5 || script_idx == 6);
                hungry.SetActive(script_idx == 7 || script_idx == 8 || script_idx == 9 || script_idx == 10 || script_idx == 11);
                Q_I.SetActive(script_idx == 12 || script_idx == 13 || script_idx == 14);
                pause.SetActive(script_idx == 16 || script_idx == 17);
            }

            SetMsg(currScript[script_idx]);
        }
        else
        {
            script_idx = 0; // 현 스크립트의 대사는 처음부터 칠 것

            total_script_idx++; // 다음 스크립트 쓸 것
            if (total_script_idx == 1)
            {
                popup.SetActive(true);
                touchPanel.SetActive(false);
            }
            else if (total_script_idx == 3)
            {
                PlayerDataMgr.playerData_SO.isEndedTutorial = true;
                Debug.Log("여기가 눌렸음333");
                PlayerDataMgr.Sync_Cache_To_Persis();
                TempQuestDatasMgr.Sync_Cache_To_Persis();
                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("여기가 눌렸음222");
                AllOff();
            }
        }
    }

    void AllOff()
    {
        textPanel.SetActive(false);
        touchPanel.SetActive(false);
        pause.SetActive(false);
        Q_I.SetActive(false);
        joystick.SetActive(false);
        talkButton.SetActive(false);
        hungry.SetActive(false);
        satisfact.SetActive(false);
        popup.SetActive(false);
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
        str_idx = 0;

        interval = 1.0f / CharPerSeconds;

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

        msgText.text += targetMsg[str_idx];

        //띄어쓰기와 .이 아닌 char에만 사운드 출력
        if (targetMsg[str_idx] != ' ' || targetMsg[str_idx] != '.')
        {
            //Sound
            audioSource.Play();
        }

        str_idx++;

        Invoke("Effecting", interval);//interval만큼 있다가 실행
    }
    void EffectEnd()
    {
        isAnim = false;
        script_idx++;
    }


    /* 0번 튜토리얼에서 사용한다.*/
    public void OnClickYes()
    {
        PlayerDataMgr.playerData_SO.tutorial_progress = 1;
        popup.SetActive(false);
    }
    public void OnClickNo()
    {
        PlayerDataMgr.playerData_SO.isEndedTutorial = true;
        gameObject.SetActive(false);
        PlayerDataMgr.Sync_Cache_To_Persis();
    }
}
