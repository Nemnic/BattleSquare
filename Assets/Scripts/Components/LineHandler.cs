using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LineHandler : MonoBehaviourPunCallbacks
{
    private PhotonView myPhotonView;
    [SerializeField] private bool isClicked = false;
    [SerializeField] private bool isNotPressedYet = true;

    [Header("Position")]
    [SerializeField] public int xPos;
    [SerializeField] public int yPos;

    [Space (10)]
    [SerializeField] public bool isVertical;

    [SerializeField, Tooltip("Unpressed is -1, when pressed will be assigned a nr from 59 and down to 0")] public int myFinishCountIndex = -1;
    private bool darkColorSet = false;


    void Awake()
    {
        this.transform.SetParent(GameObject.Find("Lines").GetComponent<Transform>(), false);

        GameManager.instance.AddLineCount();

        SetScaleAndIfVertical();

        if (Globals.instance.is_Multiplayer)
        {
            OnPhotonInstantiate();
        }

        GameManager.instance.AddLineToList(this.gameObject);

    }

    public void OnPhotonInstantiate()
    {
        object[] instantiationData = this.photonView.InstantiationData;
        Vector2 myVec2 = (Vector2)instantiationData[0];

        xPos = (int)myVec2.x;
        yPos = (int)myVec2.y;
    }

    private void SetScaleAndIfVertical()
    {
        if (gameObject.tag == "Horizontal")
        {
            isVertical = false;
            this.transform.localScale = new Vector2(Globals.instance.squareSize, 1);
        }
        else if (gameObject.tag == "Vertical")
        {
            isVertical = true;
            this.transform.localScale = new Vector2(1, Globals.instance.squareSize);
        }
        else
        {
            Debug.Log("LineHandler: ERROR, unwanted outcome");
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        myPhotonView = GetComponent<PhotonView>();
    }


    [PunRPC]
    void RPC_ButtonPressed()
    {
        isClicked = true;
    }

    public void ButtonPressed_With_RPC()
    {
        if (Globals.instance.is_Multiplayer)
        {
            myPhotonView.RPC("RPC_ButtonPressed", RpcTarget.All);
        }
        else
        {
            isClicked = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!darkColorSet)      // just to make first layer of check is a bool so it does not go through to many if statsments
        {
            if (myFinishCountIndex != -1)
            {
                if (myFinishCountIndex > GameManager.instance.finishCount)
                {
                    DarkenLine();
                }
            }
        }

        if (isNotPressedYet)
        {
            if (isClicked)
            {
                ButtonPressed();
                isNotPressedYet = false;
            }
        }
    }

    public void ButtonPressed()
    {
        SFX.instance.PlayClick();

        // Set temp color
        if (Globals.instance.IsShowingLastPressedLine)
        {
            SetDisabledColor(TurnManager.instance.GetCurrentColor());
        }

        // Disable interactable button
        gameObject.GetComponent<Button>().interactable = false;     // it goes away ?
        
        // Activate ButtonClick - (basicly main loop of the game)
        GameManager.instance.ButtonClick(xPos, yPos, isVertical);

        // Remove Line count for game finished
        GameManager.instance.RemoveLineCount();

        // Remove from list for AI to find active lines
        GameManager.instance.RemoveLineFromList(gameObject);    
        
        // Keep track of when the button was pushed for setting it to black when it was not last pressed
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
