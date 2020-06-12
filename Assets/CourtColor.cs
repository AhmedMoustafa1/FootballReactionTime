using Kandooz;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CourtColors
{
    Blue, Green, Red
}
public class CourtColor : MonoBehaviour
{


    //public Color red;
    //public Color green;
    //public Color blue;


    public Material RedIn;
    public Material RedOut;
    [Space(4)]
    public Material GreenIn;
    public Material GreenOut;
    [Space(4)]
    public Material BlueIn;
    public Material BlueOut;
    [Space(4)]

    public MeshRenderer courtMaterial;
    public CourtColors currentCourtColor;

    public IntField CurrentCourt; 
    private Color courtColor;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("change color");
        //    SetColor(currentCourtColor);
        }
    }

    private void Start()
    {
        //courtMaterial = this.gameObject.GetComponent<MeshRenderer>();

        SetColor(CurrentCourt.Value);
        Debug.Log("start: "+courtMaterial.materials[0]);
    }

    //public void SetColorWithIndex()
    //{
    //    switch (CurrentCourt.Value)
    //    {
    //        case 1:
    //            SetColor(CourtColors.Red);
    //            break;
    //        case 2:
    //            SetColor(CourtColors.Green);
    //            break;
    //        case 3:
    //            SetColor(CourtColors.Blue);
    //            break;

    //    }
    //}


    public void SetColor(int value)
    {
        Debug.Log("Setting Color with : " + value);
        switch (value)
        {
            case 2:

                Debug.Log("Setting Color with : red" + value);
                Material[] matsR = { RedOut, RedIn };
                courtMaterial.materials = matsR;
                //courtMaterial.materials[1] = RedIn;
                //courtMaterial.materials[0] = RedOut;
                break;
            case 1:
                Debug.Log("Setting Color with : green" + value);


                Material[] matsG = { GreenOut, GreenIn };
                courtMaterial.materials = matsG;
                //courtMaterial.materials[1] = GreenIn;
                //courtMaterial.materials[0] = GreenOut;


                break;
            case 3:
                Debug.Log("Setting Color with : blue" + value);


                Material[] matsB = { BlueOut,BlueIn };
                courtMaterial.materials = matsB;

                //courtMaterial.materials[1] = GreenIn;
                //courtMaterial.materials[0] = GreenOut;
                break;
            default:
                break;
        }
    }
}
