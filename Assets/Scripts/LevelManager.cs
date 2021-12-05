using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //Fields
    public GameObject rangedEnemyPrefab;
    public GameObject meleeEnemyPrefab;
    public GameObject player;

    private List<GameObject> room1Enemies = new List<GameObject>();
    private List<GameObject> room2Enemies = new List<GameObject>();
    //room3 is a pickup room with no enemies in it
    private List<GameObject> room4Enemies = new List<GameObject>();
    private List<GameObject> room5Enemies = new List<GameObject>();
    private List<GameObject> room6Enemies = new List<GameObject>();
    private List<GameObject> room7Enemies = new List<GameObject>();
    private List<GameObject> room8Enemies = new List<GameObject>();
    //Room9 is right before the boss room, so no enemies
    
    //Room 10 contains the bossMonsters
    private List<GameObject> room10Enemies = new List<GameObject>();

    //Properties
    public List<GameObject> Room1Enemies
    {
        get { return room1Enemies; }
        set { room1Enemies = value; }
    }
    public List<GameObject> Room2Enemies
    {
        get { return room2Enemies; }
        set { room2Enemies = value; }
    }
    public List<GameObject> Room4Enemies
    {
        get { return room4Enemies; }
        set { room4Enemies = value; }
    }
    public List<GameObject> Room5Enemies
    {
        get { return room5Enemies; }
        set { room5Enemies = value; }
    }
    public List<GameObject> Room6Enemies
    {
        get { return room6Enemies; }
        set { room6Enemies = value; }
    }
    public List<GameObject> Room7Enemies
    {
        get { return room7Enemies; }
        set { room7Enemies = value; }
    }
    public List<GameObject> Room8Enemies
    {
        get { return room8Enemies; }
        set { room8Enemies = value; }
    }

    public List<GameObject> Room10Enemies
    {
        get { return room10Enemies; }
        set { room10Enemies = value; }
    }

    private EnemyBehavior BehaviorScript;

    // Start is called before the first frame update
    void Start()
    {
        //Room 1
        Room1Enemies.Add(Instantiate(rangedEnemyPrefab, new Vector3(0.0f, 13.0f, -60.0f), Quaternion.identity));
        BehaviorScript = Room1Enemies[0].GetComponent<EnemyBehavior>();
        BehaviorScript.player = player;

        room1Enemies.Add(Instantiate(meleeEnemyPrefab, new Vector3(0.0f, -20.0f, -60.0f), Quaternion.identity));
        BehaviorScript = room1Enemies[1].GetComponent<EnemyBehavior>();
        BehaviorScript.player = player;


        //Room2
        Room2Enemies.Add(Instantiate(rangedEnemyPrefab, new Vector3(143.0f, 306.0f, -60.0f), Quaternion.identity));
        BehaviorScript = Room2Enemies[0].GetComponent<EnemyBehavior>();
        BehaviorScript.player = player;

        Room2Enemies.Add(Instantiate(rangedEnemyPrefab, new Vector3(0.0f, 306.0f, -60.0f), Quaternion.identity));
        BehaviorScript = Room2Enemies[1].GetComponent<EnemyBehavior>();
        BehaviorScript.player = player;

        Room2Enemies.Add(Instantiate(rangedEnemyPrefab, new Vector3(-143.0f, 306.0f, -60.0f), Quaternion.identity));
        BehaviorScript = Room2Enemies[2].GetComponent<EnemyBehavior>();
        BehaviorScript.player = player;
        BehaviorScript.currentDirection = EnemyDirection.Right;

        Room2Enemies.Add(Instantiate(meleeEnemyPrefab, new Vector3(-20.0f, 306.0f, -60.0f), Quaternion.identity));
        BehaviorScript = Room2Enemies[3].GetComponent<EnemyBehavior>();
        BehaviorScript.player = player;

        Room2Enemies.Add(Instantiate(meleeEnemyPrefab, new Vector3(20.0f, 306.0f, -60.0f), Quaternion.identity));
        BehaviorScript = Room2Enemies[4].GetComponent<EnemyBehavior>();
        BehaviorScript.player = player;

        //Room4
        Room4Enemies.Add(Instantiate(rangedEnemyPrefab, new Vector3(0.0f, 603.0f, -60.0f), Quaternion.identity));
        BehaviorScript = Room4Enemies[0].GetComponent<EnemyBehavior>();
        BehaviorScript.player = player;

        Room4Enemies.Add(Instantiate(rangedEnemyPrefab, new Vector3(166.0f, 682.0f, -60.0f), Quaternion.identity));
        BehaviorScript = Room4Enemies[1].GetComponent<EnemyBehavior>();
        BehaviorScript.player = player;

        Room4Enemies.Add(Instantiate(rangedEnemyPrefab, new Vector3(225.0f, 527.0f, -60.0f), Quaternion.identity));
        BehaviorScript = Room4Enemies[2].GetComponent<EnemyBehavior>();
        BehaviorScript.player = player;

        Room4Enemies.Add(Instantiate(rangedEnemyPrefab, new Vector3(-238.0f, 602.0f, -60.0f), Quaternion.identity));
        BehaviorScript = Room4Enemies[3].GetComponent<EnemyBehavior>();
        BehaviorScript.player = player;

        Room4Enemies.Add(Instantiate(meleeEnemyPrefab, new Vector3(0.0f, 550.0f, -60.0f), Quaternion.identity));
        BehaviorScript = Room4Enemies[4].GetComponent<EnemyBehavior>();
        BehaviorScript.player = player;

        //Room5
        Room5Enemies.Add(Instantiate(rangedEnemyPrefab, new Vector3(445.0f, 700.0f, -60.0f), Quaternion.identity));
        BehaviorScript = Room5Enemies[0].GetComponent<EnemyBehavior>();
        BehaviorScript.player = player;

        Room5Enemies.Add(Instantiate(rangedEnemyPrefab, new Vector3(662.0f, 700.0f, -60.0f), Quaternion.identity));
        BehaviorScript = Room5Enemies[1].GetComponent<EnemyBehavior>();
        BehaviorScript.player = player;

        Room5Enemies.Add(Instantiate(rangedEnemyPrefab, new Vector3(476.0f, 510.0f, -60.0f), Quaternion.identity));
        BehaviorScript = Room5Enemies[2].GetComponent<EnemyBehavior>();
        BehaviorScript.player = player;

        Room5Enemies.Add(Instantiate(rangedEnemyPrefab, new Vector3(613.0f, 510.0f, -60.0f), Quaternion.identity));
        BehaviorScript = Room5Enemies[3].GetComponent<EnemyBehavior>();
        BehaviorScript.player = player;

        Room5Enemies.Add(Instantiate(meleeEnemyPrefab, new Vector3(550.0f, 600.0f, -60.0f), Quaternion.identity));
        BehaviorScript = Room5Enemies[4].GetComponent<EnemyBehavior>();
        BehaviorScript.player = player;

        Room5Enemies.Add(Instantiate(meleeEnemyPrefab, new Vector3(650.0f, 600.0f, -60.0f), Quaternion.identity));
        BehaviorScript = Room5Enemies[5].GetComponent<EnemyBehavior>();
        BehaviorScript.player = player;

        //Room6
        Room6Enemies.Add(Instantiate(rangedEnemyPrefab, new Vector3(465.0f, -7.0f, -60.0f), Quaternion.identity));
        BehaviorScript = Room6Enemies[0].GetComponent<EnemyBehavior>();
        BehaviorScript.player = player;

        Room6Enemies.Add(Instantiate(rangedEnemyPrefab, new Vector3(731.0f, 22.0f, -60.0f), Quaternion.identity));
        BehaviorScript = Room6Enemies[1].GetComponent<EnemyBehavior>();
        BehaviorScript.player = player;

        Room6Enemies.Add(Instantiate(rangedEnemyPrefab, new Vector3(500.0f, 109.0f, -60.0f), Quaternion.identity));
        BehaviorScript = Room6Enemies[2].GetComponent<EnemyBehavior>();
        BehaviorScript.player = player;

        Room6Enemies.Add(Instantiate(rangedEnemyPrefab, new Vector3(343.0f, -83.0f, -60.0f), Quaternion.identity));
        BehaviorScript = Room6Enemies[3].GetComponent<EnemyBehavior>();
        BehaviorScript.player = player;

        Room6Enemies.Add(Instantiate(meleeEnemyPrefab, new Vector3(390.0f, 0.0f, -60.0f), Quaternion.identity));
        BehaviorScript = Room6Enemies[4].GetComponent<EnemyBehavior>();
        BehaviorScript.player = player;

        Room6Enemies.Add(Instantiate(meleeEnemyPrefab, new Vector3(500.0f, 0.0f, -60.0f), Quaternion.identity));
        BehaviorScript = Room6Enemies[5].GetComponent<EnemyBehavior>();
        BehaviorScript.player = player;

        Room6Enemies.Add(Instantiate(meleeEnemyPrefab, new Vector3(600.0f, 0.0f, -60.0f), Quaternion.identity));
        BehaviorScript = Room6Enemies[6].GetComponent<EnemyBehavior>();
        BehaviorScript.player = player;

        //Room7
        Room7Enemies.Add(Instantiate(rangedEnemyPrefab, new Vector3(1090.0f, 12.0f, -60.0f), Quaternion.identity));
        BehaviorScript = Room7Enemies[0].GetComponent<EnemyBehavior>();
        BehaviorScript.player = player;

        Room7Enemies.Add(Instantiate(meleeEnemyPrefab, new Vector3(1090.0f, 30.0f, -60.0f), Quaternion.identity));
        BehaviorScript = Room7Enemies[1].GetComponent<EnemyBehavior>();
        BehaviorScript.player = player;

        Room7Enemies.Add(Instantiate(meleeEnemyPrefab, new Vector3(1090.0f, -30.0f, -60.0f), Quaternion.identity));
        BehaviorScript = Room7Enemies[2].GetComponent<EnemyBehavior>();
        BehaviorScript.player = player;

        //Room8
        Room8Enemies.Add(Instantiate(rangedEnemyPrefab, new Vector3(865.0f, 302.0f, -60.0f), Quaternion.identity));
        BehaviorScript = Room8Enemies[0].GetComponent<EnemyBehavior>();
        BehaviorScript.player = player;

        Room8Enemies.Add(Instantiate(rangedEnemyPrefab, new Vector3(1052.0f, 302.0f, -60.0f), Quaternion.identity));
        BehaviorScript = Room8Enemies[1].GetComponent<EnemyBehavior>();
        BehaviorScript.player = player;

        Room8Enemies.Add(Instantiate(rangedEnemyPrefab, new Vector3(1122.0f, 302.0f, -60.0f), Quaternion.identity));
        BehaviorScript = Room8Enemies[2].GetComponent<EnemyBehavior>();
        BehaviorScript.player = player;

        Room8Enemies.Add(Instantiate(rangedEnemyPrefab, new Vector3(1316.0f, 302.0f, -60.0f), Quaternion.identity));
        BehaviorScript = Room8Enemies[3].GetComponent<EnemyBehavior>();
        BehaviorScript.player = player;

        //Finish Setting up enemies
        initializeEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Set all enemies that are not in the first room to be inactive
    void initializeEnemies()
    {
        //Room 2
        for(int i = 0; i < Room2Enemies.Count; i++)
        {
            if(room2Enemies[i] != null)
            {
                Room2Enemies[i].SetActive(false);
            }
        }

        //Room 4
        for (int i = 0; i < Room4Enemies.Count; i++)
        {
            if (room4Enemies[i] != null)
            {
                Room4Enemies[i].SetActive(false);
            }
        }

        //Room 5
        for (int i = 0; i < Room5Enemies.Count; i++)
        {
            if (room5Enemies[i] != null)
            {
                Room5Enemies[i].SetActive(false);
            }
        }

        //Room 6
        for (int i = 0; i < Room6Enemies.Count; i++)
        {
            if (room6Enemies[i] != null)
            {
                Room6Enemies[i].SetActive(false);
            }
        }

        //Room 7
        for (int i = 0; i < Room7Enemies.Count; i++)
        {
            if (room7Enemies[i] != null)
            {
                Room7Enemies[i].SetActive(false);
            }
        }

        //Room 8
        for (int i = 0; i < Room8Enemies.Count; i++)
        {
            if (room8Enemies[i] != null)
            {
                Room8Enemies[i].SetActive(false);
            }
        }
    }
}
