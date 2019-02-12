using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperRockScissor : MonoBehaviour
{
    [Header("Set in inspector")]
    public int numRounds = 10;
    public string playerOneInput;
    public string playerTwoInput;
    public int currentRound;

    // Start is called before the first frame update
    void Start()
    {
        // while (currentRound < numRounds)
        // {
            
        // }
    }

    // Update is called once per frame
    void Update()
    {
        getPlayerInput();

        if (playerOneInput != "" && playerTwoInput != "")
        {
            currentRound++;
            playerOneInput = "";
            playerTwoInput = "";
        }
    }

    void getPlayerInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            playerOneInput = "paper";
        }
        else if (Input.GetKey(KeyCode.S))
        {
            playerOneInput = "rock";
        }
        else if (Input.GetKey(KeyCode.D))
        {
            playerOneInput = "scissors";
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerTwoInput = "paper";
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            playerTwoInput = "rock";
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            playerTwoInput = "scissors";
        }
    }

}
