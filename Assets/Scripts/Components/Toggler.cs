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
            toggle.isOn = Globals.instance.IsShowingLastPressedLine;
        }
        else if (currentToggleString == "AIDelay")
        {
            toggle.isOn = Globals.instance.isDelayOnAI;

        }
        else if (currentToggleString == "SoundsOn")
        {
            toggle.isOn = Globals.instance.isSoundsOn;
        }
        else if (currentToggleString == "MusicOn")
        {
            toggle.isOn = Globals.instance.isMusicOn;
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
            Globals.instance.IsShowingLastPressedLine = toggle.isOn;
        }
        else if (currentToggleString == "AIDelay")
        {
            Globals.instance.isDelayOnAI = toggle.isOn;
        }
        else if (currentToggleString == "SoundsOn")
        {
            Globals.instance.isSoundsOn = toggle.isOn;
        }
        else if (currentToggleString == "MusicOn")
        {
            Globals.instance.isMusicOn = toggle.isOn;
            if (toggle.isOn)
            {
                SFX.instance.PlayMusic();
            }
            else
            {
                SFX.instance.StopMusic();
            }
        }
        else
        {
            Debug.LogError("ERROR: Because of hardcoding, Toggle at value change wrong currentToggleString");
        }

        SoundToggle();

    }

    void SoundToggle()
    {
        if (toggle.isOn)
        {
            SFX.instance.PlayToggleOn();
        }
        else
        {
            SFX.instance.PlayToggleOff();
        }
    }
}
