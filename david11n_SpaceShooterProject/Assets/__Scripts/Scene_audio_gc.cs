using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Scene_audio_gc : MonoBehaviour
{
    // buttons
    Button backButton;

    // sliders
    Slider bgSlider;
    Slider endLevelSlider;
    Slider blasterSlider;
    Slider explosionSlider;

    // dropdowns
    Dropdown bgDropdown;
    Dropdown victoryDropdown;
    Dropdown blasterDropdown;
    Dropdown explosionDropdown;

    // sound effects
    public AudioSource audioSource;
    //AudioClip bgMusic;
    AudioClip clickSound;
    AudioSource clickAS;
    AudioSource BGMusic;
    AudioSource victoryAS;
    AudioSource blasterAS;
    AudioSource explosionAS;

    // audio clip dictionaries 
    // allows for different dropdown labels and filenames
    public Dictionary<string, string> bgDropdownDict;
    public Dictionary<string, string> victoryDropdownDict;
    public Dictionary<string, string> blasterDropdownDict;
    public Dictionary<string, string> explosionDropdownDict;


    void Awake()
    {
        // setup audio clips
        // clickSound = (AudioClip)Resources.Load("Audio/click");
        // AudioSource audioSource = Instantiate(audioSource);

    }

    // Start is called before the first frame update
    void Start()
    {
        // listeners 
        GameObject go;

        // attatch AudioSource 
        //go = GameObject.Find("clickAS");
        //clickAS = go.GetComponent<AudioSource>();
        //go = GameObject.Find("BGMusic");
        //BGMusic = go.GetComponent<AudioSource>();
        //go = GameObject.Find("victoryAS");
        //victoryAS = go.GetComponent<AudioSource>();
        //go = GameObject.Find("blasterAS");
        //blasterAS = go.GetComponent<AudioSource>();
        //go = GameObject.Find("explosionAS");
        //explosionAS = go.GetComponent<AudioSource>();

        // use a dictionary to allow descriptive labels and passed filename data
        bgDropdownDict = new Dictionary<string, string>
        {
            { "Epic Sapae Jam (default)", "music_background" },
            { "Space Rocks", "Galaga_88_Arcade" },
            { "Moon Mario", "super_mario_kart"}
        };

        victoryDropdownDict = new Dictionary<string, string>
        {
            { "You're a winner (default)", "you_win" },
            { "Space Hero", "botchord"},
            { "Congradulations", "etta_congradulations"}
        };

        blasterDropdownDict = new Dictionary<string, string>
        {
            { "Laser Blast (default)", "weapon_player"},
            { "Phaser Fun", "weapon_enemy" },
            { "Pew Pew Pew", "pewpew" }
        };

        explosionDropdownDict = new Dictionary<string, string>
        {
            { "Boom (default)", "explosion_asteroid" },
            { "Bang", "explosion_enemy" },
            { "Kapow", "explosion_player" }
        };

        // attatch volume sliders
        go = GameObject.Find("gameVolumeSlider");
        bgSlider = go.GetComponent<Slider>();
        bgSlider.onValueChanged.AddListener(delegate
        {
            VolumeAdjust(bgSlider);
        });

        go = GameObject.Find("endLevelVolSlider");
        endLevelSlider = go.GetComponent<Slider>();
        endLevelSlider.onValueChanged.AddListener(delegate
        {
            VolumeAdjust(endLevelSlider);
        });

        go = GameObject.Find("blasterVolume");
        blasterSlider = go.GetComponent<Slider>();
        blasterSlider.onValueChanged.AddListener(delegate
        {
            VolumeAdjust(blasterSlider);
        });

        go = GameObject.Find("ExplosionVolume");
        explosionSlider = go.GetComponent<Slider>();
        explosionSlider.onValueChanged.AddListener(delegate
        {
            VolumeAdjust(explosionSlider);
        });

        // dropdowns
        go = GameObject.Find("bgDropdown");
        bgDropdown = go.GetComponent<Dropdown>();
        bgDropdown.ClearOptions();
        bgDropdown.AddOptions(new List<string>(bgDropdownDict.Keys) );
        bgDropdown.onValueChanged.AddListener(delegate
            { ACdropdownClick(bgDropdown);
            });

        go = GameObject.Find("victoryDropdown");
        victoryDropdown = go.GetComponent<Dropdown>();
        victoryDropdown.ClearOptions();
        victoryDropdown.AddOptions(new List<string>(victoryDropdownDict.Keys));
        victoryDropdown.onValueChanged.AddListener(delegate
            { ACdropdownClick(victoryDropdown);
            });

        go = GameObject.Find("blasterDropdown");
        blasterDropdown = go.GetComponent<Dropdown>();
        blasterDropdown.ClearOptions();
        blasterDropdown.AddOptions(new List<string>(blasterDropdownDict.Keys));
        blasterDropdown.onValueChanged.AddListener(delegate
            { ACdropdownClick(blasterDropdown);
            });

        go = GameObject.Find("explosionDropdown");
        explosionDropdown = go.GetComponent<Dropdown>();
        explosionDropdown.ClearOptions();
        explosionDropdown.AddOptions(new List<string>(explosionDropdownDict.Keys));
        explosionDropdown.onValueChanged.AddListener(delegate
            { ACdropdownClick(explosionDropdown);
            });

        // attatch back button
        go = GameObject.Find("ExitButton");
        backButton = go.GetComponent<Button>();
        backButton.onClick.AddListener(() => MenuClick("back"));
    }

    private void ACdropdownClick(Dropdown dd)
    {
        // filename of audio clip
        string fn = "";
        string soundDir = "";
        Dictionary<string, string> tempDict = new Dictionary<string, string> { };
        AudioSource tempAS = new AudioSource();

        // access the value of the dropdown
        Debug.Log("name: " + dd.name + " #: " + dd.value + " " + dd.options[dd.value].text);

        GameObject go = GameObject.Find("BGMusic");
        BGMusic = go.GetComponent<AudioSource>();
        go = GameObject.Find("victoryAS");
        victoryAS = go.GetComponent<AudioSource>();
        go = GameObject.Find("blasterAS");
        blasterAS = go.GetComponent<AudioSource>();
        go = GameObject.Find("explosionAS");
        explosionAS = go.GetComponent<AudioSource>();

        // set audioClip
        switch (dd.name)
        {
            case "bgDropdown":
                tempDict = bgDropdownDict;
                tempAS = BGMusic;
                soundDir = "BGmusic/";
                break;
            case "victoryDropdown":
                tempDict = victoryDropdownDict;
                tempAS = victoryAS;
                soundDir = "SFX/";
                break;
            case "blasterDropdown":
                tempDict = blasterDropdownDict;
                tempAS = blasterAS;
                soundDir = "SFX/";
                break;
            case "explosionDropdown":
                tempDict = explosionDropdownDict;
                tempAS = explosionAS;
                soundDir = "SFX/";
                break;
        }

        fn = tempDict[dd.options[dd.value].text];

        tempAS.Stop();
        tempAS.clip = Resources.Load("Audio/" + soundDir + fn, typeof(AudioClip)) as AudioClip;
        tempAS.Play();


    }

    private void VolumeAdjust(Slider slideValue)
    {
        //Debug.Log(slideValue + "#: " + slideValue.value);

        GameObject go = GameObject.Find("BGMusic");
        BGMusic = go.GetComponent<AudioSource>();
        go = GameObject.Find("victoryAS");
        victoryAS = go.GetComponent<AudioSource>();
        go = GameObject.Find("blasterAS");
        blasterAS = go.GetComponent<AudioSource>();
        go = GameObject.Find("explosionAS");
        explosionAS = go.GetComponent<AudioSource>();

        switch (slideValue.name)
        {
            case "gameVolumeSlider":
                BGMusic.volume = slideValue.value;
                break;
            case "endLevelVolSlider":
                victoryAS.volume = slideValue.value;
                Debug.Log(slideValue + "#: " + slideValue.value);
                break;
            case "blasterVolume":
                blasterAS.volume = slideValue.value;
                break;
            case "ExplosionVolume":
                explosionAS.volume = slideValue.value;
                break;
        }
    }

    private void MenuClick(string butNum)
    {
        switch (butNum)
        {
            case "back":
                StartCoroutine(LoadSceneMM("setup"));
                break;
        }
    }

    IEnumerator LoadSceneMM(string butNum)
    {
        GameObject go = GameObject.Find("clickAS");
        clickAS = go.GetComponent<AudioSource>();
        clickAS.Play();
        yield return new WaitForSeconds(clickAS.clip.length);

        SceneManager.LoadScene("_Scene_" + butNum);
    }
}
