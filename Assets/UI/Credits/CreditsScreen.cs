using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CreditsScreen : UIManager
{
    
    [SerializeField] UIDocument uiDocument;
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject initialPanel;
    private Button backButton;


    void OnEnable()
    {
        // Initialize the Button variable and add a click event listener

        backButton = uiDocument.rootVisualElement.Q<Button>("BackButton");

        backButton.clicked += () => TogglePanel(startPanel, initialPanel, false);

    }


    void OnDisable()
    {
        // Remove click event listeners
        backButton.clicked -= () => TogglePanel(startPanel, initialPanel, false);

    }
}
