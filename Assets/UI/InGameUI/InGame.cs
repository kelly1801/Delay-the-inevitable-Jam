using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InGame : UIManager
{
    
    [SerializeField] GameObject pausePanelToSet;
    private GameObject thisPanel;

    void OnEnable()
   {
       // Initialize the Button variable and add a click event listener
       thisPanel = gameObject;
       SetPausePanel(pausePanelToSet);
       
   }

   private void Update()
   {
       if (Input.GetKeyDown(KeyCode.Escape))
       {
           PauseGame(thisPanel, pausePanel);
       }
   }

}
