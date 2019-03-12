using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scene_mainMenu_gc: MonoBehaviour
{
    // buttons
    Button startButton, diffButton, setupButton, historyButotn, backButton;

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
       // listeners for each button
       // ...

        startButton = buttons.GetComponent<Button>();
        startButton.onClick.AddListener( () => MenuClick("start") );
        
        buttons = GameObject.Find("ExitButton");
        backButton = buttons.GetComponent<Button>();
        backButton.onClick.AddListener( () => MenuClick("back") );

    }

    private void MenuClick(string butNum)
    {
        switch(butNum)
        {
            case "start":
                StartCoroutine(LoadSceneMM() );
                break;
            case "back":
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
