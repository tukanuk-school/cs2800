using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    float startTime;

    public float GameTime;

    public string GameTimeS;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        while (true)
        {
            GameTime = Time.time;
            Debug.Log("clock: " + GameTime);
            yield return new WaitForSeconds(0.25f);
        }
    }
}
