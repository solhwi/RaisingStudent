using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogMgr : MonoBehaviour
{
    protected static DialogMgr instance;
    public static DialogMgr Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DialogMgr>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    [Header("Set In Editor")]
    [SerializeField] public Animator talkPanel;
    [SerializeField] public Animator portraitAnim;
    [SerializeField] public Image illustImg;
    [SerializeField] public TypeEffect typeEffect;
    [SerializeField] public Text npcName;


    public int talkIndex;
    Sprite prevPortrait;

    void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void TalkDataUse(NPCdata npcdata, Objdata objdata, string[] talkData)
    {
        if (talkData == null || talkIndex == talkData.Length)
        {
            //if (npcdata != null && PlayerDataMgr.playerData_SO.GetItemCountByCode(npcdata.FavoriteItemCode) > 0 && !GameMgr.talkMgr.isBonusTalk)
            //talkPopup.SetActive(true); // 선물하기 팝업

            GameMgr.talkMgr.TalkEnded();
            talkIndex = 0;
            talkPanel.SetBool("isShow", GameMgr.talkMgr.isTalk);

            return;
        }

        GameMgr.talkMgr.isTalk = true;

        if (npcdata != null)
        {
            SetIllustData(npcdata, talkData);
        }
        else
        {
            npcName.text = null;
            typeEffect.SetMsg(talkData[talkIndex]);
            illustImg.color = new Color(1, 1, 1, 0); //투명도
        }

        talkIndex++;
        talkPanel.SetBool("isShow", GameMgr.talkMgr.isTalk);
    }

    void SetIllustData(NPCdata npcdata, string[] talkData)
    {
        for (int i = 0; i < talkData[talkIndex].Length; i++)
        {
            if (talkData[talkIndex][i] == ':') break;
            else if (i == talkData[talkIndex].Length - 1) talkData[talkIndex] += ":0";
        }

        typeEffect.SetMsg(talkData[talkIndex].Split(':')[0]);
        illustImg.sprite = GenericDataMgr.genericData_SO.GetSpriteById(npcdata.ObjId, int.Parse(talkData[talkIndex].Split(':')[1]));
        illustImg.color = new Color(1, 1, 1, 1);
        npcName.text = npcdata.ObjName;

        if (prevPortrait != illustImg.sprite) //과거 초상화와 비교
        {
            portraitAnim.SetTrigger("doEffect"); //초상화 애니메이션 실행
            prevPortrait = illustImg.sprite; //과거 초상화 갱신
        }
    }
}