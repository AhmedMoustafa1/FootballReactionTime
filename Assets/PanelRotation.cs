
using Kandooz;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

namespace Assets
{
    public class PanelRotation : MonoBehaviour
    {

        private SteamVR_TrackedController trackedController;
        private SteamVR_TrackedObject trackedObject;
        public bool on = false;
        public IntField goalKeepr;


        public GameObject coolbox;
        public VRButtonSelector rightHand;
        public VRButtonSelector leftHand;
        public Transform Test;
        public Text message;


        public BoolField playerPosition;
        public BoolField panelPosition;
        public BoolField panelRotation;

        public BoolField panelHeight;
        public Transform[] points;

        private int currentRotation=1;
        private bool canFlip=true;
        void Start()
        {

            trackedObject = GetComponent<SteamVR_TrackedObject>();
            trackedController = gameObject.AddComponent<SteamVR_TrackedController>();
            trackedController.controllerIndex = (uint)trackedObject.index;
            panelHeight.Value = false;
            playerPosition.Value = false;
            panelPosition.Value = false;
            panelRotation.Value = false;
        }

        public void FixedUpdate()
        {
        }
        // Update is called once per frame
        void Update()
        {
            var device = SteamVR_Controller.Input((int)trackedObject.index);


            if (device.GetPressDown(EVRButtonId.k_EButton_ApplicationMenu) && goalKeepr.Value==5)

            {
                panelRotation.Value = !panelRotation.Value;

                coolbox.SetActive(panelRotation.Value);
                message.text = "Set Panel Rotation";
                rightHand.enabled = !panelRotation.Value;
                leftHand.enabled = !panelRotation.Value;

                panelPosition.Value = false;
                panelHeight.Value = false;
                playerPosition.Value = false;

            }

            if (panelRotation.Value)
            {
                // here wo do the left and right flip;

                if (canFlip&&device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).x > 0.5f)
                {
                    Debug.Log("yemeeeen aho");
                    canFlip = false;
                    RotLeft();

                }
                if (canFlip&&device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).x < -0.5f)
                {

                    Debug.Log("Shemaaaaaaaaaal Aho");
                    canFlip = false;
                    RotRight();

                }
                if (!canFlip && device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).x < 0.5f && device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).x > -0.5f)
                {
                    canFlip = true;
                }
              



            }


        }

        public void RotLeft()
        {
            if (currentRotation == 0)
            {
                currentRotation = 0;
            }
            else
            {
                currentRotation--;
            }
            Test.transform.position = points[currentRotation].position;
            Test.transform.rotation = points[currentRotation].rotation;

        }
        public void RotRight()
        {
            if (currentRotation == 2)
            {
                currentRotation = 2;
            }
            else
            {
                currentRotation++;
            }
            Test.transform.position = points[currentRotation].position;
            Test.transform.rotation = points[currentRotation].rotation;
        }
    }
}