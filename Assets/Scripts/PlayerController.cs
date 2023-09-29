using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float playerSneakySpeed = 1.0f;
    [SerializeField] private float playerRotationSpeed = 50.0f ;
    [SerializeField] private float jumpForce = 20.0f;
    [SerializeField] private float gravityModifier = 5.0f;
    [SerializeField] private float raycastDistance = 1.0f;  
    [SerializeField] private bool isOnGround = true;
    [SerializeField] private UIManager uiManager;
    private Rigidbody playerRb;
    private AudioManager audioManager;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioManager = FindAnyObjectByType<AudioManager>();
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    void Update()
    {
        if (!uiManager.isGameOver)
        {
            MovementPlayer();
            Jump();
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        // Verify that the player is over a structure
        isOnGround = Physics.Raycast(transform.position, Vector3.down, raycastDistance);
        animator.SetBool("TurnHead", false);
    }

    private void MovementPlayer()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float lateralInput = Input.GetAxis("Horizontal");
        bool isWalking = forwardInput != 0 || lateralInput != 0; // Variable para rastrear si el personaje está caminando
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

        // Reproducir sonido de caminar solo cuando el personaje está caminando
        animator.SetBool("Run", isWalking);

        animator.SetBool("Walk", isSneaking);

        if (isWalking)
        {
            audioManager.PlaySound(1, 0.2f); // Reproduce el sonido de caminar
        }
    }



    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
                animator.SetBool("TurnHead", true);
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                audioManager.PlaySound(0, 0.5f);
                isOnGround = false;
        }
    }
}