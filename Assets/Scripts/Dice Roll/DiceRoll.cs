using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DiceRoll : MonoBehaviour
{
    [SerializeField] private  GameObject  uiButtonPrefab;
    [SerializeField] private Button RollDiceButton;
    
    // Event to notify listeners with the result
    public event Action<int> OnDiceRolled;
    
    [SerializeField] private int numberOfDices;
    [SerializeField] private int sides;
   


    private void Start()
    {
        uiButtonPrefab.SetActive(false);
    }

    public int RollDices(int numberOfDices, int sides)
    {
        int rollResult = 0;
        for (int i = 0; i < numberOfDices; i++)
        {
            rollResult += UnityEngine.Random.Range(1,sides);
        }
        
 
        return rollResult;
    }
    public void RequestDiceRoll(int numberOfDices, int sides)
    {
        // You could add logic here (like UI updates, etc.) before triggering the dice roll
        this.numberOfDices = numberOfDices;
        this.sides = sides;
        uiButtonPrefab.SetActive(true);
    }

    public void Roll()
    {
        uiButtonPrefab.SetActive(false);
        int result = RollDices(numberOfDices, sides);
        OnDiceRolled?.Invoke(result);
    }
    
    
}
