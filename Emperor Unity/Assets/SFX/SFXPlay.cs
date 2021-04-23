using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlay : MonoBehaviour
{
    public AudioSource ButtonClick;
    public AudioSource CardClick;

    public AudioSource StartingGame;

    public void PlayButtonClick() {
        ButtonClick.Play();
    }

    public void PlayCardClick(){
        CardClick.Play();
    }

    public void PlayStartingGameClick() {
        StartingGame.Play();
    }
}
