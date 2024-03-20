using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;

    public Leccion data;
    public SubjectContainer subject;

    //Singleton es un patrón de diseño que se asegura que una clase tenga una sola instancia y
    //da un punto de acceso global a esa instancia
    private void Awake()
    {
        //creamos la instancia
        if (Instance != null)
        {
            return;
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        SaveToJSON("LeccionDummy", data);
        //SaveToJSON("SubjectDummy", subject);

        subject = LoadFromJSON<SubjectContainer>("SubjectDummy");
    }

    //<summary>
    //Esta función está encargada de almacenar objetos en archivos JSON.
    //</summary>
    //<param name="_fileName"></param>
    //<param name="_data"></param>

    public void SaveToJSON(string _fileName, object _data)
    {
        if (_data != null)
        {
            //_data va a tranformarse en un archivo JSON
            string JSONData = JsonUtility.ToJson(_data, true);

            if(JSONData.Length != 0)
            {
                Debug.Log("JSON STRING: " + JSONData);

                //creamos el archivo que se va a almacenar
                string fileName = _fileName + ".json";
                //direccion donde se va a almacenar el archivo
                string filePath = Path.Combine(Application.dataPath + "/Resources/JSONS", fileName);
                //escribir el recurso en memoria
                File.WriteAllText(filePath, JSONData);
                //para indicar en donde se guardó el archivo
                Debug.Log("JSON almacenado en la direccion: " + filePath);
            }
            else
            {
                Debug.LogWarning("ERROR- FileSystem: _data is null, check for param [object _data]");
            }
        }
        else
        {
           
        }
    }

    public T LoadFromJSON<T>(string _fileName) where T : new()
    {
        T Dato = new T();
        string path = Application.dataPath + "/Resources/JSONS/" + _fileName + ".json";
        string JSONData = "";
        if (File.Exists(path))
        {
            JSONData = File.ReadAllText(path);
            Debug.Log("JSON STRING: " + JSONData);
        }
        if (JSONData.Length != 0)
        {
            JsonUtility.FromJsonOverwrite(JSONData, Dato);
        }
        else
        {
            Debug.LogWarning("ERROR - FileSystem: JSONData is empty, check for local variable [string JSONData]");
        }
        return Dato;
    }
}
