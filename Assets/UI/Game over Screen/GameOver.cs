using UnityEngine;
using UnityEngine.UIElements;

public class GameOver : UIManager
{
    private UIDocument uiDocument;
    private Button backButton;
    [SerializeField] string buttonId = "RestartButton";

    void OnEnable()
    {
        // Initialize the Button variable and add a click event listener
        uiDocument = GetComponent<UIDocument>();
        backButton = uiDocument.rootVisualElement.Q<Button>(buttonId);
        backButton.clicked += () => LoadSceneByName("Level 1");

    }

    void OnDisable()
    {
        // Remove event listeners
        backButton.clicked -= () => LoadSceneByName("Level 1");

    }
}
