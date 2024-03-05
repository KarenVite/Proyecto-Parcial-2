using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    [Header("Level Data")]
    public Subject Lesson;
    [Header("User Interface")]
    public TMP_Text textQuestion;
    public List<Option> option;
    [Header("Game Configuration")]
    public int questionAmount = 0;
    public int currentQuestion = 0;
    public string question;
    public string correctAnswer;
    public int answerFromPlayer;
    [Header("Current Lesson")]
    public Leccion currentLesson;

    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        //Establecemos la cantidad de preguntas en la leccion
        questionAmount = Lesson.leccionList.Count;
        //Cargar la primera pregunta
        LoadQuestion();
    }

    private void LoadQuestion()
    {
        //Aseguramos que la pregunta actual este dentro de los limites
        if (currentQuestion < questionAmount)
        {
            //Establcemos la leccion actual
            currentLesson = Lesson.leccionList[currentQuestion];
            //Establecemos la pregunta
            question = currentLesson.lessons;
            //Establecemos la respuesta correcta
            correctAnswer = currentLesson.options[currentLesson.correctAnswer];
            //Establecemos la pregunta en la UI
            textQuestion.text = question;
            //Establecemos las Opciones
            for (int i = 0; i < currentLesson.options.Count; i++)
            {
                option[i].GetComponent<Option>().OptionName = currentLesson.options[i];
                option[i].GetComponent<Option>().OptionID = i;
                option[i].GetComponent<Option>().UpdateText();
            }
        }
        else
        {
            //Si llegamos al final de las preguntas
            Debug.Log("Fin de las preguntas");
        }
    }

    public void NextQuestion()
    {
        if (currentQuestion < questionAmount)
        {
            //Incrementamos el indice de la pregunta actual
            currentQuestion++;
            //Cargar la nueva pregunta
            LoadQuestion();
        }
        else
        {
            //Cambio de escena
        }
    }

    public void SetPlayerAnswer(int _answer)
    {
        answerFromPlayer = _answer;
    }
    
}