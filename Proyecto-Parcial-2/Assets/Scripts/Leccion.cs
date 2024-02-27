using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Leccion
{
    public int ID;
    public string lessons;
    public List<string> options;
    public int correctAnswer;
}

[CreateAssetMenu(fileName = "New Subject", menuName = "ScriptableObjects/New_Lesson", order = 1)]

public class Subject : ScriptableObject
{
    [Header("GameObject Configuration")]
    public int Lesson = 0;

    [Header("Lession Quest Configuration")]
    public List<Leccion> leccionList;
}