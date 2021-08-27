using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MiniPlayer : MonoBehaviour
{
    [Header("Set In Editor")]
    [SerializeField] public Slider hpbar;

    [Header("Fixed Data")]
    [SerializeField] public int hitCount = 5;

    void OnTriggerEnter2D(Collider2D other)
    {
        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.wrong);
        hitCount--;
        hpbar.value = hitCount;
        Destroy(other.gameObject);
    }
}