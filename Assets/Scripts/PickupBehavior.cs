using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehavior : MonoBehaviour
{
    PlayerBehavior playerScript;

    //OBJECT TO DELETE ONLY NEEDS TO BE SET FOR THE 2 MAJOR PICKUPS
    public GameObject objectToDelete;

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

        if (other.tag == "Fireball" && gameObject.tag == "Major Fire Pickup")
        {
            playerScript.attack += 25.0f;
            Destroy(gameObject);
            Destroy(other.gameObject);
        }

        if (other.tag == "Fireball" && gameObject.tag == "Major Shield Pickup")
        {
            playerScript.shieldRestore += 10.0f;
            Destroy(gameObject);
            Destroy(other.gameObject);
        }

        if (other.tag == "Player" || other.tag == "Fireball")
        {
            Destroy(gameObject);
            Destroy(objectToDelete);
        }
    }

}
