using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float playerSneakySpeed = 1.0f;
    [SerializeField] private float playerRotationSpeed = 70.0f ;
    [SerializeField] private float jumpForce = 15.0f;
    [SerializeField] private float glideForce = 150.0f;
    [SerializeField] private float cornConsumptionRate = 10f;
    [SerializeField] private float gravityModifier = 5.0f;
    [SerializeField] private float raycastDistance = 1f;  
    [SerializeField] private UIManager uiManager;
    
    private bool isOnGround = true;
    private bool isJumping = true;
    private bool isGliding = false;
    private bool isEating = false;
    private bool isPushing = false;
    
    
    private float maxHeight = 2.0f;
    private float jumpStartYPosition = 1.3f;
    private float eatAnimationDuration = 2.0f; 
    private float eatAnimationTimer = 0.0f;
    
    private Rigidbody playerRb;
    private AudioManager audioManager;
    private Animator animator;
    
    public float cornHarvested = 10.0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioManager = FindAnyObjectByType<AudioManager>();
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    void Update()
    {
        // Check that the player is over a structure
        isOnGround = Physics.Raycast(transform.position, Vector3.down, raycastDistance);
        
        if (!uiManager.isGameOver)
        {
            MovementPlayer();
            if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
            {
                Jump();
            }
            if (Input.GetKey(KeyCode.Space) && !isOnGround && cornHarvested > 0)
            {
                Glide();
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                StopGliding();
            }

            if (isGliding)
            {
                cornHarvested -= cornConsumptionRate * Time.deltaTime;
                if (cornHarvested <= 0)
                {
                    cornHarvested = 0;
                    StopGliding();
                }
            }
        }

        if (isEating)
        {
            eatAnimationTimer += Time.deltaTime;

            // Check if the eating animation has finished
            if (eatAnimationTimer >= eatAnimationDuration)
            {
                isEating = false;
                eatAnimationTimer = 0.0f;
                animator.SetBool("Eat", false); // Change the "Eat" animation to false
            }
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        animator.SetBool("TurnHead", false);
        animator.SetBool("Eat", false);
    
        if (other.gameObject.CompareTag("Box"))
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                other.gameObject.transform.SetParent(transform); 
                isPushing = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Goal"))
        {
            uiManager.LoadSceneByName("WonScene");
        }
        if (other.gameObject.CompareTag("Corn"))
        {
            if (cornHarvested < 100)
            {
                audioManager.PlaySound(4, 0.3f);
                animator.SetBool("Eat", true); // Activate "Eat" animation
                isEating = true;
                eatAnimationTimer = 0.0f; // restart temp
                cornHarvested += 10;
                Destroy(other.gameObject);
            }
        }
    }
    private void MovementPlayer()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float lateralInput = Input.GetAxis("Horizontal");
        bool isWalking = forwardInput != 0 || lateralInput != 0; // Variable to track if the character is walking
        bool isSneaking = Input.GetKey(KeyCode.LeftShift) && playerSneakySpeed > 0;

        // Calculate movement direction
        Vector3 localDirection = transform.forward;
        Vector3 moveDirection = Input.GetKey(KeyCode.LeftShift) ?
            localDirection * forwardInput * playerSneakySpeed :
            localDirection * forwardInput * playerSpeed;

        // Calculate the new position
        Vector3 newPosition = playerRb.position + moveDirection * Time.deltaTime;

        // Move the player to the new position
        playerRb.MovePosition(newPosition);

        // Rotate the player
        float rotation = lateralInput * playerRotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * rotation, Space.World);

        // Play walking sound only when character is walking
        animator.SetBool("Run", isWalking);

        animator.SetBool("Walk", isSneaking);

        if (isWalking)
        {
            audioManager.PlaySound(1, 0.2f); // Plays the sound of walking
        }

        if (isPushing)
        {
            if (!isWalking)
            {
                Transform box = transform.Find("Box Aux");
                if (box)
                {
                    box.SetParent(null);
                }
            }
        }
    }
    private void Jump()
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        audioManager.PlaySound(0, 0.5f);
        isOnGround = false;
        isJumping = true;
        jumpStartYPosition = transform.position.y;
    }
    private void Glide()
    {
        // Check if the player is in the max height of the jump
        if (transform.position.y >= jumpStartYPosition + maxHeight)
        {
            isGliding = true;
            playerRb.useGravity = false;
            playerRb.AddForce(Vector3.down * glideForce);
        }
    }

    private void StopGliding()
    {
        isJumping = false;
        isGliding = false;
        playerRb.useGravity = true;
        playerRb.velocity = new Vector3(playerRb.velocity.x, 0.0f, playerRb.velocity.z);
    }
}