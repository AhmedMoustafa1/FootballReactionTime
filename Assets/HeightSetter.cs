using Kandooz;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class HeightSetter : MonoBehaviour
{

    private SteamVR_TrackedController trackedController;
    private SteamVR_TrackedObject trackedObject;

    public float minHeight;
    public float maxHeight;
    public float rate;

    public GameObject coolbox;
    public VRButtonSelector rightHand;
    public VRButtonSelector leftHand;
    public Transform Test;
    public Text message;
    public GameObject[] disabledObjects;



    public BoolField playerPosition;
    public BoolField panelPosition;
    public BoolField panelRotation;

    public BoolField panelHeight;
    void Start()
    {

        trackedObject = this.GetComponent<SteamVR_TrackedObject>();
        trackedController = gameObject.AddComponent<SteamVR_TrackedController>();
        trackedController.controllerIndex = (uint)trackedObject.index;
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
            panelHeight.Value = !panelHeight.Value;

            coolbox.SetActive(panelHeight.Value);
            message.text = "Set Panel Height";
            rightHand.enabled = !panelHeight.Value;
            leftHand.enabled = !panelHeight.Value;

            panelPosition.Value = false;
            playerPosition.Value = false;
            panelRotation.Value = false;

        }
        if (panelHeight.Value)
        {
            if ((device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).y > 0.1f ) && Test.localPosition.y < maxHeight)
            {
                Test.Translate(rate * Vector3.up * Time.deltaTime);

            }
            if ((device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).y < -0.1f ) && Test.localPosition.y > minHeight)
            {
                Test.Translate(rate * Vector3.down * Time.deltaTime);

            }
        }


    }
}
