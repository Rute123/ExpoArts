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
    [SerializeField] private GameObject planeDiscovery;
    [SerializeField] private GameObject visualizerHud;
    
    // Start is called before the first frame update
    private void Start()
    {
        logo.SetActive(true);
        Invoke(nameof(HideLogo), 3);
        mainMenu.SetActive(true);
        creatorMenu.SetActive(false);
        planeDiscovery.SetActive(false);
        visualizerHud.SetActive(false);
    }

    private void HideLogo()
    {
        logo.SetActive(false);
    }

    public void EnableVisualizationMode()
    {
        planeDiscovery.SetActive(true);
        visualizerHud.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void EnableCreateMode()
    {
        planeDiscovery.SetActive(true);
        mainMenu.SetActive(false);
        creatorMenu.SetActive(true);
    }

}
