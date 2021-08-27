using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UICanvas : MonoBehaviour
{
    protected static UICanvas instance;
    public static UICanvas Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UICanvas>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    [Header("Set In Editor")]
    [SerializeField] Inventory inventory;
    [SerializeField] QuestList questList;
    [SerializeField] SatisfactSlider satisfactSlider;
    [SerializeField] Image hungryGazy;
    [SerializeField] Image hungry;
    [SerializeField] Text gold;
    [SerializeField] Text date;
    [SerializeField] Text currMapName;
    [SerializeField] GameObject menu;
    [SerializeField] public ErrorPopup errorPopup;
    [SerializeField] public ErrorText errorText;
    [SerializeField] public CutScene cutScene;

    [SerializeField] public Tutorial tutorial;

    bool IsInventoryOpen = false;
    bool IsQuestListOpen = false;

    void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        PlayerDataMgr.playerData_SO.currentMapName = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        gold.text = PlayerDataMgr.playerData_SO.gold.ToString();
        hungryGazy.fillAmount = PlayerDataMgr.playerData_SO.hungryGazy / 100f;

        if (hungryGazy.fillAmount >= 0.7f)
            hungryGazy.color = new Color(64f / 255f, 179f / 255f, 13f / 255f, 255f / 255f);
        else if (hungryGazy.fillAmount >= 0.3f)
            hungryGazy.color = new Color(250f / 255f, 192f / 255f, 58f / 255f, 1f);
        else
            hungryGazy.color = new Color(246f / 255f, 97f / 255f, 36f / 255f, 1f);

        hungry.color = (PlayerDataMgr.playerData_SO.hungryGazy == 0) ? new Color(255f, 0f, 0f) : new Color(255f, 255f, 255f);
        date.text = PlayerDataMgr.playerData_SO.GetDate();
        currMapName.text = PlayerDataMgr.playerData_SO.GetMapname();
        satisfactSlider.Set_Slider(PlayerDataMgr.playerData_SO.satisfact);
    }

    public void TurnUI(bool b)
    {
        for (int i = 0; i < 8; i++)
        {
            transform.GetChild(i).gameObject.SetActive(b);
        }
    }

    public void SetActiveInventory()
    {
        if (menu.activeSelf) return;

        if (IsQuestListOpen)
        {
            IsQuestListOpen = false;
            questList.gameObject.SetActive(IsQuestListOpen);
        }

        IsInventoryOpen = !IsInventoryOpen;
        inventory.gameObject.SetActive(IsInventoryOpen);

        if (IsInventoryOpen) inventory.OnClickTab(0);

        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.button);
    }

    public void SetActiveQuestList()
    {
        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.button);

        if (menu.activeSelf)
            return;

        if (IsInventoryOpen)
        {
            IsInventoryOpen = false;
            inventory.gameObject.SetActive(IsInventoryOpen);
        }

        IsQuestListOpen = !IsQuestListOpen;
        questList.gameObject.SetActive(IsQuestListOpen);

        if (IsQuestListOpen)
            questList.UpdateQuestList();
        else
            questList.questDescription.gameObject.SetActive(false);
    }
}