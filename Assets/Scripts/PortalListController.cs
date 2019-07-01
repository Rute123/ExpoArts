using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scripts;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PortalListController : MonoBehaviour
{
    [SerializeField] private GameDataManager gameDataManager;
    private GameData gameData;

    [SerializeField] private GameObject modelExpoPrefab;
    [SerializeField] private GameObject emptyExpoPrefab;
   
    [SerializeField] private RectTransform portalSelectorParent;

    [SerializeField] private PortalSelector portalSelectorPrefab;

    [SerializeField] private Text selectedPortalNameText;

    [SerializeField] private Button portalSelectorButton;

    public PortalSelector SelectedPortal;

    [SerializeField] private float baseWidth = 5f;
    [SerializeField] private float baseHeight = 5f;

    private VisualizerArController visualizer;

    [SerializeField] private Text debugText;

    private int qtdPortals;
    // Start is called before the first frame update
    private void OnEnable()
    {
        debugText.text = gameDataManager.gameData.portals.Count.ToString();
        gameData = gameDataManager.gameData;
        LoadPortals();
        qtdPortals = gameData.portals.Count+1;
        visualizer = GetComponent<VisualizerArController>();
        Debug.Log(JsonUtility.ToJson(gameDataManager.gameData));
    }

    private void LoadPortals()
    {
        var portals = GetPortalList();
        portals.First().Select();

        var localPosition = portalSelectorParent.localPosition;
        localPosition = new Vector3(
            qtdPortals * 0.5f * portalSelectorPrefab.gameObject.GetComponent<RectTransform>().rect.width, 
            localPosition.y,
            localPosition.z);
        portalSelectorParent.localPosition = localPosition;
    }

    private IEnumerable<PortalSelector> GetPortalList()
    {
        var portals = new List<PortalSelector>();
        var firstInstance = Instantiate(portalSelectorPrefab, portalSelectorParent);
        portals.Add(firstInstance);
        firstInstance.name = "Exposição modelo";
        firstInstance.PortalName = selectedPortalNameText;
        firstInstance.PortalListController = this;
        firstInstance.portalData = new Portal(baseWidth, baseHeight);
        firstInstance.portalPrefab = modelExpoPrefab;
        
        Debug.Log(qtdPortals);
        for (var i = 0; i < qtdPortals; i++)
        {
            var instantiatedObject = Instantiate(portalSelectorPrefab, portalSelectorParent);
            portals.Add(instantiatedObject);
            instantiatedObject.name = $"Portal: {i + 1}";
            instantiatedObject.PortalName = selectedPortalNameText;
            instantiatedObject.PortalListController = this;
            firstInstance.portalData = gameData.portals[i];
            firstInstance.portalPrefab = emptyExpoPrefab;
        }

        debugText.text = $"{portals.Count.ToString()} {qtdPortals}";
        
        return portals;
    }

    public void SelectPortal()
    {
        Debug.Log(JsonUtility.ToJson(SelectedPortal));
        visualizer.SetPrefab(SelectedPortal.portalPrefab);
        visualizer.selectedPortal = SelectedPortal.portalData;
    }

}
