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
    [SerializeField] private float animationDelay = 0.5f;
    [FormerlySerializedAs("uiButtonPrefab")] [SerializeField] private  GameObject  RollDicesButton;
    public int rollResult;
    
    private Player _player;
    private EnemyCard _enemyCard;
    
    private DiceRollAnimation _diceRollAnimation;
    public static event Action<int> OnDiceRolled;

    public bool diceRolled;
    
    public void Awake()
    {
      _diceRollAnimation = GetComponent<DiceRollAnimation>();
    }
 
    public void RequestDiceRoll(bool isEnemy)
    {
        diceRolled = false;
        // no need for creating dices and dices number because there is always one 6d dice
        Dice dice = new Dice(6);
        if (!isEnemy)
        {
            rollResult = 0;
           RollDicesButton.SetActive(true);
           
        }
        else
        {
            RollDices(dice,1);
        }
    }
    
    public void OnButtonClick()
    {
        Dice dice = new Dice(6);
        RollDices(dice, 1) ;
    }

    private void RollDices(Dice typeOfDice,int numberOfDices)
    { 
        for (int i = 0; i < numberOfDices; i++)
        {
           rollResult += typeOfDice.RollDice();
        }
        RollDicesButton.SetActive(false);
    }

    //private IEnumerator WaitForAnimationToFinish(int diceRollResult)
    //{
    //    float animationDuration = _diceRollAnimation.GetAnimationDuration(diceRollResult);
    //    yield return new WaitForSeconds(animationDuration + animationDelay);
    //    OnDiceRolled?.Invoke(diceRollResult);
    //}
}
