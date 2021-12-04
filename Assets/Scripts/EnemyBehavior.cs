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

    public EnemyDirection currentDirection;

    public GameObject projectilePrefab;

    public GameObject minorFirePickupPrefab;
    public GameObject minorShieldPickupPrefab;

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

    private float health;

    [SerializeField]
    private string rangeOrMelee;

    // used to reduce player health
    public GameObject player;
    private PlayerBehavior playerScript;

    //times the enemies shots
    float targetTime = 4.0f;


    // Start is called before the first frame update
    void Start()
    {
        enemySprite = gameObject.GetComponent<SpriteRenderer>();
        enemyCollider = gameObject.GetComponent<Collider2D>();
        playerCollider = player.GetComponent<Collider2D>();

        playerScript = player.GetComponent<PlayerBehavior>();

        health = 50;
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

        // run this code if it is a melee enemy
        else if(rangeOrMelee == "Melee")
        {
            MeleeEnemy();
            //print(Vector2.Distance(this.gameObject.transform.position, player.transform.position));

            if (Vector2.Distance(gameObject.transform.position, player.transform.position) < 20.0f)
            {
                enemyAnimator.SetBool("walk", false);
                enemyAnimator.SetBool("idle", false);
                enemyAnimator.SetBool("melee", true);
                return;
            }
            else
            {
                enemyAnimator.SetBool("walk", true);
                enemyAnimator.SetBool("idle", false);
                enemyAnimator.SetBool("melee", false);
            }

            if (Vector2.Distance(gameObject.transform.position, player.transform.position) < 128.0f)
            {
                /*switch (currentDirection)
                {
                    case EnemyDirection.Left:
                        transform.position += Vector3.left * 0.1f;
                        break;

                    case EnemyDirection.Right:
                        transform.position += Vector3.right * 0.1f;
                        break;

                    case EnemyDirection.Up:
                        transform.position += Vector3.up * 0.1f;
                        break;

                    case EnemyDirection.Down:
                        transform.position += Vector3.down * 0.1f;
                        break;
                }*/
                enemyAnimator.SetBool("walk", true);
                enemyAnimator.SetBool("idle", false);

                // Check if player is left of melee enemy
                if (player.transform.position.x < gameObject.transform.position.x)
                {
                    // Move enemy left
                    transform.position += Vector3.left * 0.3f;
                }

                // Check if player is right of melee enemy
                if (player.transform.position.x > gameObject.transform.position.x)
                {
                    // Move enemy right
                    transform.position += Vector3.right * 0.3f;
                }

                // Check if player is above melee enemy
                if (player.transform.position.y > gameObject.transform.position.y)
                {
                    // Move enemy up
                    transform.position += Vector3.up * 0.3f;
                }

                // Check if player is below melee enemy
                if (player.transform.position.y < gameObject.transform.position.y)
                {
                    // Move enemy down
                    transform.position += Vector3.down * 0.3f;
                }
            }
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

        if (other.tag == "Player" && rangeOrMelee == "Ranged")
        {
            health -= 50;
            enemySprite.color = Color.red;
            // need to reduce player health as well when we have a health system
        }


        // Reduce enemies health if they get hit by a fireball
        if (other.tag == "Fireball")
        {
            health -= playerScript.attack;
            //print("Fireball hit");
            Destroy(other.gameObject);
            enemySprite.color = Color.red;
        }

        // Destory the enemy if its health reaches 0 or less
        if (health <= 0)
        {
            int randPickup = Random.Range(0, 3);
            if(randPickup == 0)
            {
                Instantiate(minorFirePickupPrefab, gameObject.transform.position, Quaternion.identity);
            }
            else if (randPickup == 1)
            {
                Instantiate(minorShieldPickupPrefab, gameObject.transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
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
                if(rangeOrMelee == "Ranged")
                {
                    enemyAnimator.SetBool("FacingUp", false);
                    enemyAnimator.SetBool("FacingDown", false);
                    enemyAnimator.SetBool("FacingRight", false);
                    enemyAnimator.SetBool("FacingLeft", true);
                }

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
                if (rangeOrMelee == "Ranged")
                {
                    enemyAnimator.SetBool("FacingUp", false);
                    enemyAnimator.SetBool("FacingDown", false);
                    enemyAnimator.SetBool("FacingRight", false);
                    enemyAnimator.SetBool("FacingLeft", true);
                }

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
                if (rangeOrMelee == "Ranged")
                {
                    enemyAnimator.SetBool("FacingUp", false);
                    enemyAnimator.SetBool("FacingDown", false);
                    enemyAnimator.SetBool("FacingRight", false);
                    enemyAnimator.SetBool("FacingLeft", true);
                }

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
                if (rangeOrMelee == "Ranged")
                {
                    enemyAnimator.SetBool("FacingUp", false);
                    enemyAnimator.SetBool("FacingDown", false);
                    enemyAnimator.SetBool("FacingRight", false);
                    enemyAnimator.SetBool("FacingLeft", true);
                }

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

    private void MeleeEnemy()
    {
        ChangeDirection();
    }

}
