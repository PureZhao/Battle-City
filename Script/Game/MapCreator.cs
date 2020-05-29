using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class MapCreator : MonoBehaviour
{
    public static MapCreator _instance;
    public GameObject[] mapElement = new GameObject[7];
    private List<GameObject> map = new List<GameObject>();
    private List<GameObject> home = new List<GameObject>();
    private GameObject curLiveTool;              //x     0 ~ -1      y     >-3
    private bool isEditMapExist = false;
    void Awake()
    {

        _instance = this;
        if (GameObject.Find("Information") != null)
        {
            isEditMapExist = GameObject.Find("Information").GetComponent<Infomation>().IsHadEditMap;
        }
        BindMapElements();
    }
    //Bind Map Element Source
    //Brick 0   Iron 1  Grass   2   Ocean   3   SandLand    4   Heart   5   BrokenHeart     6
    void BindMapElements() {
        mapElement[0] = Resources.Load("Prefab/MapElement/Brick") as GameObject;
        mapElement[1] = Resources.Load("Prefab/MapElement/Iron") as GameObject;
        mapElement[2] = Resources.Load("Prefab/MapElement/Grass") as GameObject;
        mapElement[3] = Resources.Load("Prefab/MapElement/Ocean") as GameObject;
        mapElement[4] = Resources.Load("Prefab/MapElement/SandLand") as GameObject;
        mapElement[5] = Resources.Load("Prefab/MapElement/Heart") as GameObject;
        mapElement[6] = Resources.Load("Prefab/MapElement/BrokenHeart") as GameObject;
    }
    //Create Home
    void CreateHome()
    {
        string homeInfo = File.ReadAllText(Application.streamingAssetsPath + "/Home.txt", Encoding.UTF8);       //Read Txt
        string[] eachElementInfo = homeInfo.Split('\n');          //Depart Each Line
        foreach (string t in eachElementInfo)
        {
            string[] info = t.Split(',');                   //Depart Each Column
            float x = float.Parse(info[1]);
            float y = float.Parse(info[2]);             //Read Position4
            GameObject g = new GameObject();
            switch (info[0])
            {
                case "UpIron":
                    float[] aX = new float[2] { 0.125f, -0.125f };
                    float[] aY = new float[2] { 0.125f, 0.125f };
                    for (int id = 0; id < 2; id++)
                    {
                        g = Instantiate(mapElement[1], new Vector3(x + aX[id], y + aY[id], 0), Quaternion.identity) as GameObject;
                        home.Add(g);
                        g.transform.SetParent(transform);
                    }
                    break;
                case "UpBrick":
                    float[] bX = new float[2] { 0.125f, -0.125f };
                    float[] bY = new float[2] { 0.125f, 0.125f };
                    for (int id = 0; id < 2; id++)
                    {
                        g = Instantiate(mapElement[0], new Vector3(x + bX[id], y + bY[id], 0), Quaternion.identity) as GameObject;
                        home.Add(g);
                        g.transform.SetParent(transform);
                    }
                    break;
                case "RightIron":
                    float[] cX = new float[2] { 0.125f, 0.125f };
                    float[] cY = new float[2] { 0.125f, -0.125f };
                    for (int id = 0; id < 2; id++)
                    {
                        g = Instantiate(mapElement[1], new Vector3(x + cX[id], y + cY[id], 0), Quaternion.identity) as GameObject;
                        home.Add(g);
                        g.transform.SetParent(transform);
                    }
                    break;
                case "RightBrick":
                    float[] dX = new float[2] { 0.125f, 0.125f };
                    float[] dY = new float[2] { 0.125f, -0.125f };
                    for (int id = 0; id < 2; id++)
                    {
                        g = Instantiate(mapElement[0], new Vector3(x + dX[id], y + dY[id], 0), Quaternion.identity) as GameObject;
                        home.Add(g);
                        g.transform.SetParent(transform);
                    }
                    break;
                case "DownIron":
                    float[] eX = new float[2] { -0.125f, 0.125f };
                    float[] eY = new float[2] { -0.125f, -0.125f };
                    for (int id = 0; id < 2; id++)
                    {
                        g = Instantiate(mapElement[1], new Vector3(x + eX[id], y + eY[id], 0), Quaternion.identity) as GameObject;
                        home.Add(g);
                        g.transform.SetParent(transform);
                    }
                    break;
                case "DownBrick":
                    float[] fX = new float[2] { -0.125f, 0.125f };
                    float[] fY = new float[2] { -0.125f, -0.125f };
                    for (int id = 0; id < 2; id++)
                    {
                        g = Instantiate(mapElement[0], new Vector3(x + fX[id], y + fY[id], 0), Quaternion.identity) as GameObject;
                        home.Add(g);
                        g.transform.SetParent(transform);
                    }
                    break;
                case "LeftIron":
                    float[] gX = new float[2] { -0.125f, -0.125f };
                    float[] gY = new float[2] { 0.125f, -0.125f };
                    for (int id = 0; id < 2; id++)
                    {
                        g = Instantiate(mapElement[1], new Vector3(x + gX[id], y + gY[id], 0), Quaternion.identity) as GameObject;
                        home.Add(g);
                        g.transform.SetParent(transform);
                    }
                    break;
                case "LeftBrick":
                    float[] hX = new float[2] { -0.125f, -0.125f };
                    float[] hY = new float[2] { 0.125f, -0.125f };
                    for (int id = 0; id < 2; id++)
                    {
                        g = Instantiate(mapElement[0], new Vector3(x + hX[id], y + hY[id], 0), Quaternion.identity) as GameObject;
                        home.Add(g);
                        g.transform.SetParent(transform);
                    }
                    break;
                case "WholeIron":
                    float[] iX = new float[4] { 0.125f, 0.125f, -0.125f, -0.125f };
                    float[] iY = new float[4] { 0.125f, -0.125f, 0.125f, -0.125f };
                    for (int id = 0; id < 4; id++)
                    {
                        g = Instantiate(mapElement[1], new Vector3(x + iX[id], y + iY[id], 0f), Quaternion.identity) as GameObject;
                        home.Add(g);
                        g.transform.SetParent(transform);
                    }
                    break;
                case "WholeBrick":
                    float[] jX = new float[4] { 0.125f, 0.125f, -0.125f, -0.125f };
                    float[] jY = new float[4] { 0.125f, -0.125f, 0.125f, -0.125f };
                    for (int id = 0; id < 4; id++)
                    {
                        g = Instantiate(mapElement[0], new Vector3(x + jX[id], y + jY[id], 0f), Quaternion.identity) as GameObject;
                        home.Add(g);
                        g.transform.SetParent(transform);
                    }
                    break;
                case "Grass": Instantiate(mapElement[2], new Vector3(x, y, 0f), Quaternion.identity); break;
                case "Ocean": Instantiate(mapElement[3], new Vector3(x, y, 0f), Quaternion.identity); break;
                case "SandLand": Instantiate(mapElement[4], new Vector3(x, y, 0f), Quaternion.identity); break;
            }
        }
    }
    //Create Map
    void CreateMap() {
        string mapInfo = "";
        if (isEditMapExist)
        {
            mapInfo = File.ReadAllText(Application.streamingAssetsPath + "/EditMap.txt", Encoding.UTF8);       //Read Txt
            isEditMapExist = false;
        }
        else {
            mapInfo = File.ReadAllText(Application.streamingAssetsPath + "/"+GameManager.gameManager.CurStage.ToString()+".txt", Encoding.UTF8);       //Read Txt
        }
        string[] eachElementInfo = mapInfo.Split('\n');          //Depart Each Line
        foreach(string t in eachElementInfo){
            string[] info = t.Split(',');                   //Depart Each Column
            float x = float.Parse(info[1]);     
            float y = float.Parse(info[2]);             //Read Position4
            GameObject g = new GameObject();
            switch (info[0]) {
                case "Heart": 
                                        g = Instantiate(mapElement[5], new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                                        map.Add(g);
                                        g.transform.SetParent(transform); 
                    break;
                case "UpIron":
                    float[] aX = new float[2] { 0.125f, -0.125f };
                    float[] aY = new float[2] { 0.125f, 0.125f };
                    for (int id = 0; id < 2; id++) {
                        g = Instantiate(mapElement[1], new Vector3(x + aX[id], y + aY[id], 0), Quaternion.identity) as GameObject;
                       map.Add(g);
                       g.transform.SetParent(transform); 
                    }
                    break;
                case "UpBrick":
                    float[] bX = new float[2] { 0.125f, -0.125f };
                    float[] bY = new float[2] { 0.125f, 0.125f };
                    for (int id = 0; id < 2; id++) {
                        g = Instantiate(mapElement[0], new Vector3(x + bX[id], y + bY[id], 0), Quaternion.identity) as GameObject;
                       map.Add(g);
                       g.transform.SetParent(transform); 
                    }
                        break;
                case "RightIron":
                    float[] cX = new float[2] { 0.125f, 0.125f };
                    float[] cY = new float[2] { 0.125f, -0.125f };
                    for (int id = 0; id < 2; id++) {
                        g = Instantiate(mapElement[1], new Vector3(x + cX[id], y + cY[id], 0), Quaternion.identity) as GameObject;
                       map.Add(g);
                       g.transform.SetParent(transform); 
                    }
                    break;
                case "RightBrick":
                    float[] dX = new float[2] { 0.125f, 0.125f };
                    float[] dY = new float[2] { 0.125f, -0.125f };
                    for (int id = 0; id < 2; id++) {
                        g = Instantiate(mapElement[0], new Vector3(x + dX[id], y + dY[id], 0), Quaternion.identity) as GameObject;
                       map.Add(g);
                       g.transform.SetParent(transform); 
                    }
                    break;
                case "DownIron": 
                    float[] eX = new float[2] { -0.125f, 0.125f };
                    float[] eY = new float[2] { -0.125f, -0.125f };
                    for (int id = 0; id < 2; id++) {
                        g = Instantiate(mapElement[1], new Vector3(x + eX[id], y + eY[id], 0), Quaternion.identity) as GameObject;
                       map.Add(g);
                       g.transform.SetParent(transform); 
                    }
                    break;
                case "DownBrick":
                    float[] fX = new float[2] { -0.125f, 0.125f };
                    float[] fY = new float[2] { -0.125f, -0.125f };
                    for (int id = 0; id < 2; id++) {
                        g = Instantiate(mapElement[0], new Vector3(x + fX[id], y + fY[id], 0), Quaternion.identity) as GameObject;
                       map.Add(g);
                       g.transform.SetParent(transform); 
                    }
                    break;
                case "LeftIron": 
                     float[] gX = new float[2] { -0.125f, -0.125f };
                    float[] gY = new float[2] { 0.125f, -0.125f };
                    for (int id = 0; id < 2; id++) {
                        g = Instantiate(mapElement[1], new Vector3(x + gX[id], y + gY[id], 0), Quaternion.identity) as GameObject;
                       map.Add(g);
                       g.transform.SetParent(transform); 
                    }
                    break;
                case "LeftBrick":
                    float[] hX = new float[2] { -0.125f, -0.125f };
                    float[] hY = new float[2] { 0.125f, -0.125f };
                    for (int id = 0; id < 2; id++) {
                        g = Instantiate(mapElement[0], new Vector3(x + hX[id], y + hY[id], 0), Quaternion.identity) as GameObject;
                        map.Add(g);
                        g.transform.SetParent(transform); 
                    }
                    break;
                case "WholeIron": 
                    float[] iX = new float[4] {0.125f,0.125f,-0.125f,-0.125f };
                    float[] iY = new float[4] {0.125f,-0.125f,0.125f,-0.125f };
                    for (int id = 0; id < 4; id++) {
                        g = Instantiate(mapElement[1], new Vector3(x + iX[id], y + iY[id], 0f), Quaternion.identity) as GameObject;
                        map.Add(g);
                        g.transform.SetParent(transform);
                    }
                    break;
                case "WholeBrick": 
                    float[] jX = new float[4] {0.125f,0.125f,-0.125f,-0.125f };
                    float[] jY = new float[4] {0.125f,-0.125f,0.125f,-0.125f };
                    for (int id = 0; id < 4; id++) {
                        g = Instantiate(mapElement[0], new Vector3(x + jX[id], y + jY[id], 0f), Quaternion.identity) as GameObject;
                        map.Add(g);
                        g.transform.SetParent(transform);
                     }
                    break;
                case "Grass": Instantiate(mapElement[2], new Vector3(x, y, 0f), Quaternion.identity) ; break;
                case "Ocean": Instantiate(mapElement[3], new Vector3(x, y, 0f), Quaternion.identity); break;
                case "SandLand": Instantiate(mapElement[4], new Vector3(x, y, 0f), Quaternion.identity); break;
            }
        }
    }
    //Destroy Map
    void DestroyMap() {
        foreach (GameObject g in map) {
            Destroy(g);
        }
        map.Clear();
        foreach (GameObject t in home)
        {
            Destroy(t);
        }
        home.Clear();
    }
    //Protect Home
    IEnumerator ProtectHome()
    {
        Sprite brick = mapElement[0].GetComponent<SpriteRenderer>().sprite;
        Sprite iron = mapElement[1].GetComponent<SpriteRenderer>().sprite;
        foreach (GameObject g in home)
        {
            g.GetComponent<SpriteRenderer>().sprite = iron;
            g.tag = "Iron";
        }
        yield return new WaitForSeconds(5f);
        foreach (GameObject g in home)
        {
            g.GetComponent<SpriteRenderer>().sprite = brick;
        }
        yield return new WaitForSeconds(0.2f);
        foreach (GameObject g in home)
        {
            g.GetComponent<SpriteRenderer>().sprite = iron;
        }
        yield return new WaitForSeconds(0.2f);
        foreach (GameObject g in home)
        {
            g.GetComponent<SpriteRenderer>().sprite = brick;
        }
        yield return new WaitForSeconds(0.2f);
        foreach (GameObject g in home)
        {
            g.GetComponent<SpriteRenderer>().sprite = iron;
        }
        yield return new WaitForSeconds(0.2f);
        foreach (GameObject g in home)
        {
            g.GetComponent<SpriteRenderer>().sprite = brick;
            g.tag = "Brick";
        }
    }
    //Tool
    void CreateTool() {
        float x = 2.5f - Random.Range(0, 24) * 0.25f;
        float y = 0f;
        while (true) {
            if (x < 0f && x > -1f)
            {
                y = 2.5f - Random.Range(0, 24) * 0.25f;
                if (y >= -3f)
                {
                    break;
                }
                else
                {
                    continue;
                }
            }
            else {
                y = 2.5f - Random.Range(0, 24) * 0.25f;
                break;
            }
        }
        int id = Random.Range(0, 6);
        Destroy(curLiveTool);
        Vector3 appearPos = new Vector3(x, y, 0f);
        AudioSource.PlayClipAtPoint(ObjectPool._instance.BonusAppear, appearPos);
        curLiveTool = Instantiate(ObjectPool._instance.Tools[id], appearPos, Quaternion.identity) as GameObject;
    }
}
