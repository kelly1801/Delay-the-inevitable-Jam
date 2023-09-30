using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject[] popUps;
    [SerializeField] private GameObject enemy;
    [SerializeField] private float waitTime = 15f; // Initial wait time for the first two popups

    private int popUpIndex = 0;

    private bool waiting = true;

    private void Update()
    {
        if (waiting)
        {
            HandleWaitTime();
        }
        else
        {
            UpdatePopUps();

            switch (popUpIndex)
            {
                case 0:
                    HandleFirstPopUp();
                    break;
                case 1:
                    HandleSecondPopUp();
                    break;
                case 2:
                    HandleThirdPopUp();
                    break;
            }
        }
    }

    private void HandleWaitTime()
    {
        waitTime -= Time.deltaTime;
        if (waitTime <= 0)
        {
            if (popUpIndex < 2) // Check if it's the third popup
            {
                waiting = false;
                waitTime = 5f; // Reset wait time for the first two popups
            }
            else
            {
                waitTime = 10f; // Set wait time to 10 seconds for the third popup
                waiting = false;
            }
        }
    }

    private void HandleFirstPopUp()
    {
        if (VerticalArrowKeyPressed() || VerticalKeyboardKeyPressed() || HorizontalArrowKeyPressed() || HorizontalKeyboardKeyPressed())
        {
            popUpIndex++;
        }
    }

    private void HandleSecondPopUp()
    {
        if (SpaceKeyboardKeyPressed())
        {
            popUpIndex++;
        }
    }

    private void HandleThirdPopUp()
    {
        if (waitTime <= 0)
        {
            enemy.SetActive(true);
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }

    private void UpdatePopUps()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            popUps[i].SetActive(i == popUpIndex);
        }
    }

    private bool HorizontalArrowKeyPressed()
    {
        return Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow);
    }
    private bool VerticalArrowKeyPressed()
    {
        return Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow);
    }

    private bool HorizontalKeyboardKeyPressed()
    {
        return Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D);
    }
    private bool VerticalKeyboardKeyPressed()
    {
        return Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S);
    }

    private bool SpaceKeyboardKeyPressed()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}

