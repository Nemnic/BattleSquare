using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinScreenManger : MonoBehaviour
{
    #region Singleton
    public static WinScreenManger instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Warning! More then one instance of WinScreenManger found!");
            return;
        }

        instance = this;
    }

    #endregion

    [SerializeField] private TextMeshProUGUI textWinner;
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private GameObject finishedPanel;
    

    public void SetWinScreen()
    {
        // Calculate who won and Set Winner is Text
        SetWinner();

        // Set Score text with
        SetScore();

        // Display Game Finished Panel
        DisplayFinsishScreen();



    }


    private void SetWinner()
    {
        if (ScoreManager.instance.scoreBlue > ScoreManager.instance.scoreOrange)
        {
            textWinner.SetText("Winner is Blue");
        }
        else if (ScoreManager.instance.scoreBlue < ScoreManager.instance.scoreOrange)
        {
            textWinner.SetText("Winner is Orange");
        }
        else if (ScoreManager.instance.scoreBlue == ScoreManager.instance.scoreOrange)
        {
            textWinner.SetText("Its a DRAW!");
        }
        else
        {
            Debug.LogError("WTF, How could this happen ?");
        }

    }

    private void SetScore()
    {
        textScore.SetText("Blue got " + ScoreManager.instance.scoreBlue + " points vs Orange " + ScoreManager.instance.scoreOrange + " points");
    }

    private void DisplayFinsishScreen()
    {
        finishedPanel.SetActive(true);
    }
}
