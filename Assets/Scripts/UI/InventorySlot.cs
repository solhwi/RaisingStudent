using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{

    [Header("Set In Editor")]
    [SerializeField] public InventoryImage inventoryImage;
    [SerializeField] public List<HaveItemData> itemList;


    [Header("Set By Finder")]


    Image[] itemImages = new Image[18];
    Text[] itemCounts = new Text[18];


    public int itemSlotCount = 9;
    public int badgeSlotCount = 6;
    public int haveItemCount = 0;

    void Awake()
    {
        for (int i = 0; i < itemSlotCount; i++)
        {
            itemImages[i] = transform.GetChild(i).Find("Image").GetComponent<Image>();
            itemCounts[i] = transform.GetChild(i).Find("Count").GetComponent<Text>();
        }
    }

    public void UpdateItemList(int selectedTab)
    {
        UnsetItemList();
        SetItemList(selectedTab);
    }

    public void UnsetItemList()
    {
        for (int i = 0; i < itemSlotCount; i++)
        {
            itemImages[i].sprite = null;
            itemCounts[i].text = null;
            itemImages[i].gameObject.SetActive(false);
        }
    }

    public void SetItemList(int selectedTab)
    {
        switch (selectedTab)
        {
            case 0:
                itemList = PlayerDataMgr.playerData_SO.haveConsumeItems;
                SetItemCount();
                break;
            case 1:
                itemList = PlayerDataMgr.playerData_SO.haveOtherItems;
                SetItemCount();
                break;
            case 2:
                itemList = PlayerDataMgr.playerData_SO.haveBadgeItems;
                haveItemCount = badgeSlotCount;

                for (int i = 0; i < badgeSlotCount; i++)
                {
                    itemImages[i].sprite = itemList[i].item.sprite;

                    if (GameMgr.challengeMgr.badgeLocks[i])
                        inventoryImage.SetActiveBadge(i, true);
                }
                break;
        }
    }

    public void SetItemCount()
    {
        haveItemCount = 0;

        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].count > 0)
            {
                itemImages[haveItemCount].sprite = itemList[i].item.sprite;
                itemCounts[haveItemCount].text = "x" + itemList[i].count.ToString();
                itemImages[haveItemCount].gameObject.SetActive(true);
                haveItemCount++;
            }
        }
    }

    public void Set_BadgeItemSlot(bool active)
    {
        for (int i = 0; i < badgeSlotCount; i++)
            itemImages[i].gameObject.SetActive(active);
    }
}
