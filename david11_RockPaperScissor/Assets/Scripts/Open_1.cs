using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Open_1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("Open_2", 2.0f); // waits five seconds to give time show title
    }

    // declares a winner
    void Open_2()
    {
        SceneManager.LoadScene("_Scene_3"); // go to instructions

    }


}