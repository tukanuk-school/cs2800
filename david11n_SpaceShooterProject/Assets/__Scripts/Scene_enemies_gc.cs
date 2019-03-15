using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene_enemies_gc : MonoBehaviour
{
    public GameObject[] prefabEnemies;        // Array of Enemy prefabs

    // buttons
    Button backButton;

    Button enemy0Button;
    Button enemy1Button;
    Button enemy2Button;
    Button enemy3Button;
    Button enemy4Button;
    List<Button> eButtons;

    GameObject[] activeEnemies;

    // sliders
    Slider enemy0Slider;
    Slider enemy1Slider;
    Slider enemy2Slider;
    Slider enemy3Slider;
    Slider enemy4Slider;
    List<Slider> pSliders;

    // dropdowns
    Dropdown enemy0Dropdown;
    Dropdown enemy1Dropdown;
    Dropdown enemy2Dropdown;
    Dropdown enemy3Dropdown;
    Dropdown enemy4Dropdown;
    List<Dropdown> cDropdown;

    // sound effects
    public AudioSource audioSource;
    AudioClip clickSound;
    AudioClip bgMusic;

    // enemy
    //public GameObject obj;
    //Enemy enemy;

    void Awake()
    {
        // setup audio clips
        //clickSound = (AudioClip)Resources.Load("Audio/click");
        // AudioSource audioSource = Instantiate(audioSource);

        //enemy = obj.GetComponent<Enemy>();
        //enemy.health = 10000;
        //ScoreManage
    }

    // Start is called before the first frame update
    void Start()
    {
        // listeners 
        GameObject go;

        GameObject[] gob = GameObject.FindGameObjectsWithTag("eBut");
        foreach (var g in gob)
        {
            //eButtons.Add(g.GetComponent<Button>() );
        }

        pSliders = new List<Slider>(FindObjectsOfType<Slider>());
        cDropdown = new List<Dropdown>(FindObjectsOfType<Dropdown>());

        CreateEnemies();
        PointListener();

        go = GameObject.Find("ExitButton");
        backButton = go.GetComponent<Button>();
        backButton.onClick.AddListener(() => MenuClick("back"));

        go = GameObject.Find("clickAS");
        audioSource = go.GetComponent<AudioSource>();

        // setup the blaster sound
        //AudioSource blasterAS;
        //GameObject goBs = GameObject.Find("blasterAS");
        //blasterAS = goBs.GetComponent<AudioSource>();

    }

    private void PointListener()
    {
        pSliders[0].onValueChanged.AddListener(delegate { SliderChange(pSliders[0]); });
        pSliders[1].onValueChanged.AddListener(delegate { SliderChange(pSliders[1]); });
        pSliders[2].onValueChanged.AddListener(delegate { SliderChange(pSliders[2]); });
        pSliders[3].onValueChanged.AddListener(delegate { SliderChange(pSliders[3]); });
        pSliders[4].onValueChanged.AddListener(delegate { SliderChange(pSliders[4]); });
    }

    private void CreateEnemies()
    {
        activeEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        //foreach (var e in activeEnemies)
        //{
        //    Debug.Log(e);
        //}

        activeEnemies[0].GetComponent<Enemy>().enabled = false;
        activeEnemies[1].GetComponent<Enemy>().enabled = false;
        activeEnemies[2].GetComponent<Enemy>().enabled = false;
        activeEnemies[3].GetComponent<Enemy>().enabled = false;
        activeEnemies[4].GetComponent<Enemy>().enabled = false;



    }

    void SliderChange(Slider s)
    {
        Debug.Log("i: " + s.name + " v: " + s.value.ToString());
        switch (s.name)
        {
            case "0":
                ScoreManager.E0 = (int) s.value;
                break;
            case "1":
                ScoreManager.E1 = (int)s.value;
                break;
            case "2":
                ScoreManager.E2 = (int)s.value;
                break;
            case "3":
                ScoreManager.E3 = (int)s.value;
                break;
            case "4":
                ScoreManager.E4 = (int)s.value;
                break;
        }

        Debug.Log(ScoreManager.E0.ToString() + " " + ScoreManager.E1.ToString() + " " +
            ScoreManager.E2.ToString() + " " + ScoreManager.E3.ToString() + " " +
            ScoreManager.E4.ToString());
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
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        SceneManager.LoadScene("_Scene_" + butNum);
    }
}
