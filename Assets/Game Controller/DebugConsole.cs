using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class DebugConsole : MonoBehaviour
{
    
    public static Action<string> OnConsoleDisplay; //Event które inne klasy mogą wywołać
    
    [SerializeField] private TextMeshProUGUI consoleText; //Placeholder for text

    void OnEnable()
    {
        OnConsoleDisplay += LogToConsole; // subskrypcja
    }

    void OnDisable()
    {
        OnConsoleDisplay -= LogToConsole; // usunięcie suba
    }

    public static void Log(string content)
    {
        OnConsoleDisplay?.Invoke(content); //Uruchomienie metody
    }

    private void LogToConsole (string content)
    {
        consoleText.text += "\n" + content; //Właściwe wpisywanie w console
    }
}
