using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    float moveSpeed = 100.0f;
    Rigidbody2D rb;

    PlayerBehavior target;

    Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        //targets the player and shoots a bullet in its direction
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<PlayerBehavior>();
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(gameObject, 20f);
    }

    //checks if the player is hit by the projectile
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
