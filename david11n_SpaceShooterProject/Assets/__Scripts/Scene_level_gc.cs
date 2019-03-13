using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene_level_gc : MonoBehaviour
{
    // buttons
    Button backButton;

    Button enemy0Button;
    Button enemy1Button;
    Button enemy2Button;
    Button enemy3Button;
    Button enemy4Button;

    // sliders
    Slider enemyCountSlider;
    Slider pointWinSlider;

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

        //go = GameObject.Find("StartButton");
        //startButton = go.GetComponent<Button>();
        //startButton.onClick.AddListener(() => MenuClick("1"));

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
