using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float speed;
    public int walkCount;
    protected int currentWalkCount;

    protected Vector3 vector;

    public BoxCollider2D boxCollider;
    public LayerMask layerMask; // 통과 가능 판단
    public Animator animator;

}
