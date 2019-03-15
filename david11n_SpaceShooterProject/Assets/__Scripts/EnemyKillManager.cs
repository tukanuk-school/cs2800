using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyKillManager : MonoBehaviour
{

    public static Dictionary<Text, int> killCounts = new Dictionary<Text, int>();
    public static Text E0;
    public static Text E1;
    public static Text E2;
    public static Text E3;
    public static Text E4;

    //readonly Text scoreText;


    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.Find("E0");
        E0 = go.GetComponent<Text>();
        go = GameObject.Find("E1");
        E1 = go.GetComponent<Text>();
        go = GameObject.Find("E2");
        E2 = go.GetComponent<Text>();
        go = GameObject.Find("E3");
        E3 = go.GetComponent<Text>();
        go = GameObject.Find("E4");
        E4 = go.GetComponent<Text>();

        killCounts.Add(E0, 0);
        killCounts.Add(E1, 0);
        killCounts.Add(E2, 0);
        killCounts.Add(E3, 0);
        killCounts.Add(E4, 0);

        int i = 0;
        foreach (var kc in killCounts)
        {
            kc.Key.text = "E" + i++ + ": " + kc.Value.ToString();
        }

        //Debug.Log("kc: " + killCounts);

    }

    // Update is called once per frame
    void Update()
    {
        foreach (var kc in killCounts)
        {
            kc.Key.text = kc.Key.name + ": " + kc.Value.ToString();
        }
    }
}