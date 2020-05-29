using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
public class WriteMapinfo : MonoBehaviour
{
    string mapInfoPath = Application.streamingAssetsPath + "/MapInfo.txt";
    void Awake()
    {
        string mapInfo = "";
        foreach (Transform t in transform) {
            string curElementName = t.name.Substring(0, 2);
            switch (curElementName)
            {
                case "01": string a = "WholeBrick," + t.position.x.ToString() + "," + t.position.y.ToString() + ",0\n"; mapInfo += a; break;
                case "02": string b = "UpBrick," + t.position.x.ToString() + "," + t.position.y.ToString() + ",0\n"; mapInfo += b; break;
                case "03": string c = "RightBrick," + t.position.x.ToString() + "," + t.position.y.ToString() + ",0\n"; mapInfo += c; break;
                case "04": string d = "DownBrick," + t.position.x.ToString() + "," + t.position.y.ToString() + ",0\n"; mapInfo += d; break;
                case "05": string e = "LeftBrick," + t.position.x.ToString() + "," + t.position.y.ToString() + ",0\n"; mapInfo += e; break;
                case "06": string f = "WholeIron," + t.position.x.ToString() + "," + t.position.y.ToString() + ",0\n"; mapInfo += f; break;
                case "07": string g = "UpIron," + t.position.x.ToString() + "," + t.position.y.ToString() + ",0\n"; mapInfo += g; break;
                case "08": string h = "RightIron," + t.position.x.ToString() + "," + t.position.y.ToString() + ",0\n"; mapInfo += h; break;
                case "09": string i = "DownIron," + t.position.x.ToString() + "," + t.position.y.ToString() + ",0\n"; mapInfo += i; break;
                case "10": string j = "LeftIron," + t.position.x.ToString() + "," + t.position.y.ToString() + ",0\n"; mapInfo += j; break;
                case "11": string l = "Grass," + t.position.x.ToString() + "," + t.position.y.ToString() + ",0\n"; mapInfo += l; break;
                case "12": string m = "Ocean," + t.position.x.ToString() + "," + t.position.y.ToString() + ",0\n"; mapInfo += m; break;
                case "13": string n = "SandLand," + t.position.x.ToString() + "," + t.position.y.ToString() + ",0\n"; mapInfo += n; break;
            }
        }
        File.WriteAllText(mapInfoPath, mapInfo, Encoding.UTF8);
    }

}
