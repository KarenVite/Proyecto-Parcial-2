using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LessonContainer : MonoBehaviour
{
    [Header("GameObject Configuration")]
    public int Lection = 0;
    public int CurrentLession = 0;
    public int TotalLessions = 0;
    public bool AreAllLessonsComplete = false;

    [Header("UI Configuration")]
    public TMP_Text StageTitle;
    public TMP_Text LessonStage;

    [Header("External GameObject Configuration")]
    public GameObject lessonContainer;

    [Header("Lesson Data")]
    public ScriptableObject LessonData;

    void Start()
    {
        //si el gameObject está puesto se va a OnUpdateUI
        if (lessonContainer != null)
        {
            OnUpdateUI();
        }
        else
        {
            Debug.LogWarning("GameObject Nulo, revisa las variables de tipo GameObject lessonContainer");

        }
    }

    public void OnUpdateUI()
    {
        //Accedemos a los textos StageTitle y LessonStage
        if (StageTitle != null || LessonStage != null)
        {
            //Esto se mostrará en la UI
            StageTitle.text = "Leccion " + Lection;
            LessonStage.text = "Leccion " + CurrentLession + " de " + TotalLessions;
        }
        else
        {
            //Enviamos un mensaje por si los textos no están puestos
            Debug.LogWarning("GameObject Nulo, revisa las variables de tipo TMP_Text");
        }
    }

    //Este metodo activa/desactiva la ventana de lessonContainer

    public void EnableWindow()
    {
        OnUpdateUI();
        //hacemos que la ventana de la lección aparezca y desaparezca
        if (lessonContainer.activeSelf)
        {
            //Desactiva el objeto si está activo
            lessonContainer.SetActive(false);
        }
        else
        {
            //Activa el objeto si está desactivado
            lessonContainer.SetActive(true);
        }
    }
}
