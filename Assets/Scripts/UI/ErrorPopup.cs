using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorPopup : MonoBehaviour
{
    public void TurnOnErrorPopup()
    {
        gameObject.SetActive(true);
    }

    public void OnClickOkay()
    {
        gameObject.SetActive(false);
    }
}

