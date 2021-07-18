using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    #region Singleton
    public static ScoreManager instance;

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


    [SerializeField] public int scoreBlue;
    [SerializeField] public int scoreOrange;

    [SerializeField] private TextMeshProUGUI textBlueScore;
    [SerializeField] private TextMeshProUGUI textOrangeScore;

    // Start is called before the first frame update
    void Start()
    {
        ResetScore();

        UpdateBlueScore();
        UpdateOrangeScore();
    }

    // Update is called once per frame
    void Update()
    {
        // Test of score count
        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //    PointToBlue();
        //}
        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    PointToOrange();
        //}
    }

    private void ResetScore()
    {
        scoreBlue = 0;
        scoreOrange = 0;
    }

    public void PointToBlue()
    {
        scoreBlue++;
        UpdateBlueScore();
    }

    public void PointToOrange()
    {
        scoreOrange++;
        UpdateOrangeScore();
    }


    private void UpdateBlueScore()
    {
        textBlueScore.SetText(scoreBlue.ToString());
    }

    private void UpdateOrangeScore()
    {
        textOrangeScore.SetText(scoreOrange.ToString());
    }
}
