using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MathEquation 
{
    [SerializeField] private string equation;
    [SerializeField] private List<string> possibleAnswers;
    [SerializeField] private string correctAnswer;

    public string Equation => equation;
    public List<string> PossibleAnswers => possibleAnswers;
    public string CorrectAnswer => correctAnswer;

    public MathEquation(string equation, List<string> possibleAnswers, string correctAnswer)
    {
        this.equation = equation;
        this.possibleAnswers = possibleAnswers;
        this.correctAnswer = correctAnswer;
    }
}
