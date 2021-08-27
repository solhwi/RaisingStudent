using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Header("Set In Editor")]
    [SerializeField] public InventorySlot inventorySlot;
    [SerializeField] public InventoryDescription inventoryDescription;
    [SerializeField] public InventoryImage inventoryImage;
    [SerializeField] public GameObject popup;


    [Header("Set In Runtime")]
    Button[] itemButtons = new Button[18];

    int selectedTab = -1;
    int selectedIdx = 0;
    int badgeSlotCount = 6;
    int itemSlotCount = 18;

    void Awake()
    {
        for (int i = 0; i < itemSlotCount; i++)
        {
            int temp = i;
            itemButtons[temp] = inventorySlot.transform.GetChild(temp).GetComponent<Button>();
            itemButtons[temp].onClick.AddListener(() => OnClickItem(temp));
        }

        // tab 버튼은 editor에서 세팅
    }

    public void OnClickUse()
    {
        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.button);
        inventoryImage.Set_ItemImage(selectedIdx, false);

        if (inventoryDescription.itemName.text != "")
            popup.SetActive(true);
        else
            popup.SetActive(false);
    }

    public void OnClickYes()
    {
        //SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.button);
        SFXMgr.Instance.OverlapPlay_SFX(SFXMgr.SFXName.drink);
        PlayerDataMgr.playerData_SO.UseItemByCode(inventorySlot.itemList[selectedIdx].item.code);
        inventoryDescription.SetItemDescription(null, null);
        inventorySlot.UpdateItemList(selectedTab);
        popup.SetActive(false);
    }

    public void OnClickNo()
    {
        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.button);
        popup.SetActive(false);
    }

    public void OnClickItem(int index)
    {
        inventoryImage.Set_ItemImage(selectedIdx, false);

        if (index < inventorySlot.haveItemCount)
            inventoryDescription.SetItemDescription(inventorySlot.itemList[index].item.name, inventorySlot.itemList[index].item.description);
        else
            inventoryDescription.SetItemDescription(null, null);

        selectedIdx = index;
        inventoryImage.Set_ItemImage(selectedIdx, true);
    }

    public void OnClickTab(int itemType)
    {
        selectedTab = itemType;
        inventoryImage.Set_ItemImage(selectedIdx, false);
        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.button);

        bool isItem = true;
        if (itemType == 2) isItem = false;

        for (int i = 0; i < badgeSlotCount; i++)
            inventoryImage.SetActiveBadge(i, isItem);

        selectedIdx = 0;

        inventoryImage.Set_TabImage(selectedTab);
        inventorySlot.UpdateItemList(selectedTab);
        inventoryDescription.SetItemDescription(null, null);
        inventoryDescription.SetUseBtn(selectedTab);

        if (!isItem)
            inventorySlot.Set_BadgeItemSlot(!isItem);
    }
}