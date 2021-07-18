using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{

    #region Singleton
    public static Globals instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Warning! More then one instance of Globals found!");
            return;
        }

        instance = this;
    }

    #endregion
    [Header("Size")]
    [SerializeField] public int x_Squares = 5;
    [SerializeField] public int y_Squares = 5;
    [SerializeField, Tooltip("For Scaling. \nNot sure if using. But should. \nA float variable, Affect positioning, should affect size to")]
    public float squareSize = 2.0f;

    [Header("Check for Game Finished ")]
    [SerializeField] private int finishCount = 0;
    //[SerializeField] private bool showWinScreen = false;

    [Header("Whos turns stuff")]
    [SerializeField] public bool isBluesTurn = true;
    [SerializeField] private GameObject directionArrow;

    [Space(10)]
    public List<GameObject> squaresList = new List<GameObject>();

    [SerializeField] public bool squareDone = false;

    

    
    public void AddLineCount()
    {
        finishCount++;
    }

    public void RemoveLineCount()
    {
        finishCount--;
        if (finishCount == 0)
        {
            ShowWinScreen();
        }
        else if (finishCount < 0)
        {
            Debug.LogError("ERROR: in Globals for line check;  WTF should never be under 0");
        }
    }

    public void ShowWinScreen()
    {
        WinScreenManger.instance.SetWinScreen();
    }


    public void Add(GameObject square)
    {
        squaresList.Add(square);
    }

    public void Remove(GameObject square)
    {
        squaresList.Remove(square);
    }

    public void SwapTurn()
    {
        isBluesTurn = !isBluesTurn;
    }

    private void Update()
    {
        SetPointer();
    }

    public void SetPointer()
    {
        if (isBluesTurn)
        {
            directionArrow.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else
        {
            directionArrow.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }
}
