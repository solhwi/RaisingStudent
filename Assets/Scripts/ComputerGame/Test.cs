using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Test : MonoBehaviour
{
    void Update()
    {
        this.transform.Translate(Vector3.forward * 1f);
    }
}