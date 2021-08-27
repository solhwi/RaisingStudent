using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPoint : MonoBehaviour
{
    public string prevMapName;
    public Transform coordinates;
    GameObject coord;

    void Start()
    {
        prevMapName = PlayerDataMgr.playerData_SO.prevMapName;
        coord = GameObject.Find(prevMapName);

        if (coord != null)
        {
            coordinates = coord.GetComponent<Transform>();
        }

        if (coordinates == null)
            coordinates = GameObject.Find("House").GetComponent<Transform>();

        Player.Instance.transform.position = coordinates.position;
        BoundCamera.Instance.transform.position = new Vector3(this.transform.position.x
        , this.transform.position.y, BoundCamera.Instance.transform.position.z);
    }
}