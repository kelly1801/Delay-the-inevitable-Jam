using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] UIManager uIManager;
    [SerializeField] UIDocument UIDocument;
    private NavMeshAgent agent;
    [SerializeField] float closeDistance = 10f; // Define the distance when the color changes
    [SerializeField] GameObject GameOverScreen;
    [SerializeField] GameObject[] screens;
    private AudioManager audioManager;
    private bool hasPlayedFirstSound = false;
    private void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        agent = GetComponent<NavMeshAgent>();
        uIManager.SetGameOverPanel(GameOverScreen);
    }
    
    void Update()
    {

        if (!uIManager.isGameOver)
        {
            FollowPlayer();
        }
     
    }

    private void FollowPlayer()
    {
        agent.destination = player.position;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        VisualElement rootElement = UIDocument.rootVisualElement;


        // Add the "danger" class to the root element

        if (distanceToPlayer < closeDistance && !hasPlayedFirstSound)
        {

            rootElement.AddToClassList("danger");
            // add danger music
            audioManager.PlaySound(2, 0.3f);
            hasPlayedFirstSound = true;
            audioManager.PlaySound(3, 0.5f);
        }
        else if (distanceToPlayer >= closeDistance)
        {
            rootElement.RemoveFromClassList("danger");
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            uIManager.GameOver(screens);
        }
    }
   
}

