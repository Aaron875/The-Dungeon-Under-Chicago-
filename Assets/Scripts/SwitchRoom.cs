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

    void moveCamAndPlayer()
    {
        switch (direction)
        {
            case moveDirection.Up:
                mainCam.transform.position = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y + 297, mainCam.transform.position.z);
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 86, player.transform.position.z);
                break;

            case moveDirection.Right:
                mainCam.transform.position = new Vector3(mainCam.transform.position.x + 544, mainCam.transform.position.y, mainCam.transform.position.z);
                player.transform.position = new Vector3(player.transform.position.x + 44.5f, player.transform.position.y, player.transform.position.z);
                break;

            case moveDirection.Down:
                mainCam.transform.position = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y - 297, mainCam.transform.position.z);
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 86, player.transform.position.z);
                break;

            case moveDirection.Left:
                mainCam.transform.position = new Vector3(mainCam.transform.position.x - 544, mainCam.transform.position.y, mainCam.transform.position.z);
                player.transform.position = new Vector3(player.transform.position.x - 44.5f, player.transform.position.y, player.transform.position.z);
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
