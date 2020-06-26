using Kandooz;
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


    public BoolField playerPosition;
    public BoolField panelPosition;
    public BoolField panelRotation;

    public BoolField panelHeight;
    void Start()
    {
        trackedObject = this.GetComponent<SteamVR_TrackedObject>();
        trackedController = gameObject.AddComponent<SteamVR_TrackedController>();
        trackedController.controllerIndex = (uint)trackedObject.index;
        message.text = "Set Position";
        panelHeight.Value = false;
        playerPosition.Value = false;
        panelPosition.Value = false;
        panelRotation.Value = false;

    }


    void Update()
    {
        var device = SteamVR_Controller.Input((int)trackedObject.index);


        if (device.GetPressDown(EVRButtonId.k_EButton_A))
        {
            panelPosition.Value = !panelPosition.Value;
            coolbox.SetActive(panelPosition.Value);
            message.text = "Set Panel Position";
            rightHand.enabled = !panelPosition.Value;
            leftHand.enabled = !panelPosition.Value;


            panelHeight.Value = false;
            playerPosition.Value = false;
            panelRotation.Value = false;

        }

        if (panelPosition.Value)
        {
            if ((device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).y < -0.1f || Input.GetKeyDown(KeyCode.W)) && Test.localPosition.z < maxForward)
            {
                Test.Translate(rate * Vector3.forward * Time.deltaTime);

            }
            if ((device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).y > 0.1f || Input.GetKeyDown(KeyCode.S)) && Test.localPosition.z > maxBackward)
            {
                Test.Translate(rate * -Vector3.forward * Time.deltaTime);

            }
            if ((device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).x > 0.1f || Input.GetKeyDown(KeyCode.D)) && Test.localPosition.x > maxRight)
            {
                Test.Translate(rate * -Vector3.right * Time.deltaTime);


            }


            if ((device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).x < -0.1f || Input.GetKeyDown(KeyCode.A)) && Test.localPosition.x < maxLeft)
            {
                Test.Translate(rate * Vector3.right * Time.deltaTime);

            }
        }
    }
}