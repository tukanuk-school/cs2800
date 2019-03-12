using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    // sound effects 
    public Button startButton, exitButton;
    public AudioSource audioSource;
    public AudioClip helloAudioClip;
    public AudioClip goodbyeAudioClip;

    // cards count slider
    private Slider slider;
    private Text slideText;
    private readonly float EPSILON;

    private void Awake()
    {
        // setup audioclips
        helloAudioClip = (AudioClip)Resources.Load("Sounds/hello");
        goodbyeAudioClip = (AudioClip)Resources.Load("Sounds/good_bye");
    }

    private void Start()
    {

        // listeners for start/exit buttons
        startButton.onClick.AddListener(() => ButtonClicked(1));
        exitButton.onClick.AddListener(() => ButtonClicked(2));
        //audioSource.clip = audioClip;

        // listener for slider
        GameObject s = GameObject.Find("CardSlider");
        slider = s.GetComponent<Slider>();
        slider.onValueChanged.AddListener((slideValue) => CardSlider(slideValue));

        GameObject st = GameObject.Find("CardSliderText");
        slideText = st.GetComponent<Text>();
        slideText.text = slider.value + " Cards";
        AddButtons.NumCards = (int)slider.value;

    }

    private void CardSlider(float slideValue)
    {
        Debug.Log("Slider v: " + slideValue);

        // clamp to even numbers
        if (Math.Abs(slideValue % 2) > EPSILON)
        {
            slideValue += 1;
            slider.value = slideValue;
        }

        slideText.text = slideValue + " Cards";
        AddButtons.NumCards = (int) slideValue;
    }

    private void ButtonClicked(int v)
    {
        switch(v)
        {
            case 1:
                StartCoroutine(LoadScene1());
                break;
            case 2:
                StartCoroutine(QuitGame());
                break;
        }
            



    }

    IEnumerator QuitGame()
    {
        audioSource.clip = goodbyeAudioClip;
        audioSource.Play();
        yield return new WaitForSeconds(goodbyeAudioClip.length);
        Application.Quit();
    }

    IEnumerator LoadScene1()
    {
        audioSource.clip = helloAudioClip;
        audioSource.Play();

        // wait for the audio cliip to finish
        yield return new WaitForSeconds(helloAudioClip.length);

        // load _Scene_1
        SceneManager.LoadScene("_Scene_1");

    }
}
