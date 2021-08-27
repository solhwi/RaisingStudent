using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PrefabMove : MonoBehaviour
{
    public float speed;
    void Awake()
    {
        switch (PlayerDataMgr.playerData_SO.totalGradeProgress)
        {
            case 0: speed = 12f; break;
            case 1: speed = 13f; break;
            case 2: speed = 13f; break;
            case 3: speed = 14f; break;
            case 4: speed = 14f; break;
            case 5: speed = 15f; break;
            case 6: speed = 15f; break;
            case 7: speed = 15f; break;
            default:
                Debug.Log("StageCheck 스위치문에서 범위를 벗어남");
                speed = 15f;
                break;
        }
    }

    void Update()
    {
        if (!MiniGameMgr.miniGameMgr.IsStop())
            transform.position = new Vector3(this.transform.position.x + Time.deltaTime * speed, this.transform.position.y, this.transform.position.z);
    }
}
