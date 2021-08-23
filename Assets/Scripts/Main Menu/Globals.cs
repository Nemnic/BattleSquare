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
            //Debug.LogWarning("Warning! More then one instance of Globals found!");
            Destroy(gameObject);
            //Debug.LogWarning("The new instace of Globals has been destroyed as only one should exist");

            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            Debug.Log("we created the Global");
        }

    }

    #endregion

    [Header("VS AI Stuff")]
    [SerializeField] public bool is_VS_AI = false;
    

    [Header("Color Theme")]
    [SerializeField] public Color colorDefault = new Color(178,178,178);
    [SerializeField] public Color colorBlue = new Color(4,15,79);
    [SerializeField] public Color colorOrange = new Color(245,161,29);

    [Header("Size")]
    [SerializeField] public int x_Squares = 5;
    [SerializeField] public int y_Squares = 5;
    [SerializeField, Tooltip("For Scaling. \nNot sure if using. But should. \nA float variable, Affect positioning, should affect size to")]
    public float squareSize = 2.0f;

    [Header("Settings")]
    [SerializeField] public bool IsShowingLastPressedLine = true;
    [SerializeField] public bool isDelayOnAI = true;
    [SerializeField] public bool isSoundsOn = true;
    [SerializeField] public bool isMusicOn = true;

    public void ToggleShowLastLinePressed()
    {
        IsShowingLastPressedLine = !IsShowingLastPressedLine;
    }

}
