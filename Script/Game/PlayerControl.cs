using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private int playerID;

    private string upButton;
    private string downButton;
    private string leftButton;
    private string rightButton;
    private string fireButton;

    private GameObject shield;
    private SpriteRenderer spriteRenderer;
    private int curDir = 0;                      // 0 up 1 right 2 down 3 left
    private float moveSpeed = 2f;
    private int level = 0;

    private float shootInterval = 0.8f;
    private float shootTimer = 0.8f;
    private bool isDefend = true;
    private float defendInterval = 3f;
    private float defendTimer = 3f;
    private bool isStop = false;
    private bool isMove = true;

    private int curPicSet = 1;
    public Sprite[] curSprite1;
    public Sprite[] curSprite2;
    public Sprite[] level11;
    public Sprite[] level12;
    public Sprite[] level21;
    public Sprite[] level22;
    public Sprite[] level31;
    public Sprite[] level32;
    private AudioSource audioPlayer;
    IEnumerator SoundOn() {
        audioPlayer.mute = true;
        yield return new WaitForSeconds(1.5f);
        audioPlayer.mute = false;
    }
    void OnEnable() {
        isStop = false;
        StartCoroutine(SoundOn());
    }
    void Awake()
    {
        if (name.IndexOf('1') > 0)
        {
            upButton = "w";
            downButton = "s";
            leftButton = "a";
            rightButton = "d";
            fireButton = "space";
            playerID = 1;
        }
        else {
            upButton = "up";
            downButton = "down";
            leftButton = "left";
            rightButton = "right";
            fireButton = "right ctrl";
            playerID = 2;
        }
        shield = transform.Find("Shield").gameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioPlayer = GetComponent<AudioSource>();
    }
    
    void FixedUpdate()
    {
        if (shootTimer >= 0f)
        {
            shootTimer -= Time.fixedDeltaTime;
        }
        if (Input.GetKeyDown(fireButton) && shootTimer < 0f)
        {
            shootTimer = shootInterval;
            StartCoroutine(Shoot());
        }
        if (defendTimer >= 0f) 
        {
            defendTimer -= Time.fixedDeltaTime;
            if (defendTimer < 0) {
                isDefend = false;
                shield.SetActive(false);
            }
        }
        if (isStop) return;
        if(isMove)
            SpriteSwitch();
        Turn();
        Move();
    }
    //Movement
    void Move() {
        if (Input.GetKey(upButton)){
            if (audioPlayer.pitch < 1.4f)
                audioPlayer.pitch += 0.04f;
            isMove = true;
            transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.Self);
            return;
        }
        else if (Input.GetKey(rightButton))
        {
            if (audioPlayer.pitch < 1.4f)
                audioPlayer.pitch += 0.04f;
            isMove = true;
            transform.Translate(transform.right * moveSpeed * Time.deltaTime, Space.Self);
            return;
        }
        else if (Input.GetKey(downButton))
        {
            if (audioPlayer.pitch < 1.4f)
                audioPlayer.pitch += 0.04f;
            isMove = true;
            transform.Translate(-transform.up * moveSpeed * Time.deltaTime, Space.Self);
            return;
        }
        else if (Input.GetKey(leftButton))
        {
            if (audioPlayer.pitch < 1.4f)
                audioPlayer.pitch += 0.04f;
            isMove = true;
            transform.Translate(-transform.right * moveSpeed * Time.deltaTime, Space.Self);
            return;
        }
        else{
            audioPlayer.pitch = 1f;
            isMove = false;
        }
    }

    //Change Direction
    void Turn() {
        if (Input.GetKey(upButton) && curDir != 0)
        {
            curDir = 0;
            return;
        }
        if (Input.GetKey(rightButton) && curDir != 1)
        {
            curDir = 1;
            return;
        }
        if (Input.GetKey(downButton) && curDir != 2)
        {
            curDir = 2;
            return;
        }
        if (Input.GetKey(leftButton) && curDir != 3)
        {
            curDir = 3;
            return;
        }

    }
    void SpriteSwitch() {
        if (curPicSet == 1)
        {
            spriteRenderer.sprite = curSprite1[curDir];
            curPicSet++;
        }
        else if (curPicSet == 2)
        {
            spriteRenderer.sprite = curSprite2[curDir];
            curPicSet--;
        }
    }
    //Shoot
    IEnumerator Shoot() {
        AudioSource.PlayClipAtPoint(ObjectPool._instance.ShootAudio, transform.position);
        Vector3 pos = Vector3.zero ;
        int[] info = new int[2] { Level, PlayerID };
        switch (curDir) {
            case 0: pos = transform.position + transform.up * 0.3f; break;
            case 1: pos = transform.position + transform.right * 0.3f; break;
            case 2: pos = transform.position - transform.up * 0.3f; break;
            case 3: pos = transform.position - transform.right * 0.3f; break;
        }
        GameObject g = Instantiate(ObjectPool._instance.Bullets[curDir], pos, Quaternion.identity);
        g.SendMessage("SetInfo", info);
        if (level >= 2) {
            yield return new WaitForSeconds(0.2f);
            AudioSource.PlayClipAtPoint(ObjectPool._instance.ShootAudio, transform.position);
            GameObject t = Instantiate(ObjectPool._instance.Bullets[curDir], pos, Quaternion.identity);
            t.SendMessage("SetInfo", info);
        }
    }
    //Upgrade
    void Upgrade()
    {
        if (level == 3)
            return;
        level++;
        switch (level) {
            case 1: curSprite1 = level11; curSprite2 = level12; shootInterval /= 2f; break;
            case 2: curSprite1 = level21; curSprite2 = level22; break;
            case 3: curSprite1 = level31; curSprite2 = level32; break;
        }
    }
    //Get Shield
    void Shield() {
        shield.SetActive(true);
        defendTimer = defendInterval;
        isDefend = true;
    }
    //Get Damage
    void Dead() {
        if (isDefend)
            return;
        AudioSource.PlayClipAtPoint(ObjectPool._instance.PlayerTankDieAudio, transform.position);
        Instantiate(ObjectPool._instance.TankExplosion, transform.position, Quaternion.identity);
        GameManager.gameManager.StartCoroutine("Player"+PlayerID.ToString()+"Spawn");
        UIController._instance.SendMessage("LifeChange");
        Destroy(gameObject);
    }
    //Friendly Fire
    IEnumerator FriendlyFire() {
        isStop = true;
        float a = 0f;
        while (true) {
            spriteRenderer.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.2f);
            a += 0.2f;
            spriteRenderer.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.2f);
            a += 0.2f;
            if (a >= 2f) {
                spriteRenderer.color = new Color(1, 1, 1, 1);
                break;
            }
        }
        isStop = false;
    }
    public int PlayerID
    {
        get { return playerID; }
    }
    public int Level
    {
        get { return level; }
    }
    public int CurDir
    {
        get { return curDir; }
        set { curDir = value; }
    }
}
