using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineHandler : MonoBehaviour
{
    [Header("Position")]
    [SerializeField] public int xPos;
    [SerializeField] public int yPos;

    [Space (10)]
    [SerializeField] public bool isVertical;

    public int myFinishCountIndex = -1;
    private bool darkColorSet = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!darkColorSet)      // just to make first layer of check a bool
        {
            if (myFinishCountIndex != -1)
            {
                if (myFinishCountIndex > GameManager.instance.finishCount)
                {
                    DarkenLine();
                }
            }
        }
    }

    public void ButtonPressed()
    {
        // Set temp color
        SetDisabledColor(TurnManager.instance.GetCurrentColor());

        // Disable interactable button
        gameObject.GetComponent<Button>().interactable = false;     // it goes away ?
        
        // Activate ButtonClick - (basicly main loop of the game)
        GameManager.instance.ButtonClick(xPos, yPos, isVertical);

        // Remove Line count for game finished
        GameManager.instance.RemoveLineCount();
        myFinishCountIndex = GameManager.instance.finishCount;
    }

    public void DarkenLine()
    {
        SetDisabledColor(Color.black);
        darkColorSet = true;
    }

    private void SetDisabledColor(Color newColor)
    {
        var colors = GetComponent<Button>().colors;
        colors.disabledColor = newColor;
        GetComponent<Button>().colors = colors;
    }

}
