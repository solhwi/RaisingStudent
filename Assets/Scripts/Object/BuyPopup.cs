using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyPopup : MonoBehaviour
{
    [Header("Set In Editor")]
    [SerializeField] Text titleText;
    [SerializeField] Text descriptionText;
    [SerializeField] Image image;

    public void Set_Text(string title, string description, Sprite itemimage)
    {
        titleText.text = title;
        descriptionText.text = description;
        image.sprite = itemimage;
    }
}