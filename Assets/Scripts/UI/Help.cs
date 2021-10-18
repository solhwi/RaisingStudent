using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Help : MonoBehaviour
{
    [SerializeField] List<GameObject> helps;

    public int curr_idx;

    void OnEnable()
    {
        curr_idx = 0;
    }

    public void OnClickNext()
    {
        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.button);
        if (helps.Count - 1 == curr_idx)
        {
            helps[0].SetActive(true);
            helps[curr_idx].SetActive(false);
            gameObject.SetActive(false);
            return;
        }

        helps[curr_idx].SetActive(false);
        curr_idx++;
        helps[curr_idx].SetActive(true);
    }
}
