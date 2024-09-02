using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerRb;
    private Animator animator;
    public float speed;
    private float walkSpeed;
    private float sprintMultiply = 3f;

    public float input;
    public float jumpForce;
    public SpriteRenderer spriteRenderer;
    private bool sprint = false;
    public float jumpTime = 0.35f;
    public float jumpTimeCounter;
    private bool isJumping;
    public LayerMask groundLayer;
    private bool isGrounded;
    public Transform feetposition;
    public float groundCheckCircle;
    private string currantAnimation = "";

    void Start()
    {
        walkSpeed = speed; //sets the player movement speed
        animator = GetComponent<Animator>(); //gets the animator componant

    }

    // Update is called once per frame
    void Update()
    {
        sprint = Input.GetKey(KeyCode.LeftShift); // boolean to check if your pressing the to key to sprint 

        input = Input.GetAxisRaw("Horizontal"); //checks if the player is in the air or not
        if(input < 0)
        {
            spriteRenderer.flipX = true;
        }else if(input > 0){
            spriteRenderer.flipX = false;
        }

        isGrounded = Physics2D.OverlapCircle(feetposition.position, groundCheckCircle, groundLayer); //checks if the player is touching the ground layer based on feet pos

        if(isGrounded == true && Input.GetButton("Jump")) //checks if the player is grounded equals trie and you pressed the button to jump
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            playerRb.velocity = Vector2.up * jumpForce; //applies force to make the player jump
        }

        if(Input.GetButton("Jump") && isJumping == true)
        {
            if(jumpTimeCounter > 0)
            {
                playerRb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }else{
                isJumping = false;
            }
        }

        if(Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }

        CheckAnimation();
    }

    private void CheckAnimation() //checks which animation state the player is in
    {
        if(input != 0)
        {
            ChangeAnimation("Walking");
        }else{
            ChangeAnimation("Idle");
        }
    }

    private void ChangeAnimation(string animation, float crossfade = 0.2f) //changes the animation of the player
    {
        if(currantAnimation != animation)
        {
            currantAnimation = animation;
            animator.CrossFade(animation, crossfade);
        }
    }

    void FixedUpdate() //allows the player to move faster if holding the shift key
    {
        if(sprint == true)
        {
            speed = walkSpeed * sprintMultiply;
        }else{
            speed = walkSpeed;
        }
        playerRb.velocity = new Vector2 (input * speed, playerRb.velocity.y);
    }
}
