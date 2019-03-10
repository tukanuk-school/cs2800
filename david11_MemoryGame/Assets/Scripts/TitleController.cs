using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    public Button startButton, exitButton;
    public AudioSource audioSource;
    public AudioClip audioClip;


    private void Start()
    {

        startButton.onClick.AddListener(() => ButtonClicked(1));
        exitButton.onClick.AddListener(() => ButtonClicked(2));
        //audioSource.clip = audioClip;

    }

    private void ButtonClicked(int v)
    {
        switch(v)
        {
            case 1:
                StartCoroutine(PlayAudio());

                SceneManager.LoadScene("_Scene_1");
                break;
            case 2:
                audioSource.Play();
                Application.Quit();
                break;
        }
            



    }

    IEnumerator PlayAudio()
    {
        //yield return new WaitForSeconds(2.5f);
        audioSource.Play();
        yield return new WaitForSeconds(0.5f);
    }
}
