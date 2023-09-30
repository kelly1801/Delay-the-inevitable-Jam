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
    [SerializeField] private float playerRotationSpeed = 50.0f ;
    [SerializeField] private float jumpForce = 20.0f;
    [SerializeField] private float gravityModifier = 5.0f;
    [SerializeField] private float raycastDistance = 0.5f;  
    [SerializeField] private UIManager uiManager;
    private bool hasPowerUp = false;
    private bool isOnGround = true;
    private Rigidbody playerRb;
    private AudioManager audioManager;
    private Animator animator;
    public int cornHarvested = 0;
    private bool isEating = false;
    private float eatAnimationDuration = 2.0f; 
    private float eatAnimationTimer = 0.0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioManager = FindAnyObjectByType<AudioManager>();
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    void Update()
    {
        // Verify that the player is over a structure
        isOnGround = Physics.Raycast(transform.position, Vector3.down, raycastDistance);
        if (!uiManager.isGameOver)
        {
            MovementPlayer();
            Jump();
        }

        if (isEating)
        {
            eatAnimationTimer += Time.deltaTime;

            // Verifica si la animación de comer ha terminado
            if (eatAnimationTimer >= eatAnimationDuration)
            {
                isEating = false;
                eatAnimationTimer = 0.0f;
                animator.SetBool("Eat", false); // Cambia la animación de "Eat" a false
            }
        }

        if (cornHarvested == 10) hasPowerUp = true;
    }


    private void OnCollisionEnter(Collision other)
    {
        animator.SetBool("TurnHead", false);
        animator.SetBool("Eat", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Corn"))
        {
            audioManager.PlaySound(4, 0.3f);
            animator.SetBool("Eat", true); // Activa la animación de "Eat"
            isEating = true;
            eatAnimationTimer = 0.0f; // Reinicia el temporizador
            if (cornHarvested < 10)
            {
                cornHarvested++;
                Destroy(other.gameObject);
            }
        }
    }


    private void MovementPlayer()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float lateralInput = Input.GetAxis("Horizontal");
        bool isWalking = forwardInput != 0 || lateralInput != 0; // Variable para rastrear si el personaje est� caminando
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

        // Reproducir sonido de caminar solo cuando el personaje est� caminando
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

    private void UsePowerUp()
    {
        
    }
}