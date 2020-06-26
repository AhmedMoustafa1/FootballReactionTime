using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using Kandooz;

public class SavedData : MonoBehaviour {
    public IntField gender;
    public IntField Formation;
    public IntField Location;

    public static SavedData Instance;
    public string Experiment;
    public string FullName;
    public string FirstName;
    public string LastName;
    public string Gender;
    public string HandColor;
    public string Court;
    public string SquadFormation;
    public string PlayerPosition;
    public string PanelSize;
    public string Age;
    public string CurrentTime;
    public string CurrentDate;
    public string TotalButtonsPressed;
    public string exerciseTime;
    public string blocksCount;
    public List<int> targetPosition;
    public List<float> delay;
    public List<float> reactionTime;
    public List<string> ButtonType;
    public List<string> HandType;
    public List<string> CourtSide;
    public List<string> ButtonLayer;


    public float hitRate;
    public float ommissionRate;
    public float minReactionTime;
    public float maxReactionTime;
    public float averageReactionTime;
    public float _savedHitCount;
    public float _savedMissedCount;
    public float _savedHitCounterTotal;
    public int timeTaken;

    public List<float> sortedReactionTime;

    public UnityEvent exEnd;

    public int MaxCounts
    {
        get
        {
            switch (TargetGeneration.Instance.type)
            {
                case ExerciseType.OneLayerEx1Assessment:
                    return 16 * 3;
                case ExerciseType.OneLayerEx1Blocks:
                    return 16 * 3 * TargetGeneration.Instance.blocksCount;
                case ExerciseType.OneLayerEx1Transition:
                    return 24 * 3;
                case ExerciseType.OneLayerEx2Assessment:
                    return 60;
                case ExerciseType.OneLayerEx2Blocks:
                    return 60 * TargetGeneration.Instance.blocksCount;
                case ExerciseType.TwoLayerEx3Assessment:
                    return 16 * 3 * 2;
                case ExerciseType.TwoLayerEx3Transition:
                    return 24 * 3;
                case ExerciseType.TwoLayerEx3Blocks:
                    return 16 * 3 * 2 * TargetGeneration.Instance.blocksCount;
                case ExerciseType.TwoLayerEx4Assessment:
                    return 32 * 4;
                case ExerciseType.TwoLayerEx4Transition:
                    return 32 * 3;
                case ExerciseType.TwoLayerEx4Blocks:
                    return 32 * 4 * TargetGeneration.Instance.blocksCount;
            }
            return 0;

            //switch (TargetGeneration.Instance.type)
            //{
            //    //case ExerciseType.Counter16x6:
            //    //    return 16 * 6;
            //    //case ExerciseType.Counter16x6x3:
            //    //    return 16 * 6 * 3;
            //    //case ExerciseType.Timed:
            //    //    return 16 * 6;
            //    //case ExerciseType.Counter16x8redGreen:
            //    //    return 16 * 8;
            //    //case ExerciseType.Counter16x8x3redGreen:
            //    //    return 16 * 8 * 3;
            //    //case ExerciseType.Counter16x6TimeCalculated:
            //    //    return 16 * 6;
            //    //case ExerciseType.Counter16x8Ex4Assessment:
            //    //    return 16 * 8;
            //    //case ExerciseType.Counter16x8Ex4Training:
            //    //    return 16 * 8 * 3;
            //    //case ExerciseType.Counter16x6Ex4Timed:
            //    //    return 16 * 6;
            //    //case ExerciseType.CounterTwoMinuterRedGreen:
            //    //    return (int)_savedHitCounterTotal;
            //    default:
            //        return 16 * 6;
            //}

        }
    }



    // Use this for initialization
    void Start () {
        Instance = this;
        SetUserNameAndSurname();

    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SetTimeAndDate();

          

            Debug.Log(CurrentTime);
            //Debug.Log("Name: "+ LastName + " Gender: " + Gender + " Age: "+ Age);
          //  StoreData();
        }
        {

        }	
	}


    public string SetUpExerciseTypeForName()
    {
        string text = "";

        if (TargetGeneration.Instance.type == ExerciseType.OneLayerEx1Blocks ||
            TargetGeneration.Instance.type == ExerciseType.OneLayerEx2Blocks ||
            TargetGeneration.Instance.type == ExerciseType.TwoLayerEx3Blocks ||
            TargetGeneration.Instance.type == ExerciseType.TwoLayerEx4Blocks)
        {
            text = blocksCount + " Block";
            //if (blocksCount == 1.ToString())
            //{
            //    text = "1min";
            //}
            //if (exerciseTime == 2.ToString())
            //{
            //    text = "2min";

            //}
            //if (exerciseTime == 3.ToString())
            //{
            //    text = "3min";

            //}
        }
        else
        if (TargetGeneration.Instance.type == ExerciseType.OneLayerEx1Assessment ||
            TargetGeneration.Instance.type == ExerciseType.OneLayerEx2Assessment ||
            TargetGeneration.Instance.type == ExerciseType.TwoLayerEx3Assessment ||
            TargetGeneration.Instance.type == ExerciseType.TwoLayerEx4Assessment)
        {
            text = "High Balls";
        }
        else
        if (TargetGeneration.Instance.type == ExerciseType.OneLayerEx1Transition ||
            TargetGeneration.Instance.type == ExerciseType.OneLayerEx2Transition ||
            TargetGeneration.Instance.type == ExerciseType.TwoLayerEx3Transition ||
            TargetGeneration.Instance.type == ExerciseType.TwoLayerEx4Transition)
        {
            text = "Transition";
        }

        #region OLD
        //if (TargetGeneration.Instance.type == ExerciseType.Timed ||
        //    TargetGeneration.Instance.type == ExerciseType.Counter16x6Ex4Timed ||
        //    TargetGeneration.Instance.type == ExerciseType.CounterTwoMinuter ||
        //    TargetGeneration.Instance.type == ExerciseType.CounterTwoMinuterRedGreen)
        //{
        //    if (exerciseTime == 1.ToString())
        //    {
        //        text = "1min";
        //    }
        //    if (exerciseTime == 2.ToString())
        //    {
        //        text = "2min";

        //    }
        //    if (exerciseTime == 3.ToString())
        //    {
        //        text = "3min";

        //    }
        //}
        //if (TargetGeneration.Instance.type == ExerciseType.Counter16x6x3 || 
        //    TargetGeneration.Instance.type == ExerciseType.Counter16x8x3redGreen ||
        //    TargetGeneration.Instance.type == ExerciseType.Counter16x8Ex4Training)
        //{
        //    text = "Training";
        //}
        //if (TargetGeneration.Instance.type == ExerciseType.Counter16x6 ||
        //    TargetGeneration.Instance.type == ExerciseType.Counter16x8redGreen || 
        //    TargetGeneration.Instance.type == ExerciseType.Counter16x6TimeCalculated ||
        //    TargetGeneration.Instance.type == ExerciseType.Counter16x8Ex4Assessment)
        //{
        //    text = "Assesment";

        //}
        #endregion

        return text;
    }


    void SetPanelSize()
    {
        if (ExerciseManager.instance.builderType == BuildersType.Small) 
        {
            PanelSize = "Small";
        }
        if (ExerciseManager.instance.builderType == BuildersType.Medium)
        {
            PanelSize = "Medium";
        }
        if (ExerciseManager.instance.builderType == BuildersType.Large)
        {
            PanelSize = "Large";
        }
        PanelSize += " P" + UserManagerInput.instance.PlayerStartPosition; // Replace with Position later
    }
    void SetUserNameAndSurname()
    {
        if (UserManagerInput.instance == null)
        {
            Debug.LogError("No user manager input detected, probably started scene wrong");
            return;
        }
        FullName = UserManagerInput.instance._thisUser.lastName + UserManagerInput.instance._thisUser.firstName;
        FirstName = UserManagerInput.instance._thisUser.firstName;
        LastName = UserManagerInput.instance._thisUser.lastName;
        //FirstName  = FullName.Substring(0, FullName.IndexOf(" "));
        //LastName = FullName.Substring(FullName.IndexOf(" ") + 1);
    }
    void SetUserAgeAndGender()
    {
        if (UserManagerInput.instance == null)
        {
            Debug.LogError("No user manager input detected, probably started scene wrong");
            return;
        }

        if (UserManagerInput.instance._thisUser.userGender==GenderType.Male)
        {
            Gender = "Male";
        }
        else if (UserManagerInput.instance._thisUser.userGender == GenderType.Female)
        {
            Gender = "Female";
        }

        Age = UserManagerInput.instance._thisUser.userAge.ToString();
    }

    void SetUserHandColor()
    {
        if (UserManagerInput.instance == null)
        {
            Debug.LogError("No user manager input detected, probably started scene wrong");
            return;
        }

        if (UserManagerInput.instance._thisUser.userHandColor == global::HandColor.White)
        {
            HandColor = "White";
        }
        else if (UserManagerInput.instance._thisUser.userHandColor == global::HandColor.Black)
        {
            HandColor = "Black";
        }
        else if (UserManagerInput.instance._thisUser.userHandColor == global::HandColor.Yellow)
        {
            HandColor = "Yellow";
        }
    }
    void SetCourtColor()
    {
        if (UserManagerInput.instance == null)
        {
            Debug.LogError("No user manager input detected, probably started scene wrong");
            return;
        }

        if (gender.Value == 1)
        {
            Court = "Grass";
        }
        else if (gender.Value == 2)
        {
            Court = "Grass";
        }
      
    }
    public void SetFormation()
    {
        switch (Formation.Value)
        {
            case 0:
                SquadFormation = "4-4-2";
                break;
            case 1:
                SquadFormation = "4-3-3";
                break;
            case 2:
                SquadFormation = "3-4-3";
                break;
            case 3:
                SquadFormation = "4-4-2-2";
                break;
            case 4:
                SquadFormation = "4-2-3-1";
                break;
            case 5:
                SquadFormation = "GoalKeeper";
                break;
            default:
                SquadFormation = "4-4-2";
                break;
        }
    }
    public void SetPosition()
    {
        if (Formation.Value == 5)
        {
            if (Location.Value == 1)
            {
                PlayerPosition = "P3";
            }
            if (Location.Value == 2)
            {
                PlayerPosition = "P2";
            }
            if (Location.Value == 3)
            {
                PlayerPosition = "P1";
            }
        }
        else
        {
            PlayerPosition = (Location.Value).ToString();
        }
    }

    void SetTimeAndDate()
    {
        CurrentTime = System.DateTime.Now.ToShortTimeString();
        CurrentDate = System.DateTime.Now.ToShortDateString();
    }
    void SetExerciseTypeAndTime()
    {
        Experiment = "Ex";
        if (TargetGeneration.Instance.type == ExerciseType.OneLayerEx1Assessment || TargetGeneration.Instance.type == ExerciseType.OneLayerEx1Blocks || TargetGeneration.Instance.type == ExerciseType.OneLayerEx1Transition)
        {
            Experiment = "Ex1";
        }
        else if (TargetGeneration.Instance.type == ExerciseType.OneLayerEx2Assessment || TargetGeneration.Instance.type == ExerciseType.OneLayerEx2Blocks || TargetGeneration.Instance.type == ExerciseType.OneLayerEx2Transition)
        {
            Experiment = "Ex2";
        }
        else if (TargetGeneration.Instance.type == ExerciseType.TwoLayerEx3Assessment || TargetGeneration.Instance.type == ExerciseType.TwoLayerEx3Transition || TargetGeneration.Instance.type == ExerciseType.TwoLayerEx3Blocks)
        {
            Experiment = "Ex3";
        }
        else if (TargetGeneration.Instance.type == ExerciseType.TwoLayerEx4Assessment || TargetGeneration.Instance.type == ExerciseType.TwoLayerEx4Transition || TargetGeneration.Instance.type == ExerciseType.TwoLayerEx4Blocks)
        {
            Experiment = "Ex4";

        }
        blocksCount = TargetGeneration.Instance.blocksCount.ToString();
        exerciseTime = ExerciseManager.instance.nextExerciseTime.ToString();
    }
    void SetHitCount()
    {

        TotalButtonsPressed = (TargetGeneration.Instance.hitsCounter-1).ToString();
    }

    void UpdateData()
    {
        

        SetPanelSize();
        SetUserNameAndSurname();
        SetUserAgeAndGender();
        SetUserHandColor();
        SetCourtColor();
        SetFormation();
        SetPosition();
        SetTimeAndDate();
        SetExerciseTypeAndTime();
        SetHitCount();
    
    }

    string TimeOrModality()
    {
        string value = "";
        //if (TargetGeneration.Instance.type == ExerciseType.Timed ||
        //    TargetGeneration.Instance.type == ExerciseType.Counter16x6Ex4Timed ||
        //    TargetGeneration.Instance.type == ExerciseType.CounterTwoMinuter ||
        //    TargetGeneration.Instance.type == ExerciseType.CounterTwoMinuterRedGreen)
        //{
        //    value = "Minutes: " + exerciseTime;
        //}
        /* else*/

        if (TargetGeneration.Instance.type == ExerciseType.OneLayerEx1Assessment ||
            TargetGeneration.Instance.type == ExerciseType.OneLayerEx2Assessment ||
            TargetGeneration.Instance.type == ExerciseType.TwoLayerEx3Assessment ||
            TargetGeneration.Instance.type == ExerciseType.TwoLayerEx4Assessment)
        {
            value = "Modality:  Assessment";
        }
        else
        if (TargetGeneration.Instance.type == ExerciseType.OneLayerEx1Blocks ||
            TargetGeneration.Instance.type == ExerciseType.OneLayerEx2Blocks ||
            TargetGeneration.Instance.type == ExerciseType.TwoLayerEx3Blocks ||
            TargetGeneration.Instance.type == ExerciseType.TwoLayerEx4Blocks)
        {
            value = "Modality: " + TargetGeneration.Instance.blocksCount + " Block";
            if (TargetGeneration.Instance.blocksCount > 1) value += "s";
        }
        else
        if (TargetGeneration.Instance.type == ExerciseType.OneLayerEx1Transition ||
            TargetGeneration.Instance.type == ExerciseType.OneLayerEx2Transition || 
            TargetGeneration.Instance.type == ExerciseType.TwoLayerEx3Transition ||
            TargetGeneration.Instance.type == ExerciseType.TwoLayerEx4Transition)
        {
            value = "Modality:  Transition";
        }

        return value;
    }

    string LayerCount()
    {
        string layer = "";
        if (TargetGeneration.Instance.type == ExerciseType.TwoLayerEx3Assessment ||
            TargetGeneration.Instance.type == ExerciseType.TwoLayerEx3Blocks     ||
            TargetGeneration.Instance.type == ExerciseType.TwoLayerEx3Transition     ||
            TargetGeneration.Instance.type == ExerciseType.TwoLayerEx4Assessment ||
            TargetGeneration.Instance.type == ExerciseType.TwoLayerEx4Transition     ||
            TargetGeneration.Instance.type == ExerciseType.TwoLayerEx4Blocks)
        {
            layer = "Layer: 2" + System.Environment.NewLine;
        }

        return layer;
    }

    public void ClearLists()
    {
        targetPosition.Clear();
        delay.Clear();
        reactionTime.Clear();
        ButtonType.Clear();
        HandType.Clear();
        CourtSide.Clear();
        ButtonLayer.Clear();
    }


    public void StoreData() {
        exEnd.Invoke();

        if (UserManagerInput.instance == null)
        {
            Debug.LogError("No user manager input detected, probably started scene wrong");
            return;
        }


        //Debug.Log("### TIME TAKEN: " + timeTaken);
        UpdateData();
        string data;
        data = "Experiment: " + Experiment + System.Environment.NewLine +
                TimeOrModality()+ System.Environment.NewLine +
                LayerCount() +
                "Court: " + Court + System.Environment.NewLine +
                "Formation: " + SquadFormation + System.Environment.NewLine +
                "Postion: " + PlayerPosition + System.Environment.NewLine +
                "PanelSize: " + PanelSize + System.Environment.NewLine +
                "Date: " + CurrentDate + System.Environment.NewLine +
                "Time: " + CurrentTime + System.Environment.NewLine +
                "FirstName: " + FirstName + System.Environment.NewLine +
                "Surname: " + LastName + System.Environment.NewLine +
                "Age: " + Age + System.Environment.NewLine +
                //"HandColor: " + HandColor + System.Environment.NewLine+
                "Sex: " + Gender /*+ System.Environment.NewLine + " "*/ +  System.Environment.NewLine +

                SeriesAndTotalHits();


        //if (TargetGeneration.Instance.type == ExerciseType.Counter16x6TimeCalculated)
        //{
        //    data += "Time Taken: " + timeTaken.ToString() + System.Environment.NewLine+ System.Environment.NewLine;
        //}
        string finalAdditionalData = "";
        string additionalData = "TargetNumber;TargetPosition;SOADuration;ReactionTime; Button type" +  System.Environment.NewLine;

        int index = 1;
        string buttonType = "";
        string layerSideTag = "";

        bool isGreenRed =
            TargetGeneration.Instance.type == ExerciseType.OneLayerEx2Assessment ||
            TargetGeneration.Instance.type == ExerciseType.OneLayerEx2Transition ||
            TargetGeneration.Instance.type == ExerciseType.OneLayerEx2Blocks ||
            TargetGeneration.Instance.type == ExerciseType.TwoLayerEx4Transition ||
            TargetGeneration.Instance.type == ExerciseType.TwoLayerEx4Assessment ||
            TargetGeneration.Instance.type == ExerciseType.TwoLayerEx4Blocks;

        bool isLayered =
            TargetGeneration.Instance.type == ExerciseType.TwoLayerEx3Transition ||
            TargetGeneration.Instance.type == ExerciseType.TwoLayerEx3Assessment ||
            TargetGeneration.Instance.type == ExerciseType.TwoLayerEx3Blocks ||
            TargetGeneration.Instance.type == ExerciseType.TwoLayerEx4Transition ||
            TargetGeneration.Instance.type == ExerciseType.TwoLayerEx4Assessment ||
            TargetGeneration.Instance.type == ExerciseType.TwoLayerEx4Blocks;






        for (int i = 0; i < MaxCounts; i++)
        {
            buttonType = isGreenRed ? ButtonType[i].ToString() + ";" : "";
            layerSideTag = isLayered ? ButtonLayer[i].ToString() : "";

            additionalData = 
                index.ToString() + ";" +
                targetPosition[i].ToString() + ";" +
                (((int)(1000 * reactionTime[i]))).ToString().Replace(",", ".") + ";" +
                (delay[i] * 1000).ToString() + ";" +
                buttonType +
                HandType[i].ToString() + ";" +
                CourtSide[i] + ";" +
                layerSideTag +
                System.Environment.NewLine;

            finalAdditionalData += additionalData;
            index++;
        }

        #region Old

        //if( TargetGeneration.Instance.type == ExerciseType.Counter16x6||
        //    TargetGeneration.Instance.type == ExerciseType.Counter16x8redGreen ||
        //    TargetGeneration.Instance.type == ExerciseType.Counter16x8Ex4Assessment ||
        //    TargetGeneration.Instance.type == ExerciseType.Counter16x6TimeCalculated) {
        //    string buttonType = "";
        //    for (int i = 0; i < MaxCounts; i++)
        //    {
        //        buttonType = (TargetGeneration.Instance.type == ExerciseType.Counter16x6TimeCalculated) ? "" : ButtonType[i].ToString() + ";";

        //        additionalData = (i + 1).ToString() + ";" +
        //            targetPosition[i].ToString() + ";" +
        //            (delay[i] * 1000).ToString() + ";" +
        //             (((int)(1000 * reactionTime[i]))).ToString().Replace(",", ".") /*.ToString("N2")*/ + ";" +
        //               /*ButtonType[i].ToString() + ";"*/
        //               buttonType +
        //             HandType[i].ToString() +
        //            System.Environment.NewLine;
        //        finalAdditionalData += additionalData;
        //    }
        //}

        //if (TargetGeneration.Instance.type == ExerciseType.Counter16x6x3 ||
        //    TargetGeneration.Instance.type == ExerciseType.Counter16x8Ex4Training ||
        //    TargetGeneration.Instance.type == ExerciseType.Counter16x8x3redGreen )
        //{
        //    for (int i = 0; i < MaxCounts; i++)
        //    {

        //        additionalData = (i + 1).ToString() + ";" +
        //            targetPosition[i].ToString() + ";" +
        //            (delay[i] * 1000).ToString() + ";" +
        //            (((int)(1000 * reactionTime[i]))).ToString().Replace(",", ".") + ";" +
        //            ButtonType[i].ToString() + ";" +
        //            HandType[i].ToString() +
        //            System.Environment.NewLine;
        //        finalAdditionalData += additionalData;
        //    }
        //}

        //if (TargetGeneration.Instance.type == ExerciseType.Timed ||
        //    TargetGeneration.Instance.type == ExerciseType.Counter16x6Ex4Timed ||
        //    TargetGeneration.Instance.type == ExerciseType.CounterTwoMinuter || 
        //    TargetGeneration.Instance.type == ExerciseType.CounterTwoMinuterRedGreen)
        //{
        //    string buttonType = "";
        //    string soaDelay = "";
        //    for (int i = 0; i < reactionTime.Count-1; i++)
        //    {

        //        buttonType = (TargetGeneration.Instance.type == ExerciseType.Timed || TargetGeneration.Instance.type == ExerciseType.CounterTwoMinuter) ? "" : ButtonType[i].ToString() + ";";
        //        soaDelay = (TargetGeneration.Instance.type == ExerciseType.CounterTwoMinuter || TargetGeneration.Instance.type == ExerciseType.CounterTwoMinuterRedGreen) ? (delay[i] * 1000).ToString() + ";": "";

        //        additionalData =
        //            (i + 1).ToString() + ";" +
        //            targetPosition[i].ToString() + ";" +
        //            ((int)(1000 * reactionTime[i])).ToString().Replace(",", ".") + ";" +
        //            soaDelay +
        //            buttonType +
        //            HandType[i].ToString() +
        //            System.Environment.NewLine;
        //            finalAdditionalData += additionalData;
        //    }
        //}

        #endregion

        data += finalAdditionalData;


        //data += System.Environment.NewLine + System.Environment.NewLine +
        //    "Average Reaction Time: " + 1000 * averageReactionTime + " ms " + System.Environment.NewLine;

        #region Sorting Min Max Reaction Time  // Commented for now

        //sortedReactionTime = new List<float>();

        //    for (int i = 0; i < reactionTime.Count; i++)
        //    {
        //        sortedReactionTime.Add(reactionTime[i]);
        //    }

        //    sortedReactionTime.Sort();

        //    for (int i = sortedReactionTime.Count - 1; i >= 0; i--)
        //    {
        //        if (sortedReactionTime[i] == 0)
        //        {
        //            sortedReactionTime.RemoveAt(i);
        //        }
        //    }

        //if (sortedReactionTime.Count > 0)
        //{
        //    // OLD
        //    //if  (TargetGeneration.Instance.type == ExerciseType.Counter16x6 ||
        //    //     TargetGeneration.Instance.type == ExerciseType.Counter16x8Ex4Assessment ||
        //    //     TargetGeneration.Instance.type == ExerciseType.Counter16x8redGreen)
        //    //{
        //    //    hitRate = _savedHitCount / MaxCounts;
        //    //    ommissionRate = _savedMissedCount / MaxCounts;

        //    //}

        //    //if (TargetGeneration.Instance.type == ExerciseType.Counter16x6x3 ||
        //    //    TargetGeneration.Instance.type == ExerciseType.Counter16x8Ex4Training ||
        //    //    TargetGeneration.Instance.type == ExerciseType.Counter16x8x3redGreen)

        //    //{
        //    //    hitRate = _savedHitCount / MaxCounts;
        //    //    ommissionRate = _savedMissedCount / MaxCounts;
        //    //}
        //    // END OLD

        //    // NEW
        //    hitRate = _savedHitCount / MaxCounts;
        //    ommissionRate = _savedMissedCount / MaxCounts;
        //    // END NEW
        //    minReactionTime = sortedReactionTime[0];
        //    maxReactionTime = sortedReactionTime[sortedReactionTime.Count - 1];
        //    averageReactionTime = sortedReactionTime.Average();

        //    string StatsData = "";

        //    string hitRateOmissionRate = "Hit Rate: " + 100 * hitRate + " % " + System.Environment.NewLine +
        //            "Ommission Rate: " + 100 * ommissionRate + " % " + System.Environment.NewLine;

        //    //if (TargetGeneration.Instance.type != ExerciseType.Timed &&
        //    //   TargetGeneration.Instance.type != ExerciseType.Counter16x6TimeCalculated &&
        //    //   TargetGeneration.Instance.type != ExerciseType.CounterTwoMinuter)
        //    //{
        //        StatsData = hitRateOmissionRate;
        //    //}

        //    StatsData += "Minimum Reaction Time: " + 1000 * minReactionTime + " ms " + System.Environment.NewLine +
        //                 "Maxmum Reaction Time: " + 1000 * maxReactionTime + " ms " + System.Environment.NewLine +
        //                 "Average Reaction Time: " + 1000 * averageReactionTime + " ms " + System.Environment.NewLine;



        //    data += System.Environment.NewLine + StatsData;

        //}
        //else
        //{
        //    data += System.Environment.NewLine + "No Reactions Recorded";
        //}
        #endregion
        // Hand Color
        //data += System.Environment.NewLine +  "HandColor: " + HandColor + System.Environment.NewLine;

        CurrentDate = CurrentDate.Replace("/", "-");
        CurrentTime = CurrentTime.Replace("/", "-");
        CurrentTime = CurrentTime.Replace(":", "-");
        CurrentTime = CurrentTime.Replace(" ", "-");

        string layerNumber = isLayered ? "-Layer 2" : "";
        string fileName = "VR-VLBL-" + Experiment + "-" + SetUpExerciseTypeForName() + "-" + Court + "-" + PanelSize + layerNumber + "-" + LastName + "-" + FirstName + "-" + CurrentDate + "-" + CurrentTime;

        Debug.Log(fileName);
        Debug.Log(UserAccountManager.intance.directory);
        System.IO.File.WriteAllText(UserAccountManager.intance.directory + "\\"   + fileName+".txt", data);
        TotalButtonsPressed = 0.ToString();
    }


    string HitsPerMinute(int mins)
    {
        //string errorsOne = ", error " + TargetGeneration.Instance.errorOne;
        //string errorsTwo = ", error " + TargetGeneration.Instance.errorTwo;
        //string errorsThree = ", error " + TargetGeneration.Instance.errorThree;

        //if (TargetGeneration.Instance.type == ExerciseType.Timed ||
        //    TargetGeneration.Instance.type == ExerciseType.CounterTwoMinuter) errorsOne = errorsTwo = errorsThree = "";

        //switch (mins)
        //{
        //    case 1:
        //        int hits = TargetGeneration.Instance.hitOne ;

        //        return "1 minute: hits " + hits + ", anticipation " + TargetGeneration.Instance.anticipationOne + errorsOne;
                
        //    case 2:

        //        return "1 minute: hits " + TargetGeneration.Instance.hitOne + ", anticipation " + TargetGeneration.Instance.anticipationOne + errorsOne /*", error " + TargetGeneration.Instance.errorOne*/  + System.Environment.NewLine+
        //                "2 minute: hits " + TargetGeneration.Instance.hitTwo + ", anticipation " + TargetGeneration.Instance.anticipationTwo  + errorsTwo /*", error " + TargetGeneration.Instance.errorTwo*/ ;


        //    case 3:

        //        return "1 minute: hits " + TargetGeneration.Instance.hitOne + ", anticipation " + TargetGeneration.Instance.anticipationOne +errorsOne /*", error " + TargetGeneration.Instance.errorOne*/ + System.Environment.NewLine+
        //                "2 minute: hits " + TargetGeneration.Instance.hitTwo + ", anticipation " + TargetGeneration.Instance.anticipationTwo +errorsTwo /*", error " + TargetGeneration.Instance.errorTwo*/+ System.Environment.NewLine+
        //                 "3 minute: hits " + TargetGeneration.Instance.hitThree + ", anticipation " + TargetGeneration.Instance.anticipationThree +errorsThree/* ", error " + TargetGeneration.Instance.errorThree*/;

        //    default:
        //        break;
        //}
        return "";
    }

    string SeriesAndTotalHits()
    {

        int totalButtonsPressed = 0;
        int totalGreenPressed = 0; 
        int totalRedPressed = 0;

        for (int i = 0; i < TargetGeneration.Instance.Serieshits.Count; i++)
        {
            totalButtonsPressed += TargetGeneration.Instance.Serieshits[i].TotalHits;

            totalGreenPressed += TargetGeneration.Instance.Serieshits[i].correctHitGreen;
            totalRedPressed += TargetGeneration.Instance.Serieshits[i].correctHitRed;
        }

        string returned ="";
        #region One Layer || Ex 1
        if (TargetGeneration.Instance.type == ExerciseType.OneLayerEx1Assessment ||
            TargetGeneration.Instance.type == ExerciseType.OneLayerEx1Transition ||
            TargetGeneration.Instance.type == ExerciseType.OneLayerEx1Blocks )
        {
            returned = 
                System.Environment.NewLine + "Total buttons pressed: " + totalButtonsPressed.ToString()  +
                System.Environment.NewLine +
                System.Environment.NewLine + "Anticipation: " + TargetGeneration.Instance.buttonAnticipationCounter.ToString() +
                System.Environment.NewLine + "Omission: " + TargetGeneration.Instance.buttonMissedCounter.ToString() +
                System.Environment.NewLine + System.Environment.NewLine;

            for (int i = 0; i < TargetGeneration.Instance.Serieshits.Count; i++)
            {

                if (i > 1 && i % 4 == 0)
                {
                    returned += System.Environment.NewLine;
                }

                returned +=
                    (i + 1).ToString() + " series : " +
                     "Hits " + TargetGeneration.Instance.Serieshits[i].hits + ", " +
                     "Anticipation " + TargetGeneration.Instance.Serieshits[i].anticipations + ", " +
                     "Omission " + TargetGeneration.Instance.Serieshits[i].omissions +
                     System.Environment.NewLine;

            }

            returned += System.Environment.NewLine;

        }
        #endregion


        #region One Layer || Ex 2
        if (TargetGeneration.Instance.type == ExerciseType.OneLayerEx2Assessment ||
            TargetGeneration.Instance.type == ExerciseType.OneLayerEx2Transition ||
            TargetGeneration.Instance.type == ExerciseType.OneLayerEx2Blocks)
        {
            returned =
                "Correct Hits: Yellow " + totalGreenPressed.ToString() + ", Red " + totalRedPressed.ToString() + 
                ////(TargetGeneration.Instance.buttonPressedCounter + TargetGeneration.Instance.buttonAnticipationCounter + TargetGeneration.Instance.wrongButtonCounter).ToString() +
                //System.Environment.NewLine + "Anticipation: " + TargetGeneration.Instance.buttonAnticipationCounter.ToString() +
                //System.Environment.NewLine + "Omission: " + TargetGeneration.Instance.buttonMissedCounter.ToString() +
                //System.Environment.NewLine + "Errors: " + TargetGeneration.Instance.wrongButtonCounter.ToString() +
                /*System.Environment.NewLine + HitsPerMinute((int)ExerciseManager.instance.nextExerciseTime) + */
                System.Environment.NewLine + System.Environment.NewLine;

            for (int i = 0; i < TargetGeneration.Instance.Serieshits.Count; i++)
            {
                if (i > 1 && i % 4 == 0)
                {
                    returned += System.Environment.NewLine;
                }

                returned +=
                    (i + 1).ToString() + " series : " +
                     "Hits " + TargetGeneration.Instance.Serieshits[i].hits + ", " +
                     "Anticipation " + TargetGeneration.Instance.Serieshits[i].anticipations + ", " +
                     "Omission " + TargetGeneration.Instance.Serieshits[i].omissions + ", " +
                     "Error " + TargetGeneration.Instance.Serieshits[i].errors +
                     System.Environment.NewLine;


            }

            returned += System.Environment.NewLine;

        }
        #endregion


        #region Two Layer || Ex 3
        if (TargetGeneration.Instance.type == ExerciseType.TwoLayerEx3Assessment ||
            TargetGeneration.Instance.type == ExerciseType.TwoLayerEx3Transition ||
            TargetGeneration.Instance.type == ExerciseType.TwoLayerEx3Blocks)
        {
            returned =
                System.Environment.NewLine + "Total buttons pressed: " + totalButtonsPressed.ToString() +
                System.Environment.NewLine + /*System.Environment.NewLine +*/
                System.Environment.NewLine + "Anticipation: " + TargetGeneration.Instance.buttonAnticipationCounter.ToString() +
                System.Environment.NewLine + "Omission: " + TargetGeneration.Instance.buttonMissedCounter.ToString() +
                /*System.Environment.NewLine + HitsPerMinute((int)ExerciseManager.instance.nextExerciseTime) + */
                System.Environment.NewLine + System.Environment.NewLine;

            for (int i = 0; i < TargetGeneration.Instance.Serieshits.Count; i++)
            {
                if (i > 1 && i % 4 == 0)
                {
                    returned += System.Environment.NewLine;
                }

                returned +=
                    (i + 1).ToString() + " series : " +
                     "Hits " + TargetGeneration.Instance.Serieshits[i].hits + ", " +
                     "Anticipation " + TargetGeneration.Instance.Serieshits[i].anticipations + ", " +
                     "Omission " + TargetGeneration.Instance.Serieshits[i].omissions +
                      System.Environment.NewLine;


            }
            returned += System.Environment.NewLine;

        }
        #endregion


        #region Two Layer || Ex 4
        if (TargetGeneration.Instance.type == ExerciseType.TwoLayerEx4Assessment ||
            TargetGeneration.Instance.type == ExerciseType.TwoLayerEx4Transition ||
            TargetGeneration.Instance.type == ExerciseType.TwoLayerEx4Blocks)
        {
            returned =
                System.Environment.NewLine + "Correct Hits: Yellow " + totalGreenPressed.ToString() + ", Red " + totalRedPressed.ToString() +
                ////(TargetGeneration.Instance.buttonPressedCounter + TargetGeneration.Instance.buttonAnticipationCounter + TargetGeneration.Instance.wrongButtonCounter).ToString() +
                //System.Environment.NewLine + "Anticipation: " + TargetGeneration.Instance.buttonAnticipationCounter.ToString() +
                //System.Environment.NewLine + "Omission: " + TargetGeneration.Instance.buttonMissedCounter.ToString() +
                //System.Environment.NewLine + "Errors: " + TargetGeneration.Instance.wrongButtonCounter.ToString() +
                /*System.Environment.NewLine + HitsPerMinute((int)ExerciseManager.instance.nextExerciseTime) + */
                System.Environment.NewLine + System.Environment.NewLine;

            for (int i = 0; i < TargetGeneration.Instance.Serieshits.Count; i++)
            {

                if (i > 1 && i % 4 == 0)
                {
                    returned += System.Environment.NewLine;
                }

                returned +=
                    (i + 1).ToString() + " series : " +
                     "Hits " + TargetGeneration.Instance.Serieshits[i].hits + ", " +
                     "Anticipation " + TargetGeneration.Instance.Serieshits[i].anticipations + ", " +
                     "Omission " + TargetGeneration.Instance.Serieshits[i].omissions + ", " +
                     "Error " + TargetGeneration.Instance.Serieshits[i].errors +
                     System.Environment.NewLine;


            }

            returned += System.Environment.NewLine;
        }
        #endregion

        #region OLD
        //     if (TargetGeneration.Instance.type == ExerciseType.Counter16x6Ex4Timed ||
        //TargetGeneration.Instance.type == ExerciseType.CounterTwoMinuterRedGreen)
        //     {
        //         returned = System.Environment.NewLine + "Total buttons pressed: " +
        //             (TargetGeneration.Instance.buttonPressedCounter + TargetGeneration.Instance.buttonAnticipationCounter + TargetGeneration.Instance.wrongButtonCounter).ToString() +
        //             System.Environment.NewLine + "Omission: " + TargetGeneration.Instance.buttonMissedCounter.ToString() +
        //             System.Environment.NewLine + "Anticipation: " + TargetGeneration.Instance.buttonAnticipationCounter.ToString() +
        //             System.Environment.NewLine + "Errors: " + TargetGeneration.Instance.wrongButtonCounter.ToString() +
        //             System.Environment.NewLine + HitsPerMinute((int)ExerciseManager.instance.nextExerciseTime) + System.Environment.NewLine + System.Environment.NewLine;


        //     }
        //else if (TargetGeneration.Instance.type == ExerciseType.Timed ||
        //         TargetGeneration.Instance.type == ExerciseType.Counter16x6TimeCalculated ||
        //         TargetGeneration.Instance.type == ExerciseType.CounterTwoMinuter) 
        //{
        //    returned = System.Environment.NewLine + "Total buttons pressed: " +
        //         (TargetGeneration.Instance.buttonPressedCounter + TargetGeneration.Instance.buttonAnticipationCounter + TargetGeneration.Instance.wrongButtonCounter).ToString() +
        //        System.Environment.NewLine + "Anticipation: " + TargetGeneration.Instance.buttonAnticipationCounter.ToString();

        //    if (TargetGeneration.Instance.type != ExerciseType.Counter16x6TimeCalculated)
        //    {
        //        returned +=  System.Environment.NewLine + HitsPerMinute((int)ExerciseManager.instance.nextExerciseTime);
        //    }
        //    returned += System.Environment.NewLine + System.Environment.NewLine;
        //}
        //else 
        //{
        //    returned = "Hits: " + TargetGeneration.Instance.buttonPressedCounter.ToString() +
        //        System.Environment.NewLine + "Omission: " + TargetGeneration.Instance.buttonMissedCounter.ToString() +
        //        System.Environment.NewLine + "Anticipation: " + TargetGeneration.Instance.buttonAnticipationCounter.ToString();

        //    if (TargetGeneration.Instance.type == ExerciseType.Counter16x8x3redGreen ||
        //        TargetGeneration.Instance.type == ExerciseType.Counter16x8redGreen ||
        //        TargetGeneration.Instance.type == ExerciseType.Counter16x8Ex4Assessment ||
        //        TargetGeneration.Instance.type == ExerciseType.Counter16x8Ex4Training)
        //    {
        //        returned += System.Environment.NewLine + "Errors: " + TargetGeneration.Instance.wrongButtonCounter.ToString();
        //    }

        //    returned += System.Environment.NewLine + System.Environment.NewLine;
        //}
        #endregion
        return returned;
    }
}

