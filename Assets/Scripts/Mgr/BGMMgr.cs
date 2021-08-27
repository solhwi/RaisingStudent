using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMMgr : MonoBehaviour
{
    private static BGMMgr instance;
    [SerializeField] AudioClip BGM_Tdong;
    [SerializeField] AudioClip BGM_Hongmun;
    [SerializeField] AudioClip BGM_MiniGame;
    [SerializeField] AudioClip BGM_Home;
    [SerializeField] AudioClip BGM_Ending;
    AudioSource BGM;

    int nowIndex = 0;
    int selectedIndex = -1;

    public static BGMMgr Instance
    {
        get
        {
            return instance;
        }
        set
        {
            Instance = value;
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        instance = this;
        BGM = GetComponent<AudioSource>();

        DontDestroyOnLoad(gameObject);

        // 씬이 바뀔 때 호출되는 함수를 정합니다.
        SceneManager.activeSceneChanged += OnChangedActiveScene;
    }

    public void OnChangedActiveScene(Scene current, Scene next)
    {
        nowIndex = Set_BGMIndex(next);
        StartCoroutine(FadeOutIn());
    }

    public int Set_BGMIndex(Scene _scene)
    {
        int idx = 0;

        if (_scene.name.StartsWith("Tdong"))
            idx = 0;
        else if (_scene.name.StartsWith("Entrance") || _scene.name.StartsWith("HiddenStage"))
            idx = 1;
        else if (_scene.name.StartsWith("MiniGame"))
            idx = 2;
        else if (_scene.name.StartsWith("House") || _scene.name.StartsWith("Hall") || _scene.name.StartsWith("Prologue") || _scene.name.StartsWith("Vacation"))
            idx = 3;
        else if (_scene.name.StartsWith("Ending"))
            idx = 4;
        else
            idx = 5; // 처음 게임 시작 시 ""

        return idx;
    }

    public void Set_BGM(int idx)
    {
        switch (idx)
        {
            case 0:
                BGM.clip = BGM_Tdong;
                break;
            case 1:
                BGM.clip = BGM_Hongmun;
                break;
            case 2:
                BGM.clip = BGM_MiniGame;
                break;
            case 3:
                BGM.clip = BGM_Home;
                break;
            case 4:
                BGM.clip = BGM_Ending;
                break;
            default:
                BGM.clip = null;
                break;
        }
        if (BGM.clip != null) BGM.Play();
        else BGM.Pause();
    }

    IEnumerator FadeIn()
    {
        float f_time = 0f;
        float currVolume = BGM.volume;
        while (BGM.volume < 0.9f)
        {
            f_time += UnityEngine.Time.deltaTime;
            BGM.volume = Mathf.Lerp(currVolume, 1, f_time);
            yield return null;
        }
        BGM.volume = 1f;
    }

    IEnumerator FadeOutIn()
    {
        float f_time = 0f;
        BGM.volume = 1f;
        while (BGM.volume > 0.3f)
        {
            f_time += UnityEngine.Time.deltaTime;
            BGM.volume = Mathf.Lerp(1, 0, f_time);
            yield return null;
        }
        if (nowIndex != selectedIndex)
        {
            selectedIndex = nowIndex;
            BGM.Pause();
            Set_BGM(nowIndex);
        }
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeOut() // 미사용중
    {
        float f_time = 0f;
        float currVolume = BGM.volume;
        BGM.volume = 1f;
        while (BGM.volume > 0)
        {
            f_time += UnityEngine.Time.deltaTime;
            BGM.volume = Mathf.Lerp(currVolume, 0, f_time);
            yield return null;
        }
        BGM.Pause();
    }
}