using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ContentsMgr : MonoBehaviour
{
    public ContentsList contentsList;

    public bool ContentLock = false;
    public bool[] roadFinder = new bool[3] { false, false, false };
    public void GetContents(int ObjId)
    {
        if (!ContentLock) // 컨텐츠는 쿨타임이 있음
        {
            ContentLock = true;
            switch (ObjId)
            {
                // 퀘스트에 사용되는 특수 기물
                case 101000:
                    if (PlayerDataMgr.playerData_SO.hungryGazy <= 39 || PlayerDataMgr.playerData_SO.isStudied)
                    {
                        UICanvas.Instance.errorText.gameObject.GetComponent<Text>().text = "피로도가 부족하거나, 이미 이용했습니다.";
                        UICanvas.Instance.errorText.gameObject.SetActive(true);
                        break;
                    }
                    PlayerDataMgr.playerData_SO.isStudied = true;
                    PlayerDataMgr.playerData_SO.UseHungryGazy(40);
                    PlayerDataMgr.playerData_SO.GetSatisfact(3);
                    GameMgr.challengeMgr.SetChallengeCount(Challenge.StudyBug, 1); // 업적
                    contentsList.cutScene = UICanvas.Instance.cutScene;
                    contentsList.cutScene.gameObject.SetActive(true);
                    contentsList.cutScene.TurnOnCutScene(Cut.Study);
                    break;

                case 111000:
                    contentsList.GoToScene("Dummy");
                    break;

                case 11000:
                    if (!PlayerDataMgr.playerData_SO.isEndedTutorial && PlayerDataMgr.playerData_SO.tutorial_progress == 1)
                    {
                        PlayerDataMgr.playerData_SO.tutorial_progress = 2;
                    }
                    break;
                case 15000:
                    SFXMgr.Instance.OverlapPlay_SFX(SFXMgr.SFXName.cat);
                    break;

                case 10000:
                case 12000:
                case 13000:
                case 14000:

                case 16000:
                case 17000:
                case 18000:
                case 19000:
                case 20000:
                case 21000:
                case 22000:
                case 23000:
                case 24000:
                    break;

                // 교수님들
                case 96000:
                case 97000:
                case 98000:
                case 99000:
                    if (!PlayerDataMgr.playerData_SO.isEndedTutorial) break; // 튜토리얼땐 못 감
                    contentsList.GoToStage(ObjId);
                    break;

                // 씬 이동   
                case 301000:
                    contentsList.GoToScene("Tdong1");
                    break;
                case 303000:
                    contentsList.GoToScene("Tdong3");
                    break;

                case 303001:
                    if (!roadFinder[0])
                    {
                        roadFinder[0] = true;
                        GameMgr.challengeMgr.SetChallengeCount(Challenge.HiddenRoadFinder, 1);
                    }
                    PlayerDataMgr.Sync_Cache_To_Persis();
                    TempQuestDatasMgr.Sync_Cache_To_Persis();
                    contentsList.GoToScene("Tdong3");

                    break;
                case 304000:
                    if (!PlayerDataMgr.playerData_SO.isFoundHidden)
                    {
                        PlayerDataMgr.playerData_SO.AddGold(30000);
                        PlayerDataMgr.playerData_SO.isFoundHidden = true;
                    }
                    else
                    {
                        PlayerDataMgr.playerData_SO.AddGold(1000);
                    }

                    SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.coin);
                    PlayerDataMgr.Sync_Cache_To_Persis();
                    TempQuestDatasMgr.Sync_Cache_To_Persis();
                    contentsList.GoToScene("Tdong1");
                    break;
                case 305000:
                    contentsList.GoToScene("Tdong5");
                    break;
                case 305001:
                    if (!roadFinder[1])
                    {
                        roadFinder[1] = true;
                        GameMgr.challengeMgr.SetChallengeCount(Challenge.HiddenRoadFinder, 1);
                    }
                    PlayerDataMgr.Sync_Cache_To_Persis();
                    TempQuestDatasMgr.Sync_Cache_To_Persis();
                    contentsList.GoToScene("Tdong5");
                    break;
                case 306000:
                    contentsList.GoToScene("Tdong6");
                    break;
                case 307000:
                    contentsList.GoToScene("Tdong7");
                    break;
                case 308000:
                    if (!roadFinder[2])
                    {
                        roadFinder[2] = true;
                        GameMgr.challengeMgr.SetChallengeCount(Challenge.HiddenRoadFinder, 1);
                    }
                    PlayerDataMgr.Sync_Cache_To_Persis();
                    TempQuestDatasMgr.Sync_Cache_To_Persis();
                    contentsList.GoToScene("HiddenStage");
                    break;
                case 309000:
                    contentsList.GoToScene("Entrance");
                    break;
                case 309500:
                    Open_Door();
                    contentsList.GoToScene("Entrance");
                    break;
                case 310000:
                    contentsList.GoToScene("House");
                    break;
                case 311000:
                    contentsList.GoToScene("HallWay1");
                    break;
                case 311500:
                    Open_Door();
                    contentsList.GoToScene("HallWay1");
                    break;
                case 312000:
                    contentsList.GoToScene("HallWay2");
                    break;
                case 319000: // 홍문관에서 집앞
                    if (!PlayerDataMgr.playerData_SO.love && PlayerDataMgr.playerData_SO.girlfriendId != -1) // 누군가와 사랑에 빠지기 전임 + 여자친구에 아이디가 등록되었음
                    {
                        contentsList.cutScene = UICanvas.Instance.cutScene;
                        contentsList.cutScene.gameObject.SetActive(true);
                        PlayerDataMgr.playerData_SO.love = true;

                        switch (PlayerDataMgr.playerData_SO.girlfriendId)
                        {
                            case 10000:
                                contentsList.cutScene.TurnOnCutScene(Cut.Love_Yuzin); break;
                            case 16000:
                                contentsList.cutScene.TurnOnCutScene(Cut.Love_Nagyeong); break;
                            case 23000:
                                contentsList.cutScene.TurnOnCutScene(Cut.Love_Eunseo); break;
                        }
                    }
                    else contentsList.GoToScene("HouseFront"); break;

                case 319500:
                    Open_Door();
                    SFXMgr.Instance.Play_SFX(SFXMgr.SFXName.dooropen);
                    contentsList.GoToScene("HouseFront");
                    break;

                // 특수 기물
                case 300000:
                    contentsList.map.OnClickMap(); // 지도
                    SFXMgr.Instance.OverlapPlay_SFX(SFXMgr.SFXName.mapup);
                    break;
                case 400000:
                    contentsList.shop.OnClickShop(); // 상점
                    SFXMgr.Instance.OverlapPlay_SFX(SFXMgr.SFXName.shopbell);
                    break;
                case 401000:
                    contentsList.shop.OnClickShop(); // 상점
                    SFXMgr.Instance.OverlapPlay_SFX(SFXMgr.SFXName.shopbell);
                    break;
                case 401100:
                    contentsList.vendingGame.OnClick_VendingGame(); // 자판기 게임
                    SFXMgr.Instance.OverlapPlay_SFX(SFXMgr.SFXName.shopbell);
                    break;
                case 402000:
                    if (PlayerDataMgr.playerData_SO.SetNextDay()) // 침대
                    {
                        contentsList.cutScene = UICanvas.Instance.cutScene;
                        contentsList.cutScene.gameObject.SetActive(true);
                        contentsList.cutScene.TurnOnCutScene(Cut.Sleep);
                    }
                    else
                    {
                        UICanvas.Instance.errorText.gameObject.GetComponent<Text>().text = PlayerDataMgr.playerData_SO.GetDayOfWeek() != 2 ? "아직 수업이 남았습니다." : "아직 메인 퀘스트가 클리어되지 않았습니다.";
                        UICanvas.Instance.errorText.gameObject.SetActive(true);
                    }

                    break;
                case 403000:
                    contentsList.door = Player.Instance.scanObject.gameObject.GetComponent<Door>();
                    contentsList.door.Open_Door(); // 문
                    break;
                case 404000:
                    SFXMgr.Instance.OverlapPlay_SFX(SFXMgr.SFXName.beep);
                    Player.Instance.transform.position = new Vector3(Player.Instance.transform.position.x, Player.Instance.transform.position.y - 4, Player.Instance.transform.position.z);
                    break;
                case 405000:
                    Player.Instance.transform.position = new Vector3(Player.Instance.transform.position.x, Player.Instance.transform.position.y + 4, Player.Instance.transform.position.z);
                    break;

                case 406000:
                    contentsList.gamemap.SetActive(true);
                    break;
                case 950000:
                    GameMgr.sfxMgr.OverlapPlay_SFX(SFXMgr.SFXName.zipperoff);
                    RandomBox.instance.OnClickBox();
                    break;
            }

            if (Debug.isDebugBuild)
            {
                Debug.Log(PlayerDataMgr.playerData_SO.prevMapName);
                Debug.Log(PlayerDataMgr.playerData_SO.currentMapName);
            }
            Invoke("FreeLock", 1.0f); // 임시 방편 1초 뒤에 컨텐츠 잠금 해제
        }
    }

    private void Open_Door()
    {
        SFXMgr.Instance.OverlapPlay_SFX(SFXMgr.SFXName.dooropen);
        Player.Instance.scanObject.transform.GetChild(0).gameObject.SetActive(false);
        Player.Instance.scanObject.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void FreeLock()
    {
        ContentLock = false;
    }

}