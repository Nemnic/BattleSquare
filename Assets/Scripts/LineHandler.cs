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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonPressed()
    {
        //var button = GetComponent<Button>().colors;
        //button.normalColor = Color.black;
        //GetComponent<Button>().colors = button;
        gameObject.GetComponent<Button>().interactable = false;     // it goes away ?
        GameManager.instance.ButtonClick(xPos, yPos, isVertical);
    }
}
