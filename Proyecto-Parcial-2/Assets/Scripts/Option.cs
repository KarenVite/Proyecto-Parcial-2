using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Option : MonoBehaviour
{
    public int OptionID;
    public string OptionName;

    void Start()
    {
        //Obtenemos el componente texto para aztualizarlo al texto de la
        //pregunta de nuestro scriptableObject
        transform.GetChild(0).GetComponent<TMP_Text>().text = OptionName;
    }

    //Actualizar texto
    public void UpdateText()
    {
        //Obtenemos el child para actualizarlo a la lista del scriptable Object
        transform.GetChild(0).GetComponent<TMP_Text>().text = OptionName;
    }

    //Nos aseguramos de que una opción haya sido seleccionada y llamamos 2 funciones
    //del script: LevelManager
    public void SelectOption()
    {
        //Asignamos la respuesta correcta en función del ID del script: Leccion
        LevelManager.Instance.SetPlayerAnswer(OptionID);
        //Con LevelManager comprobamos si una respuesta fue seleccionada y si los botones son
        //interactuables
        LevelManager.Instance.CheckPlayerState();
    }
}
