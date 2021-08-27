using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDescription : MonoBehaviour
{
    [Header("Set In Editor")]
    [SerializeField] public Text itemName;
    [SerializeField] public Text itemDescription;
    [SerializeField] public GameObject useBtn;

    public void SetItemDescription(string itemName, string itemDescription)
    {
        this.itemName.text = itemName;
        this.itemDescription.text = itemDescription;
    }

    public void SetUseBtn(int selectedTab)
    {
        if (selectedTab == 0) useBtn.SetActive(true);
        else useBtn.SetActive(false);
    }
}
