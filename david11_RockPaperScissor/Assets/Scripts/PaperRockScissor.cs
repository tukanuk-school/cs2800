using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PaperRockScissor : MonoBehaviour
{
    // intial variables
    [Header("Set in inspector")]
    public int numRounds = 10;
    public string playerOneInput;
    public string playerTwoInput;
    public Text currentRound;
    public Text theWinner;

    public Text playerOneScore;
    public Text playerTwoScore;

    public int scoreCheck;
    public int copyCounter;
    public string eeText = "Seriously, ";

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

        // gets player input
        GetPlayerInput();

        // when both players have made a choice
        if (playerOneInput != "" && playerTwoInput != "")
        {
            // testing 
            Debug.Log("p1: " + playerOneInput);
            Debug.Log("p2: " + playerTwoInput);

            // if players made different choices
            if (playerOneInput.Equals(playerTwoInput) != true)
            {

                // game logic
                if (playerOneInput.Equals("paper") && playerTwoInput.Equals("rock") ||
                    playerOneInput.Equals("rock") && playerTwoInput.Equals("scissors") ||
                    playerOneInput.Equals("scissors") && playerTwoInput.Equals("paper"))
                {
                    // player one wins round
                    int point = int.Parse(playerOneScore.text);
                    point++;
                    playerOneScore.text = point.ToString();

                    if (playerOneInput.Equals("paper"))
                        WinText("Paper covers rock");
                    else if (playerOneInput.Equals("rock"))
                        WinText("Rock crushes scissors");
                    else
                        WinText("Scissors cuts paper");

                    // clears text after one second unless a winner, than extra bragging time
                    if (playerOneScore.text != "10") Invoke("ClearText", 1.0f);
                }
                else
                {
                    int point = int.Parse(playerTwoScore.text);
                    point++;
                    playerTwoScore.text = point.ToString();

                    if (playerTwoInput.Equals("paper"))
                        WinText("Paper covers rock");
                    else if (playerTwoInput.Equals("rock"))
                        WinText("Rock crushes scissors");
                    else
                        WinText("Scissors cuts paper");

                    // clears text after one second unless a winner, than extra bragging time
                    if (playerTwoScore.text != "10") Invoke("ClearText", 1.0f);
                }

                int round = int.Parse(currentRound.text);
                round++;
                currentRound.text = round.ToString();

            }

            if (playerOneInput.Equals(playerTwoInput))
            {
                copyCounter++;
                // easter egg
                if (copyCounter >= 5)
                {
                    if (copyCounter > 5)
                    {
                        eeText += "seriously, ";
                    }
                    WinText(eeText + "stop copying!");
                }
                else
                {
                    WinText("Stop copying!");
                }

                Invoke("ClearText", 1.0f);
            }

            // check for and declare a winner and reset the game
            if (int.Parse(playerOneScore.text) >= 10 || int.Parse(playerTwoScore.text) >= 10)
            {

                // prints winner text
                if (int.Parse(playerOneScore.text) >= 10)
                {
                    WinText("PLAYER ONE WINS");
                }
                else
                {
                    WinText("PLAYER TWO WINS");
                }

                // waits five seconds to give time to show winner
                Invoke("ResetGame", 5.0f);

            }

            // clears the players choice to get ready for the next round
            ClearChoice();


        }
    }

    // Watches for valid player input
    void GetPlayerInput()
    {
        // Watches for valid player input
        if (Input.GetKeyDown(KeyCode.A))
        {
            playerOneInput = "paper";
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            playerOneInput = "rock";
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            playerOneInput = "scissors";
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            playerTwoInput = "paper";
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            playerTwoInput = "rock";
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            playerTwoInput = "scissors";
        }


    }

    // clears the choices
    void ClearChoice()
    {
        playerOneInput = "";
        Debug.Log("p1: " + playerOneInput);
        playerTwoInput = "";
        Debug.Log("p2: " + playerTwoInput);
    }

    // reloads main scene
    void ResetGame()
    {
        SceneManager.LoadScene("_Scene_0"); // reset the game
    }

    // prints input text in the Winner_1 box
    void WinText(string resultText)
    {
        GameObject winnerGO = GameObject.Find("Winner_1");
        theWinner = winnerGO.GetComponent<Text>();
        theWinner.text = resultText;
    }

    // removes win text
    void ClearText()
    {
        theWinner.text = "";
    }


}
