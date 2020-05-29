using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class UIController : MonoBehaviour
{
    public static UIController _instance;
    //Round Start
    private RectTransform upCurtain;
    private RectTransform downCurtain;
    private Text curStageCentral;
    //In Game
    private Text player1Life;
    private Text player2Life;
    private GameObject player2Image;
    private RectTransform enemyCountContainer;
    private List<GameObject> enemyImageList = new List<GameObject>();
    private GameObject enemyImage;
    private Text curStageAtFlag;
    //Round End
    private GameObject endObject;
    private Text player1Score;
    private Text player2Score;
    private Text highScore;
    private Text curStageInEnd;
    private Text[] player1killCount = new Text[4];
    private Text[] player2killCount = new Text[4];
    private Text[] player1killScore = new Text[4];
    private Text[] player2killScore = new Text[4];
    private Text player1Total;
    private Text player2Total;
    //Game Over
    private RectTransform gameOver;
    private GameObject stageOver;
    //Life Change
    void LifeChange()
    {
        player1Life.text = GameManager.gameManager.Player1Life.ToString();
        player2Life.text = GameManager.gameManager.Player2Life.ToString();
    }
    //Enmey Spawn
    void EnemySpawnUIChange()
    {
        foreach (GameObject g in enemyImageList)
        {
            if (g.activeSelf == true)
            {
                g.SetActive(false);
                break;
            }
        }
    }
    //EnemyKilled
    void EnemyKilled()
    {
        GameManager.gameManager.EnemyQuantity--;
        if (GameManager.gameManager.EnemyQuantity == 0)
        {
            StartCoroutine(RoundEnd());
        }

    }
    //Round Start
    IEnumerator RoundStart() {
        GameManager.gameManager.CurStage++;
        if (GameManager.gameManager.CurStage == 36) {
            StartCoroutine(StageOver());
            yield break;
        }
        GameManager.gameManager.EnemyQuantity = 20;
        upCurtain.DOLocalMoveY(400f, 1f, true).Play();
        downCurtain.DOLocalMoveY(-400f, 1f, true).Play();
        string num;
        if (GameManager.gameManager.CurStage < 10)
        {
            num = "0" + GameManager.gameManager.CurStage.ToString();
        }
        else
        {
            num = GameManager.gameManager.CurStage.ToString();
        }
        yield return new WaitForSeconds(1f);
        curStageCentral.text = "STAGE " + num;
        curStageAtFlag.text = num;
        curStageInEnd.text = num;
        MapCreator._instance.SendMessage("DestroyMap");
        yield return new WaitForSeconds(1f);
        MapCreator._instance.SendMessage("CreateHome");
        MapCreator._instance.SendMessage("CreateMap");
        AudioSource.PlayClipAtPoint(ObjectPool._instance.GameStartAudio, Vector3.zero);
        yield return new WaitForSeconds(1f);
        GameManager.gameManager.SendMessage("EnemySpawn");
        if (GameManager.gameManager.playerMode == GameManager.PlayerMode.Single)
        {
            if (GameManager.gameManager.IsFirstRun == true)
            {
                GameManager.gameManager.StartCoroutine("Player1Spawn");
                GameManager.gameManager.IsFirstRun = false;
            }
            else
            {
                GameManager.gameManager.StartCoroutine("ResetPlayerPosition");
            }
        }
        else
        {
            if (GameManager.gameManager.IsFirstRun == true)
            {
                GameManager.gameManager.StartCoroutine("Player1Spawn");
                GameManager.gameManager.StartCoroutine("Player2Spawn");
                GameManager.gameManager.IsFirstRun = false;
            }
            else
            {
                GameManager.gameManager.StartCoroutine("ResetPlayerPosition");
            }
        }
        foreach (GameObject g in enemyImageList)
        {
            g.SetActive(true);
        }
        curStageCentral.text = "";
        upCurtain.DOLocalMoveY(800f, 1f, true).Play();
        downCurtain.DOLocalMoveY(-800f, 1f, true).Play();
    }
    //Round End
    IEnumerator RoundEnd() {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject g in players) {
            g.GetComponent<AudioSource>().mute = true;
        }
        for (int i = 0; i < 4; i++)
        {
            player1killCount[i].text = "";
            player1killScore[i].text = "";
            player2killCount[i].text = "";
            player2killScore[i].text = "";
        }
        player1Score.text = GameManager.gameManager.Player1LastRoundScore.ToString();
        player2Score.text = GameManager.gameManager.Player2LastRoundScore.ToString();
        player1Total.text = "";
        player2Total.text = "";
        int player1RoundScore = 0;
        int player2RoundScore = 0;
        int player1RoundKillCount = 0;
        int player2RoundKillCount = 0;
        for (int id = 0; id < 5; id++)
        {
            player1RoundScore += GameManager.gameManager.Player1Killed[id] * ObjectPool._instance.EachElement[id];
            player2RoundScore += GameManager.gameManager.Player2Killed[id] * ObjectPool._instance.EachElement[id];
            if (id < 4)
            {
                player1RoundKillCount += GameManager.gameManager.Player1Killed[id];
                player2RoundKillCount += GameManager.gameManager.Player2Killed[id];
            }
        }
        yield return new WaitForSeconds(3f);
        endObject.SetActive(true);
        for (int i = 0; i < 4; i++) {
            int a = GameManager.gameManager.Player1Killed[i];
            int b = GameManager.gameManager.Player2Killed[i];
            int c = Mathf.Max(a, b);
            for (int j = 0; j <= c; j++) {
                if (j <= a) {
                    player1killCount[i].text = j.ToString();
                    player1killScore[i].text = (j * ObjectPool._instance.EachElement[i]).ToString();
                }
                if (j <= b)
                {
                    player2killCount[i].text = j.ToString();
                    player2killScore[i].text = (j * ObjectPool._instance.EachElement[i]).ToString();
                }
                yield return new WaitForSeconds(0.25f);
            }
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.5f);
        player1Total.text = player1RoundKillCount.ToString();
        player2Total.text = player2RoundKillCount.ToString();
        GameManager.gameManager.Player1LastRoundScore += player1RoundScore;
        GameManager.gameManager.Player2LastRoundScore += player2RoundScore;
        player1Score.text = GameManager.gameManager.Player1LastRoundScore.ToString();
        player2Score.text = GameManager.gameManager.Player2LastRoundScore.ToString();
        yield return new WaitForSeconds(3f);
        endObject.SetActive(false);
        for (int id = 0; id < 5; id++) {
            GameManager.gameManager.Player1Killed[id] = 0;
            GameManager.gameManager.Player2Killed[id] = 0;
        }
            StartCoroutine(RoundStart());
    }
    //GameOver
    IEnumerator GameOver() {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject g in players)
        {
            g.GetComponent<PlayerControl>().enabled = false;
            g.GetComponent<AudioSource>().mute = true;
        }
        gameOver.DOLocalMoveY(0, 3f).Play();
        AudioSource.PlayClipAtPoint(ObjectPool._instance.GameOverAudio, Vector3.zero);
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene("Menu");
    }
    IEnumerator StageOver() {
        stageOver.SetActive(true);
        yield return new WaitForSeconds(1f);
        AudioSource.PlayClipAtPoint(ObjectPool._instance.GameOverAudio, Vector3.zero);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Menu");
    }
    void Awake()
    {
        _instance = this;

        upCurtain = transform.Find("UpCurtain").GetComponent<RectTransform>();
        downCurtain = transform.Find("DownCurtain").GetComponent<RectTransform>();
        curStageCentral = transform.Find("CurStageCentral").GetComponent<Text>();

        player1Life = transform.Find("Player1Life").GetComponent<Text>();
        player2Life = transform.Find("Player2Life").GetComponent<Text>();
        player2Image = transform.Find("Player2").gameObject;
        enemyCountContainer = transform.Find("EnemyCountContainer").GetComponent<RectTransform>();
        enemyImage = Resources.Load("Prefab/Effect/EnemyImage") as GameObject;
        curStageAtFlag = transform.Find("CurStageAtFlag").GetComponent<Text>();

        endObject = transform.Find("CalcScore").gameObject;
        player1Score = transform.Find("CalcScore/Player1/Player1Score").GetComponent<Text>();
        player2Score = transform.Find("CalcScore/Player2/Player2Score").GetComponent<Text>();
        highScore = transform.Find("CalcScore/HighScore").GetComponent<Text>();
        curStageInEnd = transform.Find("CalcScore/CurStage").GetComponent<Text>();
        player1killCount[0] = transform.Find("CalcScore/Player1/NormalCount").GetComponent<Text>();
        player1killCount[1] = transform.Find("CalcScore/Player1/QuickerCount").GetComponent<Text>();
        player1killCount[2] = transform.Find("CalcScore/Player1/PretenderCount").GetComponent<Text>();
        player1killCount[3] = transform.Find("CalcScore/Player1/HeavyerCount").GetComponent<Text>();
        player2killCount[0] = transform.Find("CalcScore/Player2/NormalCount").GetComponent<Text>();
        player2killCount[1] = transform.Find("CalcScore/Player2/QuickerCount").GetComponent<Text>();
        player2killCount[2] = transform.Find("CalcScore/Player2/PretenderCount").GetComponent<Text>();
        player2killCount[3] = transform.Find("CalcScore/Player2/HeavyerCount").GetComponent<Text>();
        player1killScore[0] = transform.Find("CalcScore/Player1/NormalScore").GetComponent<Text>();
        player1killScore[1] = transform.Find("CalcScore/Player1/QuickerScore").GetComponent<Text>();
        player1killScore[2] = transform.Find("CalcScore/Player1/PretenderScore").GetComponent<Text>();
        player1killScore[3] = transform.Find("CalcScore/Player1/HeavyerScore").GetComponent<Text>();
        player2killScore[0] = transform.Find("CalcScore/Player2/NormalScore").GetComponent<Text>();
        player2killScore[1] = transform.Find("CalcScore/Player2/QuickerScore").GetComponent<Text>();
        player2killScore[2] = transform.Find("CalcScore/Player2/PretenderScore").GetComponent<Text>();
        player2killScore[3] = transform.Find("CalcScore/Player2/HeavyerScore").GetComponent<Text>();
        player1Total = transform.Find("CalcScore/Player1/TotalCount").GetComponent<Text>();
        player2Total = transform.Find("CalcScore/Player2/TotalCount").GetComponent<Text>();
        gameOver = transform.Find("GameOverImage").GetComponent<RectTransform>();
        stageOver = transform.Find("StageOver").gameObject;
        for (int id = 0; id < 20; id++)
        {
            GameObject imageObj = Instantiate(enemyImage);
            imageObj.GetComponent<RectTransform>().SetParent(enemyCountContainer);
            enemyImageList.Add(imageObj);
        }
    }

    void Start() {
        if (GameManager.gameManager.playerMode == GameManager.PlayerMode.Single) {
            player2Image.SetActive(false);
            player2Life.gameObject.SetActive(false);
        }
        StartCoroutine(RoundStart());
    }

}
