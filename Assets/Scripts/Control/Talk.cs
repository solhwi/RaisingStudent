using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Talk : MonoBehaviour, IPointerDownHandler
{
    protected Player player;
    protected GameMgr gameManager;

    void Awake()
    {
        player = FindObjectOfType<Player>();
        gameManager = FindObjectOfType<GameMgr>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (player.scanObject != null)
            gameManager.Talk(player.scanObject);
    }
}
