using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SFXMgr : MonoBehaviour
{
    private static SFXMgr instance;
    [SerializeField] List<AudioClip> sfxs = new List<AudioClip>();
    AudioSource SFX;

    public enum SFXName
    {
        alarm, button, cardopen, carrierdown, cat, chime, clock, coin, correct,
        deskhit, doorclose, dooropen, mapdown, mapup, noisedown, ochestrahit, paper,
        shopbell, walk, wrong, zipperoff, zipperon, win, lose, beep, drink
    }

    public static SFXMgr Instance
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
        SFX = GetComponent<AudioSource>();

        DontDestroyOnLoad(gameObject);
    }

    public void Play_SFX(SFXName sfxName)
    {
        SFX.clip = sfxs[(int)sfxName];
        SFX.Play();
    }

    public void OverlapPlay_SFX(SFXName sfxName)
    {
        SFX.PlayOneShot(sfxs[(int)sfxName]);
    }
}