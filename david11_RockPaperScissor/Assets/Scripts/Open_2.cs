using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Open_2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Invoke("Open_0", 7.0f); // waits 7seconds to give to read instructions
    }

    // declares a winner
    void Open_0()
    {
        SceneManager.LoadScene("_Scene_0"); // go to game

    }


}