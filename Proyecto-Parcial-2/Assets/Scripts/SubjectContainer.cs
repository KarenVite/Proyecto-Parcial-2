using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

//Va a leer lo que viene del JSON que es 
public class SubjectContainer 
{
    [Header("GameObject Configuration")]
    //Definimos el n?mero de nuestra Lecci?n
    [SerializeField]
    public int Lesson = 0;

    [Header("Lession Quest Configuration")]
    [SerializeField]
    //Para definir cuantas preguntas tendr? nuestra lecci?n
    public List<Leccion> leccionList;
}