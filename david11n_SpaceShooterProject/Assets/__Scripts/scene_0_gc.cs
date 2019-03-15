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
    public AudioSource clickAS;
    public AudioSource exitAS;
    public AudioSource victoryAS;
    public AudioSource blasterAS;
    public AudioSource explosionAS;

    // BG music
    AudioSource BgAs;


    public AudioClip BgMusic { get; set; }

    void Awake()
    {
        // setup sfx
        clickAS.clip = (AudioClip)Resources.Load("Audio/SFX/click");
        exitAS.clip = Resources.Load("Audio/SFX/good_bye", typeof(AudioClip)) as AudioClip;
        victoryAS.clip = Resources.Load("Audio/SFX/you_win", typeof(AudioClip)) as AudioClip;
        blasterAS.clip = Resources.Load("Audio/SFX/weapon_player", typeof(AudioClip)) as AudioClip;
        explosionAS.clip = Resources.Load("Audio/SFX/explosion_asteroid", typeof(AudioClip)) as AudioClip;

        DontDestroyOnLoad(clickAS);
        DontDestroyOnLoad(exitAS);
        DontDestroyOnLoad(victoryAS);
        DontDestroyOnLoad(blasterAS);
        DontDestroyOnLoad(explosionAS);

        // BG Music
        GameObject go = GameObject.Find("BGMusic");
        AudioSource audio = go.GetComponent<AudioSource>();
        audio.Play();

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
        clickAS.Play();
        yield return new WaitForSeconds(clickAS.clip.length);

        SceneManager.LoadScene("_Scene_mainMenu");
    }
    
    IEnumerator QuitGame()
    {
        exitAS.Play();
        yield return new WaitForSeconds(exitAS.clip.length);

        Application.Quit();
    }
}
