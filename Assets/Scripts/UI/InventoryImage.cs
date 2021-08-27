using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryImage : MonoBehaviour
{
    [SerializeField] Sprite selectItemSlotSprite;
    [SerializeField] Sprite baseItemSoltSprite;
    [SerializeField] InventorySlot itemSlot;

    Image[] itemSlotImages = new Image[18]; // 아이템 슬롯 이미지
    [SerializeField] Image[] itemImages = new Image[18]; // 아이템 이미지
    GameObject[] selectTabObjects = new GameObject[3]; // 선택된 탭 이미지
    int itemSlotCount = 18;
    int tabCount = 3;

    void Awake()
    {
        for (int i = 0; i < itemSlotCount; i++)
        {
            itemImages[i] = itemSlot.gameObject.transform.GetChild(i).GetChild(0).GetComponent<Image>();
            itemSlotImages[i] = itemSlot.gameObject.transform.GetChild(i).GetComponent<Image>();
        }

        for (int i = 0; i < tabCount; i++)
            selectTabObjects[i] = transform.Find("SelectTabs").GetChild(i).gameObject;

        Set_TabImage(0);
    }

    public void Set_ItemImage(int itemIdx, bool isChoose) // 슬롯
    {
        if (isChoose) // 선택
            itemSlotImages[itemIdx].sprite = selectItemSlotSprite;
        else // 선택 해제
            itemSlotImages[itemIdx].sprite = baseItemSoltSprite;
    }

    public void Set_TabImage(int itemType)
    {
        for (int i = 0; i < tabCount; i++)
        {
            if (i == itemType)
                selectTabObjects[i].SetActive(true);
            else
                selectTabObjects[i].SetActive(false);
        }
    }

    public void SetActiveBadge(int _index, bool active) // 아이템
    {
        itemImages[_index].gameObject.SetActive(active);

        if (active) // 아이템 활성화 상태
            itemImages[_index].color = new Color(1f, 1f, 1f, 1f);
        else
            itemImages[_index].color = new Color(0f, 0f, 0f, 0.5f);
    }
}