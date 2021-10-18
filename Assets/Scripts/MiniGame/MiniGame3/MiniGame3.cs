using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame3 : MonoBehaviour
{
    [Header("Set In Editor")]
    [SerializeField] public TimeSlider timeSlider;
    [SerializeField] MiniGamePopup miniGamePopup;
    [SerializeField] GameObject pauseState;
    [SerializeField] Sprite[] arrowSprites = new Sprite[4];
    [SerializeField] public int getTime;
    [SerializeField] public float increaseTime;


    [Header("Set In Runtime")]
    public Image[] arrows = new Image[7];
    public Image[] arrowSlots = new Image[7];
    public List<int> arrowAnswers = new List<int>();
    bool hidingState = false;
    bool clickLook = false;

    [Header("Fixed Data")]
    int currSlotIdx = 0;
    int currClearCount = 0;
    int clearCount = 3;
    int slotCount = 7;

    void Awake()
    {
        for (int i = 0; i < slotCount; i++)
        {
            int temp = i;
            arrowSlots[temp] = transform.Find("ArrowBlock").GetChild(temp).GetComponent<Image>();
            arrows[temp] = transform.Find("ArrowBlock").GetChild(temp).GetChild(0).GetComponent<Image>();
        }
        StageCheck();
        Create_Arrows(true);
    }

    void Update()
    {
        if (!MiniGameMgr.miniGameMgr.isTikToking && timeSlider.currTime <= 0.1f && !MiniGameMgr.miniGameMgr.Lock)
        {
            SFXMgr.Instance.OverlapPlay_SFX(SFXMgr.SFXName.lose);
            miniGamePopup.OnClickPopup(false, "클리어 실패!");
        }
        if (pauseState.gameObject.activeSelf && !hidingState)
            Hiding_Images(true);

        if (hidingState && !pauseState.gameObject.activeSelf)
            Hiding_Images(false);
    }

    void StageCheck()
    {
        switch (PlayerDataMgr.playerData_SO.totalGradeProgress)
        {
            case 0: getTime = 8; increaseTime = 5f; clearCount = 5; break;
            case 1: getTime = 7; increaseTime = 4f; clearCount = 6; break;
            case 2: getTime = 6; increaseTime = 4f; clearCount = 6; break;
            case 3: getTime = 6; increaseTime = 4f; clearCount = 7; break;
            case 4: getTime = 5; increaseTime = 3.5f; clearCount = 7; break;
            case 5: getTime = 5; increaseTime = 3.5f; clearCount = 7; break;
            case 6: getTime = 4; increaseTime = 3f; clearCount = 8; break;
            case 7: getTime = 4; increaseTime = 3f; clearCount = 8; break;
            default:
                Debug.Log("StageCheck 스위치문에서 범위를 벗어남");
                getTime = 7; increaseTime = 3f; clearCount = 5;
                break;
        }


    }

    public void Hiding_Images(bool hiding)
    {
        if (miniGamePopup.gameObject.activeSelf)
            return;

        hidingState = !hidingState;

        if (hiding)
            for (int i = currSlotIdx; i < 7; i++)
                arrows[i].color = new Color(1f, 1f, 1f, 0f);
        else
            for (int i = currSlotIdx; i < 7; i++)
                arrows[i].color = new Color(1f, 1f, 1f, 1f);
    }

    public void Create_Arrows(bool first)
    {
        if (first) timeSlider.SettingTime(getTime);
        else timeSlider.SettingTime(timeSlider.currTime + increaseTime);

        StopCoroutine("Delete_Arrow");
        arrowAnswers = new List<int>();

        for (int i = 0; i < slotCount; i++)
        {
            arrowAnswers.Add(Random.Range(0, 4));

            arrowSlots[i].transform.localPosition = new Vector3(arrowSlots[i].transform.localPosition.x, -120f, 0);
            arrowSlots[i].color = new Color(arrowSlots[i].color.r, arrowSlots[i].color.g, arrowSlots[i].color.b, 1f);
            arrows[i].color = new Color(arrows[i].color.r, arrows[i].color.g, arrows[i].color.b, 1f);
            arrows[i].sprite = arrowSprites[arrowAnswers[i]];
        }
    }

    public void OnClick_Arrow(int dir)
    {
        if (MiniGameMgr.miniGameMgr.IsStop() || clickLook) return;

        if (arrowAnswers[currSlotIdx] == dir) // 정답
        {
            SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.walk);
            StartCoroutine("Delete_Arrow", currSlotIdx);
        }
        else // 오답 
        {
            SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.wrong);
            StartCoroutine("Shake_Arrow", currSlotIdx);
        }

        if (currSlotIdx >= slotCount) // 현재 단계 클리어
        {
            currSlotIdx = 0;
            currClearCount++;

            if (currClearCount == clearCount)
            {
                SFXMgr.Instance.OverlapPlay_SFX(SFXMgr.SFXName.win);
                miniGamePopup.OnClickPopup(true, "클리어 성공!"); // 올 클리어
            }
            else
                Create_Arrows(false);
        }
    }

    IEnumerator Delete_Arrow(int _index)
    {
        currSlotIdx++;

        arrows[_index].color = new Color(1f, 1f, 1f, 1f);
        Color tempColor = arrows[_index].color;
        Vector3 currPos = arrowSlots[_index].gameObject.transform.position;
        Vector3 goalPos = new Vector3(currPos.x, currPos.y - 10f, currPos.z);

        float temp = 0;

        while (tempColor.a > 0.01f)
        {
            tempColor.a -= Time.deltaTime / 0.3f;

            arrows[_index].color = tempColor;
            arrowSlots[_index].color = tempColor;
            temp += (Time.deltaTime * 3);

            arrowSlots[_index].transform.position = Vector3.Lerp(currPos, goalPos, temp);
            if (tempColor.a <= 0.01f) tempColor.a = 0f;

            yield return null;
        }

        arrows[_index].color = tempColor;
        arrowSlots[_index].color = tempColor;
    }

    IEnumerator Shake_Arrow(int _index)
    {
        float shakeTime = 0.5f;
        timeSlider.SettingTime(timeSlider.currTime - 0.5f);

        Vector3 startPosition = arrows[_index].transform.position;
        arrows[_index].color = new Color(1f, 0f, 0f, 1f);
        clickLook = true;

        float tmp = shakeTime;

        while (tmp > 0.01f)
        {
            arrowSlots[_index].transform.position = startPosition + Random.insideUnitSphere * 0.8f;
            tmp -= Time.deltaTime;
            yield return null;
        }

        arrowSlots[_index].transform.position = startPosition;
        arrows[_index].color = new Color(1f, 1f, 1f, 1f);
        clickLook = false;
    }
}