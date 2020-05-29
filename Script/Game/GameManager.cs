using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public enum PlayerMode { Single, Double };
    public PlayerMode playerMode = PlayerMode.Double;
    private int curStage = 0;
    private int player1Life = 3;
    private int player2Life = 3;
    private int player1LastRoundScore = 0;
    private int player2LastRoundScore = 0;
    private int[] player1Killed = new int[5] { 0, 0, 0, 0, 0 };     //N Q P H T
    private int[] player2Killed = new int[5] { 0, 0, 0, 0, 0 };
    private GameObject player1;
    private GameObject player2;
    private int enemyQuantity = 20;
    private int[] enemyPart = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
    private bool isFirstRun = true;

    //LifeUp
    public void AddLife(string id){
        if (id.IndexOf('1') > 0)
        {
            player1Life++;
        }
        else
        {
            player2Life++;
        }
        UIController._instance.SendMessage("LifeChange");
    }
    //Enemy Spawn
    IEnumerator EnemySpawn() {
        string enemyInfo = File.ReadAllText(Application.streamingAssetsPath + "/EnemyQuantity.txt", Encoding.UTF8);
        string[] eachLine = enemyInfo.Split('\n');
        foreach (string s in eachLine) {
            string[] lineInfo = s.Split(',');
            if (int.Parse(lineInfo[0]) == CurStage) {
                for (int id = 0; id < 8; id++) {
                    enemyPart[id] = int.Parse(lineInfo[id + 1]);
                }
                break;
            }
        }
        int left = 20;
        while (true) {
            int posID = Random.Range(0, 3);
            int enemyID;
            while (true) {
                enemyID = Random.Range(0, 8);
                if (enemyPart[enemyID] == 0) {
                    continue;
                }
                break;
            }
            GameObject t = Instantiate(ObjectPool._instance.BornEffect, ObjectPool._instance.EnemySpawnPoint[posID], Quaternion.identity) as GameObject;
            yield return new WaitForSeconds(1f);
            Destroy(t);
            Instantiate(ObjectPool._instance.EnemyPrefab[enemyID], ObjectPool._instance.EnemySpawnPoint[posID], Quaternion.identity);
            enemyPart[enemyID]--;
            UIController._instance.SendMessage("EnemySpawnUIChange");
            left--;
            if (left == 0) {
                yield break;
            }
            yield return new WaitForSeconds(Random.Range(0.5f,1.5f));
        }
    }
    //Player Respawn
    IEnumerator Player1Spawn() {
        if (playerMode == PlayerMode.Single && player1Life == 0) {
            UIController._instance.StartCoroutine("GameOver");
            yield break;
        }
        if (playerMode == PlayerMode.Double && player1Life == 0 && player2Life == 0) {
            UIController._instance.StartCoroutine("GameOver");
            yield break;
        }
        if (player1Life > 0)
        {
            GameObject t = Instantiate(ObjectPool._instance.BornEffect, ObjectPool._instance.Player1SpanwPoint, Quaternion.identity) as GameObject;
            yield return new WaitForSeconds(1.5f);
            Destroy(t);
            player1 = Instantiate(ObjectPool._instance.Player1Prefab, ObjectPool._instance.Player1SpanwPoint, Quaternion.identity) as GameObject;
            player1Life--;
            UIController._instance.SendMessage("LifeChange");
        }
    }
    IEnumerator Player2Spawn()
    {
        if (playerMode == PlayerMode.Double && player1Life == 0 && player2Life == 0)
        {
            UIController._instance.StartCoroutine("GameOver");
            yield break;
        }
        if (player2Life > 0)
        {
            GameObject t = Instantiate(ObjectPool._instance.BornEffect, ObjectPool._instance.Player2SpanwPoint, Quaternion.identity) as GameObject;
            yield return new WaitForSeconds(1.5f);
            Destroy(t);
            player2 = Instantiate(ObjectPool._instance.Player2Prefab, ObjectPool._instance.Player2SpanwPoint, Quaternion.identity) as GameObject;
            player2Life--;
            UIController._instance.SendMessage("LifeChange");
        }
    }
    IEnumerator ResetPlayerPosition() {
        player1.SetActive(false);
        if(playerMode == PlayerMode.Double)
            player2.SetActive(false);
        if (player1 != null) {
            player1.transform.position = ObjectPool._instance.Player1SpanwPoint;
            player1.GetComponent<PlayerControl>().CurDir = 0;
        }
        if (player2 != null)
        {
            player2.transform.position = ObjectPool._instance.Player2SpanwPoint;
            player2.GetComponent<PlayerControl>().CurDir = 0;
        }
        GameObject t1 = Instantiate(ObjectPool._instance.BornEffect, ObjectPool._instance.Player1SpanwPoint, Quaternion.identity) as GameObject;
        GameObject t2 = new GameObject();
        if(playerMode == PlayerMode.Double)
            t2 = Instantiate(ObjectPool._instance.BornEffect, ObjectPool._instance.Player2SpanwPoint, Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(2f);
        Destroy(t1);
        Destroy(t2);
        player1.SetActive(true);
        player1.SendMessage("Shield");
        if (playerMode == PlayerMode.Double)
        {
            player2.SetActive(true);
            player2.SendMessage("Shield");
        }
    }
    void Awake()
    {
        gameManager = this;
        if (GameObject.Find("Information") != null) {
            if (GameObject.Find("Information").GetComponent<Infomation>().SelectMode == 1)
                playerMode = PlayerMode.Single;
            if (GameObject.Find("Information").GetComponent<Infomation>().SelectMode == 2)
                playerMode = PlayerMode.Double;
        }
    }

    public int CurStage
    {
        get { return curStage; }
        set { curStage = value; }
    }
    public int Player1Life
    {
        get { return player1Life; }
        set { player1Life = value; }
    }
    public int Player2Life
    {
        get { return player2Life; }
        set { player2Life = value; }
    }
    public int Player1LastRoundScore
    {
        get { return player1LastRoundScore; }
        set { player1LastRoundScore = value; }
    }
    public int Player2LastRoundScore
    {
        get { return player2LastRoundScore; }
        set { player2LastRoundScore = value; }
    }
    public int[] Player1Killed
    {
        get { return player1Killed; }
        set { player1Killed = value; }
    }
    public int[] Player2Killed
    {
        get { return player2Killed; }
        set { player2Killed = value; }
    }




    public int EnemyQuantity
    {
        get { return enemyQuantity; }
        set { enemyQuantity = value; }
    }


    public bool IsFirstRun
    {
        get { return isFirstRun; }
        set { isFirstRun = value; }
    }

}
