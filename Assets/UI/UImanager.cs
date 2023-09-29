using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public bool isGamePaused;
    protected GameObject pausePanel;
    [SerializeField] GameObject gameOverPanel;
    
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


    public void SetPausePanel(GameObject newPausePanel)
    {
        pausePanel = newPausePanel;
    }
    public void PauseGame(GameObject inGamePanel, GameObject pausePanel)
    {

        TogglePanel(pausePanel, inGamePanel, false);
        Time.timeScale = 0.0f; // Pause the game
        isGamePaused = true;
    }

    public void ResumeGame(GameObject pausePanel)
    {

        Time.timeScale = 1.0f; // Resume the game
        isGamePaused = false;
        pausePanel.SetActive(false);
    }

    public void GameOver(GameObject[] panelsToDeactivate)
    {
        Time.timeScale = 0.0f;
        gameOverPanel.SetActive(true);
        // deactivate panels

        foreach (GameObject panel in panelsToDeactivate)
        {
            panel.SetActive(false);
        }
    }

}
