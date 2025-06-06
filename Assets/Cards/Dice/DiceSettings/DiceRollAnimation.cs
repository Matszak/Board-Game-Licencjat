using System;
using System.Collections;
using System.Collections.Generic;
using Cards.EnemyCards;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiceRollAnimation : MonoBehaviour
{
     public GameObject diceRollIcon;
     private Action _onDiceAnimationEnded;

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
 
    public void PlayAnimation(int diceRollResult, Action callback)
    {
        diceRollIcon.SetActive(true);
        animator.SetTrigger(ResultsDiceAnimation[diceRollResult - 1]);
        StartCoroutine(WaitForAnimationToFinish(diceRollResult, callback));
    }

    public void PlayAnimation(int diceRollResult)
    {
        diceRollIcon.SetActive(true);
        animator.SetTrigger(ResultsDiceAnimation[diceRollResult - 1]);
        StartCoroutine(WaitForAnimationToFinish(diceRollResult));
    }

    private IEnumerator WaitForAnimationToFinish(int diceRollResult, Action callback)
    { 
        _onDiceAnimationEnded = callback;
        float animationDuration = GetAnimationDuration(diceRollResult);
        yield return new WaitForSeconds(animationDuration + 3f);
        DisableAnimation();
        callback?.Invoke();
    }
    private IEnumerator WaitForAnimationToFinish(int diceRollResult)
    {
        float animationDuration = GetAnimationDuration(diceRollResult);
        yield return new WaitForSeconds(animationDuration + 3f);
        DisableAnimation();
    }

    private void DisableAnimation()
    {
        diceRollIcon.SetActive(false);
    }

    public float GetAnimationDuration(int diceRollResult)
    {
        // Assuming that all animations have the same length (time)
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.length;
    }
}
