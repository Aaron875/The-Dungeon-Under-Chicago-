using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        // Destory the enemy if its health reaches 0 or less
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Reduce enemies health if they get hit by a projectile
        if(other.tag == "BasicProjectile")
        {
            health -= 50;
            print("Projectile hit");
            Destroy(other.gameObject);
        }

        // Reduce enemies health if they get hit by a fireball
        if(other.tag == "Fireball")
        {
            health -= 100;
            print("Fireball hit");
            Destroy(other.gameObject);
        }
    }
}
