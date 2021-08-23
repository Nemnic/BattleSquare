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
            //Debug.LogWarning("Warning! More then one instance of SFX found!");
            Destroy(gameObject);
            //Debug.LogWarning("The new instace of SFX has been destroyed as only one should exist");

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

    // Sounds
    public AudioSource soundSelect;
    public AudioSource soundBack;
    public AudioSource soundClick;
    public AudioSource soundToggleOn;
    public AudioSource soundToggleOff;
    public AudioSource soundWin;
    public AudioSource soundLose;
    public AudioSource soundSquareDone;

    // Music
    public AudioSource song1;
    public AudioSource song2;

    #region Sounds

    public void PlaySelect()
    {
        if (Globals.instance.isSoundsOn)
        {
            soundSelect.Play();
        }
    }

    public void PlayBack()
    {
        if (Globals.instance.isSoundsOn)
        {
            soundBack.Play();
        }
    }

    public void PlayClick()
    {
        if (Globals.instance.isSoundsOn)
        {
            soundClick.Play();
        }
    }

    public void PlayToggleOn()
    {
        if (Globals.instance.isSoundsOn)
        {
            soundToggleOn.Play();
        }
    }

    public void PlayToggleOff()
    {
        if (Globals.instance.isSoundsOn)
        {
            soundToggleOff.Play();
        }
    }

    public void PlayWin()
    {
        if (Globals.instance.isSoundsOn)
        {
            soundWin.Play();
        }
    }

    public void PlayLose()
    {
        if (Globals.instance.isSoundsOn)
        {
            soundLose.Play();
        }
    }

    public void PlaySquareDone()
    {
        if (Globals.instance.isSoundsOn)
        {
            soundSquareDone.Play();
        }
    }

    #endregion

    #region Music

    public void PlaySong1()
    {
        song1.Play();
    }

    public void PlaySong2()
    {
        song2.Play();
    }

    public void PlayMusic()
    {
        song1.Play();
    }

    public void StopMusic()
    {
        song1.Stop();
    }

    #endregion
}
