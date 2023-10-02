using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InGame : UIManager
{
    
    [SerializeField] private GameObject pausePanelToSet;
    [SerializeField] private GameObject playerGameObject;
    private GameObject thisPanel;
    private PlayerController playerController;
    private float cornHarvested;
    private ProgressBar cornsHarvestedBar;

    private void Start()
    {
        // Select the component in the playerGO 
        playerController = playerGameObject.GetComponent<PlayerController>();
        // Search the Progress Bar that registry the number of corn harvested
        VisualElement screen = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Screen");
        VisualElement section = screen.Q<VisualElement>("Section");
        VisualElement harvestBar = section.Q<VisualElement>("HarvestBar");
        cornsHarvestedBar = harvestBar.Q<ProgressBar>("CornsHarvestedBar");
    }

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
       cornHarvested = playerController.cornHarvested;
       cornsHarvestedBar.value = cornHarvested;
   }

}
