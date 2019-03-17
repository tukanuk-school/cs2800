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
    Enemy enemy;
    Enemy_1 enemy_1;
    Enemy_2 enemy_2;
    Enemy_3 enemy_3;
    Enemy_4 enemy_4;

    void Awake()
    {
        // setup audio clips
        //clickSound = (AudioClip)Resources.Load("Audio/click");
        // AudioSource audioSource = Instantiate(audioSource);

        //enemy = obj.GetComponent<Enemy>();
        //enemy.health = 10000;
        //ScoreManage

        enemy = GameObject.Find("Enemy_0").GetComponent<Enemy>();
        enemy_1 = GameObject.Find("Enemy_1").GetComponent<Enemy_1>();
        enemy_2 = GameObject.Find("Enemy_2").GetComponent<Enemy_2>();
        enemy_3 = GameObject.Find("Enemy_3").GetComponent<Enemy_3>();
        enemy_4 = GameObject.Find("Enemy_4").GetComponent<Enemy_4>();
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
        ColorListener();

        go = GameObject.Find("ExitButton");
        backButton = go.GetComponent<Button>();
        backButton.onClick.AddListener(() => MenuClick("back"));

        go = GameObject.Find("clickAS");
        audioSource = go.GetComponent<AudioSource>();

    }

   private void Update()
    {
        // setup sliders a hack to make sure it only happens once 
        if (Time.time <= 0.1f)SetInitialSliders();

        enemy.SetColour(ScoreManager.E0Color);
        enemy_1.SetColour(ScoreManager.E1Color);
        enemy_2.SetColour(ScoreManager.E2Color);
        enemy_3.SetColour(ScoreManager.E3Color);
        enemy_4.SetColour(ScoreManager.E4Color);
    }

    private void SetInitialSliders()
    {
        pSliders[0].value = ScoreManager.E4;
        Text text = pSliders[0].GetComponentInChildren<Text>();
        text.text = ScoreManager.E4points.ToString();
     
        pSliders[1].value = ScoreManager.E1;
        text = pSliders[1].GetComponentInChildren<Text>();
        text.text = ScoreManager.E1points.ToString();

        pSliders[2].value = ScoreManager.E0;
        text = pSliders[2].GetComponentInChildren<Text>();
        text.text = ScoreManager.E0points.ToString();

        pSliders[3].value = ScoreManager.E2;
        text = pSliders[3].GetComponentInChildren<Text>();
        text.text = ScoreManager.E2points.ToString();

        pSliders[4].value = ScoreManager.E3;
        text = pSliders[4].GetComponentInChildren<Text>();
        text.text = ScoreManager.E3points.ToString();
    }

    private void ColorListener()
    {
        cDropdown[0].onValueChanged.AddListener(delegate { DropdownChage(cDropdown[0]); });
        cDropdown[1].onValueChanged.AddListener(delegate { DropdownChage(cDropdown[1]); });
        cDropdown[2].onValueChanged.AddListener(delegate { DropdownChage(cDropdown[2]); });
        cDropdown[3].onValueChanged.AddListener(delegate { DropdownChage(cDropdown[3]); });
        cDropdown[4].onValueChanged.AddListener(delegate { DropdownChage(cDropdown[4]); });

    }

    private void DropdownChage(Dropdown dropdown)
    {
        Debug.Log("dropdown: " + dropdown + "name: " + dropdown.name +
            " color: " + dropdown.options[dropdown.value].text);

        GameObject enemy;
        Color temp = Color.white;


        switch(dropdown.options[dropdown.value].text)
        {
            case "Colour":
                temp = Color.white;
                break;
            case "Cyan":
                temp = Color.cyan;
                break;
            case "White":
                temp = Color.white;
                break;
            case "Green":
                temp = Color.green;
                break;
            case "Magenta":
                temp = Color.magenta;
                break;
            case "Yellow":
                temp = Color.yellow;
                break;
            case "Blue":
                temp = Color.blue;
                break;
        }

        Debug.Log("TEMP COLOR: " + temp);

        switch (dropdown.name)
        {
            case "0":
                ScoreManager.E0Color = temp;
                Debug.Log("E0Color: " + ScoreManager.E0Color);
                enemy = prefabEnemies[0];
                enemy.GetComponent<Enemy>().SetColour(temp);
                Debug.Log("enemy: " + enemy);

                break;
            case "1":
                ScoreManager.E1Color = temp;
                break;
            case "2":
                ScoreManager.E2Color = temp;
                break;
            case "3":
                ScoreManager.E3Color = temp;
                break;
            case "4":
                ScoreManager.E4Color = temp;
                enemy = prefabEnemies[4];
                enemy.GetComponent<Enemy_4>().SetColour(temp);

                //enemy.SetColour(temp);

                break;
        }
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

        //activeEnemies[0].GetComponent<Enemy>().enabled = false;
        //activeEnemies[1].GetComponent<Enemy>().enabled = false;
        //activeEnemies[2].GetComponent<Enemy>().enabled = false;
        //activeEnemies[3].GetComponent<Enemy>().enabled = false;
        //activeEnemies[4].GetComponent<Enemy>().enabled = false;



    }

    void SliderChange(Slider s)
    {
        Text text = s.GetComponentInChildren<Text>();
        text.text = s.value.ToString();

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
