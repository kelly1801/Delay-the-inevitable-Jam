using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InitialMenu : UIManager
{
    [SerializeField] string sceneName;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject tutorialPanel;
    [SerializeField] GameObject initialPanel;
    [SerializeField] UIDocument uiDocument;
    private Button startButton;
    private Button tutorialButton;
    private Button settingsButton;
    [SerializeField] AudioSource effectsSource;
    [SerializeField] private AudioClip hoverSound;

    void OnEnable()
    {
        // Initialize the Button variable and add a click event listener
        startButton = uiDocument.rootVisualElement.Q<Button>("StartButton");
        tutorialButton = uiDocument.rootVisualElement.Q<Button>("TutorialButton");
        settingsButton = uiDocument.rootVisualElement.Q<Button>("SettingsButton");

        // Agregar un listener para el evento PointerEnter
        startButton.RegisterCallback<PointerEnterEvent>(OnButtonHover);
        tutorialButton.RegisterCallback<PointerEnterEvent>(OnButtonHover);
        settingsButton.RegisterCallback<PointerEnterEvent>(OnButtonHover);

        startButton.clicked += () => LoadSceneByName(sceneName);
        settingsButton.clicked += () => TogglePanel(settingsPanel, initialPanel, false);
        tutorialButton.clicked += () => TogglePanel(tutorialPanel, initialPanel, false);

        Vector3 initialPosition = startButton.transform.scale;
        Debug.Log(initialPosition);
    }

    void OnDisable()
    {
        // Remove click event listeners
        startButton.clicked -= () => LoadSceneByName(sceneName);
        settingsButton.clicked -= () => TogglePanel(settingsPanel, initialPanel, false);
        tutorialButton.clicked -= () => TogglePanel(tutorialPanel, initialPanel, false);

        // Eliminar los listeners de PointerEnter
        startButton.UnregisterCallback<PointerEnterEvent>(OnButtonHover);
        tutorialButton.UnregisterCallback<PointerEnterEvent>(OnButtonHover);
        settingsButton.UnregisterCallback<PointerEnterEvent>(OnButtonHover);
    }

    void OnButtonHover(PointerEnterEvent evt)
    {
        
        

        if (hoverSound != null)
        {
            // Play hover sound
          effectsSource.PlayOneShot(hoverSound);
        }
    }
}
