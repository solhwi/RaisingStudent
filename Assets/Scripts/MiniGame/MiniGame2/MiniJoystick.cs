using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MiniJoystick : MonoBehaviour
{
    public MiniPlayer miniPlayer;

    void Awake()
    {
        miniPlayer = FindObjectOfType<MiniPlayer>();
    }

    public void OnClickUpButton()
    {
        if (MiniGameMgr.miniGameMgr.IsStop()) return;
        if (miniPlayer.transform.position.y >= 3f)
        {
            SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.wrong);
            return;
        }
        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.button);
        miniPlayer.transform.position = new Vector3(miniPlayer.transform.position.x, miniPlayer.transform.position.y + 1.65f, miniPlayer.transform.position.z);
        miniPlayer.hpbar.transform.position = new Vector3(miniPlayer.hpbar.transform.position.x, miniPlayer.hpbar.transform.position.y + 1.65f, miniPlayer.hpbar.transform.position.z);
    }

    public void OnClickDownButton()
    {
        if (MiniGameMgr.miniGameMgr.IsStop()) return;
        if (miniPlayer.transform.position.y <= -2.5f)
        {
            SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.wrong);
            return;
        }
        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.button);
        miniPlayer.transform.position = new Vector3(miniPlayer.transform.position.x, miniPlayer.transform.position.y - 1.65f, miniPlayer.transform.position.z);
        miniPlayer.hpbar.transform.position = new Vector3(miniPlayer.hpbar.transform.position.x, miniPlayer.hpbar.transform.position.y - 1.65f, miniPlayer.hpbar.transform.position.z);
    }
}