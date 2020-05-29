using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heavyer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private int curDir = 2;                      // 0 up 1 right 2 down 3 left
    private float moveSpeed = 1f;
    private float shootInterval = 3f;
    private float shootTimer = 3f;
    private float changeDirTimer = 5f;
    private float changeDirInterval = 5f;
    private float stopTImer = 5f;
    private float stopInterval = 5f;
    private bool isStop = false;
    private int life = 3;
    private int curPicSet = 1;
    public Sprite[] curSprite1;
    public Sprite[] curSprite2;
    public Sprite[] level11;
    public Sprite[] level12;
    public Sprite[] level21;
    public Sprite[] level22;

    //Set Default Property
    void SetDefaultProperty() {
    curDir = 2;
    moveSpeed = 1f;
    shootInterval = 3f;
    shootTimer = 3f;
    changeDirTimer = 5f;
    changeDirInterval = 5f;
    stopTImer = 5f;
    stopInterval = 5f;
    isStop = false;
    life = 3;
    curPicSet = 1;
    }
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetDefaultProperty();
    }

    void FixedUpdate()
    {
        if (isStop)
        {
            stopTImer -= Time.fixedDeltaTime;
            if (stopTImer < 0)
            {
                isStop = false;
                stopTImer = stopInterval;
            }
            return;
        }
        shootTimer -= Time.fixedDeltaTime;
        changeDirTimer -= Time.fixedDeltaTime;
        if (changeDirTimer < 0)
        {
            changeDirTimer = changeDirInterval;
            ChangeDirection();
        }
        Move();
        SwitchSprite();
        if (shootTimer < 0f)
        {
            shootTimer = shootInterval;
            Shoot();
        }
    }
    //Movement
    void Move()
    {
        if (curDir == 0)
        {
            transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.Self);
            return;
        }
        else if (curDir == 1)
        {
            transform.Translate(transform.right * moveSpeed * Time.deltaTime, Space.Self);
            return;
        }
        else if (curDir == 2)
        {
            transform.Translate(-transform.up * moveSpeed * Time.deltaTime, Space.Self);
            return;
        }
        else if (curDir == 3)
        {
            transform.Translate(-transform.right * moveSpeed * Time.deltaTime, Space.Self);
            return;
        }
    }

    //Change Direction
    void ChangeDirection()
    {
        int t = Random.Range(0, 100);           //<10 up      >10 <50 down    >50 <75 left      >75 < 100   right
        if (t < 10)
        {
            curDir = 0;
            return;
        }
        if (t >= 75)
        {
            curDir = 1;
            return;
        }
        if (t >= 10 && t <= 50)
        {
            curDir = 2;
            return;
        }
        if (t > 50 && t < 75)
        {
            curDir = 3;
            return;
        }
    }
    //Switch Sprite
    void SwitchSprite()
    {
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
    void Shoot()
    {
        AudioSource.PlayClipAtPoint(ObjectPool._instance.ShootAudio, transform.position);
        if (curDir == 0)
        {
            Instantiate(ObjectPool._instance.Bullets[curDir], transform.position + transform.up * 0.3f, Quaternion.identity);
        }
        if (curDir == 1)
        {
            Instantiate(ObjectPool._instance.Bullets[curDir], transform.position + transform.right * 0.3f, Quaternion.identity);
        }
        if (curDir == 2)
        {
            Instantiate(ObjectPool._instance.Bullets[curDir], transform.position - transform.up * 0.3f, Quaternion.identity);
        }
        if (curDir == 3)
        {
            Instantiate(ObjectPool._instance.Bullets[curDir], transform.position - transform.right * 0.3f, Quaternion.identity);
        }
    }
    //Stop
    void StopMove()
    {
        isStop = true;
    }
    //Get Damage
    void Dead(int val)
    {
        life--;
        if (life == 2) {
            AudioSource.PlayClipAtPoint(ObjectPool._instance.IronHitAudio, transform.position);
            curSprite1 = level11;
            curSprite2 = level12;
            return;
        }
        if (life == 1)
        {
            AudioSource.PlayClipAtPoint(ObjectPool._instance.IronHitAudio, transform.position);
            curSprite1 = level21;
            curSprite2 = level22;
            return;
        }
        if(val == 1)
            GameManager.gameManager.Player1Killed[3]++;
        else
            GameManager.gameManager.Player2Killed[3]++;
        Instantiate(ObjectPool._instance.TankExplosion, transform.position, Quaternion.identity);
        UIController._instance.SendMessage("EnemyKilled");
        Destroy(gameObject);
    }
}
