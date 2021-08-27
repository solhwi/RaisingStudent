using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [Header("Set In Editor")]
    [SerializeField] BuyPopup buyPopup;
    [SerializeField] ErrorPopup errorPopup;
    [SerializeField] Text haveMoney;

    [Header("Set In Runtime")]
    List<Button> buyButtons = new List<Button>();
    List<Text> itemNames = new List<Text>();
    List<Text> itemPrices = new List<Text>();
    List<Image> itemImages = new List<Image>();
    List<ItemData> itemList = new List<ItemData>();


    [Header("Fixed Data")]
    int SelectedItemIdx = 0;
    bool isShopOpen = false;
    int consumeitemIndex = 7;
    int otherItemIndex = 7;

    void Awake()
    {
        haveMoney.text = PlayerDataMgr.playerData_SO.gold.ToString();

        if (gameObject.name == "ConsumeStore")
        {
            itemList = GenericDataMgr.genericData_SO.ConsumeItemList;
            SetShopItems(consumeitemIndex);
        }
        else if (gameObject.name == "OtherStore")
        {
            itemList = GenericDataMgr.genericData_SO.OtherItemList;
            SetShopItems(otherItemIndex);
        }
    }

    public void SetShopItems(int itemIndex)
    {
        for (int i = 0; i < itemIndex; i++)
        {
            int temp = i;

            buyButtons.Add(transform.Find("Items").Find("ScrollRect").Find("Item (" + temp.ToString() + ")").Find("BuyButton").GetComponent<Button>());
            itemNames.Add(transform.Find("Items").Find("ScrollRect").Find("Item (" + temp.ToString() + ")").Find("Name").GetComponent<Text>());
            itemImages.Add(transform.Find("Items").Find("ScrollRect").Find("Item (" + temp.ToString() + ")").Find("Image").GetComponent<Image>());
            itemPrices.Add(transform.Find("Items").Find("ScrollRect").Find("Item (" + temp.ToString() + ")").Find("Price").GetComponent<Text>());

            buyButtons[temp].onClick.AddListener(() => OnClickBuy(temp));
            itemNames[temp].text = itemList[temp].name;
            itemImages[temp].sprite = itemList[temp].sprite;
            itemPrices[temp].text = itemList[temp].price.ToString();
        }
    }

    public void OnClickBuy(int idx)
    {
        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.button);
        SelectedItemIdx = idx;
        buyPopup.Set_Text(itemList[SelectedItemIdx].name, itemList[SelectedItemIdx].description, itemList[SelectedItemIdx].sprite);
        buyPopup.gameObject.SetActive(true);
    }

    public void OnClickYesNo(bool Choice)
    {
        buyPopup.gameObject.SetActive(false);
        if (!Choice) return;

        if (itemList[SelectedItemIdx].price > PlayerDataMgr.playerData_SO.gold)
        {
            errorPopup.TurnOnErrorPopup();
            SFXMgr.Instance.OverlapPlay_SFX(SFXMgr.SFXName.wrong);
            return;
        }

        if (!PlayerDataMgr.playerData_SO.AddItemByCode(itemList[SelectedItemIdx].code))
        {
            errorPopup.TurnOnErrorPopup();
            SFXMgr.Instance.OverlapPlay_SFX(SFXMgr.SFXName.wrong);
            Debug.Log("인벤토리가 가득 찼거나, 잘못된 형식의 아이템입니다.");
            return;
        }

        PlayerDataMgr.playerData_SO.UseGold(itemList[SelectedItemIdx].price);
        haveMoney.text = PlayerDataMgr.playerData_SO.gold.ToString();
        SFXMgr.Instance.OverlapPlay_SFX(SFXMgr.SFXName.coin);
    }

    public void OnClickShop()
    {
        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.button);
        isShopOpen = !isShopOpen;
        gameObject.SetActive(isShopOpen);
    }
}