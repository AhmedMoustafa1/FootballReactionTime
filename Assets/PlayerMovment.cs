using Kandooz;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

namespace Assets
{
    public class PlayerMovment : MonoBehaviour
    {

        private SteamVR_TrackedController trackedController;
        private SteamVR_TrackedObject trackedObject;
        public bool on = false;

        public float minHeight;
        public float maxHeight;
        public float rate;

        public GameObject coolbox;
        public VRButtonSelector rightHand;
        public VRButtonSelector leftHand;
        public Transform Test;
        public Text message;


        public BoolField playerPosition;
        public BoolField panelPosition;
        public BoolField panelRotation;

        public BoolField panelHeight;

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


            if (device.GetPressDown(EVRButtonId.k_EButton_ApplicationMenu)/*&& device.GetPressDown(EVRButtonId.k_EButton_ApplicationMenu)*/)
            // if (Input.GetKeyDown(KeyCode.X))
            {
                playerPosition.Value = !playerPosition.Value;

                coolbox.SetActive(playerPosition.Value);
                message.text = "Set Player Position";
                rightHand.enabled = !playerPosition.Value;
                leftHand.enabled = !playerPosition.Value;

                panelPosition.Value = false;
                panelHeight.Value = false;
                panelRotation.Value = false;
            }

            if (playerPosition.Value)
            {

            
                if (device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).x > 0.1f)
                //  if (Input.GetKeyDown(KeyCode.W))
                {
                    Test.Translate(rate * Vector3.right * Time.deltaTime);

                    Debug.Log("Hoba");

                }
                if (device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).x < -0.1f)
                //  if (Input.GetKeyDown(KeyCode.S))
                {
                    Debug.Log("Hela");
                    Test.Translate(rate * -Vector3.right * Time.deltaTime);

                }

                if (device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).y > 0.1f)
                //  if (Input.GetKeyDown(KeyCode.W))
                {
                    Debug.Log("Hoba");
                    Test.Translate(rate * Vector3.forward * Time.deltaTime);

                }
                if (device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).y < -0.1f)
                //  if (Input.GetKeyDown(KeyCode.S))
                {
                    Debug.Log("Hela");
                    Test.Translate(rate * -Vector3.forward * Time.deltaTime);

                }



            }


        }
    }
}