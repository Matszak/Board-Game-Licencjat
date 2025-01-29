using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DiceRoll : MonoBehaviour
{
    [SerializeField] private  GameObject  uiButtonPrefab;
    
     
    public static event Action<int> OnDiceRolled;
  
    public void OnEnable()
    {
        //BoardGameManager.RequestDiceRoll += RollDices;
    }

    public void OnButtonClick()
    {
        Dice dice = new Dice(6);
        RollDices(dice,1);
    }
    
    public void RollDices(Dice typeOfDice,int numberOfDices)
    {
        int rollResult = 0;
        for (int i = 0; i < numberOfDices; i++)
        {
           rollResult += typeOfDice.RollDice();
        }
        OnDiceRolled?.Invoke(rollResult);
    }
}
