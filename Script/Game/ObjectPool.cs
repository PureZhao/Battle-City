using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool _instance;
    //Player
    private GameObject player1Prefab;
    private GameObject player2Prefab;
    //Enemy
    private GameObject[] enemyPrefab = new GameObject[8];//N NT Q QT H HT PY PG
    //Bullet
    private GameObject[] bullets = new GameObject[4];// 0 up 1 right 2 down 3 left
    //Effects
    private GameObject tankExplosion;
    private GameObject bulletExplosion;
    private GameObject bornEffect;
    //Bonus
    private AudioClip bonusAppear;
    private AudioClip getBonusAudio;
    private AudioClip lifeUpAudio;
    private AudioClip grenadeAudio;
    //GameProcedure
    private AudioClip gameStartAudio;
    private AudioClip gameOverAudio;
    private AudioClip playerTankDieAudio;
    private AudioClip heartExplodeAudio;
    private AudioClip hitWallAudio;
    private GameObject brokenHeartPrefab;
    //Hit
    private AudioClip shootAudio;
    private AudioClip ironHitAudio;
    //Spawn Point
    private Vector3 player1SpanwPoint = new Vector3(-1.5f, -3.5f, 0f);
    private Vector3 player2SpanwPoint = new Vector3(0.5f, -3.5f, 0f);
    private Vector3[] enemySpawnPoint = new Vector3[3] { new Vector3(-3.5f, 2.5f, 0f), new Vector3(-0.5f, 2.5f, 0f), new Vector3(2.5f, 2.5f, 0f) };
    //Tool
    private GameObject[] tools = new GameObject[6];
    //Score
    private int[] eachElement = new int[5] { 100, 200, 300, 400, 500 };

    void Awake() {
        _instance = this;

        player1Prefab = Resources.Load("Prefab/Player/Player1") as GameObject;
        player2Prefab = Resources.Load("Prefab/Player/Player2") as GameObject;

        enemyPrefab[0] = Resources.Load("Prefab/Enemy/Normal") as GameObject;
        enemyPrefab[1] = Resources.Load("Prefab/Enemy/NormalTool") as GameObject;
        enemyPrefab[2] = Resources.Load("Prefab/Enemy/Quicker") as GameObject;
        enemyPrefab[3] = Resources.Load("Prefab/Enemy/QuickerTool") as GameObject;
        enemyPrefab[4] = Resources.Load("Prefab/Enemy/Heavyer") as GameObject;
        enemyPrefab[5] = Resources.Load("Prefab/Enemy/HeavyerTool") as GameObject;
        enemyPrefab[6] = Resources.Load("Prefab/Enemy/PretenderYellow") as GameObject;
        enemyPrefab[7] = Resources.Load("Prefab/Enemy/PretenderGreen") as GameObject;

        bullets[0] = Resources.Load("Prefab/Bullet/Up") as GameObject;
        bullets[1] = Resources.Load("Prefab/Bullet/Right") as GameObject;
        bullets[2] = Resources.Load("Prefab/Bullet/Down") as GameObject;
        bullets[3] = Resources.Load("Prefab/Bullet/Left") as GameObject;

        tankExplosion = Resources.Load("Prefab/Effect/TankExplosion") as GameObject;
        bulletExplosion = Resources.Load("Prefab/Effect/BulletExplosion") as GameObject;
        bornEffect = Resources.Load("Prefab/Effect/Born") as GameObject;

        bonusAppear = Resources.Load("Audio/BonusAppear") as AudioClip;
        getBonusAudio = Resources.Load("Audio/GetBonus") as AudioClip;
        lifeUpAudio = Resources.Load("Audio/LifeUp") as AudioClip;
        grenadeAudio = Resources.Load("Audio/Grenade") as AudioClip;
        hitWallAudio = Resources.Load("Audio/HitWall") as AudioClip;
        brokenHeartPrefab = Resources.Load("Prefab/MapElement/BrokenHeart") as GameObject;

        gameStartAudio = Resources.Load("Audio/GameStart") as AudioClip;
        gameOverAudio = Resources.Load("Audio/GameOver") as AudioClip;
        playerTankDieAudio = Resources.Load("Audio/Die") as AudioClip;
        heartExplodeAudio = Resources.Load("Audio/HeartExplode") as AudioClip;

        ironHitAudio = Resources.Load("Audio/IronHit") as AudioClip;
        shootAudio = Resources.Load("Audio/Fire") as AudioClip;

        tools[0] = Resources.Load("Prefab/Bonus/AddLife") as GameObject;
        tools[1] = Resources.Load("Prefab/Bonus/DestroyEnemy") as GameObject;
        tools[2] = Resources.Load("Prefab/Bonus/ProtectHeart") as GameObject;
        tools[3] = Resources.Load("Prefab/Bonus/Shield") as GameObject;
        tools[4] = Resources.Load("Prefab/Bonus/StopEnemy") as GameObject;
        tools[5] = Resources.Load("Prefab/Bonus/Upgrade") as GameObject;
    }
    public GameObject Player1Prefab
    {
        get { return player1Prefab; }
    }
    public GameObject Player2Prefab
    {
        get { return player2Prefab; }
    }
    public GameObject[] EnemyPrefab
    {
        get { return enemyPrefab; }
    }
    public GameObject[] Bullets
    {
        get { return bullets; }
    }
    public GameObject TankExplosion
    {
        get { return tankExplosion; }
    }
    public GameObject BulletExplosion
    {
        get { return bulletExplosion; }
    }
    public GameObject BornEffect
    {
        get { return bornEffect; }
    }
    public AudioClip BonusAppear
    {
        get { return bonusAppear; }
    }
    public AudioClip GetBonusAudio
    {
        get { return getBonusAudio; }
    }
    public AudioClip LifeUpAudio
    {
        get { return lifeUpAudio; }
    }
    public AudioClip GrenadeAudio
    {
        get { return grenadeAudio; }
    }
    public AudioClip HitWallAudio
    {
        get { return hitWallAudio; }
    }
    public GameObject BrokenHeartPrefab
    {
        get { return brokenHeartPrefab; }
    }
    public AudioClip GameStartAudio
    {
        get { return gameStartAudio; }
    }
    public AudioClip GameOverAudio
    {
        get { return gameOverAudio; }
    }
    public AudioClip PlayerTankDieAudio
    {
        get { return playerTankDieAudio; }
    }
    public AudioClip HeartExplodeAudio
    {
        get { return heartExplodeAudio; }
    }
    public AudioClip ShootAudio
    {
        get { return shootAudio; }
    }
    public AudioClip IronHitAudio
    {
        get { return ironHitAudio; }
    }
    public Vector3 Player1SpanwPoint
    {
        get { return player1SpanwPoint; }
    }
    public Vector3 Player2SpanwPoint
    {
        get { return player2SpanwPoint; }
    }
    public Vector3[] EnemySpawnPoint
    {
        get { return enemySpawnPoint; }
    }
    public GameObject[] Tools
    {
        get { return tools; }
    }
    public int[] EachElement
    {
        get { return eachElement; }
    }
}
