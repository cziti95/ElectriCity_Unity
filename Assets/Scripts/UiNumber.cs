using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiNumber : MonoBehaviour
{
    public string valueName;
    public int number { get; set; }

    private Text text;
    
    void Awake()
    {
        text = GetComponent<Text>();
       // Debug.Log(text.text);
    }

    void Update()
    {
        text.text = string.Format("{0}: {1}", valueName, number);
    }
}
