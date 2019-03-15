using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseButtons : MonoBehaviour
{
    //Button resume;
    //Button restart;
    //Button difficulty;
    Button[] butt;
    GameObject pausePanel;

    // Start is called before the first frame update
    void Start()
    {
        butt = GetComponentsInChildren<Button>();
        butt[0].onClick.AddListener(Unpause);
        butt[1].onClick.AddListener(RestartLevel);
        butt[2].onClick.AddListener(MainMenu);
        pausePanel = GameObject.Find("PausePanel");

        foreach (var but in butt)
        {
            Debug.Log("n: " + but.name);
        }
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene("_Scene_1");
    }

    private void MainMenu()
    {
        SceneManager.LoadScene("_Scene_0");
    }

    private void Unpause()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
