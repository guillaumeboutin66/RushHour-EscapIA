//-----------------------------------------------------------------------
// <copyright file="ObjectController.cs" company="Google Inc.">
// Copyright 2014 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

namespace GoogleVR.HelloVR
{
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    /// <summary>Controls interactable teleporting objects in the Demo scene.</summary>
    [RequireComponent(typeof(Collider))]
    public class ObjectController : MonoBehaviour
    {
        /// <summary>
        /// The material to use when this object is inactive (not being gazed at).
        /// </summary>
        public Material inactiveMaterial;

        /// <summary>The material to use when this object is active (gazed at).</summary>
        public Material gazedAtMaterial;

        private Vector3 startingPosition;
        private Renderer myRenderer;

        public Text countText;
        public Text winText;
        private int count;


        public GameObject door1;
        public GameObject door2;
        public GameObject door3;
        public GameObject door4;


        /// <summary>Sets this instance's GazedAt state.</summary>
        /// <param name="gazedAt">
        /// Value `true` if this object is being gazed at, `false` otherwise.
        /// </param>
        public void SetGazedAt(bool gazedAt)
        {
            if (inactiveMaterial != null && gazedAtMaterial != null)
            {
                myRenderer.material = gazedAt ? gazedAtMaterial : inactiveMaterial;
                return;
            }
        }

        // /// <summary>Resets this instance and its siblings to their starting positions.</summary>
        // public void Reset()
        // {
        //     int sibIdx = transform.GetSiblingIndex();
        //     int numSibs = transform.parent.childCount;
        //     for (int i = 0; i < numSibs; i++)
        //     {
        //         GameObject sib = transform.parent.GetChild(i).gameObject;
        //         sib.transform.localPosition = startingPosition;
        //         sib.SetActive(i == sibIdx);
        //     }
        // }

        /// <summary>Calls the Recenter event.</summary>
        public void Recenter()
        {
            #if !UNITY_EDITOR
                GvrCardboardHelpers.Recenter();
            #else
            if (GvrEditorEmulator.Instance != null)
            {
                GvrEditorEmulator.Instance.Recenter();
            }
            #endif  // !UNITY_EDITOR
        }

        /// <summary>Teleport this instance randomly when triggered by a pointer click.</summary>
        /// <param name="eventData">The pointer click event which triggered this call.</param>
        public void IncrementPickUp(BaseEventData eventData)
        {
            // Only trigger on left input button, which maps to
            // Daydream controller TouchPadButton and Trigger buttons.
            PointerEventData ped = eventData as PointerEventData;
            if (ped != null)
            {
                if (ped.button != PointerEventData.InputButton.Left)
                {
                    return;
                }
            }

            int sibIdx = transform.GetSiblingIndex();
            int numSibs = transform.parent.childCount;
            sibIdx = (sibIdx + Random.Range(1, numSibs)) % numSibs;

            keepObject(gameObject);
        }

        public void OpenDoorA(BaseEventData eventData){  
            int tmpValue;
            int.TryParse(countText.text, out tmpValue);

            if(tmpValue >= 10){
                if(door1 != null){
                    keepObject(door1);
                }
            }

            if(tmpValue >= 16){
                if(door2 != null){
                    keepObject(door2);
                }
            }

            if(tmpValue >= 30){
                if(door3 != null && door4 != null){
                    keepObject(door3);
                    keepObject(door4);
                }
            }
        }

        private void keepObject(GameObject gameObject){
            gameObject.SetActive(false);


            SetGazedAt(false);

            int tmpValue;
            int.TryParse(countText.text, out tmpValue);
            count = tmpValue + 1;
            SetCountText ();

            if(gameObject.GetComponent<BoxCollider>() != null){
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
            if(gameObject.GetComponent<MeshCollider>() != null){
                gameObject.GetComponent<MeshCollider>().enabled = false;
            }
        }

        private void Start()
        {
            count = 0;
            SetCountText ();
            winText.text = "";

            startingPosition = transform.localPosition;
            myRenderer = GetComponent<Renderer>();
            SetGazedAt(false);
        }


        private void SetCountText ()
        {
            countText.text = count.ToString ();
            if (count >= 31)
            {
                winText.gameObject.SetActive(true);
                countText.gameObject.SetActive(false);
            }
        }
    }
}
