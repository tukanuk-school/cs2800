using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    private GameObject pausePanel;
    Button pauseButton;

    // Start is called before the first frame update
    void Start()
    {
        pausePanel = GameObject.Find("PausePanel");
        pausePanel.SetActive(false);

        pauseButton = GetComponent<Button>();
        pauseButton.onClick.AddListener(PauseGame);
    }

    private void PauseGame()
    {
        if (!pausePanel.activeInHierarchy)
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }
    }
}
