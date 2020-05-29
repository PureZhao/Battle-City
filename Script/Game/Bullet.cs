using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int bulletLevel = 0;
    private int playerID;
    private bool isPlayerBullet = false;
    private float moveSpeed = 5f;
    private Vector3 dir;
    void Awake()
    {
        if (name.IndexOf("Up") >= 0) {
            dir = new Vector3(0f, 1f, 0f);
        }
        if (name.IndexOf("Down") >= 0)
        {
            dir = new Vector3(0f, -1f, 0f);
        }
        if (name.IndexOf("Right") >= 0)
        {
            dir = new Vector3(1f, 0f, 0f);
        }
        if (name.IndexOf("Left") >= 0)
        {
            dir = new Vector3(-1f, 0f, 0f);
        }


    }

    void Update()
    {
        if (bulletLevel >= 1)
            moveSpeed = 7.5f;
        transform.Translate(dir * moveSpeed * Time.deltaTime,Space.Self);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Brick":
                Destroy(other.gameObject);
                Instantiate(ObjectPool._instance.BulletExplosion, transform.position, Quaternion.identity);
                Destroy(gameObject);
                break;
            case "Iron":
                Instantiate(ObjectPool._instance.BulletExplosion, transform.position, Quaternion.identity);
                if (bulletLevel == 3)
                {
                    Destroy(other.gameObject);
                    Destroy(gameObject);
                }

                else { 
                    AudioSource.PlayClipAtPoint(ObjectPool._instance.HitWallAudio, transform.position);
                    Destroy(gameObject);
                }
                break;
            case "Enemy":
                if (isPlayerBullet == true)
                {
                    Instantiate(ObjectPool._instance.BulletExplosion, transform.position, Quaternion.identity);
                    other.SendMessage("Dead",playerID);
                    Destroy(gameObject);
                }
                break;
            case "Player":
                if (isPlayerBullet == false)
                {
                    Instantiate(ObjectPool._instance.BulletExplosion, transform.position, Quaternion.identity);
                    other.SendMessage("Dead");
                    Destroy(gameObject);
                }
                else {
                    if (other.GetComponent<PlayerControl>().PlayerID == playerID)
                        return;
                    other.GetComponent<PlayerControl>().StartCoroutine("FriendlyFire");
                    Instantiate(ObjectPool._instance.BulletExplosion, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
                break;
            case "AirWall":
                AudioSource.PlayClipAtPoint(ObjectPool._instance.HitWallAudio, transform.position);
                Instantiate(ObjectPool._instance.BulletExplosion, transform.position, Quaternion.identity);
                Destroy(gameObject);
                break;
            case "Heart":
                Instantiate(ObjectPool._instance.BulletExplosion, transform.position, Quaternion.identity);
                AudioSource.PlayClipAtPoint(ObjectPool._instance.HeartExplodeAudio, Vector3.zero);
                Instantiate(ObjectPool._instance.BrokenHeartPrefab, other.transform.position, Quaternion.identity);
                Instantiate(ObjectPool._instance.TankExplosion, other.transform.position, Quaternion.identity);
                Destroy(other.gameObject);
                UIController._instance.StartCoroutine("GameOver");
                Destroy(gameObject);
                break;
            case"Bullet":
                Destroy(other.gameObject);
                Destroy(gameObject);
                break;
        }
    }

    void SetInfo(int[] val) {
        bulletLevel = val[0];
        isPlayerBullet = true;
        playerID = val[1];
    }
}
