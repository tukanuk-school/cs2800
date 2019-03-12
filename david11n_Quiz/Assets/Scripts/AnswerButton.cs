
// For more details about this quiz game can be found at
// https://unity3d.com/learn/tutorials/topics/scripting/intro-and-setup

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{

    public Text answerText;

    private AnswerData answerData;

    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    public void Setup(AnswerData data)
    {
        answerData = data;
        answerText.text = answerData.answerText;
        
    }


    public void HandleClick()
    {
        gameController.AnswerButtonClicked(answerData.isCorrect);
    }

 
}
