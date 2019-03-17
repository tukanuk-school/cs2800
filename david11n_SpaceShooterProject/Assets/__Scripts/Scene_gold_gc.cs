using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene_gold_gc : MonoBehaviour
{
    // buttons
    Button backButton;

    //Button enemy0Button;
    //Button enemy1Button;
    //Button enemy2Button;
    //Button enemy3Button;
    //Button enemy4Button;
    List<Button> enemyButtons = new List<Button>();

    // sliders
    Slider enemyCountSlider;
    Slider pointWinSlider;

    // Text
    Text enemyCountText;
    Text pointWinText;

    // sound effects
    public AudioSource audioSource;

    // enemies to include
    bool isE0Included = false;
    bool isE1Included = false;
    bool isE2Included = false;
    bool isE3Included = false;
    bool isE4Included = false;
    List<bool> isIncluded = new List<bool>();

    // colours
    Color redButton = new Color(0.8196079f, 0.3686275f, 0.4089461f, 0.5882353f);
    Color redButtonH = new Color(0.8196079f, 0.3686275f, 0.4089461f, 0.8882353f);
    Color greenButton = new Color(0.4301035f, 0.8207547f, 0.367791f, 0.5882f);
    Color greenButtonH = new Color(0.4301035f, 0.8207547f, 0.367791f, 0.8882f);

    void Awake()
    {
        // audio
        audioSource = GameObject.Find("clickAS").GetComponent<AudioSource>();

        // listeners 
        GameObject go;

        // add toggle buttons
        GameObject[] goa = GameObject.FindGameObjectsWithTag("eBut");
        foreach (GameObject g in goa)
            enemyButtons.Add(g.GetComponent<Button>());


        enemyButtons[0].onClick.AddListener(() => ToggleButton(enemyButtons[0], ref isE0Included));
        enemyButtons[1].onClick.AddListener(() => ToggleButton(enemyButtons[1], ref isE1Included));
        enemyButtons[2].onClick.AddListener(() => ToggleButton(enemyButtons[2], ref isE2Included));
        enemyButtons[3].onClick.AddListener(() => ToggleButton(enemyButtons[3], ref isE3Included));
        enemyButtons[4].onClick.AddListener(() => ToggleButton(enemyButtons[4], ref isE4Included));


        // max enemies
        enemyCountSlider = GameObject.Find("enemyCountSlider").GetComponent<Slider>();
        enemyCountSlider.onValueChanged.AddListener(EnemySilderListener);
        enemyCountText = GameObject.Find("MaxEnemiesLabel").GetComponent<Text>();


        // points to win
        pointWinSlider = GameObject.Find("levelWinSlider").GetComponent<Slider>();
        pointWinSlider.onValueChanged.AddListener(PointWinListener);
        pointWinText = GameObject.Find("levelWinLabel").GetComponent<Text>();

        // back button
        go = GameObject.Find("ExitButton");
        backButton = go.GetComponent<Button>();
        backButton.onClick.AddListener(() => MenuClick("back"));


    }

    void Start()
    {
        SetInitialSliders();
    }


    private void Update()
    {

    }


    private void SetInitialSliders()
    {
        enemyCountSlider.value = ScoreManager.GoldMaxEnemies;

        // can't set less than silver's max
        enemyCountSlider.minValue = ScoreManager.SilverMaxEnemies;

        enemyCountText.text = string.Format("Max of {0} enemies on screen", ScoreManager.GoldMaxEnemies);

        pointWinSlider.value = ScoreManager.GoldPointsToWin;

        // can't set less than silver's points
        pointWinSlider.minValue = ScoreManager.SilverPointsToWin;

        pointWinText.text = string.Format("{0} points to win level", ScoreManager.GoldPointsToWin);

    }

    void EnemySilderListener(float arg0)
    {
        ScoreManager.GoldMaxEnemies = (int)arg0;
        enemyCountText.text = string.Format("Max of {0} enemies on screen", arg0.ToString());
    }

    void PointWinListener(float arg0)
    {
        ScoreManager.GoldPointsToWin = (int)arg0;
        pointWinText.text = string.Format("{0} points to win level", arg0.ToString());
    }


    private void MenuClick(string butNum)
    {
        switch (butNum)
        {
            case "back":
                StartCoroutine(LoadSceneMM("difficulty"));
                break;
        }
    }

    IEnumerator LoadSceneMM(string butNum)
    {
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        SceneManager.LoadScene("_Scene_" + butNum);
    }

    private void ToggleButton(Button button, ref bool isClicked)
    {
        ColorBlock colorBlock = button.colors;

        if (!isClicked)
        {
            colorBlock.normalColor = greenButton;
            colorBlock.highlightedColor = greenButtonH;
            button.colors = colorBlock;
            isClicked = true;
        }
        else
        {
            colorBlock.normalColor = redButton;
            colorBlock.highlightedColor = redButtonH;
            button.colors = colorBlock;
            isClicked = false;
        }

        Debug.Log(string.Format("I: {0} {1} {2} {3} {4}", isE0Included, isE1Included, isE2Included, isE3Included, isE4Included));
        EnemiesToInclude();
    }

    private void EnemiesToInclude()
    {
        double weight = 0;

        // base set of spawn probability 
        List<double> weights = new List<double> { 0.3f, 0.2f, 0.2f, 0.2f, 0.1f };

        // detemine the weights based on how many enemy types included
        if (isE0Included) weight += weights[0];
        if (isE1Included) weight += weights[1];
        if (isE2Included) weight += weights[2];
        if (isE3Included) weight += weights[3];
        if (isE4Included) weight += weights[4];

        //Debug.Log("weight: " + weight.ToString());

        // calculate the proability of each enemy type
        for (int i = 0; i < weights.Count; ++i)
            weights[i] = Math.Round(weights[i] / weight * 10, MidpointRounding.ToEven);

        // if rounding didn't help, add increase the probability of the first item (also item with
        // highest probability 
        int newTotal = 0;
        if (isE0Included) newTotal += (int)weights[0];
        if (isE1Included) newTotal += (int)weights[1];
        if (isE2Included) newTotal += (int)weights[2];
        if (isE3Included) newTotal += (int)weights[3];
        if (isE4Included) newTotal += (int)weights[4];

        //Debug.Log("Is it 10?: " + newTotal.ToString());

        if (newTotal < 10)
        {
            if (isE0Included) weights[0]++;
            else if (isE1Included) weights[1]++;
            else if (isE2Included) weights[2]++;
            else if (isE3Included) weights[3]++;
            else if (isE4Included) weights[4]++;
        }

        // check our work... looks good
        //for (int i = 0; i < weights.Count; ++i)
        //{
        //    Debug.Log(i.ToString() + ": " + weights[i].ToString());
        //}

        // add the prefab the ScoreManager.

        // Get the prefabs from the resources folder
        GameObject[] enemyPreFabs = Resources.LoadAll<GameObject>("_Prefabs/Enemy");
        ScoreManager.goldPrefabEnemies.Clear();
        //foreach (var e in enemyPreFabs)
        //{
        //    Debug.Log("e: " + e);
        //}

        // if an enemy type should be included at the appropriate amount
        if (isE0Included)
        {
            for (int i = 0; i < weights[0]; ++i)
            {
                ScoreManager.goldPrefabEnemies.Add(enemyPreFabs[0]);
            }
        }

        if (isE1Included)
        {
            for (int i = 0; i < weights[1]; ++i)
            {
                ScoreManager.goldPrefabEnemies.Add(enemyPreFabs[1]);
            }
        }

        if (isE2Included)
        {
            for (int i = 0; i < weights[2]; ++i)
            {
                ScoreManager.goldPrefabEnemies.Add(enemyPreFabs[2]);
            }
        }

        if (isE3Included)
        {
            for (int i = 0; i < weights[3]; ++i)
            {
                ScoreManager.goldPrefabEnemies.Add(enemyPreFabs[3]);
            }
        }

        if (isE4Included)
        {
            for (int i = 0; i < weights[4]; ++i)
            {
                ScoreManager.goldPrefabEnemies.Add(enemyPreFabs[4]);
            }
        }

        string log ="";
        foreach (var v in ScoreManager.goldPrefabEnemies)
        {
            log = log + String.Format("{0} ", v.name);
        }
        Debug.Log("goldPrefabEnemies: " + log);

    }

}

