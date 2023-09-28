using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] UIManager uiManager;
    [SerializeField] UIDocument uiDocument;
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject initialPanel;
    [SerializeField] GameObject creditsPanel;
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
        creditsButton.clicked += () => uiManager.TogglePanel(creditsPanel, initialPanel, false);
        exitButton.clicked += CloseGame;

    }

    private void ResumeEvent()
    {

        gameObject.SetActive(false);
        uiManager.ResumeGame();
    }

    private void CloseGame()
    {

        gameObject.SetActive(false);
        uiManager.LoadSceneByName(START_SCENE_NAME);
    }



}
