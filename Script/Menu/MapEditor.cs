using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
public class MapEditor : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float spriteSwitchTimer = 0.2f;
    private bool isAppear = true;
    private GameObject curEditElement;
    public GameObject[] mapElement;     //brick 0 1 2 3 4 iron 5 6 7 8 9 grass 10 ocean 11 sand 12
    private Dictionary<Vector3, GameObject> mapExist = new Dictionary<Vector3, GameObject>();
    private GameObject canvas;
    private SpriteRenderer mapLimition;
    private SpriteRenderer heart;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        canvas = GameObject.Find("Canvas");
        mapLimition = GameObject.Find("Limition").GetComponent<SpriteRenderer>();
        heart = GameObject.Find("Limition/Heart").GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        SpriteSwitch();
        EditMap();
    }

    void SpriteSwitch()
    {
        spriteSwitchTimer -= Time.deltaTime;
        if (spriteSwitchTimer < 0 && isAppear == true)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0);
            isAppear = false;
            spriteSwitchTimer = 0.2f;
        }
        else if (spriteSwitchTimer < 0 && isAppear == false)
        {
            spriteRenderer.color = new Color(1, 1, 1, 1);
            isAppear = true;
            spriteSwitchTimer = 0.2f;
        }
    }

    void EditMap() {
        if (Input.GetKeyDown(KeyCode.W))
        {

            if (transform.position.y == 2.5f)
                return;
            transform.position += Vector3.up * 0.5f;
            curEditElement = null;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {

            if (transform.position.x == 2.5f)
                return;
            transform.position += Vector3.right * 0.5f;
            curEditElement = null;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (transform.position.y == -3.5f)
                return;
            transform.position -= Vector3.up * 0.5f;
            curEditElement = null;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (transform.position.x == -3.5f)
                return;
            transform.position -= Vector3.right * 0.5f;
            curEditElement = null;
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            bool isExist = mapExist.TryGetValue(transform.position, out curEditElement);
            if (isExist == false)
            {
                curEditElement = Instantiate(mapElement[0], transform.position, Quaternion.identity) as GameObject;
                mapExist.Add(transform.position, curEditElement);
            }
            else
            {
                string curElementName = curEditElement.name.Substring(0, 2);
                switch (curElementName)
                {
                    case "01": mapExist.Remove(transform.position); Destroy(curEditElement); curEditElement = Instantiate(mapElement[1], transform.position, Quaternion.identity); mapExist.Add(transform.position, curEditElement); break;
                    case "02": mapExist.Remove(transform.position); Destroy(curEditElement); curEditElement = Instantiate(mapElement[2], transform.position, Quaternion.identity); mapExist.Add(transform.position, curEditElement); break;
                    case "03": mapExist.Remove(transform.position); Destroy(curEditElement); curEditElement = Instantiate(mapElement[3], transform.position, Quaternion.identity); mapExist.Add(transform.position, curEditElement); break;
                    case "04": mapExist.Remove(transform.position); Destroy(curEditElement); curEditElement = Instantiate(mapElement[4], transform.position, Quaternion.identity); mapExist.Add(transform.position, curEditElement); break;
                    case "05": mapExist.Remove(transform.position); Destroy(curEditElement); curEditElement = Instantiate(mapElement[5], transform.position, Quaternion.identity); mapExist.Add(transform.position, curEditElement); break;
                    case "06": mapExist.Remove(transform.position); Destroy(curEditElement); curEditElement = Instantiate(mapElement[6], transform.position, Quaternion.identity); mapExist.Add(transform.position, curEditElement); break;
                    case "07": mapExist.Remove(transform.position); Destroy(curEditElement); curEditElement = Instantiate(mapElement[7], transform.position, Quaternion.identity); mapExist.Add(transform.position, curEditElement); break;
                    case "08": mapExist.Remove(transform.position); Destroy(curEditElement); curEditElement = Instantiate(mapElement[8], transform.position, Quaternion.identity); mapExist.Add(transform.position, curEditElement); break;
                    case "09": mapExist.Remove(transform.position); Destroy(curEditElement); curEditElement = Instantiate(mapElement[9], transform.position, Quaternion.identity); mapExist.Add(transform.position, curEditElement); break;
                    case "10": mapExist.Remove(transform.position); Destroy(curEditElement); curEditElement = Instantiate(mapElement[10], transform.position, Quaternion.identity); mapExist.Add(transform.position, curEditElement); break;
                    case "11": mapExist.Remove(transform.position); Destroy(curEditElement); curEditElement = Instantiate(mapElement[11], transform.position, Quaternion.identity); mapExist.Add(transform.position, curEditElement); break;
                    case "12": mapExist.Remove(transform.position); Destroy(curEditElement); curEditElement = Instantiate(mapElement[12], transform.position, Quaternion.identity); mapExist.Add(transform.position, curEditElement); break;
                    case "13": mapExist.Remove(transform.position); Destroy(curEditElement); curEditElement = null; break;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.O)) {
            string mapInfo = "Heart,-0.5,-3.5,0\n";
            foreach (KeyValuePair<Vector3, GameObject> k in mapExist) {
                if (k.Key == new Vector3(-0.5f, -3.5f, 0f) || k.Key == new Vector3(-1.5f, -3.5f, 0f) || k.Key == new Vector3(0.5f, -3.5f, 0f) || k.Key == new Vector3(-3.5f, 2.5f, 0f) || k.Key == new Vector3(-0.5f, 2.5f, 0f) || k.Key == new Vector3(2.5f, 2.5f, 0f)) {
                    Destroy(k.Value);
                    continue;
                }
                string curElementName = k.Value.name.Substring(0,2);
                switch (curElementName)
                {
                    case "01": string a = "WholeBrick,"+k.Key.x.ToString() + "," + k.Key.y.ToString() + ",0\n"; mapInfo += a; break;
                    case "02": string b = "UpBrick," + k.Key.x.ToString() + "," + k.Key.y.ToString() + ",0\n"; mapInfo += b; break;
                    case "03": string c = "RightBrick," + k.Key.x.ToString() + "," + k.Key.y.ToString() + ",0\n"; mapInfo += c; break;
                    case "04": string d = "DownBrick," + k.Key.x.ToString() + "," + k.Key.y.ToString() + ",0\n"; mapInfo += d; break;
                    case "05": string e = "LeftBrick," + k.Key.x.ToString() + "," + k.Key.y.ToString() + ",0\n"; mapInfo += e; break;
                    case "06": string f = "WholeIron," + k.Key.x.ToString() + "," + k.Key.y.ToString() + ",0\n"; mapInfo += f; break;
                    case "07": string g = "UpIron," + k.Key.x.ToString() + "," + k.Key.y.ToString() + ",0\n"; mapInfo += g; break;
                    case "08": string h = "RightIron," + k.Key.x.ToString() + "," + k.Key.y.ToString() + ",0\n"; mapInfo += h; break;
                    case "09": string i = "DownIron," + k.Key.x.ToString() + "," + k.Key.y.ToString() + ",0\n"; mapInfo += i; break;
                    case "10": string j = "LeftIron," + k.Key.x.ToString() + "," + k.Key.y.ToString() + ",0\n"; mapInfo += j; break;
                    case "11": string l = "Grass," + k.Key.x.ToString() + "," + k.Key.y.ToString() + ",0\n"; mapInfo += l; break;
                    case "12": string m = "Ocean," + k.Key.x.ToString() + "," + k.Key.y.ToString() + ",0\n"; mapInfo += m; break;
                    case "13": string n = "SandLand," + k.Key.x.ToString() + "," + k.Key.y.ToString() + ",0\n"; mapInfo += n; break;
                }
                Destroy(k.Value);
            }
            mapExist.Clear();
            string realMapInfo = mapInfo.Substring(0, mapInfo.Length - 2);
            File.WriteAllText(Application.streamingAssetsPath + "/EditMap.txt", realMapInfo, Encoding.UTF8);
            mapLimition.color = new Color(1, 1, 1, 0);
            heart.color = new Color(1, 1, 1, 0);
            transform.position = new Vector3(2000f, 2000f, 0f);
            canvas.SetActive(true);
        }
    }

}
