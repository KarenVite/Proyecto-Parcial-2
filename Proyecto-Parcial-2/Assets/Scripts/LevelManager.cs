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

    //GameObjects para la UI
    [Header("User Interface")]
    public TMP_Text textQuestion;
    public TMP_Text textQuestion1;
    public GameObject CheckButton;
    public List<Option> option;
    public GameObject AnswerContainer;
    public Color Green;
    public Color Red;
    
    //Esto recibirá el script del scriptableObject
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
            //Establecemos la leccion actual
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
                //Agreegamos el contenido(respuesta), así como su ID
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

            //Activa el AnswerContainer
            AnswerContainer.SetActive(true);

            //Si se cumple la variable isCorrect
            if (isCorrect)
            {
                //El contenedor cambia a color verde
                AnswerContainer.GetComponent<Image>().color = Green;
                textQuestion1.text = "Respuesta correcta";
                Debug.Log("Respuesta correcta. " + question + ": " + correctAnswer);
            }
            else //Si no se cumple la variable isCorrect
            {
                //El contenedor cambia a color rojo 
                AnswerContainer.GetComponent<Image>().color = Red;
                textQuestion1.text = "Respuesta Incorrecta";
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
        yield return new WaitForSeconds(2.5f);//Ajusta el tiempo que deseas mostrar el resultado
        //Oculta el contenedor
        AnswerContainer.SetActive(false);

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
        //Activamos el boton de comprobar si el correct answer es diferente a 9
        if (answerFromPlayer != 9)
        {
            CheckButton.GetComponent<Button>().interactable = true;
            CheckButton.GetComponent<Image>().color = Color.white;
            return true;
        }
        else //Desactivamos el boton si el AnswerFromPlayer es 9 (se mantiene descativado hasta que el jugador seleccione una opción)
        {
            CheckButton.GetComponent<Button>().interactable = false;
            CheckButton.GetComponent<Image>().color = Color.grey;
            return false;
        }
    }
    
}
