using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Code from Documentation:
/// https://docs.unity3d.com/2018.3/Documentation/ScriptReference/UI.Toggle-isOn.html
/// </summary>
public class Toggler : MonoBehaviour
{
    [SerializeField] private string currentToggleString;

    Toggle toggle;

    // Start is called before the first frame update
    void Start()
    {
        //Fetch the Toggle GameObject
        toggle = GetComponent<Toggle>();
        //Add listener for when the state of the Toggle changes, and output the state
        toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(toggle);
        });

        if (currentToggleString == "ShowLine")
        {
            toggle.isOn = Globals.instance.showLastPressedLine;
        }
        else
        {
            Debug.LogError("ERROR: Because of hardcoding, Toggle at start wrong currentToggleString");
        }
    }

    void ToggleValueChanged(Toggle change)
    {
        if (currentToggleString == "ShowLine")
        {
            Globals.instance.showLastPressedLine = toggle.isOn;

        }
        else
        {
            Debug.LogError("ERROR: Because of hardcoding, Toggle at value change wrong currentToggleString");
        }
    }
}
