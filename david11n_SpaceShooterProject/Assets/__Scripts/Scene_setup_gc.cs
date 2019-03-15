using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene_setup_gc : MonoBehaviour
{
    // buttons
    Button enemiesButton, audioButton, backgroundButton, backButton;

    // sound effects
    AudioSource clickAS;
    //AudioClip clickSound;
    //AudioClip bgMusic;

    void Awake()
    {
        // TODO: setup audio clips
        //clickSound = (AudioClip)Resources.Load("Audio/click");
        // AudioSource audioSource = Instantiate(audioSource);

    }

    // Start is called before the first frame update
    void Start()
    {
        // listeners for each button
        // ...

        GameObject go;
        go = GameObject.Find("EnemiesButton");
        enemiesButton= go.GetComponent<Button>();
        enemiesButton.onClick.AddListener(() => MenuClick("enemies"));

        go = GameObject.Find("AudioButton");
        audioButton = go.GetComponent<Button>();
        audioButton.onClick.AddListener(() => MenuClick("audio"));

        go = GameObject.Find("BackgroundButton");
        backgroundButton = go.GetComponent<Button>();
        backgroundButton.onClick.AddListener(() => MenuClick("background"));

        go = GameObject.Find("ExitButton");
        backButton = go.GetComponent<Button>();
        backButton.onClick.AddListener(() => MenuClick("back"));

        go = GameObject.Find("clickAS");
        clickAS = go.GetComponent<AudioSource>();

    }

    // TODO: can I avoid this and call LoadSceneMM directly?
    private void MenuClick(string butNum)
    {
        switch (butNum)
        {
            case "enemies":
                StartCoroutine(LoadSceneMM(butNum));
                break;
            case "audio":
                StartCoroutine(LoadSceneMM(butNum));
                break;
            case "background":
                StartCoroutine(LoadSceneMM(butNum));
                break;
            case "back":
                StartCoroutine(LoadSceneMM("mainMenu"));
                break;
        }
    }

    IEnumerator LoadSceneMM(string butNum)
    {
        clickAS.Play();
        yield return new WaitForSeconds(clickAS.clip.length);

        SceneManager.LoadScene("_Scene_" + butNum);
    }
}
