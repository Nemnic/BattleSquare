using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

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

    [Header("Check for Game Finished ")]
    [SerializeField] public int finishCount = 0;
    //[SerializeField] private bool showWinScreen = false;

    [Header("List and Square check")]
    public List<GameObject> squaresList = new List<GameObject>();
    public List<GameObject> lineList = new List<GameObject>();

    [SerializeField] public bool squareDone = false;


    #region Checks for Game Finished

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
            Debug.LogError("ERROR: in GameManager for line check;  WTF should never be under 0");
        }
    }

    public void ShowWinScreen()
    {
        WinScreenManger.instance.SetWinScreen();
    }

    #endregion

    #region List functions Add and remove

    public void AddSquareToList(GameObject square)
    {
        squaresList.Add(square);
    }

    public void RemoveSquareFromList(GameObject square)
    {
        squaresList.Remove(square);
    }

    public void AddLineToList(GameObject line)
    {
        lineList.Add(line);
    }

    public void RemoveLineFromList(GameObject line)
    {
        lineList.Remove(line);
    }

    #endregion

                                                        // Actual main loop 
    public void ButtonClick(int xPos, int yPos, bool isVertical)
    {
        
        //Check if it finished a square
        SetSquareSideCheck(xPos, yPos, isVertical);

        // If player did not finish a square next players turn
        if (!squareDone)
        {
            NextTurn();  
        }

        squareDone = false;

    }

    public void NextTurn()
    {
        TurnManager.instance.SwapTurn();
    }

    void SetSquareSideCheck(int xPos, int yPos, bool isVertical)
    {

        // Vertical lines
        if (isVertical)
        {

            foreach (var squareFromList in squaresList)
            {
                // Fix Right side square for leftcheck
                if (xPos == 0)
                {
                    // check same number
                    if (squareFromList.GetComponent<Square>().xPos == xPos && squareFromList.GetComponent<Square>().yPos == yPos)
                    {
                        SetIsClickedLeft(squareFromList);
                    }

                }
                else if (xPos == 6)     // TODO: change to a max variable incase we change size, no hardcoding!
                {
                    // Fix Left side square for rightcheck, Check one index less
                    if (squareFromList.GetComponent<Square>().xPos == (xPos - 1) && squareFromList.GetComponent<Square>().yPos == yPos)
                    {
                        SetIsClickedRight(squareFromList);
                    }
                }

                else
                {
                    // check same number
                    if (squareFromList.GetComponent<Square>().xPos == xPos && squareFromList.GetComponent<Square>().yPos == yPos)
                    {
                        SetIsClickedLeft(squareFromList);
                    }

                    // Fix Left side square for rightcheck, Check one index less
                    if (squareFromList.GetComponent<Square>().xPos == (xPos - 1) && squareFromList.GetComponent<Square>().yPos == yPos)
                    {
                        SetIsClickedRight(squareFromList);
                    }
                }
            }

        }
        // Horizontal lines
        else 
        {
            foreach (var squareFromList in squaresList)
            {
                // Fix bottom side square for topCheck
                if (yPos == 0)
                {
                    // Fix bottom side square with same number for check Up
                    if (squareFromList.GetComponent<Square>().xPos == xPos && squareFromList.GetComponent<Square>().yPos == yPos)
                    {
                        SetIsClickedUp(squareFromList);
                    }

                }
                else if (yPos == 6)     // TODO: change to a max variable incase we change size, no hardcoding!
                {
                    // Fix upper side square with one number less for downCheck
                    if (squareFromList.GetComponent<Square>().xPos == xPos && squareFromList.GetComponent<Square>().yPos == (yPos -1))
                    {
                        SetIsClickedDown(squareFromList);
                    }
                }

                else
                {
                    // Fix bottom side square with same number for check Up
                    if (squareFromList.GetComponent<Square>().xPos == xPos && squareFromList.GetComponent<Square>().yPos == yPos)
                    {
                        SetIsClickedUp(squareFromList);
                    }
                    // Fix upper side square with one number less for downCheck
                    if (squareFromList.GetComponent<Square>().xPos == xPos && squareFromList.GetComponent<Square>().yPos == (yPos - 1))
                    {
                        SetIsClickedDown(squareFromList);
                    }

                }
            }
        }
    }

    private static void SetIsClickedRight(GameObject squareFromList)
    {
        squareFromList.GetComponent<Square>().isClicked_Right = true;
        CheckDoneAndIncreaseSquareValue(squareFromList);
    }
    private static void SetIsClickedLeft(GameObject squareFromList)
    {
        squareFromList.GetComponent<Square>().isClicked_Left = true;
        CheckDoneAndIncreaseSquareValue(squareFromList);

    }
    private static void SetIsClickedUp(GameObject squareFromList)
    {
        squareFromList.GetComponent<Square>().isClicked_Up = true;
        CheckDoneAndIncreaseSquareValue(squareFromList);

    }
    private static void SetIsClickedDown(GameObject squareFromList)
    {
        squareFromList.GetComponent<Square>().isClicked_Down = true;
        CheckDoneAndIncreaseSquareValue(squareFromList);
    }

    private static void CheckDoneAndIncreaseSquareValue(GameObject squareFromList)
    {
        squareFromList.GetComponent<Square>().squareValue++;
        squareFromList.GetComponent<Square>().CheckIfDone();
    }
}
