using UnityEngine;
using UnityEngine.UIElements;

public class SceneChanger : UIManager
{
    private UIDocument uiDocument;
    private Button Button;
    [SerializeField] string buttonId = "RestartButton";
    [SerializeField] string sceneName;

    void OnEnable()
    {
        // Initialize the Button variable and add a click event listener
        uiDocument = GetComponent<UIDocument>();
        Button = uiDocument.rootVisualElement.Q<Button>(buttonId);
        Button.clicked += () => LoadSceneByName(sceneName);

    }

    void OnDisable()
    {
        // Remove event listeners
        Button.clicked -= () => LoadSceneByName(sceneName);

    }
}
