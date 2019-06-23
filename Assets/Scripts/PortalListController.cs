using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PortalListController : MonoBehaviour
{
    [SerializeField] private RectTransform portalSelectorParent;

    [SerializeField] private PortalSelector portalSelectorPrefab;

    [SerializeField] private int qtdPortals;

    [SerializeField] private Text selectedPortalNameText;

    [SerializeField] private Button portalSelectorButton;

    public PortalSelector SelectedPortal;
    // Start is called before the first frame update
    private void Start()
    {
        LoadPortals();
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
        for (var i = 0; i < qtdPortals; i++)
        {
            var instantiatedObject = Instantiate(portalSelectorPrefab, portalSelectorParent);
            portals.Add(instantiatedObject);
            instantiatedObject.name = $"Portal do CAC de número: {i + 1}";
            instantiatedObject.PortalName = selectedPortalNameText;
            instantiatedObject.SelectButton = portalSelectorButton;
        }
        return portals;
    }

    public void SelectPortal()
    {
        //Decidir como fazer para instanciar um portal
        Debug.Log("Portal selecionado");
    }

}
