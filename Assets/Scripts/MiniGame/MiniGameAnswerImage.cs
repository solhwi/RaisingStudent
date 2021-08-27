using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameAnswerImage : MonoBehaviour
{
    [Header("Set In Runtime")]
    [SerializeField] Image[] answerImages = new Image[6];
    [SerializeField] Sprite noAnswerImage;
    [SerializeField] Sprite answerImage;

    void Awake()
    {
        for (int i = 0; i < 6; i++)
            answerImages[i] = transform.GetChild(i).gameObject.GetComponent<Image>();
    }

    public void Change_NoAnswerImage(bool answer, int point)
    {
        if (answer)
            answerImages[point].sprite = answerImage;
        else
            answerImages[point].sprite = noAnswerImage;

        answerImages[point].color = new Color(1f, 0f, 0f, 1f);
    }
}