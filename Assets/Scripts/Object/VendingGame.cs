using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class VendingGame : MonoBehaviour
{
    [SerializeField] GameObject mainObject;
    [SerializeField] GameObject playObject;
    [SerializeField] GameObject ChoiceObject;
    [SerializeField] GameObject ErrorText;
    [SerializeField] Image itemImage;
    [SerializeField] Image selectedItemImage;
    [SerializeField] Text itemDes;
    [SerializeField] Text priceText;
    [SerializeField] Text re_priceText;

    public List<Sprite> itemImages = new List<Sprite>();
    public int price;
    public int re_price;
    int itemNum;
    bool isGameOpen = false;
    bool isGameStop = true;

    void Awake()
    {
        for (int i = 0; i < 7; i++)
            itemImages.Add(GenericDataMgr.genericData_SO.ConsumeItemList[i].sprite);

        price = 1500;
        re_price = 500;

        priceText.text = price.ToString();
        re_priceText.text = re_price.ToString();
    }

    public void OnClick_Yes() // 사용
    {
        if (isGameStop && PlayerDataMgr.playerData_SO.gold >= price)
        {
            SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.button);
            playObject.gameObject.SetActive(true);
            mainObject.gameObject.SetActive(false);
            PlayerDataMgr.playerData_SO.UseGold(price);
            StartCoroutine("Shake_ItemImage");
        }
        else
        {
            SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.wrong);
            ErrorText.gameObject.SetActive(true);
            isGameStop = true;
            Invoke("GameStop", 2f);
        }
    }

    void GameStop() => isGameStop = false;

    public void OnClick_VendingGame()
    {
        if (!isGameStop) return;

        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.button);
        isGameOpen = !isGameOpen;
        gameObject.SetActive(isGameOpen);
    }

    public void OnClick_Select()
    {
        if (!isGameStop) return;

        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.coin);
        ChoiceObject.gameObject.SetActive(false);
        StartCoroutine("Shake_ItemImage");
    }

    public void OnClick_UnSelect()
    {
        if (!isGameStop) return;

        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.button);
        mainObject.gameObject.SetActive(true);
        playObject.gameObject.SetActive(false);
        ChoiceObject.gameObject.SetActive(false);
        OnClick_VendingGame();

        string _code = GenericDataMgr.genericData_SO.ConsumeItemList[itemNum].code;
        PlayerDataMgr.playerData_SO.AddItemByCode(_code);
    }

    IEnumerator Shake_ItemImage()
    {
        float speed = 0.01f;
        float shakeTime = 3f;
        isGameStop = false;

        while (shakeTime > 0.1f)
        {
            itemNum = Random.Range(0, 7);

            yield return new WaitForSeconds(speed);
            speed += 0.01f;

            itemImage.sprite = itemImages[itemNum];
            shakeTime -= speed;
            yield return null;
        }

        selectedItemImage.sprite = itemImage.sprite;

        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.chime);
        itemDes.text = "\" " + GenericDataMgr.genericData_SO.ConsumeItemList[itemNum].name + " \"(이)가 나왔습니다.!";
        ChoiceObject.gameObject.SetActive(true);

        isGameStop = true;
    }
}