using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    #region Singleton
    public static SFX instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Warning! More then one instance of SFX found!");
            Destroy(gameObject);
            Debug.LogWarning("The new instace of SFX has been destroyed as only one should exist");

            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            Debug.Log("we created the SFX");
        }

    }

    #endregion

    public AudioSource soundSelect;
    public AudioSource soundBack;
    public AudioSource soundClick;
    public AudioSource soundToggleOn;
    public AudioSource soundToggleOff;
    public AudioSource soundWin;
    public AudioSource soundLose;
    public AudioSource soundSquareDone;

    public void PlaySelect()
    {
        soundSelect.Play();
    }

    public void PlayBack()
    {
        soundBack.Play();
    }

    public void PlayClick()
    {
        soundClick.Play();
    }

    public void PlayToggleOn()
    {
        soundToggleOn.Play();
    }

    public void PlayToggleOff()
    {
        soundToggleOff.Play();
    }
    
    public void PlayWin()
    {
        soundWin.Play();
    }

    public void PlayLose()
    {
        soundLose.Play();
    }

    public void PlaySquareDone()
    {
        soundSquareDone.Play();
    }
}
