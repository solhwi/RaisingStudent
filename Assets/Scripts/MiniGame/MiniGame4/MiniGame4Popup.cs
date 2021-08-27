using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MiniGame4Popup : MonoBehaviour
{
    [Header("Set In Editor")]
    [SerializeField] Image answerImage;
    [SerializeField] Text answerText;
    [SerializeField] Text textDes;

    [SerializeField] Sprite answerIcon;
    [SerializeField] Sprite wrongIcon;

    public void Set_Popup(bool _answer, string _textDes)
    {
        textDes.text = _textDes;

        if (_answer)
        {
            SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.chime);
            answerText.text = "정답입니다!";
            answerImage.sprite = answerIcon;
        }
        else
        {
            SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.wrong);
            answerText.text = "틀렸습니다!";
            answerImage.sprite = wrongIcon;
        }
    }
}
