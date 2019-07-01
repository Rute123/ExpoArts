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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public class VisualizerArController : MonoBehaviour
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
        /// Portal selected data
        /// </summary>
        public Portal selectedPortal;

        /// <summary>
        /// The rotation in degrees need to apply to model when the Andy model is placed.
        /// </summary>
        private const float k_ModelRotation = 0.0f;

        private bool _portalCreated = false;
        [SerializeField] private GameObject clickOnFloorTutorial;
        
        public void SetPrefab(GameObject prefab)
        {
            SpawnPrefab = prefab;
        }

        private ArCoreLifeCycleManager _arCoreLifeCycleManager;

        [SerializeField] private GameObject planeDiscovery;
        [SerializeField] private RawImage pointDownTutorial;
        [SerializeField] private GameObject moreInfo;
        [SerializeField] private GameObject emptySprite;


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
        }

        public void Update()
        {
            
            _arCoreLifeCycleManager.UpdateApplicationLifecycle();
            clickOnFloorTutorial.SetActive(!_portalCreated && !moreInfo.activeSelf && !pointDownTutorial.IsActive());
            if (_portalCreated) return;

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
            const TrackableHitFlags rayCastFilter = TrackableHitFlags.PlaneWithinPolygon |
                                                    TrackableHitFlags.FeaturePointWithSurfaceNormal;

            if (!Frame.Raycast(touch.position.x, touch.position.y, rayCastFilter, out var hit)) return;
            // Use hit pose and camera pose to check if hittest is from the
            // back of the plane, if it is, no need to create the anchor.
            if (hit.Trackable is DetectedPlane &&
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
                var portal = Instantiate(prefab, hit.Pose.position, hit.Pose.rotation);

                // Compensate for the hitPose rotation facing away from the raycast (i.e.
                // camera).
                portal.transform.Rotate(0, k_ModelRotation, 0, Space.Self);

                // Create an anchor to allow ARCore to track the hitpoint as understanding of
                // the physical world evolves.
                var anchor = hit.Trackable.CreateAnchor(hit.Pose);

                // Make Andy model a child of the anchor.
                portal.transform.parent = anchor.transform;
                _portalCreated = true;
                StartCoroutine(PlaceArts(portal, selectedPortal));
            }
        }

        public IEnumerator PlaceArts(GameObject root, Portal portal)
        {
            var expoTransform = root.GetComponentsInChildren<Transform>()[1];
            foreach (var art in portal.arts)
            {
                var emptySpriteInstance = Instantiate(emptySprite, expoTransform);
                emptySpriteInstance.transform.localPosition = art.GetPosition();
                emptySpriteInstance.transform.localRotation = art.GetRotation();
                
                var spriteRenderer = emptySpriteInstance.GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = art.Sprite;
                yield return 0;
            }
        }
    }
}
