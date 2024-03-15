using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Esta etiquetea se utiliza para indicar que una clase puede ser serializada, o sea que sus
//instancias pueden ser convertidas en un fromato que puede ser almacenado o transmitido 
[System.Serializable]

public class Leccion
{
    //Estas variables las usaremos en otros scripts y ayudarán a asignar respuestas
    public int ID;
    public string lessons;
    public List<string> options;
    public int correctAnswer;
}
