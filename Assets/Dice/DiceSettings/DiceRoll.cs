using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DiceRoll : MonoBehaviour
{   
    [SerializeField] private float animationDelay = 0.5f;
    [SerializeField] private  GameObject  uiButtonPrefab;
 
    public int rollResult;
    private Player _player;
    private Enemy _enemy;
    
    public static event Action<int, Player> OnPlayerRolled;
    public static event Action<int, Enemy> OnEnemyRolled;
    
     private DiceRollAnimation _diceRollAnimation;
 
    public void Awake()
    {
      _diceRollAnimation = GetComponent<DiceRollAnimation>();
      _player = null;
    }
 
    public void RequestDiceRoll(Player player)
    {
        rollResult = 2;
        _player = player;
        uiButtonPrefab.SetActive(true);
    }

    public void EnemyDiceRoll(Enemy enemy)
    {
        _enemy = enemy;
        rollResult = 0;
        Dice dice = new Dice(6);
        RollDices(dice,1);
    }
    
    public void OnButtonClick()
    {
        Dice dice = new Dice(2);
        RollDices(dice, 1) ;
    }

    private void RollDices(Dice typeOfDice,int numberOfDices)
    { 
        for (int i = 0; i < numberOfDices; i++)
        {
           rollResult += typeOfDice.RollDice();
        }
        // only works with 6 sides dice right now.
        if (_player != null)
        {
            _diceRollAnimation.PlayAnimation(rollResult,_player);
            StartCoroutine(WaitForAnimationToFinish(rollResult, _player));
            _player = null;
        }
        if (_enemy != null)
        {
            _diceRollAnimation.PlayAnimation(rollResult, _enemy);
            StartCoroutine(WaitForAnimationToFinish(rollResult, _enemy));
            _enemy = null;
        }   
        uiButtonPrefab.SetActive(false);
    }

    private IEnumerator WaitForAnimationToFinish(int diceRollResult, Player player)
    {
        float animationDuration = _diceRollAnimation.GetAnimationDuration(diceRollResult);
        yield return new WaitForSeconds(animationDuration + animationDelay);
        OnPlayerRolled?.Invoke(diceRollResult, player);
    }
    private IEnumerator WaitForAnimationToFinish(int diceRollResult,Enemy enemy)
    {
        float animationDuration = _diceRollAnimation.GetAnimationDuration(diceRollResult);
        yield return new WaitForSeconds(animationDuration + animationDelay);
        OnEnemyRolled?.Invoke(diceRollResult, enemy);
    }
   
}
