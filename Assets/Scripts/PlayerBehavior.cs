using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerDirection
{
    Up,
    Right,
    Down,
    Left
}

public class PlayerBehavior : MonoBehaviour
{
    public GameObject projectilePrefab;

    private PlayerDirection currentDirection = PlayerDirection.Right;
    private Vector3 velocity = Vector3.zero;

    private SpriteRenderer playerSprite;
    private Collider2D playerCollider;

    private bool collidingTop = false;
    private bool collidingLeft = false;
    private bool collidingBottom = false;
    private bool collidingRight = false;

    private GameObject projectile;
    private Vector3 projectileVelocity;

    void Start()
    {
        playerSprite = gameObject.GetComponent<SpriteRenderer>();
        playerCollider = gameObject.GetComponent<Collider2D>();
        //playerSprite.color = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {
        //Projectile
        if(projectile != null)
        {
            projectile.transform.position += projectileVelocity;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            shoot();
        }

        //Move Up
        if (Input.GetKey(KeyCode.W) && !collidingTop)
        {
            if (currentDirection != PlayerDirection.Up)
            {
                currentDirection = PlayerDirection.Up;
            }
            move(currentDirection);
            //playerSprite.color = Color.black;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            resetVelocity();
        }

        //Move Left
        if (Input.GetKey(KeyCode.A) && !collidingLeft)
        {
            if (currentDirection != PlayerDirection.Left)
            {
                currentDirection = PlayerDirection.Left;
            }
            move(currentDirection);
            //playerSprite.color = Color.yellow;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            resetVelocity();
        }

        //Move Down
        if (Input.GetKey(KeyCode.S) && !collidingBottom)
        {
            if (currentDirection != PlayerDirection.Down)
            {
                currentDirection = PlayerDirection.Down;
            }
            move(currentDirection);
            //playerSprite.color = Color.cyan;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            resetVelocity();
        }

        //Move Right
        if (Input.GetKey(KeyCode.D) && !collidingRight)
        {
            if (currentDirection != PlayerDirection.Right)
            {
                currentDirection = PlayerDirection.Right;
            }
            move(currentDirection);
            //playerSprite.color = Color.green;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            resetVelocity();
        }
    }

    //Adjust the camera and player position so that the player is always at the center of the camera
    void move(PlayerDirection direction)
    {
        switch (direction)
        {
            case PlayerDirection.Up:
                velocity = Vector3.up * .2f;
                gameObject.transform.position += velocity;
                break;

            case PlayerDirection.Down:
                velocity = Vector3.up * .2f;
                gameObject.transform.position -= velocity;
                break;

            case PlayerDirection.Left:
                velocity = Vector3.right * .2f;
                gameObject.transform.position -= velocity;
                break;

            case PlayerDirection.Right:
                velocity = Vector3.right * .2f;
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
                projectileVelocity = Vector3.up;
                instantiateSpot = gameObject.transform.position + Vector3.up;
                break;

            case PlayerDirection.Left:
                projectileVelocity = -Vector3.right;
                instantiateSpot = gameObject.transform.position - Vector3.right;
                break;

            case PlayerDirection.Down:
                projectileVelocity = -Vector3.up;
                instantiateSpot = gameObject.transform.position - Vector3.up;
                break;

            case PlayerDirection.Right:
                projectileVelocity = Vector3.right;
                instantiateSpot = gameObject.transform.position + Vector3.right;
                break;
        }

        
        projectile = Instantiate(projectilePrefab, instantiateSpot, Quaternion.identity);
        Destroy(projectile, 3.0f);
    }

    //Set the velocity vecttor to 0
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
                if (playerCollider.bounds.center.x - playerCollider.bounds.extents.x < other.bounds.center.x + other.bounds.extents.x)
                {
                    collidingRight = true;
                }
                break;

            case PlayerDirection.Left:
                if (playerCollider.bounds.center.x + playerCollider.bounds.extents.x > other.bounds.center.x - other.bounds.extents.x)
                {
                    collidingLeft = true;
                }
                break;

            case PlayerDirection.Down:
                if (playerCollider.bounds.center.y + playerCollider.bounds.extents.y > other.bounds.center.y - other.bounds.extents.y)
                {
                    collidingBottom = true;
                }
                break;

            case PlayerDirection.Up:
                if (playerCollider.bounds.center.y - playerCollider.bounds.extents.y < other.bounds.center.y + other.bounds.extents.y)
                {
                    collidingTop = true;
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
    }

    //Triggers whenever the rigid body stops colliding with a Collider2D
    private void OnTriggerExit2D(Collider2D other)
    {
        ResetCollisions();
        //Debug.Log("Collisions Reset!");
    }
}
