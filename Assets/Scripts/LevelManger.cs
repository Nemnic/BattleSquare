using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Space(10)]
    [SerializeField] private Transform folder_Line_InCanvas;
    [SerializeField] private Transform folder_Squares;
    [SerializeField] private Transform folder_Dots;

    [Header("Line size pixels")]
    [SerializeField] private int linePixelsSize;


    // Start is called before the first frame update
    void Start()
    {
        x_Squares = Globals.instance.x_Squares;
        y_Squares = Globals.instance.y_Squares;
        squareSize = Globals.instance.squareSize;

        SetupLevel();
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
                InstantiateNewObject(prefab_Dots, folder_Dots,  x,  y, 0 ,false);
            }
        }
    }
    void SetUpSquares()
    {
        for (int x = 0; x < x_Squares; x++)
        {
            for (int y = 0; y < y_Squares ; y++)
            {
                GameObject newSquare = InstantiateNewSquare(prefab_Square_Base, folder_Squares, x, y, 1, false);
                
                Globals.instance.Add(newSquare);
                
            }
        }
    }

    void SetupLines()
    {
        GameObject newObject;

        float lineLenght = linePixelsSize * squareSize;

        for (int x = 0; x < x_Squares; x++)
        {
            for (int y = 0; y < y_Squares; y++)
            {
                
                int posOffset = (x_Squares * linePixelsSize - linePixelsSize);

                if (y == 0)
                {
                    
                    newObject = Instantiate(prefab_Horicontal_Line, new Vector2(-posOffset + (x * lineLenght) ,
                        posOffset + linePixelsSize - (y * lineLenght) ), Quaternion.identity);
                    newObject.transform.SetParent(folder_Line_InCanvas, false);
                    newObject.transform.localScale = new Vector2(squareSize, 1);
                    newObject = SetLinesValues(newObject, x, y, false);
                    //InstantiateNewObject(prefab_Horicontal_Line, folder_Line_InCanvas, x, y, 1 , true);

                }

                newObject = Instantiate(prefab_Horicontal_Line, new Vector2(-posOffset + (x * squareSize * linePixelsSize) ,
                        posOffset + linePixelsSize - (y * squareSize * linePixelsSize) - linePixelsSize*2 ), Quaternion.identity);
                newObject.transform.SetParent(folder_Line_InCanvas, false);
                newObject.transform.localScale = new Vector2(squareSize, 1);
                newObject = SetLinesValues(newObject, x, y+1, false);

                //  Verticals
                float offsetVertival = (x_Squares * lineLenght) / 2;

                if (x == 0)
                {

                    newObject = Instantiate(prefab_Verticle_Line, new Vector2(-offsetVertival,
                        offsetVertival - lineLenght/2 - (y * lineLenght)), Quaternion.identity);
                    newObject.transform.SetParent(folder_Line_InCanvas, false);
                    newObject.transform.localScale = new Vector2(1, squareSize);
                    newObject = SetLinesValues(newObject, x, y, true);

                }

                newObject = Instantiate(prefab_Verticle_Line, new Vector2(-offsetVertival + lineLenght + (x * lineLenght) ,
                        offsetVertival - lineLenght/2 - (y * lineLenght
                        )), Quaternion.identity);
                newObject.transform.SetParent(folder_Line_InCanvas, false);
                newObject.transform.localScale = new Vector2(1, squareSize);
                newObject = SetLinesValues(newObject, x + 1, y, true);

            }
        }
    }

    GameObject SetLinesValues(GameObject gameObject, int x, int y, bool isVertical)
    {
        gameObject.GetComponent<LineHandler>().xPos = x;
        gameObject.GetComponent<LineHandler>().yPos = y;
        gameObject.GetComponent<LineHandler>().isVertical = isVertical;

        return gameObject;
    }

    void InstantiateNewObject(GameObject prefab, Transform folder, int x, int y, int offset, bool toScale)
    {
        GameObject newObject = Instantiate(prefab, new Vector2(-x_Squares + offset + x * squareSize, y_Squares - offset - y * squareSize), Quaternion.identity);
        newObject.transform.SetParent(folder, false);
        if (toScale)
        {
            newObject.transform.localScale = new Vector2(squareSize, 1);
        }
    }

    GameObject InstantiateNewSquare(GameObject prefab, Transform folder, int x, int y, int offset, bool toScale)
    {
        GameObject newObject = Instantiate(prefab, new Vector2(-x_Squares + offset + x * squareSize, y_Squares - offset - y * squareSize), Quaternion.identity);
        newObject.transform.SetParent(folder, false);
        if (toScale)
        {
            newObject.transform.localScale = new Vector2(squareSize, 1);
        }
        newObject.GetComponent<Square>().xPos = x;
        newObject.GetComponent<Square>().yPos = y;

        return newObject;
    }

    

}
