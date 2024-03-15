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

    //Cargar la pregunta siguiente
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
                //Agregamos el contenido(respuesta), así como su ID
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

    //Para pasar a la siguiente pregunta
    public void NextQuestion()
    {
        //Revisa la respuesta que selecciona el jugador
        if (CheckPlayerState())
        {
            //Revisamos si la respuesta es correcta o no
            bool isCorrect = currentLesson.options[answerFromPlayer] == correctAnswer;

            //Activa el AnswerContainer
            AnswerContainer.SetActive(true);

            //Revisa si la respuesta es correcta
            if (isCorrect)
            {
                //El contenedor cambia a color verde si la respuesta es correcta
                AnswerContainer.GetComponent<Image>().color = Green;
                textQuestion1.text = "Respuesta correcta";
                Debug.Log("Respuesta correcta. " + question + ": " + correctAnswer);
            }
            else //Si no es correcta
            {
                //El contenedor cambia a color rojo si la respuesta es incorrecta
                AnswerContainer.GetComponent<Image>().color = Red;
                textQuestion1.text = "Respuesta Incorrecta";
                Debug.Log("Respuesta Incorrecta. " + question + ": " + correctAnswer);
            }

            //Incrementamos el índice de la pregunta actual para que la pregunta no se repita
            currentQuestion++;

            //ShowResultAndLoadQuestion comienza una corrutina (las corrutinas comunmente son
            //utilizadas en escenarios scenarios donde se necesitan procesos de larga duración,
            //como la carga de recursos) que suspende por 2 segundos el contenedor de respuesta
            //y cambiará de pregunta
            StartCoroutine(ShowResultAndLoadQuestion(isCorrect));

            //Reinicia la respuesta del jugador para la nueva pregunta
            answerFromPlayer = 9;

        }
        else
        {
            //Cambio de escena
        }
        
    }

    //Inicia una corrutina que suspende el código dependiendo de lo que se especigique dentro
    //de esta
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

    //Asignará la respuesta del jugador
    public void SetPlayerAnswer(int _answer)
    {
        //Actualiza la respuesta del jugador
        answerFromPlayer = _answer;
    }

    //Nos aseguramos si el jugador presionó un botón para cambiar su color y activarlo
    public bool CheckPlayerState()
    {
        //Nos aseguramos si los botonoes cambian de color al ser presionados
        if (answerFromPlayer != 9)
        {
            //Actualizamos el componente boton para que sea interactuable
            CheckButton.GetComponent<Button>().interactable = true;
            //Actualizamos el componente Imagen para que cambie su color
            CheckButton.GetComponent<Image>().color = Color.white;
            return true;
        }
        else //Si no se interactua con el boton
        {
            //Actualizamos el componente boton para que no se pueda presionar
            CheckButton.GetComponent<Button>().interactable = false;
            //Actualizamos el componente Imagen para que cambie su color
            CheckButton.GetComponent<Image>().color = Color.grey;
            return false;
        }
    }
    
}
