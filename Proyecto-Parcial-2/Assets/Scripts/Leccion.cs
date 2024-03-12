using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Leccion
{
    //Las opciones que tiene el scriptableObject para las preguntas que nosotros asignamos
    public int ID;
    public string lessons;
    public List<string> options;
    public int correctAnswer;
}
