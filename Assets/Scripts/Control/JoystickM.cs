using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickM : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Joystick joystick;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (joystick.VerticalButton != 0 || joystick.HorizontalButton != 0) return;

        if (gameObject.name == "Up") joystick.VerticalButton = 1;
        else if (gameObject.name == "Down") joystick.VerticalButton = -1;
        else if (gameObject.name == "Left") joystick.HorizontalButton = -1;
        else if (gameObject.name == "Right") joystick.HorizontalButton = 1;

    }
    public void OnPointerUp(PointerEventData EventData)
    {
        if (gameObject.name == "Up") joystick.VerticalButton = 0;
        else if (gameObject.name == "Down") joystick.VerticalButton = 0;
        else if (gameObject.name == "Left") joystick.HorizontalButton = 0;
        else if (gameObject.name == "Right") joystick.HorizontalButton = 0;
    }
}
