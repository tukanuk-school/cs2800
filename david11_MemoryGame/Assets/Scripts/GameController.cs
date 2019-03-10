using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Sprite bgImage; // card bg image

    public Sprite[] puzzles;
    public List<Sprite> gamePuzzles = new List<Sprite>();

    public List<Button> btns = new List<Button>();

    public int GameScore = 1000;
    public float startGameTime, endGameTime;
    public int ScoreText;
    public float TimeText;

    public Text WinningMessageText;

    public Text ScoreTextLabel;
    public Text TimeTextLabel;

    private bool firstGuess, secondGuess;

    private int countGuesses;
    private int correctGuesses;
    private int gameGuesses;
    private int firstGuessIndex, secondGuessIndex;

    private string firstGuessPuzzle, secondGuessPuzzle;


    public AudioSource audioSource;
    public AudioClip flipItAudioClip;
    public AudioClip goodJobAudioClip;
    public AudioClip tryAgainAudioClip;
    public AudioClip YouWinAudioClip;


    private void Awake()
    {
        puzzles = Resources.LoadAll<Sprite>("Cards");
    }

    private void Start()
    {
        GetButtons();
        AddListeners();
        GetUI();
        AddGamePuzzles();
        Shuffle(gamePuzzles);

        startGameTime = Time.time;

        gameGuesses = gamePuzzles.Count / 2;
    }

    private void Update()
    {
        float tempTime = Time.time;
        TimeTextLabel.text = ((tempTime - startGameTime).ToString("###.0"));
    }

    private void GetUI()
    {
        ScoreTextLabel.text = "1000";
        TimeTextLabel.text = "0";
        WinningMessageText.text = "";
    }

    void GetButtons() 
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");

        foreach (GameObject b in objects)
        {
            btns.Add(b.GetComponent<Button>());
            btns[btns.Count - 1].image.sprite = bgImage;
            //Debug.Log("Button #: " + b);

        }
    }

    void AddGamePuzzles()
    {
        int looper = btns.Count;
        int index = 0;

        for (int i = 0; i < looper; i++)
        {
            if (index == looper / 2)
            {
                index = 0;
            }

            gamePuzzles.Add(puzzles[index]);

            index++;
        }
    }

    void AddListeners()
    {
        foreach (Button btn in btns)
        {
            btn.onClick.AddListener(() => PickAPuzzle());
        }
    }


    public void PickAPuzzle()
    {
        string cardName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        Debug.Log("Clickin a button name " + cardName);

        if (!firstGuess)
        {
            firstGuess = true;
            firstGuessIndex = int.Parse(cardName);
            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;

            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];

            audioSource.clip = flipItAudioClip;
            audioSource.Play();


        } else if (!secondGuess)
        {
            secondGuess = true;
            secondGuessIndex = int.Parse(cardName);
            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;

            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];


            StartCoroutine(CheckIfThePuzzlesMatch());
        }
    }

    IEnumerator CheckIfThePuzzlesMatch()
    {
        yield return new WaitForSeconds(0.5f);

        countGuesses++;

        if (firstGuessPuzzle == secondGuessPuzzle)
        {
            yield return new WaitForSeconds(0.25f);

            btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;

            btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);

            audioSource.clip = goodJobAudioClip;
            audioSource.Play();

            CheckIfTheGameIsFinished();
        }
        else
        {
            btns[firstGuessIndex].image.sprite = bgImage;
            btns[secondGuessIndex].image.sprite = bgImage;

            GameScore -= 40;

            audioSource.clip = tryAgainAudioClip;
            audioSource.Play();
        }

        yield return new WaitForSeconds(0.25f);
        firstGuess = secondGuess = false;
        Debug.Log("Score: " + GameScore);
        ScoreTextLabel.text = GameScore.ToString();


    }

    private void CheckIfTheGameIsFinished()
    {
        correctGuesses++;

        if (correctGuesses == gameGuesses)
        {
            endGameTime = Time.time;
            TimeTextLabel.text = endGameTime.ToString();
            Debug.Log("game finished");
            Debug.Log("It took " + countGuesses + " guesses to finish!");
            Debug.Log("It took " + (endGameTime - startGameTime) + " seconds to finish!");

            String temp = String.Format("You Win!\n Your score was " +
                "{0}. It took you {1} guesses over {2} seconds", GameScore.ToString(),
                countGuesses.ToString(), (endGameTime - startGameTime).ToString("###.0") );

            WinningMessageText.text = temp;

            audioSource.clip = YouWinAudioClip;
            audioSource.Play();

            StartCoroutine(RestartTheGame());

        }
    }

    IEnumerator RestartTheGame()
    {
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("_Scene_title");

    }

    void Shuffle (List<Sprite> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(0, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;

        }
    }
}