using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DDRJoystick : MonoBehaviour
{
    [Header("Set By Finder")]
    [SerializeField] MiniGame3 miniGame3;

    public void OnClick_Arrow(int dir)
    {
        miniGame3.OnClick_Arrow(dir);
    }
}