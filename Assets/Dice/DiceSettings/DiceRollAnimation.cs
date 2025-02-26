using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiceRollAnimation : MonoBehaviour
{
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
 
 
    
    void Start()
    {
        DiceRoll.DiceRolled += PlayAnimation;
 
    }

    private void PlayAnimation(int diceRollResult, Player player)
    {
  
         animator.SetTrigger(ResultsDiceAnimation[diceRollResult - 1]);
    
    }

 
}
