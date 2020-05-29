using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickerTool : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private int curDir = 2;                      // 0 up 1 right 2 down 3 left
    private float moveSpeed = 3f;
    private float shootInterval = 3f;
    private float shootTimer = 3f;
    private float changeDirTimer = 5f;
    private float changeDirInterval = 5f;
    private float redSpriteTimer = 0.2f;
    private float redSpriteInterval = 0.2f;
    private float stopTImer = 5f;
    private float stopInterval = 5f;
    private bool isStop = false;

    private int curPicSet = 1;
    public Sprite[] curSprite1;
    public Sprite[] curSprite2;
    //Set Default Property
    void SetDefaultProperty()
    {
        curDir = 2;
        moveSpeed = 3f;
        shootInterval = 3f;
        shootTimer = 3f;
        changeDirTimer = 5f;
        changeDirInterval = 5f;
        redSpriteTimer = 0.2f;
        redSpriteInterval = 0.2f;
        stopTImer = 5f;
        stopInterval = 5f;
        isStop = false;
        curPicSet = 1;
    }
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        SwitchSprite();
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
        redSpriteTimer -= Time.fixedDeltaTime;
        if (redSpriteTimer < 0 && curPicSet == 1)
        {
            spriteRenderer.sprite = curSprite1[curDir];
            curPicSet++;
            redSpriteTimer = redSpriteInterval;
        }
        else if (redSpriteTimer < 0 && curPicSet == 2)
        {
            spriteRenderer.sprite = curSprite2[curDir];
            curPicSet--;
            redSpriteTimer = redSpriteInterval;
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
        if (val == 1)
            GameManager.gameManager.Player1Killed[1]++;
        else
            GameManager.gameManager.Player2Killed[1]++;
        MapCreator._instance.SendMessage("CreateTool");
        Instantiate(ObjectPool._instance.TankExplosion, transform.position, Quaternion.identity);
        UIController._instance.SendMessage("EnemyKilled");
        Destroy(gameObject);
    }
}
