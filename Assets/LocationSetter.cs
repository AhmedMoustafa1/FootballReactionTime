using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class LocationSetter : MonoBehaviour
{

    private SteamVR_TrackedController trackedController;
    private SteamVR_TrackedObject trackedObject;
    public bool on = false;

    public float maxForward = 1.25f;
    public float maxBackward = -1.25f;
    public float maxRight = 1.5f;
    public float maxLeft = -1.5f;

    public float rate;

    public GameObject coolbox;
    public VRButtonSelector rightHand;
    public VRButtonSelector leftHand;
    public Text message;

    public Transform Test;
    void Start()
    {
        trackedObject = this.GetComponent<SteamVR_TrackedObject>();
        trackedController = gameObject.AddComponent<SteamVR_TrackedController>();
        trackedController.controllerIndex = (uint)trackedObject.index;
        message.text = "Set Position";

    }

    public void FixedUpdate()
    {
    }
    // Update is called once per frame
    void Update()
    {
        var device = SteamVR_Controller.Input((int)trackedObject.index);


        //if (device.GetPressDown(EVRButtonId.k_EButton_A))
        //{
        //    Debug.Log("A");
        //}

        //if(device.GetPressDown(EVRButtonId.k_EButton_ApplicationMenu))
        //{
        //    Debug.Log("app menue");
        //}
        //if(device.GetPressDown(EVRButtonId.k_EButton_Dashboard_Back))
        //{
        //    Debug.Log("db");
        //}
        //if (device.GetPressDown(EVRButtonId.k_EButton_Axis0))
        //{
        //    Debug.Log("ax0");
        //}
        //if (device.GetPressDown(EVRButtonId.k_EButton_Axis1))
        //{
        //    Debug.Log("ax1");
        //}
        //if (device.GetPressDown(EVRButtonId.k_EButton_Axis2))
        //{
        //    Debug.Log("ax2");
        //}
        //if (device.GetPressDown(EVRButtonId.k_EButton_Axis3))
        //{
        //    Debug.Log("ax3");
        //}
        //if (device.GetPressDown(EVRButtonId.k_EButton_Axis4))
        //{
        //    Debug.Log("ax4");
        //}
        // Enabling Height Adjustment
        // if (device.GetPressUp(EVRButtonId.k_EButton_SteamVR_Touchpad))
        if (device.GetPressDown(EVRButtonId.k_EButton_A)/*&& device.GetPressDown(EVRButtonId.k_EButton_ApplicationMenu)*/)
        // if (Input.GetKeyDown(KeyCode.X))
        {
            on = !on;
            coolbox.SetActive(on);
            message.text = "Set Position";
            rightHand.enabled = !on;
            leftHand.enabled = !on;
        }

        if (on)
        {
            if ((device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).y < -0.1f || Input.GetKeyDown(KeyCode.W)) && Test.localPosition.z < maxForward)
            //  if (Input.GetKeyDown(KeyCode.W))
            {
                //Debug.Log("Hoba");
                Test.Translate(rate * Vector3.forward * Time.deltaTime);

            }
            if ((device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).y > 0.1f || Input.GetKeyDown(KeyCode.S)) && Test.localPosition.z > maxBackward)
            //  if (Input.GetKeyDown(KeyCode.S))
            {
                //Debug.Log("Hela");
                Test.Translate(rate * -Vector3.forward * Time.deltaTime);

            }
            if ((device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).x > 0.1f || Input.GetKeyDown(KeyCode.D)) && Test.localPosition.x > maxRight)
            {
                Debug.Log("Hela");
                Test.Translate(rate * -Vector3.right * Time.deltaTime);

            
            }


            if ((device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).x < -0.1f || Input.GetKeyDown(KeyCode.A)) && Test.localPosition.x < maxLeft)
            //  if (Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log("Hela");
                Test.Translate(rate * Vector3.right * Time.deltaTime);

            }
        }
    }
}