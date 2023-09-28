using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InitialMenu : MonoBehaviour
{

    [SerializeField] UIManager uiManager;
    [SerializeField] string sceneName;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject tutorialPanel;
    [SerializeField] GameObject initialPanel;
    [SerializeField] UIDocument uiDocument;
    private Button startButton;
    private Button tutorialButton;
    private Button settingsButton;

    void OnEnable()
    {
        // Initialize the Button variable and add a click event listener
       startButton = uiDocument.rootVisualElement.Q<Button>("StartButton");
       tutorialButton = uiDocument.rootVisualElement.Q<Button>("TutorialButton");
       settingsButton = uiDocument.rootVisualElement.Q<Button>("SettingsButton");


        startButton.clicked += () => uiManager.LoadSceneByName(sceneName);
        settingsButton.clicked += () => uiManager.TogglePanel(settingsPanel, initialPanel, false);
        tutorialButton.clicked += () => uiManager.TogglePanel(tutorialPanel, initialPanel, false);

    }


    void OnDisable()
    {
        // Remove click event listeners
        startButton.clicked -= () => uiManager.LoadSceneByName(sceneName);
        settingsButton.clicked -= () => uiManager.TogglePanel(settingsPanel, initialPanel, false);
        tutorialButton.clicked -= () => uiManager.TogglePanel(tutorialPanel, initialPanel, false);
    }

}
