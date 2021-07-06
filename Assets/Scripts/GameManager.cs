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

    public void ButtonClick(int xPos, int yPos, bool isVertical)
    {
        
        //Check if it finished a square
        SetSquareSideCheck(xPos, yPos, isVertical);

        if (!Globals.instance.squareDone)
        {
            NextTurn();  
        }

        Globals.instance.squareDone = false;

    }

    public void NextTurn()
    {
        Globals.instance.SwapTurn();
    }

    void SetSquareSideCheck(int xPos, int yPos, bool isVertical)
    {

        // Vertical lines
        if (isVertical)
        {

            foreach (var squareFromList in Globals.instance.squaresList)
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
            foreach (var squareFromList in Globals.instance.squaresList)
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
        squareFromList.GetComponent<Square>().CheckIfDone();
    }
    private static void SetIsClickedLeft(GameObject squareFromList)
    {
        squareFromList.GetComponent<Square>().isClicked_Left = true;
        squareFromList.GetComponent<Square>().CheckIfDone();

    }
    private static void SetIsClickedUp(GameObject squareFromList)
    {
        squareFromList.GetComponent<Square>().isClicked_Up = true;
        squareFromList.GetComponent<Square>().CheckIfDone();

    }
    private static void SetIsClickedDown(GameObject squareFromList)
    {
        squareFromList.GetComponent<Square>().isClicked_Down = true;
        squareFromList.GetComponent<Square>().CheckIfDone();

    }

}
