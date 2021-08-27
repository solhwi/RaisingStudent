using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerGame : MonoBehaviour
{
    public static ComputerGame computerGame;

    [Header("Set In Editor")]
    [SerializeField] Text scoreText;
    [SerializeField] Button skillButton;

    [Header("Set By Finder")]
    [SerializeField] public GameObject uicanvas;
    [SerializeField] public GameObject boundCamera;
    [SerializeField] public PolygonCollider2D playerCollider;
    [SerializeField] public SpriteRenderer playerSpriteRenderer;

    public bool isGameOver = false;
    public bool isGameStop = false;
    public bool skillCoolTime = false;
    int score = 0;

    void Awake()
    {
        uicanvas = GameObject.Find("UICanvas");
        if (uicanvas != null) uicanvas.SetActive(false);

        playerSpriteRenderer = Player.Instance.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        playerCollider = Player.Instance.gameObject.GetComponent<PolygonCollider2D>();

        skillButton.onClick.AddListener(() => Use_Skill());
    }

    void Update()
    {
        if (isGameStop)
        {
            // 멈춰 !!
        }
        else if (isGameOver)
        {
            // 게임 오버 !!
        }
        else
        {
            scoreText.text = score.ToString();
        }
    }

    public void Use_Skill()
    {
        skillButton.gameObject.SetActive(false);
        Invoke("SkillCoolTime", 10f);

        StartCoroutine("Hiding_Player");
    }

    public IEnumerator Hiding_Player() // 그라데이션 하이딩 on, off 생각중
    {
        Color tempColor = new Color(1f, 1f, 1f, 1f);

        while (tempColor.a >= 0.4f)
        {
            tempColor.a -= Time.deltaTime / 0.4f;
            playerSpriteRenderer.color = tempColor;
            yield return null;
        }

        playerSpriteRenderer.color = tempColor;
        playerCollider.enabled = false;
        yield return new WaitForSeconds(1f);

        while (tempColor.a <= 0.95f)
        {
            tempColor.a += Time.deltaTime / 0.4f;
            playerSpriteRenderer.color = tempColor;
            yield return null;
        }

        tempColor.a = 1f;
        playerSpriteRenderer.color = tempColor;
        playerCollider.enabled = true;
    }

    void SkillCoolTime()
    {
        skillButton.gameObject.SetActive(true);
    }

}