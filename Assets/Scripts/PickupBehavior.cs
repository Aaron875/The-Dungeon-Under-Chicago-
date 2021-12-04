using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehavior : MonoBehaviour
{

    //THIS SCRIPT'S SOLE PURPOSE IS TO ALLOW PLAYERS TO GET UNREACHABLE PICKUPS BY SHOOTING THEM
    PlayerBehavior playerScript;

    private void Start()
    {
        playerScript = GameObject.FindObjectOfType<PlayerBehavior>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Fireball" && gameObject.tag == "Minor Fire Pickup")
        {
            playerScript.attack += 0.5f;
            Destroy(gameObject);
            Destroy(other.gameObject);
        }

        if (other.tag == "Fireball" && gameObject.tag == "Minor Shield Pickup")
        {
            playerScript.shieldRestore += 0.5f;
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }

}
