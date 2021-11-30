using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyDirection
{
    Up,
    Right,
    Down,
    Left
}

public class EnemyBehavior : MonoBehaviour
{
    public Animator enemyAnimator;

    [SerializeField]
    private EnemyDirection currentDirection;

    public GameObject projectilePrefab;


    private Vector3 velocity2 = Vector3.zero;

    private SpriteRenderer enemySprite;
    private Collider2D enemyCollider;
    private Collider2D playerCollider;

    private bool collidingTop = false;
    private bool collidingLeft = false;
    private bool collidingBottom = false;
    private bool collidingRight = false;

    private GameObject projectile;
    private Vector3 projectileVelocity;

    private int health;

    [SerializeField]
    private string rangeOrMelee;

    // used to reduce player health
    [SerializeField]
    private GameObject player;

    //times the enemies shots
    float targetTime = 4.0f;


    // Start is called before the first frame update
    void Start()
    {
        enemySprite = gameObject.GetComponent<SpriteRenderer>();
        enemyCollider = gameObject.GetComponent<Collider2D>();
        playerCollider = player.GetComponent<Collider2D>();
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        // run this code if it is a ranged enemy
        if (rangeOrMelee == "Ranged")
        {
            RangedEnemy();
            targetTime -= Time.deltaTime;

            if (targetTime <= 0.0f)
            {
                Shoot();
                targetTime = 4.0f;
                //print("shoot");
            }
        }

        // Destory the enemy if its health reaches 0 or less
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Reduce enemies health if they get hit by a projectile
        //if(other.tag == "BasicProjectile")
        //{
        //    health -= 50;
        //    print("Projectile hit");
        //    Destroy(other.gameObject);
        //}

        if (other.tag == "Player")
        {
            health -= 50;
            enemySprite.color = Color.red;
            // need to reduce player health as well when we have a health system
        }


        // Reduce enemies health if they get hit by a fireball
        if (other.tag == "Fireball")
        {
            health -= 25;
            //print("Fireball hit");
            Destroy(other.gameObject);
            enemySprite.color = Color.red;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enemySprite.color = Color.white;
    }

    //The enemy changes direction based on where the player is in relation to it
    private void ChangeDirection()
    {
        switch (currentDirection)
        {

            case EnemyDirection.Left:
                enemyAnimator.SetBool("FacingUp", false);
                enemyAnimator.SetBool("FacingDown", false);
                enemyAnimator.SetBool("FacingRight", false);
                enemyAnimator.SetBool("FacingLeft", true);

                enemySprite.flipX = true;

                if (playerCollider.bounds.center.y + playerCollider.bounds.extents.y < enemyCollider.bounds.center.y - enemyCollider.bounds.extents.y &&
                    playerCollider.bounds.center.x - playerCollider.bounds.extents.x < enemyCollider.bounds.center.x + enemyCollider.bounds.extents.x + 30 &&
                    playerCollider.bounds.center.x + playerCollider.bounds.extents.x > enemyCollider.bounds.center.x - enemyCollider.bounds.extents.x - 30)
                {
                    currentDirection = EnemyDirection.Down;
                    //print("Looking down");
                }
                else if (playerCollider.bounds.center.y - playerCollider.bounds.extents.y > enemyCollider.bounds.center.y + enemyCollider.bounds.extents.y &&
                    playerCollider.bounds.center.x - playerCollider.bounds.extents.x < enemyCollider.bounds.center.x + enemyCollider.bounds.extents.x + 30 &&
                    playerCollider.bounds.center.x + playerCollider.bounds.extents.x > enemyCollider.bounds.center.x - enemyCollider.bounds.extents.x - 30)
                {
                    currentDirection = EnemyDirection.Up;
                    //print("Looking up");
                }
                break;

            case EnemyDirection.Right:
                enemyAnimator.SetBool("FacingUp", false);
                enemyAnimator.SetBool("FacingDown", false);
                enemyAnimator.SetBool("FacingRight", true);
                enemyAnimator.SetBool("FacingLeft", false);

                enemySprite.flipX = false;

                if (playerCollider.bounds.center.y + playerCollider.bounds.extents.y < enemyCollider.bounds.center.y - enemyCollider.bounds.extents.y &&
                    playerCollider.bounds.center.x - playerCollider.bounds.extents.x < enemyCollider.bounds.center.x + enemyCollider.bounds.extents.x + 30 &&
                    playerCollider.bounds.center.x + playerCollider.bounds.extents.x > enemyCollider.bounds.center.x - enemyCollider.bounds.extents.x - 30)
                {
                    currentDirection = EnemyDirection.Down;
                    //print("Looking down");
                }
                else if (playerCollider.bounds.center.y - playerCollider.bounds.extents.y > enemyCollider.bounds.center.y + enemyCollider.bounds.extents.y &&
                    playerCollider.bounds.center.x - playerCollider.bounds.extents.x < enemyCollider.bounds.center.x + enemyCollider.bounds.extents.x + 30 &&
                    playerCollider.bounds.center.x + playerCollider.bounds.extents.x > enemyCollider.bounds.center.x - enemyCollider.bounds.extents.x - 30)
                {
                    currentDirection = EnemyDirection.Up;
                    //print("Looking up");
                }
                break;


            case EnemyDirection.Down:
                enemyAnimator.SetBool("FacingUp", false);
                enemyAnimator.SetBool("FacingDown", true);
                enemyAnimator.SetBool("FacingRight", false);
                enemyAnimator.SetBool("FacingLeft", false);

                enemySprite.flipX = false;

                if (playerCollider.bounds.center.x + playerCollider.bounds.extents.x < enemyCollider.bounds.center.x - enemyCollider.bounds.extents.x - 30)
                {
                    currentDirection = EnemyDirection.Left;
                    //print("Looking left");

                }
                else if (playerCollider.bounds.center.x + playerCollider.bounds.extents.x < enemyCollider.bounds.center.x - enemyCollider.bounds.extents.x &&
                        playerCollider.bounds.center.y - playerCollider.bounds.extents.y < enemyCollider.bounds.center.y + enemyCollider.bounds.extents.y &&
                        playerCollider.bounds.center.y + playerCollider.bounds.extents.y > enemyCollider.bounds.center.y - enemyCollider.bounds.extents.y &&
                        playerCollider.bounds.center.x + playerCollider.bounds.extents.x > enemyCollider.bounds.center.x - enemyCollider.bounds.extents.x - 30)
                {
                    currentDirection = EnemyDirection.Left;
                    //print("Looking left");

                }
                else if (playerCollider.bounds.center.x - playerCollider.bounds.extents.x > enemyCollider.bounds.center.x + enemyCollider.bounds.extents.x + 30)
                {
                    currentDirection = EnemyDirection.Right;
                    //print("Looking Right");

                }
                else if (playerCollider.bounds.center.x - playerCollider.bounds.extents.x > enemyCollider.bounds.center.x + enemyCollider.bounds.extents.x &&
                        playerCollider.bounds.center.y - playerCollider.bounds.extents.y < enemyCollider.bounds.center.y + enemyCollider.bounds.extents.y &&
                        playerCollider.bounds.center.y + playerCollider.bounds.extents.y > enemyCollider.bounds.center.y - enemyCollider.bounds.extents.y &&
                        playerCollider.bounds.center.x - playerCollider.bounds.extents.x < enemyCollider.bounds.center.x + enemyCollider.bounds.extents.x + 30)
                {
                    currentDirection = EnemyDirection.Right;
                    //print("Looking Right");

                }
                break;

            case EnemyDirection.Up:
                enemyAnimator.SetBool("FacingUp", true);
                enemyAnimator.SetBool("FacingDown", false);
                enemyAnimator.SetBool("FacingRight", false);
                enemyAnimator.SetBool("FacingLeft", false);

                enemySprite.flipX = false;

                if (playerCollider.bounds.center.x + playerCollider.bounds.extents.x < enemyCollider.bounds.center.x - enemyCollider.bounds.extents.x - 30)
                {
                    currentDirection = EnemyDirection.Left;
                    //print("Looking left");

                }
                else if (playerCollider.bounds.center.x + playerCollider.bounds.extents.x < enemyCollider.bounds.center.x - enemyCollider.bounds.extents.x &&
                        playerCollider.bounds.center.y - playerCollider.bounds.extents.y < enemyCollider.bounds.center.y + enemyCollider.bounds.extents.y &&
                        playerCollider.bounds.center.y + playerCollider.bounds.extents.y > enemyCollider.bounds.center.y - enemyCollider.bounds.extents.y &&
                        playerCollider.bounds.center.x + playerCollider.bounds.extents.x > enemyCollider.bounds.center.x - enemyCollider.bounds.extents.x - 30)
                {
                    currentDirection = EnemyDirection.Left;
                    //print("Looking left");

                }
                else if (playerCollider.bounds.center.x - playerCollider.bounds.extents.x > enemyCollider.bounds.center.x + enemyCollider.bounds.extents.x + 30)
                {
                    currentDirection = EnemyDirection.Right;
                    //print("Looking Right");

                }
                else if (playerCollider.bounds.center.x - playerCollider.bounds.extents.x > enemyCollider.bounds.center.x + enemyCollider.bounds.extents.x &&
                        playerCollider.bounds.center.y - playerCollider.bounds.extents.y < enemyCollider.bounds.center.y + enemyCollider.bounds.extents.y &&
                        playerCollider.bounds.center.y + playerCollider.bounds.extents.y > enemyCollider.bounds.center.y - enemyCollider.bounds.extents.y &&
                        playerCollider.bounds.center.x - playerCollider.bounds.extents.x < enemyCollider.bounds.center.x + enemyCollider.bounds.extents.x + 30)
                {
                    currentDirection = EnemyDirection.Right;
                    //print("Looking Right");

                }
                break;
        }
    }

    void Shoot()
    {
        projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    }

    private void RangedEnemy()
    {
        ChangeDirection();
    }

}
