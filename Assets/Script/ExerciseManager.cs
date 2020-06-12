using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum BuildersType
{
    Large,
    Medium, 
    Small
}


public class ExerciseManager : MonoBehaviour {
    public static ExerciseManager instance;

    public PanelBuilder panelBuilderSmall;
    public PanelBuilder panelBuilderMedium;
    public PanelBuilder panelBuilderLarge;

    public UnityEvent restart;
    public BuildersType builderType;

    private PanelBuilder currentBuilder;


    private List<GameObject> currentButtonsList;

    private int currentIndex = 0;

    private int layersCount = 1;
    private int blocksCount = 1;

    public ExerciseType nextExerciseType = ExerciseType.OneLayerEx1Assessment;
    public float nextExerciseTime = 2;
    private void Awake()
    {
        instance = this;
    }

    private bool halfCount = false;


    public void OnLargePanelsPressed()
    {
        currentBuilder = panelBuilderLarge;
        builderType = BuildersType.Large;
    }
    public void OnMediumPanelsPressed()
    {
        currentBuilder = panelBuilderMedium;
        builderType = BuildersType.Medium;
    }
    public void OnSmallPanelsPressed()
    {
        currentBuilder = panelBuilderSmall;
        builderType = BuildersType.Small;

    }



    //public void OnLayerNumberButtonPressed(int layersCount = 1)
    //{
    //    this.layersCount = layersCount == 2 ? 2 : 1;
    //}

    private void StartExcersice()
    {
       

        UI3DManager.instance.SetBuilderSize();
        currentButtonsList = new List<GameObject>(currentBuilder.Build(layersCount,halfCount:halfCount));
        Debug.Log(currentButtonsList.Count);
        restart.Invoke();
        TargetGeneration.Instance.PlayTargets(currentButtonsList, nextExerciseTime, nextExerciseType, blocksCount);
    }


    #region Start Exercise Button Calls

    public void StartOneLayerExOneAssessment()
    {
        nextExerciseType = ExerciseType.OneLayerEx1Assessment;
        layersCount = 1;
        blocksCount = 1;
        halfCount = false;
        StartExcersice();
    }

    public void StartOneLayerExOneBlocks(int blockCount = 1)
    {
        nextExerciseType = ExerciseType.OneLayerEx1Blocks;
        layersCount = 1;
        //blocksCount = blockCount;
        blocksCount = Mathf.Min(blockCount, 3);
        blocksCount = Mathf.Max(1, blocksCount);
        halfCount = false;
        StartExcersice();
    }

    public void StartOneLayerExOneTransition()
    {
        nextExerciseType = ExerciseType.OneLayerEx1Transition;
        layersCount = 1;
        blocksCount = 1;
        halfCount = true;
        StartExcersice();
    }

    public void StartOneLayerExTwoAssessment()
    {
        nextExerciseType = ExerciseType.OneLayerEx2Assessment;
        layersCount = 1;
        blocksCount = 1;

        ExerciseButton.redSoa = new List<float>() { 0.6f, 1f, 1.2f };
        ExerciseButton.redSoa.Shuffle();

        halfCount = false;
        StartExcersice();
    }


    public void StartOneLayerExTwoBlocks(int blockCount = 1)
    {
        nextExerciseType = ExerciseType.OneLayerEx2Blocks;
        layersCount = 1;
        //blocksCount = blockCount;
        blocksCount = Mathf.Min(blockCount, 3);
        blocksCount = Mathf.Max(1, blocksCount);
        
        ExerciseButton.redSoa = new List<float>() { 0.6f, 1f, 1.2f };
        ExerciseButton.redSoa.Shuffle();

        halfCount = false;
        StartExcersice();
    }

    public void StartTwoLayerExThreeAssessment()
    {
        nextExerciseType = ExerciseType.TwoLayerEx3Assessment;
        layersCount = 2;
        blocksCount = 1;
        halfCount = false;
        StartExcersice();
    }


    public void StartTwoLayerExThreeTransition()
    {
        nextExerciseType = ExerciseType.TwoLayerEx3Transition;
        layersCount = 2;
        blocksCount = 1;
        halfCount = true;
        StartExcersice();
    }

    public void StartTwoLayerExThreeBlocks(int blockCount = 1)
    {
        nextExerciseType = ExerciseType.TwoLayerEx3Blocks;
        layersCount = 2;
        blocksCount = Mathf.Min(blockCount, 2);
        blocksCount = Mathf.Max(1, blocksCount);

        halfCount = false;
        StartExcersice();
    }


    public void StartTwoLayerExFourAssessment()
    {
        nextExerciseType = ExerciseType.TwoLayerEx4Assessment;
        layersCount = 2;
        blocksCount = 1;

        ExerciseButton.redSoa = new List<float>() { 0.7f, 1f, 1.2f };
        ExerciseButton.redSoa.Shuffle();

        halfCount = false;
        StartExcersice();
    }


    public void StartTwoLayerExFourTransition()
    {
        nextExerciseType = ExerciseType.TwoLayerEx4Transition;
        layersCount = 2;
        blocksCount = 1;

        ExerciseButton.redSoa = new List<float>(){ 0.8f, 1f, 1.2f };
        ExerciseButton.redSoa.Shuffle();

        halfCount = true;
        StartExcersice();
    }

    public void StartTwoLayerExFourBlocks(int blockCount = 1)
    {
        nextExerciseType = ExerciseType.TwoLayerEx4Blocks;
        layersCount = 2;
        blocksCount = Mathf.Min(blockCount, 2);
        blocksCount = Mathf.Max(1, blocksCount);

        ExerciseButton.redSoa = new List<float>() { 0.7f, 1f, 1.2f };
        ExerciseButton.redSoa.Shuffle();


        halfCount = false;
        StartExcersice();
    }

    #endregion
    ////////////////////////////////////////////////////////////////////////////
    #region OLD

    public void SetExersiceTime(float time)
    {

        Debug.Log("1- SetExersiceTime");
        nextExerciseTime = time;
        //nextExerciseType = ExerciseType.Timed;

        StartExcersice();
    }
    public void StartCounterTimedExercise()
    {

       // nextExerciseType = ExerciseType.Counter16x6TimeCalculated;

        StartExcersice();
    }
    public void StartExerciseTwoAssesment()
    {
        //nextExerciseType = ExerciseType.Counter16x6;
        StartExcersice();

    }
    public void StartExerciseTwoTraining()
    {
        //nextExerciseType = ExerciseType.Counter16x6x3;

        StartExcersice();
    }
    public void StartExerciseTwoTwoMinuter()
    {
        //nextExerciseType = ExerciseType.CounterTwoMinuter;
        nextExerciseTime = 2;
        StartExcersice();
    }
    public void StartExerciseThreeTwoMinuter()
    {
        //nextExerciseType = ExerciseType.CounterTwoMinuterRedGreen;
        nextExerciseTime = 2;
        StartExcersice();
    }
    public void StartExerciseThreeAssesment()
    {
       // nextExerciseType = ExerciseType.Counter16x8redGreen;

        StartExcersice();
    }
    public void StartExerciseThreeTraining()
    {
        //nextExerciseType = ExerciseType.Counter16x8x3redGreen;

        StartExcersice();
    }
    public void StartExerciseFourAssesment()
    {
        //nextExerciseType = ExerciseType.Counter16x8Ex4Assessment;

        StartExcersice();
    }
    public void StartExerciseFourTraining()
    {
        //nextExerciseType = ExerciseType.Counter16x8Ex4Training;

        StartExcersice();
    }
    public void StartExerciseFourTimed(float time)
    {
        //nextExerciseType = ExerciseType.Counter16x6Ex4Timed;
        nextExerciseTime = time;

        StartExcersice();
    }
    #endregion
}
