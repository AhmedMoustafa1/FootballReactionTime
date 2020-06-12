using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public enum ButtonType
{
    Green,
    Red
}
public enum PanelSize
{
    L,
    M,
    S
}
public enum ExerciseType
{
    //Counter16x6,
    //Counter16x6x3,
    //Timed,
    //Counter16x8redGreen,
    //Counter16x8x3redGreen,
    //Counter16x6TimeCalculated,
    //Counter16x8Ex4Assessment,
    //Counter16x8Ex4Training,
    //Counter16x6Ex4Timed,
    //CounterTwoMinuter,
    //CounterTwoMinuterRedGreen,

    OneLayerEx1Assessment,
    OneLayerEx1Blocks,
    OneLayerEx1Transition,
    OneLayerEx2Assessment,
    OneLayerEx2Blocks,
    OneLayerEx2Transition,
    TwoLayerEx3Assessment,
    TwoLayerEx3Transition,
    TwoLayerEx3Blocks,
    TwoLayerEx4Assessment,
    TwoLayerEx4Transition,
    TwoLayerEx4Blocks
}

public class SeriesHits
{
    public int hits = 0;
    public int anticipations = 0;
    public int omissions = 0;
    public int errors = 0;

    public int correctHitRed = 0;
    public int correctHitGreen = 0;

    public void ScoreHit(ButtonType buttonType)
    {
        //hits++;   ////////FOR NOW
        if (buttonType == ButtonType.Green)
        {
            correctHitGreen++;
            hits++;
        }
        else if (buttonType == ButtonType.Red) correctHitRed++;
    }
    public int TotalHits
    {
        get
        {
            return (hits + anticipations + errors);
        }
    }

    public int TotalButtonsAppeared
    {
        get
        {
            return (hits + anticipations + omissions + errors);
        }
    }

}

public class TargetGeneration : MonoBehaviour
{

    private static TargetGeneration instance;

    public ExerciseType type;

    public int blocksCount = 1;
    public int layersCount = 1;

    [SerializeField] private List<GameObject> activeButtons;
    [SerializeField] private List<GameObject> usedButtons;
    public ButtonBehaviour currentButton;
    [Space(10)]
    public Timer TimerObject;
    [Space(10)]
    public AudioClip startedAudio;
    public AudioClip EndeddAudio;
    public AudioClip CancelledAudio;
    public AudioClip ExitedAudio;
    public AudioClip PauseAudio90Sec;
    public AudioClip PauseAudio30Sec;
    public AudioClip PauseAudio40Sec;
    public AudioClip PauseAudio20Sec;
    public AudioClip ResumedAudio;
    [Space(10)]
    public int hitsCounter = 0;
    public int buttonPressedCounter = 0;
    public int buttonAnticipationCounter = 0;
    public int buttonMissedCounter = 0;
    public int wrongButtonCounter = 0;
    private int maxCounts = 0;
    public float CurrentExerciseTime;
    public float exerciseTime = 0;
    public float lifetime = 0;
    [Space(10)]
    public bool gameEnded;
    public bool gamePaused = false;
    public bool exerciseEnded = false;
    private bool started = false;
    [Space(10)]
    public Text excerciseName;
    public Text hitCount;
    public Text missCount;
    public Text wrongCount;
    public Text anticiapationCount;
    public Text averageReactionText;

    [Space(10)]
    public Text debuger;
    public Text pauseWaitTimeText;
    public Text timeText;

    [Space(10)]
    public Text UIExcerciseName;
    public Text UIHitCount;
    public Text UIMissCount;
    public Text UIWrongCount;
    public Text UIAnticiapationCount;
    public Text UIAverageReaction;
    [Space(10)]
    public GameObject UIScreenTimer;
    [Space(10)]
    public GameObject[] ExamButtons;
    public List<float> reactionsTimesOfPressed;
    [Space(10)]
    public GameObject startPlayingButton;

    private float time = 0;
    private int pauseTime = 30;
    [Space(10)]

    public Transform positionParent;
    public Vector3 rightSidePosition;
    public Vector3 leftSidePosition;

    private string hitsFromSeries = "";
    private string missFromSeries = "";
    private string anticipationFromSeries = "";
    private string errorsFromSeries = "";


    private float greenIndex = 0;
    private AudioSource audioSource;
    private bool onTheRight = true;
    private string courtSide = "Rs";
    string currentExcerciseName = "";

    private List<ExerciseButton> buttons;
    private List<GameObject> buttonsList;

    private List<SeriesHits> serieshits;

    public HeightSetter heightSetter;
    public LocationSetter locationSetter;
    public PanelSize currentPanelSize;

    private float GreenInstances
    {
        get
        {
            float returned = 0;

            for (int i = 0; i < Serieshits.Count; i++)
            {
                returned += Serieshits[i].correctHitGreen + Serieshits[i].omissions + Serieshits[i].anticipations;
            }

            return returned;
        }
    }

    public static TargetGeneration Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject newGO = new GameObject();
                newGO.name = "TargetGenerator";
                instance = newGO.AddComponent<TargetGeneration>();
            }
            return instance;
        }
    }
    private int HitsCounter
    {
        get
        {
            return hitsCounter;
        }

        set
        {
            hitsCounter = value;
            if (/*hitsCounter == maxCounts + 1*/ CheckGameEndCondition())
            {
                gameEnded = true;
                StopAllCoroutines();
            }
        }
    }
    public bool NeedPause
    {
        get
        {
            bool needPause = false;
            pauseTime = 30;

            switch (type)
            {
                #region One Layer || Ex 1 Assessment
                case ExerciseType.OneLayerEx1Assessment:
                    if ((GreenInstances) % 12 == 0 && GreenInstances > 1)
                    {
                        //StartCoroutine(SwitchPosition());
                        pauseTime = 20;
                        // if(GreenInstances % 24 == 0) StartCoroutine(SwitchPosition(keepSideValue: true));
                        needPause = true;
                    }
                    else
                    {
                        needPause = false;
                    }
                    //needPause = false;
                    break;
                #endregion
                #region One Layer || Ex 1 Blocks
                case ExerciseType.OneLayerEx1Blocks:
                    pauseTime = 40;
                    switch (blocksCount)
                    {
                        case 1:
                            if ((GreenInstances) % 12 == 0 && GreenInstances > 1)
                            {
                                //StartCoroutine(SwitchPosition());
                                pauseTime = 20;
                                // if (GreenInstances % 24 == 0) StartCoroutine(SwitchPosition(keepSideValue: true));
                                needPause = true;
                            }
                            else
                            {
                                needPause = false;
                            }
                            break;
                        case 2:

                            if (hitsCounter == (16 * 3) + 1 || hitsCounter == (16 * 3 * 2) + 1)
                            {
                                //StartCoroutine(SwitchPosition());
                                Serieshits.Add(new SeriesHits());
                                needPause = true;
                            }
                            else if ((GreenInstances) % 12 == 0 && GreenInstances > 1 && GreenInstances % 48 != 0)
                            {
                                //StartCoroutine(SwitchPosition());
                                pauseTime = 20;
                                needPause = true;
                            }
                            else
                            {
                                needPause = false;
                            }
                            break;
                        case 3:
                            if (hitsCounter == (16 * 3) + 1 || hitsCounter == (16 * 3 * 2) + 1)
                            {
                                //StartCoroutine(SwitchPosition());
                                Serieshits.Add(new SeriesHits());
                                needPause = true;
                            }
                            else if ((GreenInstances) % 24 == 0 && GreenInstances > 1 && GreenInstances % 48 != 0)
                            {
                                //StartCoroutine(SwitchPosition());
                                pauseTime = 20;
                                needPause = true;
                            }
                            else
                            {
                                needPause = false;
                            }

                            break;
                    }
                    break;
                #endregion
                #region One Layer || Ex 1 Transition
                case ExerciseType.OneLayerEx1Transition:
                    if (GreenInstances % 24 == 0 && GreenInstances > 1 && GreenInstances % (24 * 3) /*48*/ != 0)
                    {
                        pauseTime = 20;
                        Serieshits.Add(new SeriesHits());
                        needPause = true;
                    }
                    else
                    {
                        needPause = false;
                    }
                    break;
                #endregion
                #region One Layer || Ex 2 Assessment
                case ExerciseType.OneLayerEx2Assessment:

                    if ((GreenInstances) % 12 == 0 && GreenInstances % 48 != 0 /* <- New*/ && GreenInstances > 2 && (GreenInstances / 48) >= greenIndex)  //48 is total greens
                    {
                        greenIndex = (GreenInstances / 48) + 0.1f;
                        //if (GreenInstances % 24 == 0 && GreenInstances % 48 != 0) StartCoroutine(SwitchPosition(keepSideValue: true));
                        //StartCoroutine(SwitchPosition());
                        pauseTime = 20;
                        needPause = true;
                    }
                    else
                    {
                        needPause = false;
                    }

                    //needPause = false;

                    break;
                #endregion
                #region One Layer || Ex 2 Blocks
                case ExerciseType.OneLayerEx2Blocks:

                    switch (blocksCount)
                    {
                        case 1:
                            //needPause = false;
                            if ((GreenInstances) % 12 == 0 && GreenInstances % 48 != 0 && GreenInstances > 2 && (GreenInstances / 48) >= greenIndex)
                            {
                                greenIndex = (GreenInstances / 48) + 0.1f;
                                //StartCoroutine(SwitchPosition());
                                //if (GreenInstances % 24 == 0 && GreenInstances % 48 != 0) StartCoroutine(SwitchPosition(keepSideValue: true));
                                pauseTime = 20;
                                needPause = true;
                            }
                            else
                            {
                                needPause = false;
                            }
                            //Same as EX2Assessment

                            break;
                        case 2:
                        case 3:
                            //if (hitsCounter == ((16 * 4)) || hitsCounter == (((16 * 4) - 1) * 2) + 1)

                            if (hitsCounter == 60 + 1 || hitsCounter == (60 * 2) + 1) // TEST
                            {
                                pauseTime = 40;
                                SetButtonsList();

                                needPause = true;
                                Serieshits.Add(new SeriesHits());
                                //StartCoroutine(SwitchPosition());
                            }
                            else if ((GreenInstances) % 24 == 0 && GreenInstances > 2 && (GreenInstances / (48 * blocksCount) >= greenIndex) && GreenInstances % 48 != 0)
                            {
                                greenIndex = (GreenInstances / (48 * blocksCount)) + 0.1f;
                                pauseTime = 20;
                                needPause = true;
                            }
                            else
                            {
                                needPause = false;
                            }
                            break;
                    }
                    break;
                #endregion
                #region Two Layer || Ex 3 Assessment
                case ExerciseType.TwoLayerEx3Assessment:
                    //if (hitsCounter == (24 * 4))
                    //{
                    //    Debug.Log("24 * 4");
                    //    //Reset Position                        
                    //    StartCoroutine(SwitchPosition(true, false, 2f));
                    //    needPause = false;
                    //}
                    //else 
                    if (hitsCounter > 1 && (hitsCounter - 1) % 24 == 0)
                    {
                        Debug.Log("24");
                        pauseTime = 20;
                        needPause = true;
                        Serieshits.Add(new SeriesHits());
                        //Switch Position
                        //StartCoroutine(SwitchPosition());

                    }
                    else if ((GreenInstances) % 12 == 0 && GreenInstances > 1 && GreenInstances % 24 != 0)
                    {
                        pauseTime = 20;
                        needPause = true;
                    }
                    else
                    {
                        needPause = false;
                    }
                    break;
                #endregion
                #region Two Layer || Ex 3 Transition
                case ExerciseType.TwoLayerEx3Transition:
                    if (hitsCounter == 24 + 1 || hitsCounter == ((24 * 2) + 1))
                    {
                        Debug.Log("Transition first pause");
                        pauseTime = 20;
                        needPause = true;
                        Serieshits.Add(new SeriesHits());
                    }
                    else if ((GreenInstances) % 12 == 0 && GreenInstances > 1 && GreenInstances % (24 * 3) /*was 48 (24 * 2) */ != 0 && GreenInstances % 24 != 0)
                    {
                        pauseTime = 20;
                        needPause = true;
                    }
                    else
                    {
                        needPause = false;
                    }
                    break;
                #endregion
                #region Two Layer || Ex 3 Blocks
                case ExerciseType.TwoLayerEx3Blocks:


                    if (hitsCounter == (24 * 4) + 1 && blocksCount == 2)
                    {
                        //Reset Position
                        //StartCoroutine(SwitchPosition(true));
                        SetButtonsList();
                        pauseTime = 90;
                        needPause = true;
                        Serieshits.Add(new SeriesHits());
                    }
                    else if (hitsCounter > 1 && (hitsCounter - 1) % 24 == 0) // Block Count 1
                    {

                        pauseTime = 20;
                        needPause = true;
                        Serieshits.Add(new SeriesHits());
                        //Switch Position
                        // StartCoroutine(SwitchPosition());
                    }
                    else if ((GreenInstances) % 12 == 0 && GreenInstances > 2 && blocksCount == 1 && GreenInstances % 24 != 0 && GreenInstances % (24 * 4) != 0) // NEW
                    {
                        //StartCoroutine(SwitchPosition());
                        pauseTime = 20;
                        needPause = true;
                    }
                    else
                    {
                        needPause = false;
                    }
                    break;
                #endregion
                #region Two Layer || Ex 4 Assessment  AND Two Layer || Ex 4 Blocks
                case ExerciseType.TwoLayerEx4Assessment:
                case ExerciseType.TwoLayerEx4Blocks:

                    if (hitsCounter == (32 * 4) + 1 && blocksCount == 2)
                    {
                        SetButtonsList();
                        //Reset Position
                        pauseTime = 90;
                        needPause = true;
                        Serieshits.Add(new SeriesHits());
                    }
                    else if ((GreenInstances) % 12 == 0 && GreenInstances > 1 && (GreenInstances / (24 * 4 * 2)) >= greenIndex && blocksCount == 2 && GreenInstances % 32 != 0 && GreenInstances % 24 != 0) // NEW
                    {
                        greenIndex = (GreenInstances / (24 * 4 * 2)) + 0.1f;
                        pauseTime = 20;
                        needPause = true;
                    }
                    else if (hitsCounter > 1 && (hitsCounter - 1) % 32 == 0) // BlocksCount = 1 && assessment && 2
                    {
                        Debug.Log("32");
                        needPause = true;
                        Serieshits.Add(new SeriesHits());

                        pauseTime = (Serieshits.Count % 3 == 0) && Serieshits.Count > 1 && blocksCount < 2 ? 40 : 20; // for Blocks 1 and Assessment

                        //StartCoroutine(SwitchPosition());
                    }
                    else if ((GreenInstances) % 12 == 0 && GreenInstances > 1 && (GreenInstances / (24 * 4)) >= greenIndex && blocksCount == 1 && GreenInstances % 32 != 0 && GreenInstances % 24 != 0) // NEW
                    {
                        greenIndex = (GreenInstances / (24 * 4)) + 0.1f;

                        //StartCoroutine(SwitchPosition());
                        pauseTime = 20;
                        needPause = true;
                    }
                    else
                    {
                        needPause = false;
                    }
                    break;
                #endregion
                #region Two Layer || Ex 4 Transition
                case ExerciseType.TwoLayerEx4Transition:
                    if (hitsCounter == 32 + 1 || hitsCounter == ((32 * 2) + 1))
                    {
                        //Reset Position
                        pauseTime = 30;
                        needPause = true;
                        Serieshits.Add(new SeriesHits());
                    }
                    else if ((GreenInstances) % 12 == 0 && GreenInstances > 1 && (GreenInstances / (24 * 4)) >= greenIndex && GreenInstances % 32 != 0 && GreenInstances % (32 * 3) != 0 && GreenInstances % 24 != 0) // NEW
                    {
                        greenIndex = (GreenInstances / (24 * 4)) + 0.1f;
                        pauseTime = 20;
                        needPause = true;
                    }
                    else
                    {
                        needPause = false;
                    }
                    break;
                    #endregion
                    #region Two Layer || Ex 4 Blocks
                    #endregion
            }

            return needPause;
        }
    }

    private float AverageReaction
    {
        get
        {
            float avg = 0;
            if (reactionsTimesOfPressed != null && reactionsTimesOfPressed.Count > 0)
            {
                for (int i = 0; i < reactionsTimesOfPressed.Count; i++)
                {
                    avg += reactionsTimesOfPressed[i];
                }
                avg /= reactionsTimesOfPressed.Count;
            }
            return avg;
        }
    }

    public List<SeriesHits> Serieshits
    {
        get
        {
            return serieshits;
        }

        private set
        {
            serieshits = value;
        }
    }

    private bool CheckGameEndCondition()
    {
        bool ended = true;

        switch (type)
        {
            case ExerciseType.OneLayerEx1Assessment:
            case ExerciseType.OneLayerEx1Blocks:
            case ExerciseType.OneLayerEx1Transition:
            case ExerciseType.OneLayerEx2Assessment:
            case ExerciseType.OneLayerEx2Blocks:
            case ExerciseType.OneLayerEx2Transition:
            case ExerciseType.TwoLayerEx3Assessment:
            case ExerciseType.TwoLayerEx3Transition:
            case ExerciseType.TwoLayerEx3Blocks:
            case ExerciseType.TwoLayerEx4Assessment:
            case ExerciseType.TwoLayerEx4Transition:
            case ExerciseType.TwoLayerEx4Blocks:
                ended = (hitsCounter == (maxCounts + 1));
                break;
        }

        return ended;
    }

    public void Awake()
    {
        instance = this;
        audioSource = this.gameObject.GetComponent<AudioSource>();
        pauseWaitTimeText.gameObject.SetActive(false);

        UIExcerciseName.transform.parent.gameObject.SetActive(false);
        excerciseName.transform.parent.gameObject.SetActive(false);



        if (PauseAudio90Sec == null) PauseAudio90Sec = Resources.Load<AudioClip>("90SecondsPause");
        if (PauseAudio40Sec == null) PauseAudio40Sec = Resources.Load<AudioClip>("40SecondsPause");
        if (PauseAudio30Sec == null) PauseAudio30Sec = Resources.Load<AudioClip>("30SecondsPause");
        if (PauseAudio20Sec == null) PauseAudio20Sec = Resources.Load<AudioClip>("20SecondsPause");
    }




    public void PlayTargets(List<GameObject> buttonsList, float exerciseTime, ExerciseType type, int blocksCount)
    {


        this.buttonsList = buttonsList;



        //SeriesHits to Save Hits per series and to update UI
        Serieshits = new List<SeriesHits>();
        Serieshits.Add(new SeriesHits());

        usedButtons = new List<GameObject>();
        CurrentExerciseTime = exerciseTime;
        this.exerciseTime = exerciseTime;
        this.type = type;
        this.blocksCount = blocksCount;
        layersCount = this.buttonsList.Count > 16 ? 2 : 1;

        // StartCoroutine(SwitchPosition(startPosition: true,noDelay: true));
        SetEndingAndExerciseUIName();


        //activeButtons = new List<GameObject>(buttonsList);
        //activeButtons.Shuffle();



        //Make a list of the exercise buttons
        buttons = new List<ExerciseButton>();
        ExerciseButton button;
        for (int i = 0; i < buttonsList.Count; i++)
        {
            button = buttonsList[i].GetComponent<ExerciseButton>();
            if (button != null)
            {
                //button.SetButtonTypesAndSOA(null, null);
                buttons.Add(button);
            }
        }

        //Make the active buttons list first time, will be overwritten on later
        activeButtons = new List<GameObject>(buttonsList);

        reactionsTimesOfPressed = new List<float>();

        //Debug.Log("activeButtons.Count "+activeButtons.Count);

        #region UI

        //Set Buttons
        SetButtonsList();

        UIExcerciseName.transform.parent.gameObject.SetActive(true);
        excerciseName.transform.parent.gameObject.SetActive(true);
        excerciseName.text = UIExcerciseName.text = currentExcerciseName;

        SetUIBoxSize();

        hitCount.text = "Hits: 0";
        UIHitCount.text = "Hits: 0";

        UIAnticiapationCount.text = "Anticipation: 0";
        anticiapationCount.text = "Anticipation: 0";

        missCount.text = "Miss: 0";
        UIMissCount.text = "Miss: 0";

        wrongCount.text = "Wrong: 0";
        UIWrongCount.text = "Wrong: 0";


        averageReactionText.text = "Average Reaction Time: 0";
        UIAverageReaction.text = "Average Reaction Time: 0";

        pauseWaitTimeText.gameObject.GetComponent<Text>().text = "";
        UIScreenTimer.gameObject.GetComponent<Text>().text = "Time: 00:00";

        missCount.gameObject.SetActive(true);
        UIMissCount.gameObject.SetActive(true);

        bool hasErrorUI = (this.type == ExerciseType.OneLayerEx2Assessment ||
                           this.type == ExerciseType.OneLayerEx2Blocks ||
                           this.type == ExerciseType.TwoLayerEx4Assessment ||
                           this.type == ExerciseType.TwoLayerEx4Transition ||
                           this.type == ExerciseType.TwoLayerEx4Blocks);

        //Debug.Log(this.type);
        wrongCount.gameObject.SetActive(hasErrorUI);
        UIWrongCount.gameObject.SetActive(hasErrorUI);

        //Enable the timer UI
        UIScreenTimer.SetActive(true);


        UpdateUI();
        #endregion



        //Just to avoid errors

        StartCoroutine(DoAfter(0, () =>
        {
            for (int i = 0; i < /*activeButtons*/ buttonsList.Count; i++)
            {
                //activeButtons[i].GetComponent<ExerciseButton>().SetFlashMaterial();
                //this.buttonsList[i].GetComponent<ExerciseButton>().SetFlashMaterial();
                this.buttonsList[i].GetComponent<ExerciseButton>().SetOn();
            }
        }));

        if (startPlayingButton != null) startPlayingButton.SetActive(true);
        StartCoroutine(DoAfter(1, StartExercise));
        //StartCoroutine(DoAfter(2, StartExercise));
        //StartCoroutine(StartGameDelay());
    }

    private void StartExercise()
    {
        pauseWaitTimeText.gameObject.SetActive(false);
        ExamButtons = GameObject.FindGameObjectsWithTag("ExamButton");
        buttonMissedCounter = 0;
        buttonPressedCounter = 0;
        buttonAnticipationCounter = 0;
        wrongButtonCounter = 0;
        HitsCounter = 0;
        gamePaused = false;
        gameEnded = false;

        greenIndex = 0;


        TimerObject.timeScoreCounter = 0;
        SavedData.Instance.ClearLists();
        if (!audioSource) audioSource = this.GetComponent<AudioSource>();

        if (activeButtons.Count < 0)
            throw new UnityException("Error Setting First Button active buttons empty");

        exerciseEnded = false;
        started = true;




        if (startPlayingButton == null)
        {
            Debug.Log("Waiting 3 seconds");
            StartCoroutine(DoAfter(3, StartPlayingButtonPressed));
        }//StartPlayingButtonPressed();


    }


    public void StartPlayingButtonPressed()
    {
        StartCoroutine(DoAfter(1, () =>
        {
            TimerObject.GetComponent<Timer>().StartTimer(-2, TimerType.Increment);

            for (int i = 0; i < this.buttonsList.Count; i++)
            {
                this.buttonsList[i].GetComponent<ExerciseButton>().Flash();
            }

            StartCoroutine(StartAfter(2, PlayNextButton));
        }));
    }


    private void SetButtonsList()
    {
        List<ButtonType> types;// = new List<ButtonType>() { ButtonType.Green, ButtonType.Green, ButtonType.Green, ButtonType.Green };
        List<float> soas;// = new List<float>() { 0.25f, 0.5f, 0.1f };
        List<ExerciseButton> tmpList = new List<ExerciseButton>();
        List<ExerciseButton> formedList = new List<ExerciseButton>();



        //A list to combine the active buttons
        List<GameObject> fullList = new List<GameObject>();
        switch (type)
        {
            #region 1 Layer || Ex 1
            case ExerciseType.OneLayerEx1Blocks:
            case ExerciseType.OneLayerEx1Assessment:
            case ExerciseType.OneLayerEx1Transition:
                {
                    //activeButtons.Shuffle(); // 2
                    buttons.Shuffle(); // 1

                    types = new List<ButtonType>() { ButtonType.Green, ButtonType.Green, ButtonType.Green };
                    List<List<float>> soasGroups = new List<List<float>>()
                    {
                    new List<float>(){ 0.6f, 1.0f, 1.2f } ,
                    new List<float>(){ 1.0f, 1.2f, 0.6f } ,
                    new List<float>(){ 1.2f, 0.6f, 1.0f }
                    };
                    int soaGroup = UnityEngine.Random.Range(0, 3);

                    for (int i = 0; i < buttons.Count; i++)
                    {


                        buttons[i].SetButtonTypesAndSOA(types, soasGroups[soaGroup], shuffleSoas: false);
                        buttons[i].soaGroup = soaGroup + 1;

                        soaGroup++;
                        if (soaGroup == 3) soaGroup = 0;
                    }

                    ShuffleActiveButtonsBySOA();

                    break;
                }
            #endregion
            #region 1 Layer || Ex 2
            case ExerciseType.OneLayerEx2Blocks:
            case ExerciseType.OneLayerEx2Assessment:
                {
                    buttons.Shuffle(); // 1

                    soas = new List<float>() { 0.6f, 1f, 1.2f };
                    soas.Shuffle();

                    types = new List<ButtonType>() { ButtonType.Green, ButtonType.Green, ButtonType.Green };
                    List<ButtonType> typesWithRed = new List<ButtonType>() { ButtonType.Green, ButtonType.Red, ButtonType.Green, ButtonType.Red, ButtonType.Green, ButtonType.Red };
                    //typesWithRed.Shuffle();
                    typesWithRed.ShiftLeft(UnityEngine.Random.Range(1, 4));

                    List<List<float>> soasGroups = new List<List<float>>
                    {
                        new List<float>() { 0.6f, 1.0f, 1.2f },
                        new List<float>() { 1.2f, 0.6f, 1.0f },
                        new List<float>() { 1.0f, 1.2f, 0.6f }
                    };

                    int soaGroup = UnityEngine.Random.Range(0, 3);
                    for (int i = 0; i < buttons.Count; i++)
                    {

                        if (buttons[i].buttonID <= 4)
                        {
                            buttons[i].SetButtonTypesAndSOA(typesWithRed, soasGroups[soaGroup], shuffleSoas: false, shuffleTypes: false);
                            typesWithRed.ShiftLeft();
                        }
                        else
                        {
                            buttons[i].SetButtonTypesAndSOA(types, soasGroups[soaGroup], shuffleSoas: false, shuffleTypes: false);
                        }
                        buttons[i].soaGroup = soaGroup + 1;

                        soaGroup++;
                        if (soaGroup == 3) soaGroup = 0;
                    }

                    buttons.Shuffle();

                    formedList = new List<ExerciseButton>();
                    for (int f = 0; f < 3; f++)
                    {

                        tmpList = new List<ExerciseButton>(buttons);


                        for (int i = 0; i < buttons.Count; i++)
                        {
                            if (buttons[i].buttonID <= 4) tmpList.Add(buttons[i]);
                        }

                        #region Soa shuffle
                        //Shuffling TmpList


                        //Make a List of Lists of Groups
                        List<List<ExerciseButton>> soaGroupButtonsLists = new List<List<ExerciseButton>>();
                        soaGroupButtonsLists = new List<List<ExerciseButton>>();
                        for (int i = 0; i < 3; i++)
                        {
                            soaGroupButtonsLists.Add(new List<ExerciseButton>());
                        }

                        //Fill Soa Groups list of Lists
                        for (int i = 0; i < tmpList.Count; i++)
                        {
                            switch (tmpList[i].soaGroup)
                            {
                                case 1:
                                    soaGroupButtonsLists[0].Add(tmpList[i]);
                                    break;
                                case 2:
                                    soaGroupButtonsLists[1].Add(tmpList[i]);
                                    break;
                                case 3:
                                    soaGroupButtonsLists[2].Add(tmpList[i]);
                                    break;
                                default:
                                    soaGroupButtonsLists[0].Add(tmpList[i]); // If there is a problem , just add it in group 1 (0)
                                    break;
                            }
                        }

                        //Shuffle The Buttons inside the groups
                        for (int i = 0; i < soaGroupButtonsLists.Count; i++)
                        {
                            soaGroupButtonsLists[i].Shuffle();
                        }

                        //Clear the tmp list , we have it in the soa groups
                        tmpList = new List<ExerciseButton>();

                        List<int> groupList = new List<int>() { 0, 1, 2 };
                        groupList.Shuffle();
                        int currentInt = 0;
                        //int counts = 0;
                        //List<int> indexOrder = new List<int>();
                        //Debug.Log("Count \n" + soaGroupButtonsLists[0].Count + "\n" + soaGroupButtonsLists[1].Count + "\n" + soaGroupButtonsLists[2].Count);

                        //refill the list now again but shuffled
                        while (soaGroupButtonsLists[0].Count > 0 || soaGroupButtonsLists[1].Count > 0 || soaGroupButtonsLists[2].Count > 0)
                        {
                            //counts++; //Testing

                            currentInt = groupList[0];
                            groupList.RemoveAt(0);

                            //indexOrder.Add(currentInt); //Testing

                            if (groupList.Count == 0)
                            {
                                groupList = new List<int>() { 0, 1, 2 };
                                groupList.Shuffle();
                            }

                            if (soaGroupButtonsLists[currentInt].Count == 0)
                            {
                                continue;
                            }

                            tmpList.Add(soaGroupButtonsLists[currentInt][0]);
                            soaGroupButtonsLists[currentInt].RemoveAt(0);
                        }

                        // TmpList is Soa Shuffled
                        #endregion

                        if (formedList.Count > 0) // make sure ends dont match
                        {
                            if (formedList[formedList.Count - 1].soaGroup == tmpList[0].soaGroup)
                            {
                                tmpList.ShiftLeft();
                            }
                        }

                        for (int k = 0; k < tmpList.Count; k++)
                        {
                            formedList.Add(tmpList[k]);
                        }
                    }



                    Debug.Log("formedList count = " + formedList.Count);


                    activeButtons = new List<GameObject>();
                    for (int i = 0; i < formedList.Count; i++)
                    {
                        activeButtons.Add(formedList[i].gameObject);
                    }
                    usedButtons = new List<GameObject>();
                    //Debug.Log("activeButtons Size = " + activeButtons.Count);

                    break;
                }
            #endregion
            #region 2 Layer || Ex 3
            case ExerciseType.TwoLayerEx3Transition:
            case ExerciseType.TwoLayerEx3Assessment:
            case ExerciseType.TwoLayerEx3Blocks:
                {
                    //activeButtons.Shuffle(); // 2
                    //buttons.Shuffle(); // 1

                    List<ExerciseButton> firstLayerButtons = buttons.FindAll(x => x.buttonLayer == 1);
                    List<ExerciseButton> secondLayerButtons = buttons.FindAll(x => x.buttonLayer == 2);

                    //firstLayerButtons.Shuffle();
                    //secondLayerButtons.Shuffle();





                    float minWait = 0.6f;
                    if (type == ExerciseType.TwoLayerEx3Transition) minWait = 0.8f;

                    types = new List<ButtonType>() { ButtonType.Green, ButtonType.Green, ButtonType.Green };
                    //soas = new List<float>() { 0.7f, 1.0f, 1.2f };
                    //soas = new List<float>() { 1.2f, 0.7f, 1.0f };
                    //soas = new List<float>() { 1.0f, 1.2f, 0.7f };
                    List<List<float>> soasGroups = new List<List<float>>
                    {
                        new List<float>() { minWait, 1.0f, 1.2f },
                        new List<float>() { 1.2f, minWait, 1.0f },
                        new List<float>() { 1.0f, 1.2f, minWait }
                    };

                    int soaGroup = UnityEngine.Random.Range(0, 3);
                    for (int i = 0; i < buttons.Count; i++)
                    {
                        //if (i % 10 == 0 && i > 0)
                        //{
                        //    soas = soas.ShiftLeft(1);
                        //}


                        buttons[i].SetButtonTypesAndSOA(types, soasGroups[soaGroup], shuffleSoas: false);
                        buttons[i].soaGroup = soaGroup + 1;

                        soaGroup++;
                        if (soaGroup == 3) soaGroup = 0;
                    }
                    //ShuffleActiveButtonsBySOA();
                    int counts = type == ExerciseType.TwoLayerEx3Transition ? 4 : 8;
                    for (int j = 0; j < counts; j++)  // 12 x 8 = 96
                    {

                        for (int i = 0; i < 6; i++)
                        {
                            if (firstLayerButtons.Count == 0) firstLayerButtons = buttons.FindAll(x => x.buttonLayer == 1);
                            tmpList.Add(firstLayerButtons[0]);
                            firstLayerButtons.RemoveAt(0);
                        }
                        for (int i = 0; i < 6; i++)
                        {
                            if (secondLayerButtons.Count == 0) secondLayerButtons = buttons.FindAll(x => x.buttonLayer == 2);
                            tmpList.Add(secondLayerButtons[0]);
                            secondLayerButtons.RemoveAt(0);
                        }
                        //tmpList.Shuffle();
                        #region SOA setting
                        /// SETTING SOA
                        List<List<ExerciseButton>> soaGroupButtonsLists = new List<List<ExerciseButton>>();

                        soaGroupButtonsLists = new List<List<ExerciseButton>>();
                        for (int i = 0; i < 3; i++)
                        {
                            soaGroupButtonsLists.Add(new List<ExerciseButton>());
                        }

                        for (int g = 0; g < tmpList.Count; g++)
                        {
                            switch (tmpList[g].soaGroup)
                            {
                                case 1:
                                    soaGroupButtonsLists[0].Add(tmpList[g]);
                                    break;
                                case 2:
                                    soaGroupButtonsLists[1].Add(tmpList[g]);
                                    break;
                                case 3:
                                    soaGroupButtonsLists[2].Add(tmpList[g]);
                                    break;
                            }
                        }
                        for (int i = 0; i < soaGroupButtonsLists.Count; i++)
                        {
                            soaGroupButtonsLists[i].Shuffle();
                        }

                        tmpList = new List<ExerciseButton>();

                        List<int> groupList = new List<int>() { 0, 1, 2 };
                        groupList.Shuffle();
                        int currentInt = 0;
                        //int counts = 0;
                        //List<int> indexOrder = new List<int>();
                        //Debug.Log("Count \n" + soaGroupButtonsLists[0].Count + "\n" + soaGroupButtonsLists[1].Count + "\n" + soaGroupButtonsLists[2].Count);

                        while (soaGroupButtonsLists[0].Count > 0 || soaGroupButtonsLists[1].Count > 0 || soaGroupButtonsLists[2].Count > 0)
                        {
                            //counts++; //Testing

                            currentInt = groupList[0];
                            groupList.RemoveAt(0);

                            //indexOrder.Add(currentInt); //Testing

                            if (groupList.Count == 0)
                            {
                                groupList = new List<int>() { 0, 1, 2 };
                                groupList.Shuffle();
                            }

                            if (soaGroupButtonsLists[currentInt].Count == 0)
                            {
                                continue;
                            }

                            tmpList.Add(soaGroupButtonsLists[currentInt][0]);
                            soaGroupButtonsLists[currentInt].RemoveAt(0);
                        }

                        //string debgtst = "";
                        //for (int i = 0; i < indexOrder.Count; i++)
                        //{
                        //    debgtst += indexOrder[i] + "\n";
                        //}
                        //Debug.Log("Exit While after : " + counts + "\n" + debgtst);

                        //Check last with first if same group
                        if (formedList.Count > 0)
                        {
                            if (formedList[formedList.Count - 1].soaGroup == tmpList[0].soaGroup)
                            {
                                tmpList.ShiftLeft();
                            }
                        }

                        ///
                        #endregion

                        for (int i = 0; i < tmpList.Count; i++)
                        {
                            formedList.Add(tmpList[i]);
                        }
                        tmpList = new List<ExerciseButton>();
                    }

                    activeButtons = new List<GameObject>();
                    for (int i = 0; i < formedList.Count; i++)
                    {
                        activeButtons.Add(formedList[i].gameObject);
                    }
                    break;
                }
            #endregion
            #region 2 Layer || Ex 4
            case ExerciseType.TwoLayerEx4Transition:
            case ExerciseType.TwoLayerEx4Assessment:
            case ExerciseType.TwoLayerEx4Blocks:
                {
                    buttons.Shuffle();
                    List<ExerciseButton> firstLayerButtons = buttons.FindAll(x => x.buttonLayer == 1);
                    List<ExerciseButton> secondLayerButtons = buttons.FindAll(x => x.buttonLayer == 2);
                    firstLayerButtons.Shuffle();
                    secondLayerButtons.Shuffle();

                    float minWait = 0.7f;
                    if (type == ExerciseType.TwoLayerEx4Transition) minWait = 0.8f;

                    List<List<float>> soasGroups = new List<List<float>>
                    {
                        new List<float>() { minWait, 1.0f, 1.2f },
                        new List<float>() { 1.2f, minWait, 1.0f },
                        new List<float>() { 1.0f, 1.2f, minWait }
                    };

                    types = new List<ButtonType>() { ButtonType.Green, ButtonType.Green, ButtonType.Green };
                    List<ButtonType> typesWithRed = new List<ButtonType>() { ButtonType.Red, ButtonType.Green, ButtonType.Red, ButtonType.Green, ButtonType.Red, ButtonType.Green };
                    //typesWithRed.ShiftLeft(UnityEngine.Random.Range(1, 4));

                    int soaGroup = UnityEngine.Random.Range(0, 3);
                    for (int i = 0; i < buttons.Count; i++)
                    {

                        if (buttons[i].buttonID <= 4)
                        {
                            buttons[i].SetButtonTypesAndSOA(typesWithRed, soasGroups[soaGroup], shuffleSoas: false, shuffleTypes: false);
                            //typesWithRed.ShiftLeft();
                        }
                        else
                        {
                            buttons[i].SetButtonTypesAndSOA(types, soasGroups[soaGroup], shuffleSoas: false, shuffleTypes: false);
                        }
                        buttons[i].soaGroup = soaGroup + 1;

                        soaGroup++;
                        if (soaGroup == 3) soaGroup = 0;
                    }


                    List<List<ExerciseButton>> fourButtonGroupLayerOne = new List<List<ExerciseButton>>();
                    List<List<ExerciseButton>> fourButtonGroupLayerTwo = new List<List<ExerciseButton>>();

                    List<ExerciseButton> topFourButtonsLayerOne = firstLayerButtons.FindAll(x => x.buttonID <= 4);
                    List<ExerciseButton> topFourButtonsLayerTwo = secondLayerButtons.FindAll(x => x.buttonID <= 4);
                    topFourButtonsLayerOne.Shuffle();
                    topFourButtonsLayerTwo.Shuffle();

                    int groupsCount = type == ExerciseType.TwoLayerEx4Transition ? 4 : 4;
                    int buttonsCountPerGroup = type == ExerciseType.TwoLayerEx4Transition ? 2 : 4;

                    //Make 4 Groups of 4 Buttons
                    for (int i = 0; i < groupsCount; i++)
                    {
                        fourButtonGroupLayerOne.Add(new List<ExerciseButton>());
                        fourButtonGroupLayerTwo.Add(new List<ExerciseButton>());

                        for (int j = 0; j < buttonsCountPerGroup; j++)
                        {
                            if (firstLayerButtons.Count > 0)
                            {
                                fourButtonGroupLayerOne[i].Add(firstLayerButtons[0]);
                                firstLayerButtons.RemoveAt(0);
                            }
                            else Debug.Log("No more buttons , no button added");

                            if (secondLayerButtons.Count > 0)
                            {
                                fourButtonGroupLayerTwo[i].Add(secondLayerButtons[0]);
                                secondLayerButtons.RemoveAt(0);
                            }
                            else Debug.Log("No more buttons , no button added");

                            #region for now
                            ////Make top 4 buttons in same group have same Red Soa 
                            //typesWithRed = new List<ButtonType>() { ButtonType.Red, ButtonType.Green, ButtonType.Red, ButtonType.Green, ButtonType.Red, ButtonType.Green };
                            //if (fourButtonGroupLayerOne[i][fourButtonGroupLayerOne[i].Count - 1].buttonID <= 4)
                            //{
                            //    fourButtonGroupLayerOne[i][fourButtonGroupLayerOne[i].Count - 1].SetButtonTypesAndSOA(typesWithRed, shuffleTypes: false);
                            //}
                            //if (fourButtonGroupLayerTwo[i][fourButtonGroupLayerTwo[i].Count - 1].buttonID <= 4)
                            //{
                            //    fourButtonGroupLayerTwo[i][fourButtonGroupLayerTwo[i].Count - 1].SetButtonTypesAndSOA(typesWithRed, shuffleTypes: false);
                            //}
                            ////typesWithRed.ShiftLeft();
                            #endregion
                        }
                    }

                    fourButtonGroupLayerOne.Shuffle();
                    fourButtonGroupLayerTwo.Shuffle();


                    formedList = new List<ExerciseButton>();

                    int groupNotIncludedIndex = 0;
                    int transitionIndex = 0;

                    //Make 4 lists and put  them in one list
                    for (int i = 0; i < 4; i++)
                    {
                        tmpList = new List<ExerciseButton>();
                        //Add Top Layer , four buttons . Fixed for All exercise
                        if (type == ExerciseType.TwoLayerEx4Transition)
                        {
                            for (int j = 0; j < 2; j++)
                            {
                                tmpList.Add(topFourButtonsLayerOne[j + transitionIndex]);
                                tmpList.Add(topFourButtonsLayerTwo[j + transitionIndex]);
                            }
                            transitionIndex = transitionIndex == 0 ? 2 : 0;
                        }
                        else
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                tmpList.Add(topFourButtonsLayerOne[j]);
                                tmpList.Add(topFourButtonsLayerTwo[j]);
                            }
                        }

                        Debug.Log("4? " + tmpList.Count);

                        //Add Groups to the 
                        for (int j = 0; j < groupsCount; j++)
                        {
                            if (j != groupNotIncludedIndex)
                            {
                                for (int k = 0; k < buttonsCountPerGroup; k++)
                                {
                                    tmpList.Add(fourButtonGroupLayerOne[j][k]);
                                    tmpList.Add(fourButtonGroupLayerTwo[j][k]);
                                }
                            }
                        }


                        groupNotIncludedIndex++;

                        tmpList.Shuffle();

                        ////
                        //int cc = 0;
                        //for (int x = 0; x < tmpList.Count / 2; x++)
                        //{
                        //    if (tmpList[x].buttonID <= 4) cc++;
                        //}
                        //Debug.Log("64? " + tmpList.Count + " C " + cc);
                        ////


                        //Do SOA Shuffle here
                        #region Soa shuffle
                        //Shuffling TmpList

                        //Make a List of Lists of Groups
                        List<List<ExerciseButton>> soaGroupButtonsLists = new List<List<ExerciseButton>>();
                        soaGroupButtonsLists = new List<List<ExerciseButton>>();
                        for (int x = 0; x < 3; x++)
                        {
                            soaGroupButtonsLists.Add(new List<ExerciseButton>());
                        }

                        //Fill Soa Groups list of Lists
                        for (int x = 0; x < tmpList.Count; x++)
                        {
                            switch (tmpList[x].soaGroup)
                            {
                                case 1:
                                    soaGroupButtonsLists[0].Add(tmpList[x]);
                                    break;
                                case 2:
                                    soaGroupButtonsLists[1].Add(tmpList[x]);
                                    break;
                                case 3:
                                    soaGroupButtonsLists[2].Add(tmpList[x]);
                                    break;
                                default:
                                    soaGroupButtonsLists[0].Add(tmpList[x]); // If there is a problem , just add it in group 1 (0)
                                    break;
                            }
                        }

                        //Shuffle The Buttons inside the groups
                        for (int x = 0; x < soaGroupButtonsLists.Count; x++)
                        {
                            soaGroupButtonsLists[x].Shuffle();
                        }

                        //Clear the tmp list , we have it in the soa groups
                        tmpList = new List<ExerciseButton>();

                        List<int> groupList = new List<int>() { 0, 1, 2 };
                        groupList.Shuffle();
                        int currentInt = 0;
                        //int counts = 0;
                        //List<int> indexOrder = new List<int>();
                        //Debug.Log("Count \n" + soaGroupButtonsLists[0].Count + "\n" + soaGroupButtonsLists[1].Count + "\n" + soaGroupButtonsLists[2].Count);

                        //refill the list now again but shuffled
                        while (soaGroupButtonsLists[0].Count > 0 || soaGroupButtonsLists[1].Count > 0 || soaGroupButtonsLists[2].Count > 0)
                        {
                            //counts++; //Testing

                            currentInt = groupList[0];
                            groupList.RemoveAt(0);

                            //indexOrder.Add(currentInt); //Testing

                            if (groupList.Count == 0)
                            {
                                groupList = new List<int>() { 0, 1, 2 };
                                groupList.Shuffle();
                            }

                            if (soaGroupButtonsLists[currentInt].Count == 0)
                            {
                                continue;
                            }

                            tmpList.Add(soaGroupButtonsLists[currentInt][0]);
                            soaGroupButtonsLists[currentInt].RemoveAt(0);
                        }

                        // TmpList is Soa Shuffled
                        #endregion




                        if (formedList.Count > 0) // make sure ends dont match
                        {
                            if (formedList[formedList.Count - 1].soaGroup == tmpList[0].soaGroup)
                            {
                                tmpList.ShiftLeft();
                            }
                        }
                        formedList.AddRange(tmpList);
                    }

                    Debug.Log(formedList.Count);


                    //Manually set types for Transition mode , cuz kosomaha
                    if (type == ExerciseType.TwoLayerEx4Transition)
                    {
                        //Clear their types. will set the types one by one
                        //for (int i = 0; i < 4; i++)
                        //{
                        //    topFourButtonsLayerOne[i].ClearTypes();
                        //    topFourButtonsLayerTwo[i].ClearTypes();
                        //}
                        for (int i = 0; i < formedList.Count; i++)
                        {
                            formedList[i].ClearTypes();
                        }

                        int tIndex = 0;
                        for (int i = 0; i < formedList.Count; i++)
                        {
                            if (i % 16 == 0 && i > 0) tIndex = tIndex == 0 ? 2 : 0;

                            if (
                                (formedList[i].buttonID == topFourButtonsLayerOne[0 + tIndex].buttonID ||
                                formedList[i].buttonID == topFourButtonsLayerOne[1 + tIndex].buttonID) &&
                                formedList[i].buttonLayer == 1)
                            {
                                if (formedList[i].GetLastTypeRed() == false)
                                {
                                    formedList[i].AddToTypes(ButtonType.Red);
                                }
                                else
                                {
                                    formedList[i].AddToTypes(ButtonType.Green);
                                }

                            }
                            else if (
                                (formedList[i].buttonID == topFourButtonsLayerTwo[0 + tIndex].buttonID ||
                                formedList[i].buttonID == topFourButtonsLayerTwo[1 + tIndex].buttonID) &&
                                formedList[i].buttonLayer == 2)
                            {
                                if (formedList[i].GetLastTypeRed() == false)
                                {
                                    formedList[i].AddToTypes(ButtonType.Red);
                                }
                                else
                                {
                                    formedList[i].AddToTypes(ButtonType.Green);
                                }
                            }
                            else
                            {
                                formedList[i].AddToTypes(ButtonType.Green);
                            }
                        }
                    }

                    //Add the formed list to the active buttons
                    activeButtons = new List<GameObject>();
                    for (int i = 0; i < formedList.Count; i++)
                    {
                        activeButtons.Add(formedList[i].gameObject);
                    }

                    break;
                }
                #endregion
        }
    }


    private void ShuffleActiveButtonsBySOA()
    {
        if (activeButtons.Count == 0) return;


        #region Soa shuffle
        //Shuffling TmpList

        List<ExerciseButton> tmpList = new List<ExerciseButton>(buttons);

        //Make a List of Lists of Groups
        List<List<ExerciseButton>> soaGroupButtonsLists = new List<List<ExerciseButton>>();
        soaGroupButtonsLists = new List<List<ExerciseButton>>();
        for (int i = 0; i < 3; i++)
        {
            soaGroupButtonsLists.Add(new List<ExerciseButton>());
        }

        //Fill Soa Groups list of Lists
        for (int i = 0; i < tmpList.Count; i++)
        {
            switch (tmpList[i].soaGroup)
            {
                case 1:
                    soaGroupButtonsLists[0].Add(tmpList[i]);
                    break;
                case 2:
                    soaGroupButtonsLists[1].Add(tmpList[i]);
                    break;
                case 3:
                    soaGroupButtonsLists[2].Add(tmpList[i]);
                    break;
                default:
                    soaGroupButtonsLists[0].Add(tmpList[i]); // If there is a problem , just add it in group 1 (0)
                    break;
            }
        }

        //Shuffle The Buttons inside the groups
        for (int i = 0; i < soaGroupButtonsLists.Count; i++)
        {
            soaGroupButtonsLists[i].Shuffle();
        }

        //Clear the tmp list , we have it in the soa groups
        tmpList = new List<ExerciseButton>();

        List<int> groupList = new List<int>() { 0, 1, 2 };
        groupList.Shuffle();
        int currentInt;

        //refill the list now again but shuffled
        while (soaGroupButtonsLists[0].Count > 0 || soaGroupButtonsLists[1].Count > 0 || soaGroupButtonsLists[2].Count > 0)
        {

            currentInt = groupList[0];
            groupList.RemoveAt(0);


            if (groupList.Count == 0)
            {
                groupList = new List<int>() { 0, 1, 2 };
                groupList.Shuffle();
            }

            if (soaGroupButtonsLists[currentInt].Count == 0)
            {
                continue;
            }

            tmpList.Add(soaGroupButtonsLists[currentInt][0]);
            soaGroupButtonsLists[currentInt].RemoveAt(0);
        }

        // TmpList is Soa Shuffled
        #endregion

        activeButtons = new List<GameObject>();
        for (int i = 0; i < tmpList.Count; i++)
        {
            activeButtons.Add(tmpList[i].gameObject);
        }

        #region old
        //float lastusedSOA = activeButtons[activeButtons.Count - 1].GetComponent<ExerciseButton>().delayValues[0];
        ////Debug.Log("Last SOA "+ lastusedSOA);

        //List<float> soasUsed = activeButtons[0].GetComponent<ExerciseButton>().delayValues;
        //List<List<GameObject>> listoflistsofButtons = new List<List<GameObject>>()
        //{
        //    activeButtons.FindAll(x => x.GetComponent<ExerciseButton>().delayValues[0] == soasUsed[0]),
        //    activeButtons.FindAll(x => x.GetComponent<ExerciseButton>().delayValues[0] == soasUsed[1]),
        //    activeButtons.FindAll(x => x.GetComponent<ExerciseButton>().delayValues[0] == soasUsed[2])
        //};

        //int buttonsCount = activeButtons.Count;
        //activeButtons = new List<GameObject>();
        //List<int> randomingList = new List<int>();
        //for (int i = 0; i < listoflistsofButtons.Count; i++)
        //{
        //    if (listoflistsofButtons[i] != null && listoflistsofButtons[i].Count > 0) randomingList.Add(i);
        //}
        //randomingList.Shuffle();

        //if (listoflistsofButtons[randomingList[0]][0].GetComponent<ExerciseButton>().delayValues[0] == lastusedSOA)
        //{
        //    //Debug.Log("happened here: lastusedSOA "+ lastusedSOA+" button SOA "+ listoflistsofButtons[randomingList[0]][0].GetComponent<ExerciseButton>().delayValues[0]);
        //    randomingList.ShiftLeft();
        //}


        //int usedRandom;
        //for (int i = 0; i < buttonsCount; i++)
        //{
        //    usedRandom = randomingList[0];

        //    activeButtons.Add(listoflistsofButtons[randomingList[0]][0]);
        //    listoflistsofButtons[randomingList[0]].RemoveAt(0);
        //    randomingList.RemoveAt(0);

        //    if(randomingList.Count == 0)
        //    {
        //        for (int j = 0; j < listoflistsofButtons.Count; j++)
        //        {
        //            if (listoflistsofButtons[j].Count > 0) randomingList.Add(j);
        //        }
        //        randomingList.Shuffle();

        //        if (randomingList.Count > 0 )
        //        {
        //            if(usedRandom == randomingList[0])
        //            {
        //                //Debug.Log("happened");
        //                randomingList = randomingList.ShiftLeft(1);
        //            }
        //        }
        //    }


        //}
        #endregion

    }


    private IEnumerator SwitchPosition(bool startPosition = false, bool noDelay = false, float delay = 0.75f, bool keepSideValue = false)
    {



        if (noDelay)
        {
            yield return new WaitForSeconds(0.1f);
        }
        else
        {
            yield return new WaitForSeconds(delay);
        }

        //LeanTween.cancel(positionParent.gameObject); // old
        LeanTween.cancelAll();
        if (startPosition)
        {
            // Debug.Log("Start Position");
            onTheRight = false;
        }

        if (onTheRight)
        {
            //Debug.Log("Switched position to the Left");
            onTheRight = false;
            if (keepSideValue) onTheRight = true;

            leftSidePosition.z = positionParent.transform.position.z;
            positionParent.LeanMove(leftSidePosition, 0.8f);
        }
        else
        {
            //Debug.Log("Switched position to the Right");
            onTheRight = true;
            if (keepSideValue) onTheRight = false;
            rightSidePosition.z = positionParent.transform.position.z;

            positionParent.LeanMove(rightSidePosition, 0.8f);
        }
    }






    private void UpdateUI()
    {

        hitsFromSeries = Serieshits[0].hits.ToString();
        missFromSeries = Serieshits[0].omissions.ToString();
        anticipationFromSeries = Serieshits[0].anticipations.ToString();
        errorsFromSeries = Serieshits[0].errors.ToString();

        for (int i = 1; i < Serieshits.Count; i++)
        {
            hitsFromSeries += " - " + Serieshits[i].hits;
            missFromSeries += " - " + Serieshits[i].omissions;
            anticipationFromSeries += " - " + Serieshits[i].anticipations;
            errorsFromSeries += " - " + Serieshits[i].errors;
        }



        hitCount.text = "Hits: " + hitsFromSeries;
        UIHitCount.text = "Hits: " + hitsFromSeries;

        missCount.text = "Miss: " + missFromSeries;
        UIMissCount.text = "Miss: " + missFromSeries;

        anticiapationCount.text = "Anticipation: " + anticipationFromSeries;
        UIAnticiapationCount.text = "Anticipation: " + anticipationFromSeries;

        wrongCount.text = "Wrong: " + errorsFromSeries;
        UIWrongCount.text = "Wrong: " + errorsFromSeries;

        averageReactionText.text = "Average Reaction Time: " + (int)(AverageReaction * 1000);
        UIAverageReaction.text = averageReactionText.text;
    }

    private void SetUIBoxSize()
    {

        switch (type)
        {
            case ExerciseType.OneLayerEx1Assessment:
            case ExerciseType.OneLayerEx1Blocks:
            case ExerciseType.OneLayerEx1Transition:
            case ExerciseType.OneLayerEx2Assessment:
            case ExerciseType.OneLayerEx2Blocks:
            case ExerciseType.TwoLayerEx3Assessment:
            case ExerciseType.TwoLayerEx3Transition:
            case ExerciseType.TwoLayerEx4Transition:
                UIExcerciseName.rectTransform.sizeDelta = new Vector2(250, UIExcerciseName.rectTransform.sizeDelta.y);
                break;
            case ExerciseType.TwoLayerEx3Blocks:
            case ExerciseType.TwoLayerEx4Blocks:
            case ExerciseType.TwoLayerEx4Assessment:
                UIExcerciseName.rectTransform.sizeDelta = new Vector2(300, UIExcerciseName.rectTransform.sizeDelta.y);
                break;
            default:
                break;
        }
    }



    public void PlayNextButton()
    {
        HitsCounter++;

        //UPDATE UI
        UpdateUI();

        if (gameEnded)
        {
            StartCoroutine(GameEndedWait());
            //GameEnded();
        }
        else if (NeedPause)
        {
            PausePlay();
        }
        else
        {
            if (!gameEnded) StartCoroutine(DelayedPlayedNextButton(0));
        }



    }

    IEnumerator DelayedPlayedNextButton(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        //Close it first to wait for SOA
        if (currentButton != null)
        {
            currentButton.GetComponent<ExerciseButton>().Active = false;
        }

        if (!gamePaused)
        {

            GameObject button;

            if (activeButtons.Count > 0)
            {
                button = activeButtons[0];
            }
            else
            {
                Debug.Log("Reused List");
                //activeButtons.Clear();
                activeButtons = new List<GameObject>(usedButtons);

                //activeButtons.Shuffle();
                //ShuffleActiveButtonsBySOA();
                SetButtonsList();

                usedButtons = new List<GameObject>();
                button = activeButtons[0];
            }

            currentButton = button.GetComponent<ButtonBehaviour>();

            int id = currentButton.gameObject.GetComponent<ExerciseButton>().buttonID;

            SavedData.Instance.targetPosition.Add(/*17*/ /*((16*2) + 1) -*/ id);

            //Start the Button by setting Active
            currentButton.GetComponent<ExerciseButton>().Active = true;

            usedButtons.Add(button);
            activeButtons.Remove(button);
        }
        UpdateUI();


    }




    private AudioClip pausingAudioClip;
    private void PausePlay()
    {
        if (gameEnded) return;

        time = pauseTime;

        ////Select Pause Time AudioClip
        pausingAudioClip = PauseAudio30Sec;
        if (pauseTime == 20) pausingAudioClip = PauseAudio20Sec;
        else if (pauseTime == 30) pausingAudioClip = PauseAudio30Sec;
        else if (pauseTime == 40) pausingAudioClip = PauseAudio40Sec;
        else if (pauseTime == 90) pausingAudioClip = PauseAudio90Sec;
        ////

        StartCoroutine(StartAfter(pauseTime - 1, Resuming));
        pauseWaitTimeText.gameObject.SetActive(true);

        currentButton.GetComponent<ExerciseButton>().Active = false;

        gamePaused = true;
        audioSource.clip = pausingAudioClip;
        audioSource.Stop();
        audioSource.Play();
    }


    public void ButtonPressed(ButtonType type, float elapsedTime)
    {
        if (exerciseEnded) return;


        if (type == ButtonType.Green)
        {
            //serieshits[serieshits.Count - 1].hits++;
            //serieshits[serieshits.Count - 1].correctHitGreen++;
            if (elapsedTime > .1f)
            {
                buttonPressedCounter += 1;
                Serieshits[Serieshits.Count - 1].ScoreHit(type);
                reactionsTimesOfPressed.Add(elapsedTime);
            }
            else
            {
                buttonAnticipationCounter += 1;
                Serieshits[Serieshits.Count - 1].anticipations++;
            }

        }
        else
        if (type == ButtonType.Red)
        {
            wrongButtonCounter += 1;
            Serieshits[Serieshits.Count - 1].errors++;
        }
        UpdateUI();

        //Save the Side of Court
        if (onTheRight) SavedData.Instance.CourtSide.Add("Rs");
        else SavedData.Instance.CourtSide.Add("Ls");

        //SavedData.Instance.CourtSide.Add(courtSide);

        PlayNextButton();
    }

    public void ButtonNotPressed(ButtonType type)
    {
        //Debug.Log(" Button not pressed " + lifetime);

        SavedData.Instance.HandType.Add("None");



        if (type == ButtonType.Green)
        {
            buttonMissedCounter += 1;
            Serieshits[Serieshits.Count - 1].omissions++;
        }

        if (type == ButtonType.Red)
        {
            buttonPressedCounter += 1;
            Serieshits[Serieshits.Count - 1].ScoreHit(type);

            //serieshits[serieshits.Count - 1].hits++;
            //serieshits[serieshits.Count - 1].correctHitRed++;
        }

        //Save the Side of Court
        if (onTheRight) SavedData.Instance.CourtSide.Add("Rs");
        else SavedData.Instance.CourtSide.Add("Ls");
        //SavedData.Instance.CourtSide.Add(courtSide);

        UpdateUI();

    }
    //



    public void ClearTimer()
    {
        timeText.gameObject.GetComponent<Text>().text = "";
        pauseWaitTimeText.gameObject.SetActive(false);
    }

    private void SetEndingAndExerciseUIName()
    {

        switch (type)
        {
            case ExerciseType.OneLayerEx1Assessment:
                currentExcerciseName = "One Layer, " + currentPanelSize + ",  Exercise 1: Assessment";
                maxCounts = 16 * 3;
                break;
            case ExerciseType.OneLayerEx1Blocks:
                currentExcerciseName = "One Layer, " + currentPanelSize + ",  Exercise 1: " + blocksCount + " Block";
                if (blocksCount > 1) currentExcerciseName += "s";

                maxCounts = 16 * 3 * blocksCount;
                break;
            case ExerciseType.OneLayerEx1Transition:
                currentExcerciseName = "One Layer, " + currentPanelSize + ",  Exercise 1: Transition";
                maxCounts = 24 * 3; //was * 2
                break;
            case ExerciseType.OneLayerEx2Assessment:
                currentExcerciseName = "One Layer, " + currentPanelSize + ",  Exercise 2: Assessment";
                maxCounts = (16 * 3) + 12;
                break;
            case ExerciseType.OneLayerEx2Blocks:
                currentExcerciseName = "One Layer, " + currentPanelSize + ",  Exercise 2: " + blocksCount + " Block";
                if (blocksCount > 1) currentExcerciseName += "s";

                maxCounts = blocksCount * (60);
                //Debug.Log(maxCounts);
                break;
            case ExerciseType.OneLayerEx2Transition:
                currentExcerciseName = "One Layer, " + currentPanelSize + ",  Exercise 2: Transition";
                maxCounts = 8 * 4;
                break;
            case ExerciseType.TwoLayerEx3Assessment:
                currentExcerciseName = "Two Layer, " + currentPanelSize + ",  Exercise 3: Assessment";
                maxCounts = 16 * 3 * 2;
                break;
            case ExerciseType.TwoLayerEx3Transition:
                currentExcerciseName = "Two Layer, " + currentPanelSize + ",  Exercise 3: Transition";
                maxCounts = 8 * 3 * 3; // was * 2
                break;
            case ExerciseType.TwoLayerEx3Blocks:
                currentExcerciseName = "Two Layer, " + currentPanelSize + ",  Exercise 3: " + blocksCount + " Block";
                if (blocksCount > 1) currentExcerciseName += "s";

                maxCounts = 16 * 3 * 2 * blocksCount;
                break;
            case ExerciseType.TwoLayerEx4Assessment:
                currentExcerciseName = "Two Layer, " + currentPanelSize + ",  Exercise 4: Assessment";
                maxCounts = 32 * 4;

                break;
            case ExerciseType.TwoLayerEx4Transition:
                currentExcerciseName = "Two Layer, " + currentPanelSize + ",  Exercise 4: Transition";
                maxCounts = 32 * 3; // was *2

                break;
            case ExerciseType.TwoLayerEx4Blocks:
                currentExcerciseName = "Two Layer, " + currentPanelSize + ",  Exercise 4: " + blocksCount + " Block";
                if (blocksCount > 1) currentExcerciseName += "s";

                maxCounts = 32 * 4 * blocksCount;
                break;
            default:
                break;
        }
    }

    public UnityEvent exEnd;
    public IEnumerator GameEndedWait()
    {
        //keep last button On (reenable it)
        //if (currentButton != null)
        //{
        //    currentButton.GetComponent<ExerciseButton>().Flash();
        //}


        yield return new WaitForSeconds(0.2f);

        audioSource.clip = EndeddAudio;
        audioSource.Stop();
        audioSource.Play();
        setControlState(true);
        yield return new WaitForSeconds(3);
        exEnd.Invoke();

        GameEnded();
    }
    public void GameEnded()
    {
        StopAllCoroutines();
        //Save the play
        SavedData.Instance._savedHitCount = buttonPressedCounter;
        SavedData.Instance._savedMissedCount = buttonMissedCounter;
        SavedData.Instance._savedHitCounterTotal = buttonPressedCounter + buttonMissedCounter;
        SavedData.Instance.averageReactionTime = AverageReaction;


        //Close all buttons
        //for (int i = 0; i < ExamButtons.Length; i++)
        //{
        //    ExamButtons[i].GetComponent<ExerciseButton>().StopAllCoroutines();
        //    ExamButtons[i].GetComponent<ExerciseButton>().Active = false;
        //    ExamButtons[i].GetComponent<ExerciseButton>().Blackout();
        //}
        for (int i = 0; i < this.buttonsList.Count; i++)
        {
            this.buttonsList[i].GetComponent<ExerciseButton>().StopAllCoroutines();
            this.buttonsList[i].GetComponent<ExerciseButton>().Active = false;
            this.buttonsList[i].GetComponent<ExerciseButton>().Blackout();
        }


        //if (type == ExerciseType.Timed || 
        //    type == ExerciseType.Counter16x6Ex4Timed || 
        //    type == ExerciseType.CounterTwoMinuter || 
        //    type == ExerciseType.CounterTwoMinuterRedGreen)
        //{
        //    SavedData.instance.reactionTime.Add(0);
        //}
        //else if (type == ExerciseType.Counter16x6TimeCalculated)
        //{
        //    SavedData.instance.timeTaken = timeTaken;
        //}

        //Switch back to position
        //StartCoroutine(SwitchPosition(true));

        //FOR NOW
        //if ( type != ExerciseType.TwoLayerEx3Transition &&
        //    type != ExerciseType.TwoLayerEx4Transition)
        //{
        SavedData.Instance.StoreData();
        //}

        HitsCounter = 0;
        //audioSource.clip = EndeddAudio;

        //audioSource.Stop();
        //audioSource.Play();

        //Debug.Log("ended audio");
        exerciseEnded = true;
        started = false;


        TimerObject.StopTimer();
        //TimerObject.timeScoreCounter = 0;

        currentButton.GetComponent<ExerciseButton>().Active = false;
        currentButton.GetComponent<ExerciseButton>().Blackout();


        #region Hi It's my shit again
        StopAllCoroutines();

        //buttonMissedCounter = 0;
        //buttonPressedCounter = 0;
        #endregion
    }

    public void GameExited()
    {
        HitsCounter = 0;


        if (!exerciseEnded)
        {
            audioSource.clip = CancelledAudio;
            audioSource.Stop();
            audioSource.Play();
            setControlState(true);

        }
        DisableUIScore();

        exerciseEnded = true;
        started = false;
        //currentButton.GetComponent<MaterialChanger>().SetOffMaterial();
        if (currentButton != null)
        {
            currentButton.GetComponent<ExerciseButton>().Active = false;
            currentButton.GetComponent<ExerciseButton>().Blackout();
        }


        StopAllCoroutines();
        TimerObject.gameObject.GetComponent<Timer>().TimeEnded();

        //Switch back to position
        // StartCoroutine(SwitchPosition(true));
    }

    public void DisableUIScore()
    {
        excerciseName.text = "";
        hitCount.text = "";
        missCount.text = "";
        wrongCount.text = "";
        anticiapationCount.text = "";

        UIExcerciseName.text = "";
        UIHitCount.text = "";
        UIMissCount.text = "";
        UIWrongCount.text = "";
        UIAnticiapationCount.text = "";

        UIExcerciseName.transform.parent.gameObject.SetActive(false);
        excerciseName.transform.parent.gameObject.SetActive(false);
        pauseWaitTimeText.gameObject.GetComponent<Text>().text = "";
        timeText.gameObject.GetComponent<Text>().text = "";

        UIScreenTimer.gameObject.GetComponent<Text>().text = "";
    }

    private IEnumerator StartGameDelay()
    {
        yield return new WaitForSeconds(3);
        StartExercise();
    }

    public IEnumerator StartAfter(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        audioSource.clip = startedAudio;
        audioSource.Stop();
        audioSource.Play();
        action();
        setControlState(false);
    }



    public IEnumerator DoAfter(float seconds, Action action)
    {
        if (seconds <= 0)
        {
            yield return new WaitForEndOfFrame();
        }
        else
        {
            yield return new WaitForSeconds(seconds);
        }

        action();
    }

    public void Resuming()
    {
        gamePaused = false;
        audioSource.clip = ResumedAudio;
        audioSource.Stop();
        audioSource.Play();
        StartCoroutine(DelayedPlayedNextButton(1));
        pauseWaitTimeText.gameObject.SetActive(false);


    }

    public void QuittingApplication()
    {
        audioSource.clip = ExitedAudio;
        audioSource.Stop();
        audioSource.Play();
        StartCoroutine(TargetGeneration.Instance.StartAfter(5, Exiting));
        pauseWaitTimeText.gameObject.SetActive(false);
    }

    public void Exiting()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void Update()
    {
        time -= Time.deltaTime;
        pauseWaitTimeText.text = "00:" + time.ToString("00");
        lifetime += Time.deltaTime;
        debuger.text = lifetime.ToString();
        if (!started) return;
        // TimerBtngaga

        #region Old Timer Stuff
        //NEW
        //if(TimerObject.timeScoreCounter < 60)
        //{
        //    hitOne = buttonPressedCounter;
        //    anticipationOne = buttonAnticipationCounter;
        //    errorOne = wrongButtonCounter;
        //    missOne = buttonMissedCounter;

        //    hitTwo = 0;
        //    anticipationTwo = 0;
        //    errorTwo = 0;
        //    missTwo = 0;

        //    hitThree = 0;
        //    anticipationThree = 0;
        //    errorThree = 0;
        //    missThree = 0;
        //}
        //else if (TimerObject.timeScoreCounter < 120)
        //{

        //    hitTwo = buttonPressedCounter - hitOne;
        //    anticipationTwo = buttonAnticipationCounter - anticipationOne;
        //    errorTwo = wrongButtonCounter - errorOne;
        //    missTwo = buttonMissedCounter - missOne;

        //    hitThree = 0;
        //    anticipationThree = 0;
        //    errorThree = 0;
        //    missThree = 0;
        //}
        //else if (TimerObject.timeScoreCounter < 180)
        //{
        //    hitThree = buttonPressedCounter - hitTwo - hitOne;
        //    anticipationThree = buttonAnticipationCounter - anticipationOne - anticipationTwo;
        //    errorThree = wrongButtonCounter - errorTwo - errorOne;
        //    missThree = buttonMissedCounter - missTwo - missOne;
        //}

        //// END NEW
        //if (TimerObject.timeScoreCounter == 60)
        //{
        //    hitOne = buttonPressedCounter;
        //    anticipationOne = buttonAnticipationCounter;
        //    errorOne = wrongButtonCounter;
        //    missOne = buttonMissedCounter;

        //}

        //if (TimerObject.timeScoreCounter == 120)
        //{
        //    hitTwo = buttonPressedCounter - hitOne;
        //    anticipationTwo = buttonAnticipationCounter - anticipationOne;
        //    errorTwo = wrongButtonCounter - errorOne;
        //    missTwo = buttonMissedCounter - missOne;


        //}

        //if (TimerObject.timeScoreCounter == 180)
        //{
        //    hitThree = buttonPressedCounter - hitTwo - hitOne;
        //    anticipationThree = buttonAnticipationCounter - anticipationOne - anticipationTwo;
        //    errorThree = wrongButtonCounter - errorTwo - errorOne ;
        //    missThree = buttonMissedCounter - missTwo - missOne;
        //}
        //// NEW
        //if( TimerObject.timeScoreCounter != 60 &&
        //    TimerObject.timeScoreCounter != 120 )
        //    //&&
        //    //TimerObject.timeScoreCounter != 180)
        //UpdateUIHitAnticipationAndErrorsCounterText();
        #endregion
    }

    public void setControlState(bool active)
    {
        locationSetter.enabled = active;
        heightSetter.enabled = active;
    }
    public void SetPanelSize(int size)
    {
        switch (size)
        {
            case 1:
                currentPanelSize = PanelSize.S;
                break;
            case 2:
                currentPanelSize = PanelSize.M;
                break;
            case 3:
                currentPanelSize = PanelSize.L;
                break;
            default:
                break;
        }
    }
}