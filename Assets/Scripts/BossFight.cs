using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossFight : MonoBehaviour
{
    public GameObject player;

    public GameObject phase1EnemyPrefab;
    public GameObject phase2EnemyPrefab;
    public GameObject phase3EnemyPrefab;
    public GameObject phase4EnemyPrefab;

    public Camera cam;

    List<List<GameObject>> Phases = new List<List<GameObject>>();

    [SerializeField]
    private List<GameObject> Phase1Enemies = new List<GameObject>(); //4 enemies

    [SerializeField]
    private List<GameObject> Phase2Enemies = new List<GameObject>(); //4 enemies

    [SerializeField]
    private List<GameObject> Phase3Enemies = new List<GameObject>(); //4 enemies

    [SerializeField]
    private List<GameObject> Phase4Enemies = new List<GameObject>(); //1 enemy

    SpriteRenderer enemySprite;
    EnemyBehavior enemyScript;

    [SerializeField]
    int ActivePhase;

    void Start()
    {
        ActivePhase = 0;


        //Phase 1
        Phases.Add(Phase1Enemies);
        Phases[0].Add(Instantiate(phase1EnemyPrefab, new Vector3(2380, 665, -60), Quaternion.identity));
        enemyScript = Phases[0][0].GetComponent<EnemyBehavior>();
        enemyScript.player = player;
        Phases[0][0].SetActive(false);

        Phases[0].Add(Instantiate(phase1EnemyPrefab, new Vector3(2380, 552, -60), Quaternion.identity));
        enemyScript = Phases[0][1].GetComponent<EnemyBehavior>();
        enemyScript.player = player;
        Phases[0][1].SetActive(false);

        Phases[0].Add(Instantiate(phase1EnemyPrefab, new Vector3(1962, 552, -60), Quaternion.identity));
        enemySprite = Phases[0][2].GetComponent<SpriteRenderer>();
        enemyScript = Phases[0][2].GetComponent<EnemyBehavior>();
        enemySprite.flipX = true;
        enemyScript.player = player;
        Phases[0][2].SetActive(false);

        Phases[0].Add(Instantiate(phase1EnemyPrefab, new Vector3(1962, 665, -60), Quaternion.identity));
        enemySprite = Phases[0][3].GetComponent<SpriteRenderer>();
        enemyScript = Phases[0][3].GetComponent<EnemyBehavior>();
        enemySprite.flipX = true;
        enemyScript.player = player;
        Phases[0][3].SetActive(false);

        //Phase 2
        Phases.Add(Phase2Enemies);
        Phases[1].Add(Instantiate(phase2EnemyPrefab, new Vector3(2369, 691, -60), Quaternion.identity));
        enemyScript = Phases[1][0].GetComponent<EnemyBehavior>();
        enemyScript.player = player;
        Phases[1][0].SetActive(false);

        Phases[1].Add(Instantiate(phase2EnemyPrefab, new Vector3(2369, 566, -60), Quaternion.identity));
        enemyScript = Phases[1][1].GetComponent<EnemyBehavior>();
        enemyScript.player = player;
        Phases[1][1].SetActive(false);

        Phases[1].Add(Instantiate(phase2EnemyPrefab, new Vector3(1968, 566, -60), Quaternion.identity));
        enemySprite = Phases[1][2].GetComponent<SpriteRenderer>();
        enemyScript = Phases[1][2].GetComponent<EnemyBehavior>();
        enemySprite.flipX = true;
        enemyScript.player = player;
        Phases[1][2].SetActive(false);

        Phases[1].Add(Instantiate(phase2EnemyPrefab, new Vector3(1968, 691, -60), Quaternion.identity));
        enemySprite = Phases[1][3].GetComponent<SpriteRenderer>();
        enemyScript = Phases[1][3].GetComponent<EnemyBehavior>();
        enemySprite.flipX = true;
        enemyScript.player = player;
        Phases[1][3].SetActive(false);

        //Phase 3
        Phases.Add(Phase3Enemies);
        Phases[2].Add(Instantiate(phase3EnemyPrefab, new Vector3(2365, 713, -60), Quaternion.identity));
        enemyScript = Phases[2][0].GetComponent<EnemyBehavior>();
        enemyScript.player = player;
        Phases[2][0].SetActive(false);

        Phases[2].Add(Instantiate(phase3EnemyPrefab, new Vector3(2365, 592, -65), Quaternion.identity));
        enemyScript = Phases[2][1].GetComponent<EnemyBehavior>();
        enemyScript.player = player;
        Phases[2][1].SetActive(false);

        Phases[2].Add(Instantiate(phase3EnemyPrefab, new Vector3(1973, 592, -65), Quaternion.identity));
        enemySprite = Phases[2][2].GetComponent<SpriteRenderer>();
        enemyScript = Phases[2][2].GetComponent<EnemyBehavior>();
        enemySprite.flipX = true;
        enemyScript.player = player;
        Phases[2][2].SetActive(false);

        Phases[2].Add(Instantiate(phase3EnemyPrefab, new Vector3(1973, 713, -60), Quaternion.identity));
        enemySprite = Phases[2][3].GetComponent<SpriteRenderer>();
        enemyScript = Phases[2][3].GetComponent<EnemyBehavior>();
        enemySprite.flipX = true;
        enemyScript.player = player;
        Phases[2][3].SetActive(false);

        //Phase 4
        Phases.Add(Phase4Enemies);
        Phases[3].Add(Instantiate(phase4EnemyPrefab, new Vector3(2183, 689, -60), Quaternion.identity));
        enemyScript = Phases[3][0].GetComponent<EnemyBehavior>();
        enemyScript.player = player;
        Phases[3][0].SetActive(false);
    }

    void Update()
    {
        if(player.transform.position.x > 1900)
        {
            ActivateNextPhase(ActivePhase);
        }

        switch (ActivePhase)
        {
            case 0:
                if (!EnemiesRemaining(Phase1Enemies))
                {
                    ActivePhase = 1;
                    ActivateNextPhase(ActivePhase);
                }
                break;

            case 1:
                if (!EnemiesRemaining(Phase2Enemies))
                {
                    ActivePhase = 2;
                    ActivateNextPhase(ActivePhase);
                }
                break;

            case 2:
                if (!EnemiesRemaining(Phase3Enemies))
                {
                    ActivePhase = 3;
                    ActivateNextPhase(ActivePhase);
                    ResizeCamera();
                }
                break;

            case 3:
                if (!EnemiesRemaining(Phase4Enemies))
                {
                    ActivePhase = 4;
                    ActivateNextPhase(ActivePhase);
                    ResizeCamera();
                }
                break;

            case 5:
                //Advance to Win Screen
                SceneManager.LoadScene("GameWin");
                break;

            default:
                return;
        }
    }

    bool EnemiesRemaining(List<GameObject> enemies)
    {
        bool enemiesRemaining = false;
        for(int i = 0; i < enemies.Count; i++)
        {
            if(enemies[i] != null)
            {
                enemiesRemaining = true;
            }
        }
        return enemiesRemaining;
    }

    void ActivateNextPhase(int activePhase)
    {
        for(int i = 0; i < Phases[activePhase].Count; i++)
        {
            if(Phases[activePhase][i] != null)
            {
                Phases[activePhase][i].SetActive(true);
            }
        }
    }

    void ResizeCamera()
    {
        cam.orthographicSize *= 1.5f;
    }
}
