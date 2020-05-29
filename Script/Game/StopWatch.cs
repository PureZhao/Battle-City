using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopWatch : MonoBehaviour
{
    private float timer = 0.2f;
    private bool isActive = true;
    private SpriteRenderer spriteRenderer;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(ObjectPool._instance.GetBonusAudio, transform.position);
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (other.GetComponent<PlayerControl>().PlayerID == 1)
                GameManager.gameManager.Player1Killed[4]++;
            else
                GameManager.gameManager.Player2Killed[4]++;
            foreach (GameObject g in enemies)
            {
                g.SendMessage("StopMove");
            }
            Destroy(gameObject);
        }
    }


    void LateUpdate()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            if (isActive)
            {
                spriteRenderer.color = new Color(1, 1, 1, 0);
                isActive = false;
            }
            else
            {
                spriteRenderer.color = new Color(1, 1, 1, 1);
                isActive = true;
            }
            timer = 0.2f;
        }
    }
}
