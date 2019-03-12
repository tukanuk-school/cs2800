
// For more details about this quiz game can be found at
// https://unity3d.com/learn/tutorials/topics/scripting/intro-and-setup

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoundData 
{

    public string name;
    public int timeLimitInSeconds;
    public int pointsAddedForCorrectAnswer;
    public QuestionData[] questions;

}
