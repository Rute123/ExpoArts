using Scripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageSelector : MonoBehaviour, IPointerClickHandler
{
    public CreatorArController controller;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        controller.SetImage(GetComponent<Image>());
    }
}
