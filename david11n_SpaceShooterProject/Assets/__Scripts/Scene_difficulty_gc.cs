using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene_difficulty_gc : MonoBehaviour
{
    // buttons
    Button backButton;

    Button bronzeButton;
    Button silverButton;
    Button goldButton;

    // sound effects
    public AudioSource clickAS;
    //AudioClip clickSound;
    //AudioClip bgMusic;

    void Awake()
    {
        //  setup audio clips
        //clickSound = (AudioClip)Resources.Load("Audio/click");
        // AudioSource audioSource = Instantiate(audioSource);

    }

    // Start is called before the first frame update
    void Start()
    {
        // listeners 
        GameObject go;

        go = GameObject.Find("BronzeButton");
        bronzeButton = go.GetComponent<Button>();
        bronzeButton.onClick.AddListener(() => MenuClick("bronze"));

        go = GameObject.Find("SilverButton");
        silverButton = go.GetComponent<Button>();
        silverButton.onClick.AddListener(() => MenuClick("silver"));

        go = GameObject.Find("GoldButton");
        goldButton = go.GetComponent<Button>();
        goldButton.onClick.AddListener(() => MenuClick("gold"));

        go = GameObject.Find("ExitButton");
        backButton = go.GetComponent<Button>();
        backButton.onClick.AddListener(() => MenuClick("back"));

        clickAS = GameObject.Find("clickAS").GetComponent<AudioSource>();

    }

    private void MenuClick(string butNum)
    {
        switch (butNum)
        {
            case "bronze":
                StartCoroutine(LoadSceneMM(butNum));
                break;
            case "silver":
                StartCoroutine(LoadSceneMM(butNum));
                break;
            case "gold":
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

    //IEnumerator QuitGame()
    //{
    //    audioSource.clip = clickSound;
    //    audioSource.Play();
    //    yield return new WaitForSeconds(clickSound.length);

    //    SceneManager.LoadScene("_Scene_0");
    //}
}
