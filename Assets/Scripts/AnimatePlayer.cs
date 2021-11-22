using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Directions
{
    Up = 1,
    Right = 2,
    Down = 3,
    Left = 4
}


public class AnimatePlayer : MonoBehaviour
{
    public Animator playerAnimator;

    private SpriteRenderer spriteRenderer;

    private bool moving;
    private bool shooting;

    private bool spriteFlipped;

    private Directions currentDirection;

    private void Start()
    {
        ResetPlayerAnimation();
        SetAnimationVariables();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        CheckInput();
        SetAnimationVariables();
    }

    //Checks for player Input
    void CheckInput()
    {
        //Check if the player is shooting
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shooting = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            shooting = false;
        }

        //If no keys are down, reset moving variable
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            moving = false;
            return; //No need to check everything else if no directional keys are pressed
        }

        //if W and S are both pressed, A and D are both pressed, or all directional buttons are pressed, player is not in motion
        if ((Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)))
        {
            moving = false;
            return; //No need to check individual keys if the player cannot be moving
        }

        //Check which direction the player is moving in
        //Player will animate in the direction of the last key pressed
        //e.g. if moving up (W) and then moving to the right (D), player will move in up and to the right, but will play the "move right" animation
        if (Input.GetKey(KeyCode.W))
        {
            moving = true;
            spriteRenderer.flipX = false;
            currentDirection = Directions.Up;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moving = true;
            spriteRenderer.flipX = false;
            currentDirection = Directions.Down;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moving = true;
            spriteRenderer.flipX = true;
            currentDirection = Directions.Left;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moving = true;
            spriteRenderer.flipX = false;
            currentDirection = Directions.Right;
        }
    }

    //Adjusts variables in the player animator 
    void SetAnimationVariables()
    {
        playerAnimator.SetBool("moving", moving);
        playerAnimator.SetBool("shooting", shooting);
        playerAnimator.SetFloat("direction", (float)currentDirection);
    }

    //Resets player to default facing right (like at the start of a level)
    //public method so it can be called from other scripts
    public void ResetPlayerAnimation()
    {
        moving = false;
        shooting = false;
        currentDirection = Directions.Right;
    }
}
