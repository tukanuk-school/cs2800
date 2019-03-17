using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelChecker : MonoBehaviour
{
    GameObject levelPanel;
    Text nextLevelText;
    Text nextLevelInfoText;
    string scoreToAdvance;
    string tagline;

    // sound fx
    AudioSource victoryAS;

    private void Awake()
    {
        nextLevelText = GameObject.Find("nextLevelText").GetComponent<Text>();
        nextLevelInfoText = GameObject.Find("nextLevelInfoText").GetComponent<Text>();

        levelPanel = GameObject.Find("levelPanel");
        if (levelPanel.activeInHierarchy == true)
        {
            levelPanel.SetActive(false);
        }

        // setup the victory sound
        victoryAS = GameObject.Find("victoryAS").GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        EndOfLevelCheck();
    }

    private void EndOfLevelCheck()
    {
        // check for end of level
        if (ScoreManager.score >= ScoreManager.scoreToNextLevel)
        {       
            levelPanel.SetActive(true);
            Time.timeScale = 0;
            Nextlevel();

        }

        //Debug.Log(string.Format("s: {0} of {1}", ScoreManager.score, Main.S.levelWinPoints));
    }

    void Nextlevel()
    {
        if (ScoreManager.currentGameLevel == ScoreManager.GameLevels.Bronze)
        {
            ScoreManager.currentGameLevel = ScoreManager.GameLevels.Silver;
            ScoreManager.scoreToNextLevel = ScoreManager.SilverPointsToWin;
            ScoreManager.currentPrefabEnemies = ScoreManager.silverPrefabEnemies;
            //scoreToAdvance = ScoreManager.SilverPointsToWin.ToString();
            tagline = "You can do it!";
        } 
        else if (ScoreManager.currentGameLevel == ScoreManager.GameLevels.Silver)
        {
            ScoreManager.currentGameLevel = ScoreManager.GameLevels.Gold;
            ScoreManager.scoreToNextLevel = ScoreManager.SilverPointsToWin;
            ScoreManager.currentPrefabEnemies = ScoreManager.goldPrefabEnemies;
            //scoreToAdvance = ScoreManager.GoldPointsToWin.ToString();
            tagline = "Almost there!";
        }
        else if (ScoreManager.currentGameLevel == ScoreManager.GameLevels.Gold)
        {
            // if alread on Gold just keep going...
            ScoreManager.currentGameLevel = ScoreManager.GameLevels.Gold;
            // add your old score to the gold score so each level needs more and more points
            ScoreManager.scoreToNextLevel = ScoreManager.score + ScoreManager.GoldPointsToWin;
            ScoreManager.currentPrefabEnemies = ScoreManager.goldPrefabEnemies;
            //scoreToAdvance = ScoreManager.GoldPointsToWin.ToString();
            tagline = "You did it! ...except they just keep coming...";
        }

        nextLevelText.text = string.Format("{0} wave \nincoming", ScoreManager.currentGameLevel);
        nextLevelInfoText.text = string.Format("Reach {0} points to advance.\n {1}", 
            ScoreManager.scoreToNextLevel.ToString(), tagline);

        Debug.Log("Next level: " + ScoreManager.currentGameLevel);

        StartCoroutine(StartNextLevel());
    }

    IEnumerator StartNextLevel()
    {
        // setup the victory sound
        victoryAS.Play();
        yield return new WaitForSecondsRealtime(5.0f);
        Time.timeScale = 1;
        SceneManager.LoadScene("_Scene_1");
    }
}