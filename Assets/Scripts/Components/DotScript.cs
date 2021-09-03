using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotScript : MonoBehaviour
{
    // if we want scaling option later on
    //private bool doWeWannaScaleIt = false;

    private void Awake()
    {
        this.transform.SetParent(GameObject.Find("Dots Button").GetComponent<Transform>(), false);


        // Incase we want to scale  - Not in use atm    - if used might need to be recalculated
        //if (doWeWannaScaleIt)
        //{
        //    this.transform.localScale = new Vector2(Globals.instance.squareSize, 1);
        //}

    }
}
