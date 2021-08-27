using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonCtrl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Image image;
    [SerializeField] ProfessoImage professoImage;

    public void OnPointerDown(PointerEventData eventData)
    {
        SFXMgr.Instance.OverlapPlay_SFX(SFXMgr.SFXName.deskhit);
        StartCoroutine("Image_Transform");
        professoImage.Click(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopCoroutine("Image_Transform");
        professoImage.Click(false);
        image.transform.localScale = new Vector3(1f, 1f, 0f);
    }

    IEnumerator Image_Transform()
    {
        float imageScale = 1f;
        image.transform.localScale = new Vector3(imageScale, imageScale, 0f);

        while (imageScale < 1.2f)
        {
            imageScale += Time.deltaTime * 3;
            image.transform.localScale = new Vector3(imageScale, imageScale, 0f);
            yield return null;
        }
    }
}