using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// stores game settings
public class ScoreManager : MonoBehaviour
{
    // current level
    public enum GameLevels { Bronze, Silver, Gold };
    public static GameLevels currentGameLevel = GameLevels.Bronze;
    public static List<GameObject> currentPrefabEnemies;

    // score
    public static int score;
    public static int scoreToNextLevel;
    Text scoreText;

    // enemy health
    public static int E0 { get; set; } = 10;
    public static int E1 { get; set; } = 7;
    public static int E2 { get; set; } = 15;
    public static int E3 { get; set; } = 5;
    public static int E4 { get; set; } = 10;

    // enemy points
    public static int E0points { get; set; } = 10;
    public static int E1points { get; set; } = 20;
    public static int E2points { get; set; } = 30;
    public static int E3points { get; set; } = 80;
    public static int E4points { get; set; } = 100;

    // enemy colour
    public static Color E0Color { get; set; } = Color.cyan;
    public static Color E1Color { get; set; } = Color.white;
    public static Color E2Color { get; set; } = Color.green;
    public static Color E3Color { get; set; } = Color.magenta;
    public static Color E4Color { get; set; } = Color.yellow;

    // bronze settings
    public static int BronzeMaxEnemies { get; set; } = 6;
    public static int BronzePointsToWin { get; set; } = 50;
    public static List<GameObject> bronzePrefabEnemies = new List<GameObject>();

    // silver settings
    public static int SilverMaxEnemies { get; set; } = 13;
    public static int SilverPointsToWin { get; set; } = 100;
    public static List<GameObject> silverPrefabEnemies;

    // gold settings
    public static int GoldMaxEnemies { get; set; } = 16;
    public static int GoldPointsToWin { get; set; } = 200;
    public static List<GameObject> goldPrefabEnemies;

    // default PrefabEnemies
    [Header("Set in Inspector:")]
    public GameObject[] prefabEnemies;


    private void Awake()
    {
        // setup the default enemy types
        bronzePrefabEnemies = new List<GameObject>(prefabEnemies);
        silverPrefabEnemies = new List<GameObject>(prefabEnemies);
        goldPrefabEnemies = new List<GameObject>(prefabEnemies);
        currentPrefabEnemies = new List<GameObject>(bronzePrefabEnemies);
    }


    //// Start is called before the first frame update
    //void Start()
    //{

    //    scoreText = GetComponent<Text>();
    //    score = 0;

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    scoreText.text = score.ToString();
    //}
}
