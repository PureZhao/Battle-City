using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text;
public class OptionCursor : MonoBehaviour
{
    private Vector3[] pos = new Vector3[3] { new Vector3(-200f, -140f, 0f), new Vector3(-200f, -210f, 0f), new Vector3(-200f, -280f, 0f) };      //选择光标位置
    private Image cursorImage;
    public Sprite[] cursorSprite;
    private float spriteSwitchTimer = 0.2f;
    private int curSprite = 0;
    private Infomation info;
    private SpriteRenderer mapLimition;
    private SpriteRenderer heart;
    private GameObject editor;
    void Awake() {
        cursorImage = GetComponent<Image>();
        mapLimition = GameObject.Find("Limition").GetComponent<SpriteRenderer>();
        heart = GameObject.Find("Limition/Heart").GetComponent<SpriteRenderer>();
        editor = GameObject.Find("Limition").transform.Find("Editor").gameObject;
        info = GameObject.Find("Information").GetComponent<Infomation>();
        DontDestroyOnLoad(info.gameObject);
    }

    void ChangeOption() {
        if (Input.GetKeyDown(KeyCode.W)) {
            if (info.SelectMode == 1)
            {
                info.SelectMode = 3;
            }
            else {
                info.SelectMode--;
            }
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            if (info.SelectMode == 3)
            {
                info.SelectMode = 1;
            }
            else
            {
                info.SelectMode++;
            }
        }
        GetComponent<RectTransform>().localPosition = pos[info.SelectMode - 1];
        if (Input.GetKeyDown(KeyCode.Space)) {
            DoChoice();
        }
    }
    void SpriteSwitch() {
        spriteSwitchTimer -= Time.deltaTime;
        if (spriteSwitchTimer < 0 && curSprite == 0)
        {
            cursorImage.sprite = cursorSprite[curSprite];
            curSprite++;
            spriteSwitchTimer = 0.2f;
        }
        else if (spriteSwitchTimer < 0 && curSprite == 1)
        {
            cursorImage.sprite = cursorSprite[curSprite];
            curSprite--;
            spriteSwitchTimer = 0.2f;
        }
    }
    void DoChoice() {
        if (info.SelectMode == 1 || info.SelectMode == 2)
        {
            SceneManager.LoadScene("Game");
        }
        else {
            mapLimition.color = new Color(1, 1, 1, 1);
            heart.color = new Color(1, 1, 1, 1);
            editor.transform.position = new Vector3(-0.5f, 0f, 0f);
            info.IsHadEditMap = true;
            File.WriteAllText(Application.streamingAssetsPath + "/EditMap.txt", "", Encoding.UTF8);
            transform.parent.parent.gameObject.SetActive(false);
        }
    }
    void Update()
    {
        SpriteSwitch();
        ChangeOption();
    }
}
