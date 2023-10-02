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
    [SerializeField] private float flyForce = 15.0f;
    [SerializeField] private float gravityModifier = 5.0f;
    [SerializeField] private float raycastDistance = 0.5f;  
    [SerializeField] private UIManager uiManager;
    private bool isOnGround = true;
    private Rigidbody playerRb;
    private AudioManager audioManager;
    private Animator animator;
    public float cornHarvested = 10.0f;
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
            //Fly();
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

        if (cornHarvested ==  100) {
            PowerUp();
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        animator.SetBool("TurnHead", false);
        animator.SetBool("Eat", false);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Goal"))
        {
            uiManager.LoadSceneByName("WonScene");

        }
        if (other.gameObject.CompareTag("Corn"))
        {
            audioManager.PlaySound(4, 0.3f);
            animator.SetBool("Eat", true); // Activate "Eat" animation
            isEating = true;
            eatAnimationTimer = 0.0f; // restart temp
            if (cornHarvested < 100)
            {
                cornHarvested += 10;
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

    private void PowerUp()
    {
        jumpForce = 25;
        StartCoroutine(ReduceCornHarvestedOverTime());
    }

    private IEnumerator ReduceCornHarvestedOverTime()
    {
        float duration = 10f; // The duration over which you want to reduce cornHarvested (in seconds)
        float targetCorn = 10f; // The target value for cornHarvested

        float startCorn = cornHarvested;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            cornHarvested = Mathf.Lerp(startCorn, targetCorn, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure cornHarvested reaches the exact target value to avoid approximation errors
        cornHarvested = targetCorn;

        // Additional logic after reducing cornHarvested can be placed here
    }

    // private void Fly()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space) && !isOnGround && cornHarvested > 0)
    //     {
    //         playerRb.useGravity = false;
    //         playerRb.velocity = new Vector3(playerRb.velocity.x, 0.0f,playerRb.velocity.z);
    //         playerRb.AddForce(Vector3.down * flyForce);
    //         cornHarvested -= 0.1f;
    //
    //         // Asegurarse de que no se salga de control aplicando una fuerza máxima hacia arriba
    //         if (playerRb.velocity.y > 0 && playerRb.velocity.y > flyForce)
    //         {
    //             playerRb.velocity = new Vector3(playerRb.velocity.x, flyForce, playerRb.velocity.z);
    //         }
    //     }
    //
    //     if (Input.GetKeyUp(KeyCode.Space) || cornHarvested <= 0)
    //     {
    //         playerRb.AddForce(Vector3.down * flyForce);
    //         playerRb.useGravity = true;
    //     }
    // }
}