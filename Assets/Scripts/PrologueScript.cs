using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrologueScript : MonoBehaviour
{
    public GameObject my_Panel;
    List<List<string>> prologue_script;
    [SerializeField] public int total_Idx;
    public int curr_idx;

    [SerializeField] public Text msgText;
    bool isAnim = false;
    string targetMsg;
    public int CharPerSeconds;
    float interval; //재귀함수에 들어갈 공백시간 값
    int index_str;
    public AudioSource audioSource; //Sound
    public bool isScriptEnd;


    void Awake()
    {
        curr_idx = 0;
        isScriptEnd = false;

        prologue_script = new List<List<string>>();
        prologue_script.Add(new List<string>());

        prologue_script[0].Add("...오늘은 대학교 합격 발표날");
        prologue_script[0].Add("아...");
        prologue_script[0].Add("제발...");
        prologue_script[0].Add("이거도 예비뜨면 진짜...");

        prologue_script.Add(new List<string>());
        prologue_script[1].Add("...");
        prologue_script[1].Add("어...?");

        prologue_script.Add(new List<string>());
        prologue_script[2].Add("이야 진짜?!");
        prologue_script[2].Add("진짜야?!?!?!");
        prologue_script[2].Add("......");
        prologue_script[2].Add("...&%^(..");
        prologue_script[2].Add("....");
        prologue_script[2].Add("...");

        prologue_script.Add(new List<string>());
        prologue_script[3].Add("(나는 집이 멀어서 자취방을 구하게 됐다.)");
        prologue_script[3].Add("정말 싸게 구했어.");
        prologue_script[3].Add("앞으로는 멋진 일만 펼쳐질 거야.");
        prologue_script[3].Add("새 학교는 어떤 곳일까?!");
        prologue_script[3].Add("....");
        prologue_script[3].Add("...");

    }

    void Start()
    {
        StartCoroutine(Prologue_Text_Routine());
    }

    IEnumerator Prologue_Text_Routine()
    {
        yield return new WaitUntil(() => gameObject.GetComponent<CanvasGroup>().alpha > 0.1f);
        my_Panel.SetActive(true);
    }

    public void OnClickPanel()
    {
        if (gameObject.GetComponent<CanvasGroup>().alpha < 0.9f) return;

        if (prologue_script[total_Idx].Count > curr_idx)
        {
            SetMsg(prologue_script[total_Idx][curr_idx]);
        }
        else
        {
            isScriptEnd = true;
            my_Panel.SetActive(false);
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
        curr_idx++;
    }

}
