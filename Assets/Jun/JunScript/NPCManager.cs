using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCMove
{
    [Tooltip("NPCMove를 체크하면 NPC가 움직임")]
    public bool NPCmove;
    public string[] direction; // npc가 움직일 방향 설정
    [Range(1, 5)]
    public int frequency; // npc가 움직일 방향으로 얼마나 빠른 속도로 움직일 것인가.
}
public class NPCManager : MonoBehaviour

{
    [SerializeField]
    public NPCMove npc;
    void Start()
    {

    }

    void Update()
    {

    }
}
