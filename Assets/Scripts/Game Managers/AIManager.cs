using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AIManager : MonoBehaviour
{
    #region Singleton
    public static AIManager instance;

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

    //Debug check: keep track of AI move nr 
    [SerializeField] private int moveNr = 0;        // For Debuging

    [SerializeField] private int successrate = 90;

    private float timer = 0;

    public void ActivateTurn()          // Change name to "Do turn" ?
    {
        if (Globals.instance.isDelayOnAI)
        {
            timer += Time.deltaTime;

            if (timer > 0.5f)
            {
                DoTurn();
                timer = 0;
            }
        }
        else
        {
            DoTurn();
        }
    }

    private void DoTurn()
    {
        // Find and select a random aktiv line
        int listMax = GameManager.instance.lineList.Count;
        int linePicked = Random.Range(0, listMax);
        int randomChance = Random.Range(1, 101);


        if (listMax > 0)
        {
            int tempLine = GameManager.instance.PickLineFromHighValueForAI();

            if (tempLine != -1)         // if Line found with value 3 => will give point
            {
                if (successrate > randomChance)
                {
                    linePicked = tempLine;
                    Debug.Log("Random = " + randomChance + ", Picked used, ");
                }
                else
                {
                    Debug.Log("Random = " + randomChance + ", Random used, ");
                }
            }

            GameObject clickingObject = GameManager.instance.lineList[linePicked];

            moveNr++;
            Debug.Log("AI did a move! Move nr: " + moveNr + " , and used the nr: " + linePicked + " from LinePicked");

            // Aktivate that line
            clickingObject.GetComponent<LineHandler>().ButtonPressed();
        }
        else
        {
            Debug.LogError("AI Manager: No more list to click! Game should have not have activated this function at this point!");
        }
    }
}
