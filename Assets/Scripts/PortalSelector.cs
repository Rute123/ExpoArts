using System.Collections;
using System.Collections.Generic;
using Scripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PortalSelector : MonoBehaviour, IPointerClickHandler
{
    public Text PortalName { get; set; }
    public PortalListController PortalListController;
    public GameObject portalPrefab;
    public Portal portalData;

    public void OnPointerClick(PointerEventData eventData)
    {
        Select();
    }

    public void Select()
    {
        PortalName.text = name;
        PortalListController.SelectedPortal = this;
        Debug.Log(PortalListController.SelectedPortal);
        PortalListController.SelectPortal();
    }
}
