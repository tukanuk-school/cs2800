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

    // new scale
    Vector3 newScale = new Vector3(1, 1, 0);

    // dropdowns
    Dropdown bgDropdown;

    // picture frame
    Image bgImage;

    // sound effects
    public AudioSource audioSource;
    AudioClip clickSound;
    AudioClip bgMusic;
    AudioSource clickAS;

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

        go = GameObject.Find("xAxisSlider");
        xSlider = go.GetComponent<Slider>();
        xSlider.onValueChanged.AddListener(delegate {
            ScaleAdjust(xSlider);
        });

        go = GameObject.Find("yAxisSlider");
        ySlider = go.GetComponent<Slider>();
        ySlider.onValueChanged.AddListener(delegate {
            ScaleAdjust(ySlider);
        });

        go = GameObject.Find("ExitButton");
        backButton = go.GetComponent<Button>();
        backButton.onClick.AddListener(() => MenuClick("back"));

        go = GameObject.Find("clickAS");
        clickAS = go.GetComponent<AudioSource>();

    }

    private void ScaleAdjust(Slider slideValue)
    {
        Debug.Log(slideValue + "#: " + slideValue.value);

        GameObject go = GameObject.Find("BGSampleImage");
        Transform t = go.GetComponent<Transform>();

        newScale = t.localScale;

        if (slideValue.name == "xAxisSlider")
            newScale.x = slideValue.value;
        else
            newScale.y = slideValue.value;
        
        t.localScale = newScale;


    }

    private void BGdropdownClick(Dropdown dd)
    {
        // access the value of the dropdown
        Debug.Log("#: " + dd.value + " " + dd.options[dd.value].text);

        // bg image frame
        GameObject go = GameObject.Find("BGSampleImage");
        Image image = go.GetComponent<Image>();

        // set BGImage
        switch (dd.value) 
        {
            case 0:
                image.sprite = Resources.Load("Backgrounds/space_disco", typeof(Sprite)) as Sprite;
                break;
            case 1:
                image.sprite = Resources.Load("Backgrounds/space_cloud", typeof(Sprite)) as Sprite;
                break;
            case 2:
                image.sprite = Resources.Load("Backgrounds/nebularific", typeof(Sprite)) as Sprite;
                break;
            case 3:
                image.sprite = Resources.Load("Backgrounds/warp_speed", typeof(Sprite)) as Sprite;
                break;

        }
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
        //clickAS.Play();
        yield return new WaitForSeconds(clickAS.clip.length);

        GameObject go = GameObject.Find("BGImage");

        // set scale of BG
        Transform t = go.GetComponent<Transform>();
        Debug.Log("newscale: " + newScale);
        newScale.x *= 60; // adjust for base scale
        newScale.y *= 90; // adjust for base scale
        t.localScale = newScale;

        // set BG iamge
        Debug.Log("Sprite: " + GameObject.Find("BGSampleImage").GetComponent<Image>().sprite);

        go = GameObject.Find("BGImage");
        Renderer r = go.GetComponent<Renderer>();

        Debug.Log("material: " + r.material);

        switch (GameObject.Find("BGSampleImage").GetComponent<Image>().sprite.name)
        {
            case "nebularific":
                r.material = Resources.Load("_Materials/nebularific", typeof(Material)) as Material;
                break;
            case "space_cloud":
                r.material = Resources.Load("_Materials/space_cloud", typeof(Material)) as Material;
                break;
            case "space_disco":
                r.material = Resources.Load("_Materials/space_disco", typeof(Material)) as Material;
                break;
            case "warp_speed":
                r.material = Resources.Load("_Materials/warp_speed", typeof(Material)) as Material;
                break;
        }

        SceneManager.LoadScene("_Scene_" + butNum);
    }
}
