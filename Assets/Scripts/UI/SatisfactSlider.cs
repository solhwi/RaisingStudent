using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SatisfactSlider : MonoBehaviour
{
    [SerializeField] Image satisfact;
    [SerializeField] Image emotion;
    [SerializeField] Sprite happy;
    [SerializeField] Sprite plain;
    [SerializeField] Sprite anger;

    float value = 0f;

    void OnEnable()
    {
        satisfact.fillAmount = PlayerDataMgr.playerData_SO.satisfact / 100f;
        StartCoroutine(CheckSatisfact());
    }

    IEnumerator CheckSatisfact()
    {
        yield return new WaitUntil(() => PlayerDataMgr.playerData_SO.satisfact < 1);
        PlayerDataMgr.playerData_SO.isFailed = true;
        SceneLoader.Instance.LoadScene("Ending");
    }

    public void Set_Slider(int _value)
    {
        if (value == _value / 100f)
            return;

        value = _value / 100f;

        if (value >= 0.7f)
        {
            emotion.sprite = happy;
            satisfact.color = new Color(64f / 255f, 215f / 255f, 13f / 255f, 255f / 255f);
        }

        else if (value >= 0.3f)
        {
            emotion.sprite = plain;
            satisfact.color = new Color(250f / 255f, 192f / 255f, 58f / 255f, 1f);

        }

        else
        {
            emotion.sprite = anger;
            satisfact.color = new Color(246f / 255f, 97f / 255f, 36f / 255f, 1f);
        }


        satisfact.fillAmount = value;
    }
}