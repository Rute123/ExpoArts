//-----------------------------------------------------------------------
// <copyright file="HelloARController.cs" company="Google">
//
// Copyright 2017 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------


using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using UnityEngine.UI;

namespace Scripts
{
    using GoogleARCore;
    using UnityEngine;
    using UnityEngine.EventSystems;
#if UNITY_EDITOR
    // Set up touch input propagation while using Instant Preview in the editor.
    using Input = GoogleARCore.InstantPreviewInput;

#endif

    /// <summary>
    /// Controls the HelloAR example.
    /// </summary>
    [RequireComponent(typeof(ArCoreLifeCycleManager))]
    public class CreatorArController : MonoBehaviour
    {
        /// <summary>
        /// The first-person camera being used to render the passthrough camera image (i.e. AR
        /// background).
        /// </summary>
        public Camera FirstPersonCamera;

        /// <summary>
        /// A prefab for tracking and visualizing detected planes.
        /// </summary>
        public GameObject DetectedPlanePrefab;

        /// <summary>
        /// A model to place when a raycast from a user touch hits a plane.
        /// </summary>
        public GameObject SpawnPrefab;

        /// <summary>
        /// The rotation in degrees need to apply to model when the Andy model is placed.
        /// </summary>
        private const float k_ModelRotation = 0.0f;

        private bool _portalCreated = false;

        [SerializeField] private GameObject preview;

        private GameObject _previewInstance;
        private GameObject _portalInstance;

        [SerializeField]
        private CreatePortalManager createPortalManager;
        
        public void SetPrefab(GameObject prefab)
        {
            SpawnPrefab = prefab;
        }

        private ArCoreLifeCycleManager _arCoreLifeCycleManager;

        [SerializeField] private GameObject planeDiscovery;


        private void Awake()
        {
            _arCoreLifeCycleManager = GetComponent<ArCoreLifeCycleManager>();
        }

        private void OnEnable()
        {
            planeDiscovery.SetActive(true);
        }

        private void OnDisable()
        {
            planeDiscovery.SetActive(false);
            Destroy(_previewInstance);
            Destroy(_portalInstance);
        }

        public void Update()
        {
            _arCoreLifeCycleManager.UpdateApplicationLifecycle();

            if (_portalCreated)
            {
                if (_previewInstance == null) return;
                PreviewArtPosition();
                return;
            }

            // If the player has not touched the screen, we are done with this update.
            Touch touch;
            if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
            {
                return;
            }

            // Should not handle input if the player is pointing on UI.
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                return;
            }

            // Raycast against the location the player touched to search for planes.
            TrackableHit hit;
            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
                                              TrackableHitFlags.FeaturePointWithSurfaceNormal;

            if (!_portalCreated && Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
            {
                // Use hit pose and camera pose to check if hittest is from the
                // back of the plane, if it is, no need to create the anchor.
                if ((hit.Trackable is DetectedPlane) &&
                    Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position,
                        hit.Pose.rotation * Vector3.up) < 0)
                {
                    Debug.Log("Hit at back of the current DetectedPlane");
                }
                else
                {
                    // Choose the Andy model for the Trackable that got hit.
                    var prefab = SpawnPrefab;
                    if (hit.Trackable is FeaturePoint)
                        return;

                    // Instantiate Andy model at the hit pose.
                    _portalInstance = Instantiate(prefab, hit.Pose.position, hit.Pose.rotation);

                    // Compensate for the hitPose rotation facing away from the raycast (i.e.
                    // camera).
                    _portalInstance.transform.Rotate(0, k_ModelRotation, 0, Space.Self);

                    // Create an anchor to allow ARCore to track the hitpoint as understanding of
                    // the physical world evolves.
                    var anchor = hit.Trackable.CreateAnchor(hit.Pose);

                    // Make Andy model a child of the anchor.
                    _portalInstance.transform.parent = anchor.transform;
                    _portalCreated = true;
                }
            }
        }

        private void PreviewArtPosition()
        {
            var firstCamera = FirstPersonCamera.transform;
            var ray = new Ray(firstCamera.position, firstCamera.forward);
            if (!Physics.Raycast(ray, out var rayCastHit, 500)) return;
            _previewInstance.SetActive(true);
            var parentTransform = rayCastHit.collider.transform;
            _previewInstance.transform.forward = rayCastHit.normal;
            _previewInstance.transform.position = firstCamera.position + (rayCastHit.point - firstCamera.position)*0.999f;
//            _instance.transform.parent = parentTransform;
        }

        public void SetImage(Image image)
        {
            // Create Texture from selected image
            if (_previewInstance != null)
            {
                
//                FindObjectOfType<GameDataManager>().AddImage(_previewInstance.transform.localScale,
//                    _previewInstance.GetComponent<SpriteRenderer>().sprite);
            }

            if (_previewInstance == null)
                _previewInstance = Instantiate(preview);
            
            var spriteRenderer = _previewInstance.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = image.sprite;
            _previewInstance.transform.localScale = Vector3.one / 25f;
        }

        public void AttachImage()
        {
            _previewInstance.transform.parent = _portalInstance.GetComponentsInChildren<Transform>()[1];
            createPortalManager.AddImage(_previewInstance.GetComponent<SpriteRenderer>().sprite,
                _previewInstance);
            _previewInstance = Instantiate(preview);
        }
        
    }
}