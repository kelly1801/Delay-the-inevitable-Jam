using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BackButton : UIManager
{


    private UIDocument uiDocument;
    private Button backButton;
    [SerializeField] GameObject panelToGoBack;
    [SerializeField] string buttonId = "BackButton";
    private GameObject thisPanel;
    [SerializeField] bool isOpen;




    void OnEnable()
    {
        // Initialize the Button variable and add a click event listener
        thisPanel = gameObject;
        uiDocument = GetComponent<UIDocument>(); 
        backButton = uiDocument.rootVisualElement.Q<Button>(buttonId);
        backButton.clicked += () => TogglePanel(panelToGoBack, thisPanel, isOpen);

    }

    void OnDisable()
    {
        // Remove event listeners
        backButton.clicked -= () => TogglePanel(panelToGoBack, thisPanel, isOpen);

    }
}
