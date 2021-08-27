using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    public int CharPerSeconds;
    public GameObject EndCorsur;
    public bool isAnim; //애니메이션 진행 중인지 알 수 있는 bool값
    int index;
    Text msgText;
    string targetMsg;
    float interval; //재귀함수에 들어갈 공백시간 값
    AudioSource audioSource; //Sound

    private void Awake()
    {
        msgText = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
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
        index = 0;

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

        msgText.text += targetMsg[index];

        //띄어쓰기와 .이 아닌 char에만 사운드 출력
        if (targetMsg[index] != ' ' || targetMsg[index] != '.')
        {
            //Sound
            audioSource.Play();
        }

        index++;

        Invoke("Effecting", interval);//interval만큼 있다가 실행
    }
    void EffectEnd()
    {
        isAnim = false;
        EndCorsur.SetActive(true);
    }
}
