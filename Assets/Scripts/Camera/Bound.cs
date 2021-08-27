using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bound : MonoBehaviour
{
    void Start()
    {
        BoundCamera.Instance.SetBound(GetComponent<BoxCollider2D>());
    }
}
