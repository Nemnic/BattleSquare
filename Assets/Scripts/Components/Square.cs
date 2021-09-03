using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{

    [Header("Position")]
    [SerializeField] public int xPos;
    [SerializeField] public int yPos;

    [SerializeField, Space(5)] private int defaultValueForErrorCheck = -1 ;


    [Header("Side Checks")]
    public bool isClicked_Left = false;
    public bool isClicked_Right = false;
    public bool isClicked_Up = false;
    public bool isClicked_Down = false;
    public bool isDone = false;                                 // TODO : check if still wanted it to remain or implement it properly

    [SerializeField] public int squareValue = 0;

    // if we want scaling option later on
    //private bool doWeWannaScaleIt = false;

    private void Awake()
    {   
        // Set default to
        xPos = defaultValueForErrorCheck;
        yPos = defaultValueForErrorCheck;

        this.transform.SetParent(GameObject.Find("Squares").GetComponent<Transform>(), false);

        // Incase we want to scale  - Not in use atm    - if used might need to be recalculated
        //if (doWeWannaScaleIt)
        //{
        //    this.transform.localScale = new Vector2(Globals.instance.squareSize, 1);
        //}

        GameManager.instance.AddSquareToList(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = Globals.instance.colorDefault;

        SetColor();
    }

    public bool CheckSquareStatus()
    {

        if (isClicked_Left && isClicked_Right && isClicked_Up && isClicked_Down)
        {
            //isDone = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetColor()
    {
        if (!isDone)
        {
            if (CheckSquareStatus())
            {
                if (TurnManager.instance.isBluesTurn)
                {
                    gameObject.GetComponent<Renderer>().material.color = Globals.instance.colorBlue;
                }
                else
                {
                    gameObject.GetComponent<Renderer>().material.color = Globals.instance.colorOrange;
                }

            }
        }
    }

    public void CheckIfDone()
    {

        if (CheckSquareStatus())
        {
            SFX.instance.PlaySquareDone();

            if (TurnManager.instance.isBluesTurn)
            {
                ScoreManager.instance.PointToBlue();
                gameObject.GetComponent<Renderer>().material.color = Globals.instance.colorBlue;

                Debug.Log("CheckIfDone returned : true at :" + xPos + " , " + yPos + " and addad a point to Blue");
            }
            else
            {
                ScoreManager.instance.PointToOrange();
                gameObject.GetComponent<Renderer>().material.color = Globals.instance.colorOrange;

                Debug.Log("CheckIfDone returned : true at :" + xPos + " , " + yPos + " and addad a point to Orange");
            }

            GameManager.instance.squareDone = true;

        }
        else
        {
            //Debug.Log("CheckIfDone returned : false at :" + xPos + " , " + yPos + " This square not done");
        }

    }

}
