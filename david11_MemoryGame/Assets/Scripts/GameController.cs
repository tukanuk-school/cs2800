using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Sprite bgImage;

    public Sprite[] puzzles;
    public List<Sprite> gamePuzzles = new List<Sprite>();


    public List<Button> btns = new List<Button>();

    private bool firstGuess, secondGuess;

    private int countGuesses;
    private int correctGuesses;
    private int gameGuesses;
    private int firstGuessIndex, secondGuessIndex;

    private string firstGuessPuzzle, secondGuessPuzzle;


    private void Awake()
    {
        puzzles = Resources.LoadAll<Sprite>("Cards");
    }

    private void Start()
    {
        GetButtons();
        AddListeners();
        AddGamePuzzles();
        Shuffle(gamePuzzles);

        gameGuesses = gamePuzzles.Count / 2;
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

        } else if (!secondGuess)
        {
            secondGuess = true;
            secondGuessIndex = int.Parse(cardName);
            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;

            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];


            StartCoroutine(CheckIfThePuzzlesMatch());

            //if(firstGuessPuzzle == secondGuessPuzzle)
            //{
            //    Debug.Log("Puzzles match");
            //}
            //else
            //{
            //    Debug.Log("Puzzles don't match");
            //}
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


            CheckIfTheGameIsFinished();
        }
        else
        {
            btns[firstGuessIndex].image.sprite = bgImage;
            btns[secondGuessIndex].image.sprite = bgImage;
        }

        yield return new WaitForSeconds(0.25f);
        firstGuess = secondGuess = false;

    }

    private void CheckIfTheGameIsFinished()
    {
        correctGuesses++;

        if (correctGuesses == gameGuesses)
        {
            Debug.Log("game finsihed");
            Debug.Log("It took " + countGuesses + " to finish!");
        }
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