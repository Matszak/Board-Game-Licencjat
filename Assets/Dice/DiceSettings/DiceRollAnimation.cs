using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiceRollAnimation : MonoBehaviour
{
     public GameObject diceRollIcon;
     private Player _player;

     public void OnEnable()
     {
         GameManager.Instance.TurnStarted += InstanceOnTurnStarted;
     }

     private void InstanceOnTurnStarted(GameManager.TurnStatedData obj)
     {
         _player = obj.Player;
     }

     public void Start()
     {
         diceRollIcon.SetActive(false);
     }
    private static readonly int[] ResultsDiceAnimation = new[]
    {
        Animator.StringToHash("LandOn1"),
        Animator.StringToHash("LandOn2"),
        Animator.StringToHash("LandOn3"),
        Animator.StringToHash("LandOn4"),
        Animator.StringToHash("LandOn5"),
        Animator.StringToHash("LandOn6"),
    };
 
     [SerializeField] private Animator animator;
 
    public void PlayAnimation(int diceRollResult, Player player)
    {
        if(_player != player) return;
        _player.PlayerObject.GetComponent<PlayerMovement>().OnEndMovePlayerMove += DisableAnimation;
        diceRollIcon.SetActive(true);
        animator.SetTrigger(ResultsDiceAnimation[diceRollResult - 1]);
        
    }
    
    public void PlayAnimation(int diceRollResult, Enemy enemy)
    {
        FightSystem.endFight += DisableAnimationFight;
        diceRollIcon.SetActive(true);
        animator.SetTrigger(ResultsDiceAnimation[diceRollResult - 1]);
        
    }

    private void DisableAnimationFight(bool win, Player player)
    {
        diceRollIcon.SetActive(false);
    }


    private void DisableAnimation(Player obj)
    {
        diceRollIcon.SetActive(false);
    }

    public float GetAnimationDuration(int diceRollResult)
    {
        // Assuming that all animations have the same length (time), but you can get it from the Animator for each result
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.length;
    }

    private void OnDestroy()
    {
        if (_player == null) return;
        _player.PlayerObject.GetComponent<PlayerMovement>().OnEndMovePlayerMove -= DisableAnimation;
    }

    private void OnDisable()
    {
        if (_player == null) return;
        _player.PlayerObject.GetComponent<PlayerMovement>().OnEndMovePlayerMove -= DisableAnimation;
    }   
   

}
