using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    [Header("Level Data")]
    public Subject Lesson;

    [Header("User Interface")]
    public TMP_Text textQuestion;
    public GameObject CheckButton;
    public List<Option> option;
    public GameObject AnswerContainer;
    public Color Green;
    public Color Red;
    

    [Header("Game Configuration")]
    public int questionAmount = 0;
    public int currentQuestion = 0;
    public string question;
    public string correctAnswer;
    public int answerFromPlayer = 9;

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
        if (CheckPlayerState())
        {
            //Revisamos si la respuesta es correcta o no
            bool isCorrect = currentLesson.options[answerFromPlayer] == correctAnswer;

            AnswerContainer.SetActive(true);

            if (isCorrect)
            {
                AnswerContainer.GetComponent<Image>().color = Green;
                Debug.Log("Respuesta correcta. " + question + ": " + correctAnswer);
            }
            else
            {
                AnswerContainer.GetComponent<Image>().color = Red;
                Debug.Log("Respuesta Incorrecta. " + question + ": " + correctAnswer);
            }

            //Incrementamos el índice de la pregunta actual
            currentQuestion++;

            //Mostrar el resultado durante un tiempo (puedes usar una coroutine o Invoke)
            StartCoroutine(ShowResultAndLoadQuestion(isCorrect));

            //Reset answer from player
            answerFromPlayer = 9;

        }
        else
        {
            //Cambio de escena
        }
        
    }

    private IEnumerator ShowResultAndLoadQuestion(bool isCorrect)
    {
        yield return new WaitForSeconds(2.5f);//Ajusta el tiempo que deseas mostrar el rsultado

        //Cargar la nueva pregunta
        LoadQuestion();

        //Activa el botón después de mostrar el resultado
        //Puedes hacer esto aquí o en LoadQuestion(), dependiendo de tu estructuraa
        //por ejemplo, si el botón está en el mismo GameObject que el script:
        //GetComponent<Button>().intercatable = true;
        CheckPlayerState();
    }

    public void SetPlayerAnswer(int _answer)
    {
        answerFromPlayer = _answer;
    }

    public bool CheckPlayerState()
    {
        if (answerFromPlayer != 9)
        {
            CheckButton.GetComponent<Button>().interactable = true;
            CheckButton.GetComponent<Image>().color = Color.white;
            return true;
        }
        else
        {
            CheckButton.GetComponent<Button>().interactable = false;
            CheckButton.GetComponent<Image>().color = Color.grey;
            return false;
        }
    }
    
}
