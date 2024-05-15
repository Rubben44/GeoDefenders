using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathManager : MonoBehaviour
{
    public static MathManager Instance { get; private set; }
    
    void Awake()
    {
        Instance = this;
    }

    public void CheckAnswer(MathEquation equation, string playerAnswer, Tower tower)
    {
        bool isCorrect = equation.CorrectAnswer == playerAnswer;

        if (isCorrect)
        {
            tower.ReloadAmmo();
        }
    }
}
