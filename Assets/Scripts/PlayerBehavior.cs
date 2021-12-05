using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerDirection
{
    Up,
    Right,
    Down,
    Left
}

public class PlayerBehavior : MonoBehaviour
{
    PauseGame pauseMenu;

    //public GameObject projectilePrefab;
    public GameObject fireballPrefab;

    //Direction and velocity
    private PlayerDirection currentDirection = PlayerDirection.Right;
    private Vector3 velocity = Vector3.zero;

    //Sprite information
    private SpriteRenderer playerSprite;
    private Collider2D playerCollider;

    //Wall collision bools
    private bool collidingTop = false;
    private bool collidingLeft = false;
    private bool collidingBottom = false;
    private bool collidingRight = false;

    //Projectile information
    private GameObject projectile;
    private Vector3 projectileVelocity;

    //private List<GameObject> projectiles = new List<GameObject>();

    //Player stat(s)
    public float health = 100; //100
    public float attack = 10; //10
    public float shieldRestore = 0.5f;
    private bool shielding = false;

    //bool basicProjectileActive = true;
    bool fireBallActive = true;

    void Start()
    {
        playerSprite = gameObject.GetComponent<SpriteRenderer>();
        playerCollider = gameObject.GetComponent<Collider2D>();

        pauseMenu = GameObject.FindObjectOfType<PauseGame>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.gamePaused)
        {
            //Projectile
            if (projectile != null)
            {
                projectile.transform.position += projectileVelocity;
            }

            if (Input.GetKeyDown(KeyCode.Space) && !shielding)
            {
                shoot();
            }

            //Move Up
            if (Input.GetKey(KeyCode.W) && !collidingTop && !shielding)
            {
                if (currentDirection != PlayerDirection.Up)
                {
                    currentDirection = PlayerDirection.Up;
                }
                move(currentDirection);
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                resetVelocity();
            }

            //Move Left
            if (Input.GetKey(KeyCode.A) && !collidingLeft && !shielding)
            {
                if (currentDirection != PlayerDirection.Left)
                {
                    currentDirection = PlayerDirection.Left;
                }
                move(currentDirection);
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                resetVelocity();
            }

            //Move Down
            if (Input.GetKey(KeyCode.S) && !collidingBottom && !shielding)
            {
                if (currentDirection != PlayerDirection.Down)
                {
                    currentDirection = PlayerDirection.Down;
                }
                move(currentDirection);
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                resetVelocity();
            }

            //Move Right
            if (Input.GetKey(KeyCode.D) && !collidingRight && !shielding)
            {
                if (currentDirection != PlayerDirection.Right)
                {
                    currentDirection = PlayerDirection.Right;
                }
                move(currentDirection);
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                resetVelocity();
            }

            if (Input.GetKey(KeyCode.LeftShift) && velocity == Vector3.zero)
            {
                shielding = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                shielding = false;
            }
        }

    }

    //Adjust the camera and player position so that the player is always at the center of the camera
    void move(PlayerDirection direction)
    {
        switch (direction)
        {
            case PlayerDirection.Up:
                velocity = Vector3.up * .5f;
                gameObject.transform.position += velocity;
                break;

            case PlayerDirection.Down:
                velocity = Vector3.up * .5f;
                gameObject.transform.position -= velocity;
                break;

            case PlayerDirection.Left:
                velocity = Vector3.right * .5f;
                gameObject.transform.position -= velocity;
                break;

            case PlayerDirection.Right:
                velocity = Vector3.right * .5f;
                gameObject.transform.position += velocity;
                break;
        }
    }

    void shoot()
    {
        Vector3 instantiateSpot = Vector3.zero;
        switch (currentDirection)
        {
            case PlayerDirection.Up:
                projectileVelocity = Vector3.up * 3f;
                instantiateSpot = gameObject.transform.position + Vector3.up;
                break;

            case PlayerDirection.Left:
                projectileVelocity = -Vector3.right * 3f;
                instantiateSpot = gameObject.transform.position - Vector3.right;
                break;

            case PlayerDirection.Down:
                projectileVelocity = -Vector3.up * 3f;
                instantiateSpot = gameObject.transform.position - Vector3.up;
                break;

            case PlayerDirection.Right:
                projectileVelocity = Vector3.right * 3f;
                instantiateSpot = gameObject.transform.position + Vector3.right;
                break;
        }

        if (fireBallActive)
        {
            Destroy(projectile);
            projectile = Instantiate(fireballPrefab, instantiateSpot, Quaternion.identity);
            switch (currentDirection) //Change Orientation of the projectile
            {
                case PlayerDirection.Up:
                    projectile.transform.eulerAngles = new Vector3(0.0f, 0.0f, 90.0f);
                    break;
                case PlayerDirection.Down:
                    projectile.transform.eulerAngles = new Vector3(0.0f, 0.0f, -90.0f);
                    break;
                case PlayerDirection.Left:
                    projectile.transform.eulerAngles = new Vector3(0.0f, 0.0f, 180.0f);
                    break;
            }
            Destroy(projectile, 3.0f);
        }

    }

    //Set the velocity vector to 0
    public void resetVelocity()
    {
        velocity = Vector3.zero;
    }

    //Figure out which direction the collsion occured in (AABB collision check)
    void CheckCollisionDirection(Collider2D other)
    {
        switch (currentDirection)
        {
            case PlayerDirection.Right:
                if (playerCollider.bounds.center.x - playerCollider.bounds.extents.x < other.bounds.center.x + other.bounds.extents.x && other.tag != "Enemy Bullet")
                {
                    collidingRight = true;
                    //Debug.Log("Collision Active");
                }
                break;

            case PlayerDirection.Left:
                if (playerCollider.bounds.center.x + playerCollider.bounds.extents.x > other.bounds.center.x - other.bounds.extents.x && other.tag != "Enemy Bullet")
                {
                    collidingLeft = true;
                    //Debug.Log("Collision Active");
                }
                break;

            case PlayerDirection.Down:
                if (playerCollider.bounds.center.y + playerCollider.bounds.extents.y > other.bounds.center.y - other.bounds.extents.y && other.tag != "Enemy Bullet")
                {
                    collidingBottom = true;
                    //Debug.Log("Collision Active");
                }
                break;

            case PlayerDirection.Up:
                if (playerCollider.bounds.center.y - playerCollider.bounds.extents.y < other.bounds.center.y + other.bounds.extents.y && other.tag != "Enemy Bullet")
                {
                    collidingTop = true;
                    //Debug.Log("Collision Active");
                }
                break;
        }
    }

    //Reset all collision bools
    void ResetCollisions()
    {
        collidingTop = false;
        collidingLeft = false;
        collidingBottom = false;
        collidingRight = false;
    }

    //Triggers whenever the rigid body collides with a Collider2D
    void OnTriggerEnter2D(Collider2D other)
    {

        CheckCollisionDirection(other);

        if (other.tag == "Projectile Catcher")
        {
            ResetCollisions();
            return;
        }

        //Enemy Interaction
        if ((other.tag == "Enemy Bullet" || other.tag == "Melee Enemy") && !shielding)
        {
            health -= 10;
            playerSprite.color = Color.red;
        }

        if (other.tag == "Ranged Enemy" && !shielding)
        {
            health -= 5;
            playerSprite.color = Color.red;
        }

        if ((other.tag == "Enemy Bullet" || other.tag == "Melee Enemy") && shielding)
        {
            if (health + 0.5f <= 100)
            {
                health += shieldRestore;
            }
            if (health > 100)
            {
                health = 100;
            }
            playerSprite.color = Color.yellow;
        }

        //Pickups
        //Minor pickups are ones that are dropped by regular enemies
        if (other.tag == "Minor Fire Pickup")
        {
            attack += 0.5f;
            Destroy(other.gameObject);
            ResetCollisions();
        }
        if (other.tag == "Minor Shield Pickup")
        {
            shieldRestore += 0.5f;
            Destroy(other.gameObject);
            ResetCollisions();
        }

        //Death
        if (health <= 0)
        {
            SceneManager.LoadScene("GameOver");
            //health = 100;
        }

    }

    //Triggers whenever the rigid body stops colliding with a Collider2D
    private void OnTriggerExit2D(Collider2D other)
    {
        ResetCollisions();
        //Debug.Log("Collisions Reset!");

        if (playerSprite.color != Color.white)
        {
            playerSprite.color = Color.white;
        }
    }
}
