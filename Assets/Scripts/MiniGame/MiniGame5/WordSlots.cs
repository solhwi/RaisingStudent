using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordSlots : MonoBehaviour
{
    [Header("Set In Editor")]
    [SerializeField] WordNote wordNote;

    [Header("Set In Runtime")]
    [SerializeField] Image[] slotImages = new Image[12];
    [SerializeField] Image[] wordSlotImages = new Image[12];
    [SerializeField] Text[] wordSlotTexts = new Text[12];

    Color[] colors = new Color[6];
    string[,] words = new string[6, 6] {
        { "double", "float", "string", "int", "char", "bool" },
        { "class", "struct", "static", "gloval", "extern", "instance" },
        { "LinkedList", "Stack", "Deque", "Queue", "Heap", "Tree" },
        { "Hash", "Greedy", "Dynamic", "BFS", "BruteForce", "Dijkstra" },
        { "Assembly", "Dictionary", "Algorithm", "Function", "Variable", "Parameter" },
        { "virtual", "abstract", "interface", "public", "private", "protected" }
    };

    int slotIndex = 12;

    void Awake()
    {
        for (int i = 0; i < slotIndex; i++)
        {
            slotImages[i] = gameObject.transform.GetChild(i).gameObject.GetComponent<Image>();
            wordSlotImages[i] = gameObject.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Image>();
            wordSlotTexts[i] = gameObject.transform.GetChild(i).GetChild(1).gameObject.GetComponent<Text>();
        }

        for (int i = 0; i < slotIndex / 2; i++)
            colors[i] = new Color(wordSlotImages[i].color.r, wordSlotImages[i].color.g, wordSlotImages[i].color.b);

        Create_words(0);
    }

    public void Create_words(int _num)
    {
        int random1 = 0;
        int random2 = 0;
        string tempStr;
        Color tempColor = new Color();

        for (int i = 0; i < slotIndex; i++) // 컬러, 단어 셋팅
        {
            int temp = i / 2;
            wordSlotImages[i].color = colors[temp];
            wordSlotTexts[i].text = words[_num, temp];
        }

        for (int i = 0; i < slotIndex * 2; i++) // 단어 섞기
        {
            random1 = Random.Range(0, 12);
            random2 = Random.Range(0, 12);

            tempStr = wordSlotTexts[random1].text;
            wordSlotTexts[random1].text = wordSlotTexts[random2].text;
            wordSlotTexts[random2].text = tempStr;
        }

        for (int i = 0; i < slotIndex * 2; i++) // 컬러 섞기
        {
            random1 = Random.Range(0, 12);
            random2 = Random.Range(0, 12);

            tempColor = wordSlotImages[random1].color;
            wordSlotImages[random1].color = wordSlotImages[random2].color;
            wordSlotImages[random2].color = tempColor;
        }

        for (int i = 0; i < slotIndex; i++)
        {
            slotImages[i].color = new Color(slotImages[i].color.r, slotImages[i].color.g, slotImages[i].color.b, 1f);
            wordSlotImages[i].color = new Color(wordSlotImages[i].color.r, wordSlotImages[i].color.g, wordSlotImages[i].color.b, 1f);
        }
    }

    public void Select_WordSlot(int index, bool select)
    {
        if (select) // 선택 (투명도 올리기)
            wordSlotImages[index].color = new Color(wordSlotImages[index].color.r, wordSlotImages[index].color.g, wordSlotImages[index].color.b, 0.5f);
        else // 선택 해제 (투명도 내리기)
            wordSlotImages[index].color = new Color(wordSlotImages[index].color.r, wordSlotImages[index].color.g, wordSlotImages[index].color.b, 1f);
    }

    public void Hiding_Words(bool hiding)
    {
        if (hiding) // 글씨 숨기기
            for (int i = 0; i < slotIndex; i++)
                wordSlotTexts[i].color = new Color(wordSlotTexts[i].color.r, wordSlotTexts[i].color.g, wordSlotTexts[i].color.b, 0f);
        else // 글씨 보이기
            for (int i = 0; i < slotIndex; i++)
                wordSlotTexts[i].color = new Color(wordSlotTexts[i].color.r, wordSlotTexts[i].color.g, wordSlotTexts[i].color.b, 1f);
    }

    public bool Compare_Words(int index1, int index2)
    {
        Select_WordSlot(index1, false);

        if (wordSlotTexts[index1].text == wordSlotTexts[index2].text)
        {
            wordNote.InputWord(wordSlotTexts[index1].text);

            wordSlotTexts[index1].text = "";
            wordSlotTexts[index2].text = "";

            slotImages[index1].color = new Color(slotImages[index1].color.r, slotImages[index1].color.b, slotImages[index1].color.g, 0f);
            wordSlotImages[index1].color = new Color(wordSlotImages[index1].color.r, wordSlotImages[index1].color.b, wordSlotImages[index1].color.g, 0f);

            slotImages[index2].color = new Color(slotImages[index2].color.r, slotImages[index2].color.b, slotImages[index2].color.g, 0f);
            wordSlotImages[index2].color = new Color(wordSlotImages[index2].color.r, wordSlotImages[index2].color.b, wordSlotImages[index2].color.g, 0f);

            SFXMgr.Instance.OverlapPlay_SFX(SFXMgr.SFXName.chime);
            return true;
        }

        SFXMgr.Instance.OverlapPlay_SFX(SFXMgr.SFXName.wrong);
        return false;
    }

    public bool SlotText_NullCheck(int _index)
    {
        if (wordSlotTexts[_index].text == "")
            return true;
        return false;
    }
}