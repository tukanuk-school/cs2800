using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene_0_gc : MonoBehaviour
{
    // buttons
    Button startButton, exitButton;

    // sound effects
    public AudioSource audioSource;
    AudioClip clickSound;
    AudioClip bgMusic;
    
    void Awake()
    {
        // TODO: setup audio clips
        clickSound = (AudioClip) Resources.Load("Audio/click");
        // AudioSource audioSource = Instantiate(audioSource);

    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.Find("StartButton");
        startButton = go.GetComponent<Button>();
        startButton.onClick.AddListener( () => MenuClick("start") );
        
        go = GameObject.Find("ExitButton");
        exitButton = go.GetComponent<Button>();
        exitButton.onClick.AddListener( () => MenuClick("exit") );

    }

    private void MenuClick(string butNum)
    {
        switch(butNum)
        {
            case "start":
                StartCoroutine(LoadSceneMM() );
                break;
            case "exit":
                StartCoroutine(QuitGame() );
                break;
        }
    }

    IEnumerator LoadSceneMM()
    {
        audioSource.clip = clickSound;
        audioSource.Play();
        yield return new WaitForSeconds(clickSound.length);

        SceneManager.LoadScene("_Scene_mainMenu");
    }
    
    IEnumerator QuitGame()
    {
        audioSource.clip = clickSound;
        audioSource.Play();
        yield return new WaitForSeconds(clickSound.length);

        Application.Quit();
    }
}
