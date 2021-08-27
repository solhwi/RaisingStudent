using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Cut
{
    Sleep,
    Study,
    Love_Yuzin,
    Love_Nagyeong,
    Love_Eunseo

}

public class CutScene : MonoBehaviour
{
    private float fadeTime = 0.8f;

    [SerializeField] public CanvasGroup canvasGroup;
    [SerializeField] private Image cutScene;
    [SerializeField] private List<Sprite> cuts;
    Cut currCut;

    public void TurnOnCutScene(Cut cut)
    {
        SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.chime);
        currCut = cut;
        UICanvas.Instance.TurnUI(false);
        gameObject.GetComponent<Animator>().SetInteger("cut_type", (int)cut);
        //StartCoroutine(FadeBackground(canvasGroup));
    }

    public void FadeBackground()
    {
        // float timeElapsed = 0f;
        // while (timeElapsed < fadeTime)
        // {
        //     fadeIn.alpha = Mathf.Lerp(0f, 1f, timeElapsed / fadeTime);
        //     yield return 0;
        //     timeElapsed += Time.deltaTime;
        // }
        // fadeIn.alpha = 1f;
        // yield return new WaitForSeconds(1f);
        switch (currCut)
        {
            case Cut.Sleep:
                if (PlayerDataMgr.playerData_SO.isEndedGrade)
                {
                    PlayerDataMgr.playerData_SO.isEndedGrade = false;
                    SceneLoader.Instance.LoadScene("Vacation");
                }
                else SceneLoader.Instance.LoadScene(PlayerDataMgr.playerData_SO.currentMapName);

                break;

            case Cut.Study:
                SceneLoader.Instance.LoadScene(PlayerDataMgr.playerData_SO.currentMapName);
                break;

            case Cut.Love_Yuzin:
            case Cut.Love_Nagyeong:
            case Cut.Love_Eunseo:
                GameMgr.challengeMgr.ChallengeQuestClear(Challenge.Love);
                SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.chime);
                GameMgr.contentsMgr.contentsList.GoToScene("HouseFront");
                break;
        }


    }

}
