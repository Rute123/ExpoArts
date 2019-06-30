using System.Collections;
using System.Collections.Generic;
using GoogleARCore.Examples.Common;
using Scripts;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class CreatePortalManager : MonoBehaviour
{

    [SerializeField] private float canonicalWidth = 5f;
    [SerializeField] private float canonicalHeight = 5f;
    [SerializeField] private float width = 5f;

    [SerializeField] private float height = 5f;

    [SerializeField] private GameObject backButton;

    [SerializeField] private GameObject createPortal;

    [SerializeField] private Text hints;

    [SerializeField] private Button saveButton;
    [SerializeField] private Button addImageButton;
    [SerializeField] private Button homeButton;

    [SerializeField] private PointcloudVisualizer Pointcloud;
    [SerializeField] private CreatorArController creatorController;
 

    [SerializeField]
    private GameObject portalPrefab;
    
    // Start is called before the first frame update

    private CreatePortalState state;

    private void OnEnable()
    {
        hints.gameObject.SetActive(true);
        state = CreatePortalState.CreatingEnvironment;
        backButton.SetActive(true);
        createPortal.SetActive(true);
        homeButton.gameObject.SetActive(false);
        creatorController.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        creatorController.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (state == CreatePortalState.CreatingEnvironment)
        {
            hints.text = Mathf.Abs(Input.acceleration.z) < 0.7 ? "Aponte para o dispositivo chão" : "Percorra a área desejada para criar seu portal";
            width = Pointcloud.boudingBox.getSubPlane().x;
            height = Pointcloud.boudingBox.getSubPlane().y;
            return;
        }

        hints.text = "Clique no piso escaneado para criar o portal";
    }
    
    
    public void CreatePortal()
    {
        var portal = portalPrefab.GetComponentsInChildren<Transform>()[1];
        var localScale = portal.localScale;
        localScale = new Vector3(localScale.x*(width/canonicalWidth),
            localScale.y, localScale.z*(height/canonicalHeight));
        portal.localScale = localScale;

        var localPosition = portal.localPosition;
        localPosition = new Vector3(localPosition.x*(width/canonicalWidth),localPosition.y,
            localPosition.z*(height/canonicalHeight));
        portal.localPosition = localPosition;
    }

    public void FinalizeCreation()
    {
        state = CreatePortalState.AddingImages;
        createPortal.SetActive(false);
        homeButton.gameObject.SetActive(false);
        creatorController.gameObject.SetActive(true);
    }
    
}


internal enum CreatePortalState
{
    CreatingEnvironment = 0,
    AddingImages = 1
}
