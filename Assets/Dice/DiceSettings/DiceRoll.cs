using System;
using System.Collections;
using System.Collections.Generic;
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
     
    public static event Action<int, Player> DiceRolled;
 
    
     private DiceRollAnimation _diceRollAnimation;
 
    public void Awake()
    {
      _diceRollAnimation = GetComponent<DiceRollAnimation>();
    }
 
    public void RequestDiceRoll(Player player)
    {
        rollResult = 0;
        _player = player;
        uiButtonPrefab.SetActive(true);
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
        // only works with 6 sides dice right now.
        _diceRollAnimation.PlayAnimation(rollResult,_player);
        StartCoroutine(WaitForAnimationToFinish(rollResult, _player));
        uiButtonPrefab.SetActive(false);
    } 
    
    private IEnumerator WaitForAnimationToFinish(int diceRollResult, Player player)
    {
        // Get the animation duration (assuming all animations have the same time duration)
        float animationDuration = _diceRollAnimation.GetAnimationDuration(diceRollResult);

        // Wait for the duration of the animation
        yield return new WaitForSeconds(animationDuration + animationDelay);

        // After the animation finishes, invoke the DiceRolled event and 
        // hide image that is showing rolling dice
        DiceRolled?.Invoke(diceRollResult, player);
    }
}
