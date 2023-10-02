using UnityEngine;
using UnityEngine.UIElements;

public class PauseScreen : UIManager
{
    
    [SerializeField] UIDocument uiDocument;
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject initialPanel;
    [SerializeField] GameObject creditsPanel;
    [SerializeField] GameObject inGamePanel;
    [SerializeField] const string START_SCENE_NAME = "InitialMenu";
    private Button resumeButton;
    private Button creditsButton;
    private Button exitButton;

    void OnEnable()
    {
        // Initialize the Button variable and add a click event listener

        resumeButton = uiDocument.rootVisualElement.Q<Button>("ResumeButton");
        creditsButton = uiDocument.rootVisualElement.Q<Button>("CreditsButton");
        exitButton = uiDocument.rootVisualElement.Q<Button>("ExitButton");

        resumeButton.clicked += ResumeEvent;
        creditsButton.clicked += () => TogglePanel(creditsPanel, initialPanel, false);
        exitButton.clicked += CloseGame;

    }

    private void ResumeEvent()
    {

       
        ResumeGame(gameObject);
        inGamePanel.SetActive(true);

    }

    private void CloseGame()
    {
        ResumeGame(gameObject);
        gameObject.SetActive(false);
        LoadSceneByName(START_SCENE_NAME);
    }



}
