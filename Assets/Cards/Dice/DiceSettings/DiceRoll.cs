using System;
using System.Collections;
using System.Collections.Generic;
using Cards.EnemyCards;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DiceRoll : MonoBehaviour
{   
 
    [FormerlySerializedAs("uiButtonPrefab")] [SerializeField] private  GameObject  RollDicesButton;
    [SerializeField] private Player _player;
    private EnemyCard _enemyCard;
    private DiceRollAnimation _diceRollAnimation;
    private int _rollResult;

 
    private Action<int> _onDiceRolled;
    
    public void Awake()
    {
      _diceRollAnimation = GetComponent<DiceRollAnimation>();
      RollDicesButton.SetActive(false);
    }
 
    public void RequestDiceRoll(bool isEnemy, Action<int> callback)
    {
        _onDiceRolled = callback;
        // no need for creating dices and dices number because there is always one 6d dice
        Dice dice = new Dice(6);
        if (!isEnemy)
        {
            RollDicesButton.SetActive(true);
        }
        else
        {
            RollDices(dice,1, _onDiceRolled);
        }
    }
    
    public void OnButtonClick()
    {
        Dice dice = new Dice(6);
        RollDices(dice, 1, _onDiceRolled) ;
    }

    private void RollDices(Dice typeOfDice,int numberOfDices, Action<int> callback = null)
    {
        _onDiceRolled = callback;
        _rollResult = 0;
        for (int i = 0; i < numberOfDices; i++)
        {
           _rollResult += typeOfDice.RollDice();
        }
        RollDicesButton.SetActive(false);
        _diceRollAnimation.PlayAnimation(_rollResult, () => 
            _onDiceRolled?.Invoke(_rollResult));
    }
    
    
}
