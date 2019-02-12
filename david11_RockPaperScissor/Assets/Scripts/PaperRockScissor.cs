using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaperRockScissor : MonoBehaviour
{
    [Header("Set in inspector")]
    public int numRounds = 10;
    public string playerOneInput;
    public string playerTwoInput;
    public Text currentRound;

    public Text playerOneScore;
    public Text playerTwoScore;

    // Start is called before the first frame update
    void Start()
    {
        // find reference to round counter
        GameObject roundCounterGO = GameObject.Find("RoundLabelCounter");
        currentRound = roundCounterGO.GetComponent<Text>();
        currentRound.text = "1";

        // find reference to player one score
        GameObject playerOneScoreGO = GameObject.Find("PlayerOneScore");
        playerOneScore = playerOneScoreGO.GetComponent<Text>();
        playerOneScore.text = "0";

        // find reference to player two score
        GameObject playerTwoScoreGO = GameObject.Find("PlayerTwoScore");
        playerTwoScore = playerTwoScoreGO.GetComponent<Text>();
        playerTwoScore.text = "0";
    }

    // Update is called once per frame
    void Update()
    {

        GetPlayerInput();

        if (playerOneInput != "" && playerTwoInput != "")
        {
            if (playerOneInput.Equals(playerTwoInput) != true)
            {

                // game logic
                if (playerOneInput.Equals("paper") && playerTwoInput.Equals("rock") ||
                    playerOneInput.Equals("rock") && playerTwoInput.Equals("scissors") ||
                    playerOneInput.Equals("scissors") && playerTwoInput.Equals("paper"))
                {
                    int point = int.Parse(playerOneScore.text);
                    point++;
                    playerOneScore.text = point.ToString();
                }
                else
                {
                    int point = int.Parse(playerTwoScore.text);
                    point++;
                    playerTwoScore.text = point.ToString();
                }

                int round = int.Parse(currentRound.text);
                round++;
                currentRound.text = round.ToString();

            }

            playerOneInput = "";
            playerTwoInput = "";
        }
    }

    // Watches for valid player input
    void GetPlayerInput()
    {
        // Watches for valid player input
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
