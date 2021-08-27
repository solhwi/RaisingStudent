using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ProfessoImage : MonoBehaviour
{
    public Sprite clickSprite;
    public Sprite unclickSprite;

    public Image professoImage;

    void Start()
    {
        professoImage = this.gameObject.GetComponent<Image>();
    }

    public void Click(bool click)
    {

        if (click)
        {
            Debug.Log("함수 실행1");
            professoImage.sprite = clickSprite;
        }
        else
        {
            Debug.Log("함수 실행2");
            professoImage.sprite = unclickSprite;
        }
    }
}