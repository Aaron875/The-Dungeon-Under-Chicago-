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

    public GameObject fireballPrefab;

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

    bool basicProjectileActive = true;
    bool fireBallActive = false;

    void Start()
    {
        playerSprite = gameObject.GetComponent<SpriteRenderer>();
        playerCollider = gameObject.GetComponent<Collider2D>();
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

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            basicProjectileActive = true;
            fireBallActive = false;
            //print("Basic Projectile Now Active");
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            fireBallActive = true;
            basicProjectileActive = false;
            //print("Fireball now active");
        }

        //Move Up
        if (Input.GetKey(KeyCode.W) && !collidingTop)
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
        if (Input.GetKey(KeyCode.A) && !collidingLeft)
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
        if (Input.GetKey(KeyCode.S) && !collidingBottom)
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
        if (Input.GetKey(KeyCode.D) && !collidingRight)
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
                projectileVelocity = Vector3.up * 1f;
                instantiateSpot = gameObject.transform.position + Vector3.up;
                break;

            case PlayerDirection.Left:
                projectileVelocity = -Vector3.right * 1f;
                instantiateSpot = gameObject.transform.position - Vector3.right;
                break;

            case PlayerDirection.Down:
                projectileVelocity = -Vector3.up * 1f;
                instantiateSpot = gameObject.transform.position - Vector3.up;
                break;

            case PlayerDirection.Right:
                projectileVelocity = Vector3.right * 1f;
                instantiateSpot = gameObject.transform.position + Vector3.right;
                break;
        }

        if(basicProjectileActive)
        {
            projectile = Instantiate(projectilePrefab, instantiateSpot, Quaternion.identity);
            Destroy(projectile, 3.0f);
        }

        else if(fireBallActive)
        {
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
                if (playerCollider.bounds.center.x - playerCollider.bounds.extents.x < other.bounds.center.x + other.bounds.extents.x)
                {
                    collidingRight = true;
                    Debug.Log("Collision Active");
                }
                break;

            case PlayerDirection.Left:
                if (playerCollider.bounds.center.x + playerCollider.bounds.extents.x > other.bounds.center.x - other.bounds.extents.x)
                {
                    collidingLeft = true;
                    Debug.Log("Collision Active");
                }
                break;

            case PlayerDirection.Down:
                if (playerCollider.bounds.center.y + playerCollider.bounds.extents.y > other.bounds.center.y - other.bounds.extents.y)
                {
                    collidingBottom = true;
                    Debug.Log("Collision Active");
                }
                break;

            case PlayerDirection.Up:
                if (playerCollider.bounds.center.y - playerCollider.bounds.extents.y < other.bounds.center.y + other.bounds.extents.y)
                {
                    collidingTop = true;
                    Debug.Log("Collision Active");
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
        Debug.Log("Collisions Reset!");
    }
}
