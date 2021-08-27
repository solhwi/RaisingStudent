using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Collider2D doorCollider;
    GameObject openDoor;
    GameObject closeDoor;
    void Awake()
    {
        doorCollider = gameObject.GetComponent<Collider2D>();
        openDoor = doorCollider.gameObject.transform.GetChild(0).gameObject;
        closeDoor = doorCollider.gameObject.transform.GetChild(1).gameObject;
    }
    public void Open_Door()
    {
        SFXMgr.Instance.OverlapPlay_SFX(SFXMgr.SFXName.dooropen);
        if (!doorCollider.isTrigger)
        {
            doorCollider.isTrigger = true;
            openDoor.SetActive(true);
            closeDoor.SetActive(false);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        SFXMgr.Instance.OverlapPlay_SFX(SFXMgr.SFXName.doorclose);
        if (doorCollider.isTrigger)
        {
            doorCollider.isTrigger = false;
            openDoor.SetActive(false);
            closeDoor.SetActive(true);
        }
    }
}