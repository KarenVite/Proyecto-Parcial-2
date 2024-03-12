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
        //Obtenemos el componente texto de la opción y será igual al nombre
        transform.GetChild(0).GetComponent<TMP_Text>().text = OptionName;
    }

    public void UpdateText()
    {
        //Actualizará el texto conforme avancemos
        transform.GetChild(0).GetComponent<TMP_Text>().text = OptionName;
    }

    public void SelectOption()
    {
        //Mandamos el contenido de OptionID al setPlayerAnswer que se encuentra en el script LevelManager
        LevelManager.Instance.SetPlayerAnswer(OptionID);
        //
        LevelManager.Instance.CheckPlayerState();
    }
}
