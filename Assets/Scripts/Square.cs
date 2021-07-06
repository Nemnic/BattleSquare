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
    public bool isDone = false;

    [Header("Whos Turn")]
    [SerializeField] private Color colorDefault = new Color(178,178,178);
    [SerializeField] private Color colorBlue = new Color(4,15,79);
    [SerializeField] private Color colorOrange = new Color(245,161,29);

    private void Awake()
    {   
        // Set default to
        xPos = defaultValueForErrorCheck;
        yPos = defaultValueForErrorCheck;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = colorDefault;

        SetColor();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            gameObject.GetComponent<Renderer>().material.color = colorOrange;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            gameObject.GetComponent<Renderer>().material.color = colorBlue;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            gameObject.GetComponent<Renderer>().material.color = colorDefault;
        }

        //SetColor();
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
                if (Globals.instance.isBluesTurn)
                {
                    gameObject.GetComponent<Renderer>().material.color = colorBlue;
                }
                else
                {
                    gameObject.GetComponent<Renderer>().material.color = colorOrange;
                }

            }
        }
        

        //gameObject.GetComponent<Renderer>().material.color = new Color(0, 255, 0);
        //gameObject.GetComponent<Renderer>().material.color = new Color(4, 15, 79);

    }

    public void CheckIfDone()
    {

        if (CheckSquareStatus())
        {
            if (Globals.instance.isBluesTurn)
            {
                ScoreManager.instance.PointToBlue();
                gameObject.GetComponent<Renderer>().material.color = colorBlue;
            }
            else
            {
                ScoreManager.instance.PointToOrange();
                gameObject.GetComponent<Renderer>().material.color = colorOrange;
            }

            Globals.instance.squareDone = true;

            Debug.Log("CheckIfDone returned : true at :" + xPos +" , "+ yPos + " and addad a point");
        }
        else
        {
            Debug.Log("CheckIfDone returned : false at :" + xPos + " , " + yPos + " This square not done");

        }

    }

}
