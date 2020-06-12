using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerGender
{
    Male,
    Female
}

public class BallMaterials : ScriptableObject {


    public PlayerGender playerGender = PlayerGender.Male;

    public Material[] redMaterialsMale;
    public Material[] flashMaterialsMale;
    public Material[] normalMaterialMale;


    public Material[] redMaterialsFemale;
    public Material[] flashMaterialsFemale;
    public Material[] normalMaterialFemale;

    public Material[] GetRedBalls()
    {
        if (playerGender == PlayerGender.Male) return redMaterialsMale;
        else return redMaterialsFemale;
    }
    public Material[] GetYellowBalls()
    {
        if (playerGender == PlayerGender.Male) return normalMaterialMale;
        else return normalMaterialFemale;
    }
    public Material[] GetFlashBalls()
    {
        if (playerGender == PlayerGender.Male) return flashMaterialsMale;
        else return flashMaterialsFemale;
    }


}
