using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InGame : UIManager
{
    private Button pauseButton;
    private UIDocument uiDocument;
    private const string BUTTON_ID = "PauseButton";
    [SerializeField] GameObject pausePanelToSet;
    private GameObject thisPanel;


    void OnEnable()
    {
        // Initialize the Button variable and add a click event listener
        thisPanel = gameObject;
        uiDocument = GetComponent<UIDocument>();
        SetPausePanel(pausePanelToSet);
        pauseButton = uiDocument.rootVisualElement.Q<Button>(BUTTON_ID);
        pauseButton.clicked += () => PauseGame(thisPanel, pausePanel);
        
    }


    void OnDisable()
    {
        // Remove event listeners
        pauseButton.clicked -= () => PauseGame(thisPanel, pausePanel);

    }
}
