using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// For more details about this quiz game can be found at
// https://unity3d.com/learn/tutorials/topics/scripting/intro-and-setup

public class MenuScreenController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game");

    }


    public void ExitGame()
    {
        Application.Quit();

    }
}