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
    
    // Start is called before the first frame update
    private void Awake()
    {
        logo.SetActive(true);
        Invoke(nameof(HideLogo), 3);
        EnableMainMenu();
    }

    private void HideLogo()
    {
        logo.SetActive(false);
    }

    public void EnableMainMenu()
    {
        mainMenu.SetActive(true);
        creatorMenu.SetActive(false);
        creatorController.SetActive(false);
        visualizerHud.SetActive(false);
        visualizerController.SetActive(false);
    }

    public void EnableVisualizationMode()
    {
        creatorController.SetActive(false);
        visualizerHud.SetActive(true);
        mainMenu.SetActive(false);
        visualizerController.SetActive(true);
    }

    public void EnableCreateMode()
    {
        creatorController.SetActive(true);
        mainMenu.SetActive(false);
        creatorMenu.SetActive(true);
        visualizerController.SetActive(false);
    }

}
