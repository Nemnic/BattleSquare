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


    [Header("VS AI Stuff")]
    [SerializeField] public bool is_VS_AI = false;
    
}
