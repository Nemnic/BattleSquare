using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;

public class LevelManger : MonoBehaviour
{

    private int x_Squares ;
    private int y_Squares ;
    private float squareSize ;

    [Header("Prefabs")]
    [SerializeField] private GameObject prefab_Dots;
    [SerializeField] private GameObject prefab_Verticle_Line;
    [SerializeField] private GameObject prefab_Horicontal_Line;
    [SerializeField] private GameObject prefab_Square_Base;
    //[SerializeField] private GameObject prefab_Square_Blue;
    //[SerializeField] private GameObject prefab_Square_Orange;

    [Header("Line size pixels")]
    [SerializeField] private int linePixelsSize;

    // Private info for setting Lines and dots with offsets
    public float lineLenght;
    public float lineLenghtAndHalf;
    public int magicNrExtraOffsetsForDots = 3;              // Not sure why, but correction was needed ... (offseting for diffrent origins?)

    PhotonView photonView;


    // Start is called before the first frame update
    void Start()
    {
        photonView = PhotonView.Get(this);

        x_Squares = Globals.instance.x_Squares;
        y_Squares = Globals.instance.y_Squares;
        squareSize = Globals.instance.squareSize;

        lineLenght = linePixelsSize * squareSize;
        lineLenghtAndHalf = lineLenght + (lineLenght /4);           // TODO: more Clearity needed .. why is it 4 not 2 ?


        if (!Globals.instance.is_Multiplayer)
        {
            SetupLevel();
        }
        else if (PhotonNetwork.IsMasterClient)
        {
            SetupLevel();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetupLevel()
    {
        SetUpDots();
        SetupLines();
        SetUpSquares();
    }

    void SetUpDots()
    {
        for (int x = 0; x < x_Squares + 1; x++)
        {
            for (int y = 0; y < y_Squares + 1; y++)
            {
                InstantiateNewDot(prefab_Dots, x * linePixelsSize - lineLenghtAndHalf + magicNrExtraOffsetsForDots, y * linePixelsSize - lineLenghtAndHalf + magicNrExtraOffsetsForDots);
            }
        }
    }

    void InstantiateNewDot(GameObject prefab, float x, float y)
    {
        GameObject newObject;

        if (Globals.instance.is_Multiplayer)
        {
            newObject = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "DotButton"), new Vector2(-x_Squares + x * squareSize, y_Squares - y * squareSize), Quaternion.identity);
        }
        else
        {
            newObject = Instantiate(prefab, new Vector2(-x_Squares + x * squareSize, y_Squares - y * squareSize), Quaternion.identity);
        }
    }

    void SetUpSquares()
    {
        object[] myCustomInitData = new object[1];

        for (int x = 0; x < x_Squares; x++)
        {
            for (int y = 0; y < y_Squares ; y++)
            {
                myCustomInitData[0] = new Vector2(x, y);
                int offset = 1;


                if (Globals.instance.is_Multiplayer)
                {
                    PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Square"), new Vector2(-x_Squares + offset + x * squareSize, y_Squares - offset - y * squareSize), Quaternion.identity, 0, myCustomInitData);
                }
                else
                {
                    InstantiateNewSquare(prefab_Square_Base, x, y, offset);
                }
            }
        }
    }

    GameObject InstantiateNewSquare(GameObject prefab,  int x, int y, int offset)
    {
        GameObject newObject = Instantiate(prefab, new Vector2(-x_Squares + offset + x * squareSize, y_Squares - offset - y * squareSize), Quaternion.identity);

        newObject.GetComponent<Square>().xPos = x;
        newObject.GetComponent<Square>().yPos = y;

        return newObject;
    }


    void SetupLines()
    {
        GameObject newObject;
        object[] myCustomInitData = new object[1];

        for (int x = 0; x < x_Squares; x++)
        {
            for (int y = 0; y < y_Squares; y++)
            {

                int posOffset = (x_Squares * linePixelsSize - linePixelsSize);

                if (y == 0)
                {
                    if (Globals.instance.is_Multiplayer)
                    {
                        myCustomInitData[0] = new Vector2(x, y);
                        newObject = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Button-line-Horizontal"), new Vector2(-posOffset + (x * lineLenght),
                            posOffset + linePixelsSize - (y * lineLenght)), Quaternion.identity, 0, myCustomInitData);
                    }
                    else
                    {
                        newObject = Instantiate(prefab_Horicontal_Line, new Vector2(-posOffset + (x * lineLenght),
                        posOffset + linePixelsSize - (y * lineLenght)), Quaternion.identity);

                        newObject = SetLinesValues(newObject, x, y);
                    }
                }

                if (Globals.instance.is_Multiplayer)
                {
                    myCustomInitData[0] = new Vector2(x, y + 1);
                    newObject = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Button-line-Horizontal"), new Vector2(-posOffset + (x * squareSize * linePixelsSize),
                            posOffset + linePixelsSize - (y * squareSize * linePixelsSize) - linePixelsSize * 2), Quaternion.identity, 0, myCustomInitData);
                }
                else
                {
                    newObject = Instantiate(prefab_Horicontal_Line, new Vector2(-posOffset + (x * squareSize * linePixelsSize),
                        posOffset + linePixelsSize - (y * squareSize * linePixelsSize) - linePixelsSize * 2), Quaternion.identity);
                    newObject = SetLinesValues(newObject, x, y + 1);
                }


                //  Verticals
                float offsetVertival = (x_Squares * lineLenght) / 2;
                if (x == 0)
                {
                    if (Globals.instance.is_Multiplayer)
                    {
                        myCustomInitData[0] = new Vector2(x, y);
                        newObject = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Button-line-Vertical"), new Vector2(-offsetVertival,
                            offsetVertival - lineLenght / 2 - (y * lineLenght)), Quaternion.identity, 0, myCustomInitData);
                    }
                    else
                    {
                        newObject = Instantiate(prefab_Verticle_Line, new Vector2(-offsetVertival,
                        offsetVertival - lineLenght / 2 - (y * lineLenght)), Quaternion.identity);
                        newObject = SetLinesValues(newObject, x, y);
                    }
                }

                if (Globals.instance.is_Multiplayer)
                {
                    myCustomInitData[0] = new Vector2(x + 1, y);
                    newObject = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Button-line-Vertical"), new Vector2(-offsetVertival + lineLenght + (x * lineLenght),
                            offsetVertival - lineLenght / 2 - (y * lineLenght)), Quaternion.identity, 0, myCustomInitData);
                }
                else
                {
                    newObject = Instantiate(prefab_Verticle_Line, new Vector2(-offsetVertival + lineLenght + (x * lineLenght),
                            offsetVertival - lineLenght / 2 - (y * lineLenght)), Quaternion.identity);
                    newObject = SetLinesValues(newObject, x + 1, y);
                }
            }
        }
    }

    GameObject SetLinesValues(GameObject gameObject, int x, int y)
    {
        gameObject.GetComponent<LineHandler>().xPos = x;
        gameObject.GetComponent<LineHandler>().yPos = y;


        return gameObject;
    }
}
