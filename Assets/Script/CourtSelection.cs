using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CourtSelection : MonoBehaviour {

    public void Start()
    {

    }
    public Toggle redToggle;
    public bool RedSelected
    {
        set
        {
            if (value)
            {
              //  courtColor.SetColor(CourtColors.Red);
                UserManagerInput.instance.courtColor = CourtColors.Red;
            }
        }
    }

    public bool GreenSelected
    {
        set
        {
            if (value)
            {
              //  courtColor.SetColor(CourtColors.Green);
                UserManagerInput.instance.courtColor = CourtColors.Green;
            }
        }
    }

    public bool BlueSelected
    {
        set
        {
            if (value)
            {
             //   courtColor.SetColor(CourtColors.Blue);
                UserManagerInput.instance.courtColor = CourtColors.Blue;
            }
        }
    }

    

    CourtColor courtColor;


    private void Awake()
    {
      //  courtColor = this.GetComponent<CourtColor>();
    //    courtColor.currentCourtColor = CourtColors.Red;
       // courtColor.SetColor(CourtColors.Red);
      //  UserManagerInput.instance.courtColor = CourtColors.Red;
        if (redToggle != null) redToggle.isOn = true;
    }
}
