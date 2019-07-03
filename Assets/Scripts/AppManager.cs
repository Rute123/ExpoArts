using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AppManager : MonoBehaviour
{

    [SerializeField] private GameObject logo;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject creatorMenu;
    [SerializeField] private GameObject creatorController;
    [SerializeField] private GameObject visualizerController;
    [SerializeField] private GameObject visualizerHud;
    [SerializeField] private GameObject listPortalPanel;
    
    // Start is called before the first frame update
    private void Awake()
    {
        logo.SetActive(true);
        mainMenu.SetActive(false);
        creatorMenu.SetActive(false);
        creatorController.SetActive(false);
        visualizerHud.SetActive(false);
        visualizerController.SetActive(false);
        listPortalPanel.SetActive(false);
        Invoke(nameof(EnableMainMenu), 3);
    }

    public void EnableMainMenu()
    {
        logo.SetActive(false);
        mainMenu.SetActive(true);
        creatorMenu.SetActive(false);
        creatorController.SetActive(false);
        visualizerHud.SetActive(false);
        visualizerController.SetActive(false);
        listPortalPanel.SetActive(false);
    }

    public void EnableVisualizationMode()
    {
        creatorController.SetActive(false);
        visualizerHud.SetActive(true);
        mainMenu.SetActive(false);
        visualizerController.SetActive(true);
        listPortalPanel.SetActive(true);
    }

    public void EnableCreateMode()
    {
        creatorController.SetActive(true);
        mainMenu.SetActive(false);
        creatorMenu.SetActive(true);
        visualizerController.SetActive(false);
        listPortalPanel.SetActive(false);
    }

}
