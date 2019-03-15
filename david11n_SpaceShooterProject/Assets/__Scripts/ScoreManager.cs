using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    // score
    public static int score;
    Text scoreText;

    // base enemy health
    private static int e0 = 10;
    private static int e1 = 20;
    private static int e2 = 10;
    private static int e3 = 60;
    private static int e4 = 10;

    public static int E0 { get => e0; set => e0 = value; }
    public static int E1 { get => e1; set => e1 = value; }
    public static int E2 { get => e2; set => e2 = value; }
    public static int E3 { get => e3; set => e3 = value; }
    public static int E4 { get => e4; set => e4 = value; }


    // Start is called before the first frame update
    void Start()
    {

        scoreText = GetComponent<Text>();
        Debug.Log("scoreText WHAT: " + scoreText);
        score = 0;

    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
    }
}
