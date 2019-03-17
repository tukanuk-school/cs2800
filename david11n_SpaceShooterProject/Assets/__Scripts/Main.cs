using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    static public Main S; // A singleton for Main
    static Dictionary<WeaponType, WeaponDefinition> WEAP_DICT;


    [Header("Set in Inspector")]
    public GameObject[] prefabEnemies;        // Array of Enemy prefabs
    public float enemySpawnPerSecond = 0.5f;  // # / enemies per second
    public float enemyDefaultPadding = 1.5f;  // Padding for position
    public WeaponDefinition[] weaponDefinitions;
    public GameObject prefabPowerUp;
    public WeaponType[] powerUpFrequnecy = {
                WeaponType.blaster, WeaponType.blaster, WeaponType.spread, WeaponType.shield
           };

    private BoundsCheck bndCheck;

    // UI text
    float startTime, runningGameTime, endTime;
    public Text score;
    Text level;
    Text gameTime;
    Text E0;
    Text E1;
    Text E2;
    Text E3;
    Text E4;

    // level settings
    public int maxEnemyOnScreen;
    public int levelWinPoints;
    public ScoreManager.GameLevels levelDifficulty;

    void Awake()
    {
        S = this;

        // Set bndCheck to referecne trhe BoundsCheck component on this GameObject
        bndCheck = GetComponent<BoundsCheck>();

        // A generic dictionary with WeaponType as the key
        WEAP_DICT = new Dictionary<WeaponType, WeaponDefinition>();
        foreach (WeaponDefinition def in weaponDefinitions)
        {
            WEAP_DICT[def.type] = def;
        }

        // UI

        GameObject go = GameObject.Find("time");
        gameTime = go.GetComponent<Text>();
        //Debug.Log("clock at start: " + gameTime.text);

        go = GameObject.Find("score");
        score = go.GetComponent<Text>();
        //Debug.Log("score at start: " + score.text);

        go = GameObject.Find("level");
        level = go.GetComponent<Text>();

        go = GameObject.Find("E0");
        E0= go.GetComponent<Text>();

        go = GameObject.Find("E1");
        E1 = go.GetComponent<Text>();

        go = GameObject.Find("E2");
        E2 = go.GetComponent<Text>();

        go = GameObject.Find("E3");
        E3 = go.GetComponent<Text>();

        go = GameObject.Find("E4");
        E4 = go.GetComponent<Text>();

    }

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        //Debug.Log("Start time: " + startTime.ToString("000.0"));

        // set inital prefabEnemies
        prefabEnemies = ScoreManager.currentPrefabEnemies.ToArray();

        // set level settings
        levelDifficulty = ScoreManager.currentGameLevel;
        levelWinPoints = ScoreManager.scoreToNextLevel;

        switch (levelDifficulty)
        {
            case ScoreManager.GameLevels.Bronze:
                maxEnemyOnScreen = ScoreManager.BronzeMaxEnemies;
                level.text = ScoreManager.currentGameLevel.ToString();
                break;
            case ScoreManager.GameLevels.Silver:
                maxEnemyOnScreen = ScoreManager.SilverMaxEnemies;
                level.text = ScoreManager.currentGameLevel.ToString();
                break;
            case ScoreManager.GameLevels.Gold:
                maxEnemyOnScreen = ScoreManager.GoldMaxEnemies;
                level.text = ScoreManager.currentGameLevel.ToString();
                break;
        }

        Debug.Log(string.Format("{0} level: {1} points needed. {2} enemies onscreen",
            levelDifficulty, levelWinPoints.ToString(), maxEnemyOnScreen.ToString()));

        // Invoke SpawnEnemy() once (in 2 seconds)
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Main: " + t.ToString());
        runningGameTime += Time.deltaTime;
        gameTime.text = runningGameTime.ToString("##0.0");
    }

    public void SpawnEnemy()
    {
        if (EnemyCount() < maxEnemyOnScreen)
        {
            // Pick a random enemy PreFab to instantiate
            int ndx = UnityEngine.Random.Range(0, prefabEnemies.Length);
            GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);

            // Position the enemy above the screen with a random x position 
            float enemyPadding = enemyDefaultPadding;
            if (go.GetComponent<BoundsCheck>() != null)
            {
                enemyPadding = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
            }

            // set the initial position for the spawned Enemy
            Vector3 pos = Vector3.zero;
            float xMin = -bndCheck.camWidth + enemyPadding;
            float xMax = bndCheck.camWidth - enemyPadding;
            pos.x = UnityEngine.Random.Range(xMin, xMax);
            pos.y = bndCheck.camHeight + enemyPadding;
            go.transform.position = pos;
        }
        // Invoke SpawnEnemy() again
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
    }

    // counts the enemies on screen so we can know if there are too many
    private int EnemyCount()
    {
        GameObject[] goa = GameObject.FindGameObjectsWithTag("Enemy");
        //Debug.Log("Enemy onscree: " + goa.Length.ToString());
        return goa.Length;
    }

    public void DelayedRestart(float delay)
    {
        Invoke("Restart", delay);
    }

    public void Restart()
    {
        SceneManager.LoadScene("_Scene_0");
    }

    /// <summary>
    /// Static function that gets a WeaponDefinition from the WEAP_DICT static
    /// protected field of the Main class.
    /// </summary>
    /// <returns>The WeaponDefinition or, if there is no WeaponDefinition with
    /// the WeaponType passed in, returns a new WeaponDefinition with a
    /// WeaponType of none..</returns>
    /// <param name = "wt" > The WeaponType of the desired WeaponDefinition</param>
    /// 
    static public WeaponDefinition GetWeaponDefinition(WeaponType wt)
    {
        if (WEAP_DICT.ContainsKey(wt))
        {
            return (WEAP_DICT[wt]);
        }

        return (new WeaponDefinition());
    }
    public void IncreaseScore()
    {
        Debug.Log("++score!");

    }

    public void ShipDestroyed(Enemy e)
    {
        // potentially generate a PowerUp
        if (UnityEngine.Random.value <= e.powerUpDropChance)
        {
            int ndx = UnityEngine.Random.Range(0, powerUpFrequnecy.Length);
            WeaponType puType = powerUpFrequnecy[ndx];

            // spawn a powerup
            GameObject go = Instantiate(prefabPowerUp) as GameObject;
            PowerUp pu = go.GetComponent<PowerUp>();
            pu.SetType(puType);

            // set it to the position of the destroyed ship
            pu.transform.position = e.transform.position;
        }


    }





}
