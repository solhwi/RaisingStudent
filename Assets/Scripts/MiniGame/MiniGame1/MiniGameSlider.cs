using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameSlider : MonoBehaviour
{
    [Header("Set In Runtime")]
    List<float> willAmountOperationsQueue = new List<float>();
    float currFillRate;
    public float currMana;
    public float goalMana;

    [Header("Set in Editor")]
    [Range(40f, 200f)] [SerializeField] float operationProcessSpeed;
    [SerializeField] float maxMana;
    [SerializeField] float fillRatePerSec;

    Slider mySlider;
    bool isOperationProcessing;

    void Start()
    {
        StageCheck();
        ResetFillRate();
        mySlider = GetComponentInChildren<Slider>();
        mySlider.value = 1f;
        currMana = 100f;
        goalMana = 100f;
        willAmountOperationsQueue.Clear();
        isOperationProcessing = false;
    }
    void Update()
    {
        if (!MiniGameMgr.miniGameMgr.IsStop()) Process();
    }

    void StageCheck()
    {
        switch (PlayerDataMgr.playerData_SO.totalGradeProgress)
        {
            case 0: fillRatePerSec = 22; break;
            case 1: fillRatePerSec = 23; break;
            case 2: fillRatePerSec = 23; break;
            case 3: fillRatePerSec = 24; break;
            case 4: fillRatePerSec = 24; break;
            case 5: fillRatePerSec = 25; break;
            case 6: fillRatePerSec = 25; break;
            case 7: fillRatePerSec = 26; break;
            default:
                Debug.Log("StageCheck 스위치문에서 범위를 벗어남");
                fillRatePerSec = 17;
                break;
        }

        if (PlayerDataMgr.playerData_SO.totalGradeProgress == 0 && PlayerDataMgr.playerData_SO.dayProgress < 2)
        {
            fillRatePerSec = 15;
        }
    }

    public void ResetFillRate()
    {
        currFillRate = -fillRatePerSec;
    }
    public void SetFillRate(float regenRate)
    {
        currFillRate = regenRate;
    }
    public void Process()
    {
        // 현재 Operation이 실행중
        if (isOperationProcessing)
        {
            // 채울건 채우고
            goalMana = Mathf.Clamp(goalMana + currFillRate * Time.deltaTime, 0f, maxMana);

            // currMana과 fillAmount를 갱신.
            if (goalMana < currMana)
            {
                currMana = Mathf.Clamp(currMana - operationProcessSpeed * Time.deltaTime, goalMana, maxMana);
                mySlider.value = currMana / maxMana;

                if (Mathf.Abs(currMana - goalMana) <= Mathf.Epsilon)
                {
                    isOperationProcessing = false;
                }
            }
            else
            {
                currMana = Mathf.Clamp(currMana + operationProcessSpeed * Time.deltaTime, 0f, goalMana);
                mySlider.value = currMana / maxMana;

                if (Mathf.Abs(currMana - goalMana) <= Mathf.Epsilon)
                {
                    isOperationProcessing = false;
                }
            }

        }
        // 현재 Operation이 안실행중
        else
        {
            // 실행할 Operation이 남아 있음.
            if (willAmountOperationsQueue.Count > 0)
            {
                // Operation을 시작하자.
                float amount = willAmountOperationsQueue[0];
                willAmountOperationsQueue.RemoveAt(0);

                goalMana = Mathf.Clamp(goalMana + amount, 0f, maxMana);
                isOperationProcessing = true;
            }
            // 실행할 Operation이 남아 있지 않음.
            else
            {
                // fillRatePerSec만큼 채우자.
                goalMana = Mathf.Clamp(goalMana + currFillRate * Time.deltaTime, 0f, maxMana);
                currMana = goalMana;
                mySlider.value = currMana / maxMana;
            }
        }
    }
    public void UseMana(float amount) // 큐에 클릭 횟수 쌓기
    {
        if (MiniGameMgr.miniGameMgr.IsStop()) return;
        if (!IsEnoughMana((int)amount)) return;
        willAmountOperationsQueue.Add(amount);
    }
    public bool IsEnoughMana(int cost)
    {
        if (cost < 0) return true;

        float realWill = goalMana;
        foreach (var v in willAmountOperationsQueue)
        {
            realWill += v;
        }

        if (realWill - cost > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}