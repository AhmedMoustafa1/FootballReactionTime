using Kandooz;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelBuilder : MonoBehaviour {

    public GameObject panelButtonMale;
    public GameObject panelButtonFemale;
    public GameObject ExeriseManager;
    public GameObject startPlayingButton;

    [Tooltip("Order: 0 -> Front , 1 -> Back")]
    public GameObject[] frameObjects;
    public Transform parent;
    public float spacingX = 0.540f; //was 0.15f
    public float spacingY = 0.273f;
    public float midSpacingGapX = 0;
    public float midSpacingGapY = 0;
    public int rowsCount = 4;
    public int columnsCount = 4;

    [SerializeField] private Pooler pooler;

    private float startX = 0;
    private float startY = 0;
    private float currentX = 0;
    private float currentY = 0;

    public int buttonID=0;
    public IntField gender;
    //public List<GameObject> ButtonsofTarget = new List<GameObject>();

    List<GameObject> list;
    public List<GameObject> Build(int layersCount = 1, float zSpacing = 0.4f, bool halfCount = false)
    {
        if (pooler == null) pooler = this.GetComponent<Pooler>();
        if (pooler == null) 
        {
            pooler = this.gameObject.AddComponent<Pooler>();
            pooler.ParentObject = parent.gameObject;
        }

        if(pooler.PooledObject == null)
        {
            //if (gender == 1)
            //{
            //    pooler.PooledObject = panelButtonFemale;
            //}
            //if (gender == 2)
            //{
            //    pooler.PooledObject = panelButtonMale;

            //}
            pooler.PooledObject = gender.Value == 1 ? panelButtonFemale : panelButtonMale;
        }
            pooler.SetAllOff();

        //Give the target generator the start game button for the panels
        if (startPlayingButton != null) TargetGeneration.Instance.startPlayingButton = startPlayingButton;

        list = new List<GameObject>();

        for (int i = 0; i < frameObjects.Length; i++)
        {
            frameObjects[i].SetActive(false);
        }


        for (int x = 0; x < layersCount; x++)
        {
            buttonID = 0;
            int n = (rowsCount / 2) - 1;
            float w = 0.07f/2 ;//panelButton.transform.localScale.x;
            float stepX = (w + spacingX);
            float stepY = (w + spacingY);
            string buttonLayer = x == 0 ? "V" : "L";
            if (rowsCount % 2 == 0) startX = ((spacingX / 2) + (w / 2)) + ((spacingX + w) * n) + (midSpacingGapX / 2);
            else
            {
                n = (rowsCount - 1) / 2;
                startX = (w + spacingX) * n + (midSpacingGapX / 2);
            }

            if (columnsCount % 2 == 0) startY = ((spacingY / 2) + (w / 2)) + ((spacingY + w) * n) + (midSpacingGapY / 2);
            else
            {
                n = (columnsCount - 1) / 2;
                startY = (w + spacingY) * n + (midSpacingGapY / 2);
            }

            currentX = startX; // was -startX
            currentY = startY; // was -startX

            for (int i = 0; i < rowsCount; i++)
            {
                for (int j = 0; j < columnsCount; j++)
                {
                    GameObject newButton = pooler.Get();// Instantiate(panelButton, parent);
                    newButton.transform.localPosition = new Vector3(currentX, currentY, -zSpacing * x);
                    list.Add(newButton);
                    buttonID++;

                    newButton.gameObject.GetComponent<ExerciseButton>().buttonID = buttonID;
                    newButton.gameObject.GetComponent<ExerciseButton>().buttonLayer = x + 1;

                    if (frameObjects.Length > x && layersCount > 1) newButton.gameObject.GetComponent<ExerciseButton>().frame = frameObjects[x];
                    else
                    {
                        newButton.gameObject.GetComponent<ExerciseButton>().frame = null;
                    }


                    newButton.gameObject.name = "Exercise Button " + buttonID.ToString() + " " + buttonLayer;

                    currentX -= stepX; // was +=
                    if (j == (columnsCount / 2) - 1 && columnsCount % 2 == 0) currentX -= midSpacingGapX; 
                }
                currentX = startX; // was -startX
                currentY -= stepY; // was +=
                if (halfCount && i == (rowsCount / 2) - 1) break;
                if (i == (rowsCount / 2) - 1 && rowsCount % 2 == 0) currentY -= midSpacingGapY;
            }

            //Turn on a frame
            //if(frameObjects.Length > x) frameObjects[x].SetActive(true);

        }
           
       // }
        return list;

    }


    

}
