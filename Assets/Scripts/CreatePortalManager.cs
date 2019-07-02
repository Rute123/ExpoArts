using System;
using System.Collections.Generic;
using GoogleARCore.Examples.Common;
using Scripts;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] private float maxHeightImageSelector = 250f;

    private List<Texture2D> textures;

    [SerializeField] private GameObject portalPrefab;

    // Start is called before the first frame update

    private CreatePortalState state;

    [SerializeField] private GameObject imagesContainer;
    [SerializeField] private GameObject imageSelectorPrefab;

    [SerializeField] private GameDataManager gameDataManager;

    public Portal TempPortal { get; private set; }

    private void OnEnable()
    {
        hints.gameObject.SetActive(true);
        state = CreatePortalState.CreatingEnvironment;
        backButton.SetActive(true);
        createPortal.SetActive(true);
        homeButton.gameObject.SetActive(false);
        creatorController.gameObject.SetActive(false);
        textures = new List<Texture2D>();
        saveButton.gameObject.SetActive(false);
        addImageButton.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        creatorController.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (state == CreatePortalState.CreatingEnvironment)
        {
            hints.text = Mathf.Abs(Input.acceleration.z) < 0.7
                ? "Aponte para o dispositivo chão"
                : "Percorra a área desejada para criar seu portal";
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
        localScale = new Vector3(localScale.x * (width / canonicalWidth),
            localScale.y, localScale.z * (height / canonicalHeight));
        portal.localScale = localScale;

        var localPosition = portal.localPosition;
        localPosition = new Vector3(localPosition.x * (width / canonicalWidth), localPosition.y,
            localPosition.z * (height / canonicalHeight));
        portal.localPosition = localPosition;

        TempPortal = new Portal(width, height);
    }

    public void FinalizeCreation()
    {
        state = CreatePortalState.AddingImages;
        createPortal.SetActive(false);
        homeButton.gameObject.SetActive(true);
        creatorController.gameObject.SetActive(true);
        saveButton.gameObject.SetActive(true);
        addImageButton.gameObject.SetActive(true);
        PickImages();
    }

    private void PickImages()
    {
        NativeGallery.Permission permission = NativeGallery.GetImagesFromGallery((paths) =>
        {
//    Debug.Log( "Image path: " + path );
            foreach (var path in paths)
            {
                // Create Texture from selected image
                var texture = NativeGallery.LoadImageAtPath(path, -1, false);
                
                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }

                textures.Add(texture);
                var imageSelector = Instantiate(imageSelectorPrefab, imagesContainer.transform);
                var createdImage = imageSelector.GetComponent<Image>();
                createdImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
                    new Vector2(0.5f, 0.5f));
                createdImage.rectTransform.sizeDelta = new Vector2(
                    (float) texture.width / texture.height * maxHeightImageSelector,
                    maxHeightImageSelector);
                imageSelector.GetComponent<ImageSelector>().controller = creatorController;
            }
        }, "Selecione suas obras de arte", "image/png");
        Debug.Log("Permission result: " + permission);
    }

    public void AddImage(Sprite sprite, GameObject spriteGameObject)
    {
        TempPortal.arts.Add(new Picture(spriteGameObject.transform.localPosition,
            spriteGameObject.transform.localRotation, spriteGameObject.transform.localScale,
            sprite));
    }

    public void SavePortal()
    {
        gameDataManager.Save(TempPortal);
    }
}


internal enum CreatePortalState
{
    CreatingEnvironment = 0,
    AddingImages = 1
}