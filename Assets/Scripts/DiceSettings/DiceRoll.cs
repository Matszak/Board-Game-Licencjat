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
    
    public int rollResult;
    private Player _player;
     
    public static event Action<int, Player> DiceRolled;
    

    public void RequestDiceRoll(Player player)
    {
        _player = player;
        uiButtonPrefab.SetActive(true);
    }


    public void OnButtonClick()
    {
        Dice dice = new Dice(6);
        RollDices(dice,1);
    }
    
    public void RollDices(Dice typeOfDice,int numberOfDices)
    {
        for (int i = 0; i < numberOfDices; i++)
        {
           rollResult += typeOfDice.RollDice();
        }
        uiButtonPrefab.SetActive(false);
        DiceRolled?.Invoke(rollResult,_player);
    }
}
