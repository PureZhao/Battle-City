using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocean : MonoBehaviour
{
    public Sprite[] oceanSprite;
    private float spriteSwitchTimer = 0.4f;
    private float spriteSwitchInterval = 0.4f;
    private int curSprite = 0;
    private SpriteRenderer spriteRenderer;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        SpriteSwitch();
    }
    void SpriteSwitch()
    {
        spriteSwitchTimer -= Time.deltaTime;
        if (spriteSwitchTimer < 0 && curSprite == 0)
        {
            spriteRenderer.sprite = oceanSprite[curSprite];
            curSprite++;
            spriteSwitchTimer = spriteSwitchInterval;
        }
        else if (spriteSwitchTimer < 0 && curSprite == 1)
        {
            spriteRenderer.sprite = oceanSprite[curSprite];
            curSprite--;
            spriteSwitchTimer = spriteSwitchInterval;
        }
    }
}
