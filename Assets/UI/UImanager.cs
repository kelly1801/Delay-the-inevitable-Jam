using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public bool isGamePaused;
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
   
  
    public void TogglePanel(GameObject panelToChange, GameObject currentPanel, bool isPanelOpen)
    {
        isPanelOpen = !isPanelOpen; // Toggle the panel's state
        panelToChange.SetActive(isPanelOpen); // Set the panel's visibility based on the state
        currentPanel.SetActive(!isPanelOpen); // Toggle the script's GameObject visibility
    }

    public void PauseGame()
    {
        Time.timeScale = 0.0f; // Pause the game
        isGamePaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f; // Resume the game
        isGamePaused = false;
    }

}
