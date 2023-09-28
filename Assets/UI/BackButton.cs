using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BackButton : MonoBehaviour
{


    private UIManager uiManager;
    private UIDocument uiDocument;
    private Button backButton;
    [SerializeField] GameObject panelToGoBack;
    private GameObject thisPanel;



    void OnEnable()
    {
        // Initialize the Button variable and add a click event listener
        thisPanel = gameObject;
        uiManager = GetComponent<UIManager>();
        uiDocument = GetComponent<UIDocument>(); 
        backButton = uiDocument.rootVisualElement.Q<Button>("BackButton");
        backButton.clicked += () => uiManager.TogglePanel(panelToGoBack, thisPanel, false);

    }

    void OnDisable()
    {
        // Remove event listeners
        backButton.clicked -= () => uiManager.TogglePanel(panelToGoBack, thisPanel, false);

    }
}
