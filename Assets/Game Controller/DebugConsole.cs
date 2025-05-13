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
    public static Action<string> OnConsoleDisplayCentered;
    
    [SerializeField] private TextMeshProUGUI consoleText; //Placeholder for text
    
    void OnEnable()
    {
        OnConsoleDisplay += LogToConsole; // subskrypcja
        OnConsoleDisplayCentered += LogToConsoleCentered;
    }

    void OnDisable()
    {
        OnConsoleDisplay -= LogToConsole; // usunięcie suba
        OnConsoleDisplayCentered -= LogToConsoleCentered;
    }

    public static void Log(string content)
    {
        OnConsoleDisplay?.Invoke(content); // Wywoływanie klasy poprzez log
    }

    public static void LogCentered(string content)
    {
        OnConsoleDisplayCentered?.Invoke(content);
    }
    
    private void LogToConsole (string content)
    {
        consoleText.text += "\n" + content;  //wyświetlanie kontentu
    }
    
    private void LogToConsoleCentered(string content)
    {
        consoleText.text += $"\n<align=center>{content}</align>";
    }
}
