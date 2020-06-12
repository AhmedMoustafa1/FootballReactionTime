using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI3DManager : MonoBehaviour {


    public static UI3DManager instance;
    public GameObject exerciseSelectionPanel;
    public GameObject exerciseOnePanel;
    public GameObject exerciseTwoPanel;
    public GameObject exerciseThreePanel;
    public GameObject exerciseFourPanel;
    public GameObject buttonSizeSelectionPanel;
    public GameObject SmallPanel;
    public GameObject MediumPanel;
    public GameObject LargePanel;

    public GameObject InGamePanel;


    // Use this for initialization
    void Start ()
    {
        instance = this;
	}


    public void SetBuilderSize()
    {
        if (ExerciseManager.instance.builderType == BuildersType.Medium)
        {
            SmallPanel.gameObject.SetActive(false);
            MediumPanel.gameObject.SetActive(true);
            LargePanel.gameObject.SetActive(false);

        }

        if (ExerciseManager.instance.builderType == BuildersType.Small)
        {
            MediumPanel.gameObject.SetActive(false);
            SmallPanel.gameObject.SetActive(true);
            LargePanel.gameObject.SetActive(false);

        }
        if (ExerciseManager.instance.builderType == BuildersType.Large)
        {
            SmallPanel.gameObject.SetActive(false);
            MediumPanel.gameObject.SetActive(false);
            LargePanel.gameObject.SetActive(true);

        }
    }

    public void DesetBuilder()
    {
        MediumPanel.gameObject.SetActive(false);
        SmallPanel.gameObject.SetActive(false);
        LargePanel.gameObject.SetActive(false);

    }


    public void OnButtonSizeTypePressed()
    {
        buttonSizeSelectionPanel.gameObject.SetActive(false);
        exerciseSelectionPanel.gameObject.SetActive(true);
    }

    //Exercise Buttons Panel
    public void OnExerciseOnePressed()
    {
        exerciseSelectionPanel.gameObject.SetActive(false);
        exerciseOnePanel.gameObject.SetActive(true);
    }
    public void OnExerciseTwoPressed()
    {
        exerciseSelectionPanel.gameObject.SetActive(false);
        exerciseTwoPanel.gameObject.SetActive(true);
    }
    public void OnExerciseThreePressed()
    {
        exerciseSelectionPanel.gameObject.SetActive(false);
        exerciseThreePanel.gameObject.SetActive(true);
    }
    public void OnExerciseFourPressed()
    {
        exerciseSelectionPanel.gameObject.SetActive(false);
        exerciseFourPanel.gameObject.SetActive(true);
    }


    //Exercise 1
    public void OnExerciseOneButtonsPressed()
    {
        exerciseSelectionPanel.gameObject.SetActive(false);
        exerciseOnePanel.gameObject.SetActive(false);

        InGamePanel.gameObject.SetActive(true);
    }

    //Exercise 2
    public void OnExerciseTwoButtonsPressed()
    {
        exerciseSelectionPanel.gameObject.SetActive(false);
        exerciseTwoPanel.gameObject.SetActive(false);
        InGamePanel.gameObject.SetActive(true);
    }

    //Exercise 3
    public void OnExerciseThreeButtonsPressed()
    {
        exerciseSelectionPanel.gameObject.SetActive(false);
        exerciseThreePanel.gameObject.SetActive(false);
        InGamePanel.gameObject.SetActive(true);


    }

    public void OnExerciseFourButtonsPressed()
    {
        exerciseSelectionPanel.gameObject.SetActive(false);
        exerciseFourPanel.gameObject.SetActive(false);
        InGamePanel.gameObject.SetActive(true);
    }


    public void OnBackToExercisePanel()
    {
        exerciseSelectionPanel.gameObject.SetActive(true);
        exerciseOnePanel.gameObject.SetActive(false);
        exerciseTwoPanel.gameObject.SetActive(false);
        exerciseThreePanel.gameObject.SetActive(false);
        exerciseFourPanel.gameObject.SetActive(false);
        InGamePanel.gameObject.SetActive(false);

    }


    public void OnBackToSizePanel()
    {
        exerciseSelectionPanel.gameObject.SetActive(false);
        buttonSizeSelectionPanel.gameObject.SetActive(true);
        InGamePanel.gameObject.SetActive(false);
    }


    public void ExitButtonPressed()
    {
        InGamePanel.gameObject.SetActive(false);
        exerciseSelectionPanel.gameObject.SetActive(true);
    }
}
