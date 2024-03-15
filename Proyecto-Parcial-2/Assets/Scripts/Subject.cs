using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este Script es para configurar nuestro scriptableObject

[CreateAssetMenu(fileName = "New Subject", menuName = "ScriptableObjects/New_Lesson", order = 1)]

public class Subject : ScriptableObject
{
    [Header("GameObject Configuration")]
    //Definimos el número de nuestra Lección
    public int Lesson = 0;

    [Header("Lession Quest Configuration")]
    //Para definir cuantas preguntas tendrá nuestra lección
    public List<Leccion> leccionList;
}