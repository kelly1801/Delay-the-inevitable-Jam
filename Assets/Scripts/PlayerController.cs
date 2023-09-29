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

    void Start()
    {
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
    }

    private void MovementPlayer()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float lateralInput = Input.GetAxis("Horizontal");

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
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
        }
    }
}