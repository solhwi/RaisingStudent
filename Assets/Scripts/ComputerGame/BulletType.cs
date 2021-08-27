using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletType : MonoBehaviour
{
    [Header("Set In Editor")]
    [SerializeField] public int dirX;
    [SerializeField] public int dirY;

    // [SerializeField] public bool hyperbolic;

    public float bulletSpeed = 10f;

    void Update()
    {
        if (ComputerGame.computerGame.isGameStop || ComputerGame.computerGame.isGameOver)
            transform.position = new Vector3(this.transform.position.x + Time.deltaTime * bulletSpeed * dirX, this.transform.transform.position.y + Time.deltaTime * bulletSpeed * dirY, this.transform.position.z);
    }

}
