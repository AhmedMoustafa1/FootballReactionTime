using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ExerciseButton : MonoBehaviour
{

    private enum SoundType
    {
        Start,
        Answer,
        Tick
    }


    private static int redSoaIndex = 0;
    public static List<float> redSoa = new List<float>() { 0.7f, 1.2f, 1f };

    public int reds = 0;
    public int greens = 0;

    public int buttonID;
    public int buttonLayer = 1;
    public Material flash;

    public AudioClip startSound;
    public AudioClip answerSound;
    public AudioClip tickSound;

    private AudioSource aSource;
    private bool active = false;

    private MaterialChanger materialChanger;
    private Material red;
    private Material green;

    [SerializeField]
    private ButtonType type;

    public List<float> delayValues;

    public int soaGroup;
    //[HideInInspector]
    public List<ButtonType> types;

    //private int delayCounter = 0;
    public float elapsedTime;
    public int index = 0;
    private int soaIndex = 0;
    public float currentSoa = 0;
    public int ex4TimedIndex = 0;
    public Material orange;

    public GameObject frame;

    public void Start()
    {
        //SetButtonTypesAndSOA();
        red = Resources.Load<Material>("red");
        green = Resources.Load<Material>("green");
        this.materialChanger = this.GetComponent<MaterialChanger>();
        ExerciseManager.instance.restart.AddListener(OnRestart);
        this.GetComponent<Collider>().enabled = false;

        aSource = this.GetComponent<AudioSource>();

        if (startSound == null) startSound = Resources.Load<AudioClip>("StartSound");
        if (answerSound == null) answerSound = Resources.Load<AudioClip>("AnswerSound");
        if (tickSound == null) tickSound = Resources.Load<AudioClip>("tick");

    }


    public void SetButtonTypesAndSOA(List<ButtonType> buttonTypes = null, List<float> soas = null, bool shuffleTypes = true, bool shuffleSoas = true)
    {
        soaIndex = 0;
        redSoaIndex = 0;
        index = 0;

        if (soas == null || soas.Count == 0)
        {
            //delayValues = new List<float>() { 0.7f, 1f, 1.2f };
            shuffleSoas = false;
        }
        else
        {
            delayValues = new List<float>(soas);
        }

        if (buttonTypes == null || buttonTypes.Count == 0)
        {
            types = new List<ButtonType>() { ButtonType.Green };
            shuffleTypes = false;
        }
        else
        {
            types = new List<ButtonType>(buttonTypes);
        }


        if (shuffleTypes) types.Shuffle();
        if (shuffleSoas) delayValues.Shuffle();
        redSoa.Shuffle();
    }


    public void AddToTypes(ButtonType addedType)
    {
        if(types != null)
        {
            types.Add(addedType);
        }
    }

    public bool GetLastTypeRed()
    {
        if(types.Count > 0)
        {
            return types[types.Count - 1] == ButtonType.Red;
        } 
        else
        {
            return false;
        }
    }
    public void ClearTypes()
    {
        types = new List<ButtonType>();
    }

    public bool Active
    {

        get
        {
            return active;
        }
        set
        {
            if (!materialChanger)
                this.materialChanger = this.GetComponent<MaterialChanger>();

            if (value)
            {
                StartCoroutine(StartAfterSOA());
            }
            else
            {
                materialChanger.SetFlashMaterial();
                this.GetComponent<Collider>().enabled = false;
                elapsedTime = 0;
            }
            active = value;
            if (frame != null && active == false) frame.SetActive(active);

        }

    }

    public ButtonType Type
    {
        get
        {
            return type;
        }

        set
        {
            type = value;
            materialChanger.SetMaterial(value);
            //switch (value)
            //{
            //    case ButtonType.Green:
            //        materialChanger.SetMaterial(value);
            //        materialChanger.SetMaterial(green);
            //        break;
            //    case ButtonType.Red:
            //        materialChanger.SetMaterial(value);
            //        materialChanger.SetMaterial(red);
            //        break;
            //    default:
            //        break;
            //}
        }
    }

    ButtonType tempType;
    private IEnumerator StartAfterSOA()
    {
        if (types.Count > 0) tempType = types[index];
        else tempType = ButtonType.Green;

        currentSoa = (tempType == ButtonType.Green || (delayValues[soaIndex]  == 0) ) ? delayValues[soaIndex] : redSoa[redSoaIndex]/*0.1f*/;

        //if ((TargetGeneration.Instance.type == ExerciseType.Counter16x8redGreen ||
        //     TargetGeneration.Instance.type == ExerciseType.Counter16x8x3redGreen) && 
        //     delayValues[soaIndex] == 1)
        //{
        //    currentSoa = .1f;
        //}
        
        SavedData.Instance.delay.Add(currentSoa);


        if (!TargetGeneration.Instance.gamePaused)
        {

            //300ms before the soa to play the sound
            yield return new WaitForSeconds(Mathf.Max(0 , currentSoa - 0.3f));

            PlaySound(SoundType.Start);

            yield return new WaitForSeconds(0.3f);
            //Debug.Log("ET: " + elapsedTime);



            this.GetComponent<Collider>().enabled = true;

            if (frame != null) frame.SetActive(true);




            if (TargetGeneration.Instance.type == ExerciseType.OneLayerEx2Assessment ||
                TargetGeneration.Instance.type == ExerciseType.OneLayerEx2Blocks ||
                TargetGeneration.Instance.type == ExerciseType.TwoLayerEx4Transition ||
                TargetGeneration.Instance.type == ExerciseType.TwoLayerEx4Blocks ||
                TargetGeneration.Instance.type == ExerciseType.TwoLayerEx4Assessment)
            {
                Type = types[index];
                if (Type == ButtonType.Green) greens++; else reds++;
                //if (index == types.Count - 1) types.Shuffle();
            }
            else
            {
                Type = ButtonType.Green;
                greens++;
            }

            if (types.Count > 0)
            {
                index = (index + 1) % types.Count;
            }
            if (delayValues.Count > 0)
            {
                soaIndex = Type == ButtonType.Green ? (soaIndex + 1) % delayValues.Count : soaIndex;
            }
            if (redSoa.Count > 0)
            {
                float usedSOA = currentSoa;
                redSoaIndex = Type == ButtonType.Red ? (redSoaIndex + 1) % redSoa.Count : redSoaIndex;
                if (redSoaIndex == 0)
                {
                    redSoa.Shuffle();
                    if (redSoa[0] == currentSoa) redSoa = redSoa.ShiftLeft();
                }
            }

            //if (type == ButtonType.Green) greens++;
            //if (type == ButtonType.Red) reds++;

            elapsedTime = 0;
            StartCoroutine(ButtonOn());
        }
    }

    float delay = 1f;

    //On Time
    IEnumerator ButtonOn()
    {
        delay = 1f;
        switch (Type)
        {
            case ButtonType.Green:
                //if (TargetGeneration.Instance.type == ExerciseType.Counter16x8Ex4Assessment ||
                //    TargetGeneration.Instance.type == ExerciseType.Counter16x8Ex4Training ||
                //    TargetGeneration.Instance.type == ExerciseType.Counter16x6Ex4Timed)
                //    delay = 2f;
                //else
                switch (TargetGeneration.Instance.type)
                {
                    case ExerciseType.OneLayerEx1Assessment:
                    case ExerciseType.OneLayerEx1Blocks:
                    case ExerciseType.OneLayerEx2Assessment:
                    case ExerciseType.OneLayerEx2Blocks:

                        if (currentSoa < 1f)
                        {
                            delay = 0.6f;
                        }
                        else
                        {
                            delay = 1.3f;
                        }

                        break;
                    case ExerciseType.TwoLayerEx3Assessment:
                    case ExerciseType.TwoLayerEx3Blocks:
                    case ExerciseType.TwoLayerEx4Assessment:
                    case ExerciseType.TwoLayerEx4Blocks:

                        if (currentSoa < 1f)
                        {
                            delay = 0.7f;
                        }
                        else
                        {
                            delay = 1.3f;
                        }

                        break;
                    case ExerciseType.TwoLayerEx3Transition:
                    case ExerciseType.TwoLayerEx4Transition:

                        if (currentSoa < 1f)
                        {
                            delay = 0.8f;
                        }
                        else
                        {
                            delay = 1.2f;
                        }

                        break;
                    case ExerciseType.OneLayerEx1Transition:
                    case ExerciseType.OneLayerEx2Transition:

                        if (currentSoa < 1f)
                        {
                            delay = 0.6f;
                        }
                        else
                        {
                            delay = 1.2f;
                        }

                        break;
                    default:
                        break;
                }

                break;
            case ButtonType.Red:
                //if (TargetGeneration.Instance.type == ExerciseType.Counter16x8Ex4Assessment ||
                //    TargetGeneration.Instance.type == ExerciseType.Counter16x8Ex4Training ||
                //    TargetGeneration.Instance.type == ExerciseType.Counter16x6Ex4Timed)
                //    delay = 1f;
                //else
                //    delay = 1f;
                delay = 0.8f;
                break;
            default:
                delay = 1f;
                break;
        }


        //delay -= 0.003f;
        elapsedTime = 0;

        yield return new WaitForSeconds(delay);


       // Debug.Log("elapsedTime : " + elapsedTime + " - Delay : " + delay + " - soa : " + currentSoa);
        ButtonNotPressedExecution();

    }

    private void ButtonNotPressedExecution()
    {
        this.Active = false;
        SavedData.Instance.reactionTime.Add(0);

        if (!TargetGeneration.Instance.exerciseEnded)
        {

            TargetGeneration.Instance.ButtonNotPressed(Type);
            string buttonTypeColor;
            if (Type.ToString() == "Green")
            {
                buttonTypeColor = "Yellow";
            }
            else if (Type.ToString() == "Red")
            {
                buttonTypeColor = "Red";

            }
            else
            {
                buttonTypeColor = "none";
            }
            SavedData.Instance.ButtonType.Add(buttonTypeColor);
            SavedData.Instance.ButtonLayer.Add((buttonLayer == 1 ? "V" : "L"));


            TargetGeneration.Instance.PlayNextButton();

        }
    }

    private void PlaySound(SoundType soundname)
    {
        switch (soundname)
        {
            case SoundType.Start:
                aSource.clip = startSound;

                break;

            case SoundType.Answer:
                aSource.clip = answerSound;

                break;

            case SoundType.Tick:
                aSource.clip = tickSound;

                break;

            default:
                break;
        }
        aSource.pitch = 1;
        aSource.volume = 1;

        aSource.Stop();
        aSource.Play();
    }


    public void OnRestart()
    {
        this.materialChanger.SetFlashMaterial();
        this.GetComponent<Collider>().enabled = false;
        elapsedTime = 0;
    }

    public void ButtonPressed()
    {
        if (Active)
        {
            PlaySound(SoundType.Answer);

            float time = elapsedTime;
            if (elapsedTime > delay)
            {
                time = delay - Random.Range(0.001f,0.01f);
                //Debug.Log("elapsedTime = " + elapsedTime);
            }
            Active = false; //NEw
            
            Hands data = this.GetComponent<ButtonBehaviour>().lastHand;
            SavedData.Instance.HandType.Add(data.ToString());
            string buttonTypeColor;
            if (Type.ToString()=="Green")
            {
                buttonTypeColor = "Yellow";
            }
            else if (Type.ToString() == "Red")
            {
                buttonTypeColor = "Red";

            }
            else
            {
                buttonTypeColor = "none";
            }
            SavedData.Instance.ButtonType.Add(buttonTypeColor);
            SavedData.Instance.ButtonLayer.Add((buttonLayer == 1 ? "V" : "L"));
            SavedData.Instance.reactionTime.Add(time);



            TargetGeneration.Instance.ButtonPressed(Type, time);
            StopAllCoroutines();
        }
    }


    public void Blackout()
    {

        PlaySound(SoundType.Tick);

        //materialChanger.SetMaterial(orange);
        materialChanger.SetYellowMaterial();
    }

    public void SetOn()
    {
        materialChanger.SetYellowMaterial();
    }


    public void Flash()
    {
        StopAllCoroutines();
        StartCoroutine(FlachTwice());
    }

    IEnumerator FlachTwice()
    {
        materialChanger.SetFlashMaterial();
        //materialChanger.SetYellowMaterial();
        PlaySound(SoundType.Tick);

        yield return new WaitForSeconds(0.5f);

        materialChanger.SetYellowMaterial();
        //materialChanger.SetFlashMaterial();

        yield return new WaitForSeconds(0.5f);

        materialChanger.SetFlashMaterial();
        //materialChanger.SetYellowMaterial();

        PlaySound(SoundType.Tick);

        yield return new WaitForSeconds(0.5f);

        materialChanger.SetYellowMaterial();

        yield return new WaitForSeconds(0.5f);

        PlaySound(SoundType.Tick);
        materialChanger.SetFlashMaterial();
    }

    public void SetMaterialOn()
    {
        //materialChanger.SetOnMaterial();
        Type = Type; // New
    }

    public void SetFlashMaterial()
    {
        //materialChanger.SetMaterial(flash);
        materialChanger.SetFlashMaterial();
    }

    private void FixedUpdate()
    {
        if (active)
        {
            elapsedTime += Time.deltaTime;
        }
    }



}
