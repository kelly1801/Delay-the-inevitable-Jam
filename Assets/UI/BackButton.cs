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
    [SerializeField] private AudioClip hoverSound; // Variable para el sonido de hover
    [SerializeField] AudioSource audioSource;
    void OnEnable()
    {
        // Initialize the Button variable and add a click event listener
        thisPanel = gameObject;
        uiDocument = GetComponent<UIDocument>();
        backButton = uiDocument.rootVisualElement.Q<Button>(buttonId);
       
        // Agregar un listener para el evento PointerEnter
        backButton.RegisterCallback<PointerEnterEvent>(OnButtonHover);
        backButton.clicked += () => TogglePanel(panelToGoBack, thisPanel, isOpen);
    }

    void OnDisable()
    {
        // Remove event listeners
        backButton.clicked -= () => TogglePanel(panelToGoBack, thisPanel, isOpen);

        // Eliminar el listener de PointerEnter
        backButton.UnregisterCallback<PointerEnterEvent>(OnButtonHover);
    }

    void OnButtonHover(PointerEnterEvent evt)
    {
        // Verificar si se ha asignado un sonido de hover
        if (hoverSound != null)
        {
            // Reproducir el sonido de hover en la posición del botón
            audioSource.PlayOneShot(hoverSound);
        }
    }
}
