using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene_background_gc : MonoBehaviour
{
    // buttons
    Button backButton;

    // sliders
    Slider xSlider;
    Slider ySlider;

    // dropdowns
    Dropdown bgDropdown;

    // picture frame
    Image bgImage;

    // sound effects
    public AudioSource audioSource;
    AudioClip clickSound;
    AudioClip bgMusic;

    void Awake()
    {
        // TODO: setup audio clips
        clickSound = (AudioClip)Resources.Load("Audio/click");
        // AudioSource audioSource = Instantiate(audioSource);

    }

    // Start is called before the first frame update
    void Start()
    {
        // listeners 
        GameObject go;

        go = GameObject.Find("Dropdown");
        bgDropdown= go.GetComponent<Dropdown>();
        bgDropdown.onValueChanged.AddListener(delegate
        {
           BGdropdownClick(bgDropdown);
        });

        //go = GameObject.Find("DifficultyButton");
        //diffButton = go.GetComponent<Button>();
        //diffButton.onClick.AddListener(() => MenuClick("difficulty"));

        //go = GameObject.Find("SetupButton");
        //setupButton = go.GetComponent<Button>();
        //setupButton.onClick.AddListener(() => MenuClick("setup"));

        //go = GameObject.Find("HistoryButton");
        //historyButton = go.GetComponent<Button>();
        //historyButton.onClick.AddListener(() => MenuClick("history"));

        go = GameObject.Find("ExitButton");
        backButton = go.GetComponent<Button>();
        backButton.onClick.AddListener(() => MenuClick("back"));

        GameObject quad = GameObject.Find("ImageBG");
        Renderer r = quad.GetComponent<Renderer>();
        r.material.mainTexture = (Texture)Resources.Load("Backgrounds/warp_speed");

    }

    private void BGdropdownClick(Dropdown dd)
    {
        // access the value of the dropdown
        Debug.Log(dd.options[dd.value].text);

        // set BGImage
        // add in Switch statement
        GameObject go = GameObject.Find("BGImage");
        Image image = go.GetComponent<Image>();
        Sprite sprite = Resources.Load("Backgrounds/space_disco", typeof(Sprite)) as Sprite;
        image.sprite = sprite;

        // update BG on BACK
    }

    private void MenuClick(string butNum)
    {
        switch (butNum)
        {
            case "1":
                StartCoroutine(LoadSceneMM(butNum));
                break;
            case "difficulty":
                StartCoroutine(LoadSceneMM(butNum));
                break;
            case "setup":
                StartCoroutine(LoadSceneMM(butNum));
                break;
            case "history":
                StartCoroutine(LoadSceneMM(butNum));
                break;
            case "back":
                StartCoroutine(LoadSceneMM("setup"));
                break;
        }
    }

    IEnumerator LoadSceneMM(string butNum)
    {
        audioSource.clip = clickSound;
        audioSource.Play();
        yield return new WaitForSeconds(clickSound.length);

        SceneManager.LoadScene("_Scene_" + butNum);
    }

    IEnumerator QuitGame()
    {
        audioSource.clip = clickSound;
        audioSource.Play();
        yield return new WaitForSeconds(clickSound.length);

        SceneManager.LoadScene("_Scene_0");
    }
}
