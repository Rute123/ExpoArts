using System.Collections;
using System.Collections.Generic;
using Scripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PortalSelector : MonoBehaviour, IPointerClickHandler
{
    public Text PortalName { get; set; }
    public Button SelectButton { get; set; }
    public PortalListController PortalListController;

    public void OnPointerClick(PointerEventData eventData)
    {
        Select();
    }

    public void Select()
    {
        PortalName.text = name;
        PortalListController.SelectedPortal = this;
    }
}
