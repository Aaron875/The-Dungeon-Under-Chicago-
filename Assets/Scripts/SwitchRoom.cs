using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum moveDirection
{
    Up,
    Right,
    Down,
    Left
}

public class SwitchRoom : MonoBehaviour
{
    public moveDirection direction;
    public Camera mainCam;

    public GameObject player;
    public GameObject lvlManager;

    public int currentLevel;
    public int targetLevel;

    private LevelManager managerScript;

    private void Start()
    {
        managerScript = lvlManager.GetComponent<LevelManager>();
    }

    void moveCamAndPlayer()
    {
        switch (direction)
        {
            case moveDirection.Up:
                mainCam.transform.position = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y + 297, mainCam.transform.position.z);
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 86, player.transform.position.z);
                adjustEnemies();
                break;

            case moveDirection.Right:
                mainCam.transform.position = new Vector3(mainCam.transform.position.x + 544, mainCam.transform.position.y, mainCam.transform.position.z);
                player.transform.position = new Vector3(player.transform.position.x + 44.5f, player.transform.position.y, player.transform.position.z);
                adjustEnemies();
                break;

            case moveDirection.Down:
                mainCam.transform.position = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y - 297, mainCam.transform.position.z);
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 86, player.transform.position.z);
                adjustEnemies();
                break;

            case moveDirection.Left:
                mainCam.transform.position = new Vector3(mainCam.transform.position.x - 544, mainCam.transform.position.y, mainCam.transform.position.z);
                player.transform.position = new Vector3(player.transform.position.x - 44.5f, player.transform.position.y, player.transform.position.z);
                adjustEnemies();
                break;
        }
    }

    void adjustEnemies()
    {
        switch (currentLevel)
        {
            case 1:
                for (int i = 0; i < managerScript.Room1Enemies.Count; i++)
                {
                    if(managerScript.Room1Enemies[i] != null)
                    {
                        managerScript.Room1Enemies[i].SetActive(false);
                    }
                }
                break;

            case 2:
                for (int i = 0; i < managerScript.Room2Enemies.Count; i++)
                {
                    if (managerScript.Room2Enemies[i] != null)
                    {
                        managerScript.Room2Enemies[i].SetActive(false);
                    }
                }
                break;

            case 4:
                for (int i = 0; i < managerScript.Room4Enemies.Count; i++)
                {
                    if (managerScript.Room4Enemies[i] != null)
                    {
                        managerScript.Room4Enemies[i].SetActive(false);
                    }
                }
                break;

            case 5:
                for (int i = 0; i < managerScript.Room5Enemies.Count; i++)
                {
                    if (managerScript.Room5Enemies[i] != null)
                    {
                        managerScript.Room5Enemies[i].SetActive(false);
                    }
                }
                break;

            case 6:
                for (int i = 0; i < managerScript.Room6Enemies.Count; i++)
                {
                    if (managerScript.Room6Enemies[i] != null)
                    {
                        managerScript.Room6Enemies[i].SetActive(false);
                    }
                }
                break;

            case 7:
                for (int i = 0; i < managerScript.Room7Enemies.Count; i++)
                {
                    if (managerScript.Room7Enemies[i] != null)
                    {
                        managerScript.Room7Enemies[i].SetActive(false);
                    }
                }
                break;

            case 8:
                for (int i = 0; i < managerScript.Room8Enemies.Count; i++)
                {
                    if (managerScript.Room8Enemies[i] != null)
                    {
                        managerScript.Room8Enemies[i].SetActive(false);
                    }
                }
                break;
        }

        switch (targetLevel)
        {
            case 1:
                for (int i = 0; i < managerScript.Room1Enemies.Count; i++)
                {
                    if (managerScript.Room1Enemies[i] != null)
                    {
                        managerScript.Room1Enemies[i].SetActive(true);
                    }
                }
                break;

            case 2:
                for (int i = 0; i < managerScript.Room2Enemies.Count; i++)
                {
                    if (managerScript.Room2Enemies[i] != null)
                    {
                        managerScript.Room2Enemies[i].SetActive(true);
                    }
                }
                break;

            case 4:
                for (int i = 0; i < managerScript.Room4Enemies.Count; i++)
                {
                    if (managerScript.Room4Enemies[i] != null)
                    {
                        managerScript.Room4Enemies[i].SetActive(true);
                    }
                }
                break;

            case 5:
                for (int i = 0; i < managerScript.Room5Enemies.Count; i++)
                {
                    if (managerScript.Room5Enemies[i] != null)
                    {
                        managerScript.Room5Enemies[i].SetActive(true);
                    }
                }
                break;

            case 6:
                for (int i = 0; i < managerScript.Room6Enemies.Count; i++)
                {
                    if (managerScript.Room6Enemies[i] != null)
                    {
                        managerScript.Room6Enemies[i].SetActive(true);
                    }
                }
                break;

            case 7:
                for (int i = 0; i < managerScript.Room7Enemies.Count; i++)
                {
                    if (managerScript.Room7Enemies[i] != null)
                    {
                        managerScript.Room7Enemies[i].SetActive(true);
                    }
                }
                break;

            case 8:
                for (int i = 0; i < managerScript.Room8Enemies.Count; i++)
                {
                    if (managerScript.Room8Enemies[i] != null)
                    {
                        managerScript.Room8Enemies[i].SetActive(true);
                    }
                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            moveCamAndPlayer();
        }
    }
}
