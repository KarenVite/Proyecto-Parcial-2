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
        transform.GetChild(0).GetComponent<TMP_Text>().text = OptionName;
    }
}
