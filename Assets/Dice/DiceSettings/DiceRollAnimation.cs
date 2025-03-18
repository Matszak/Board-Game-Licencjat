using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiceRollAnimation : MonoBehaviour
{
     public GameObject diceRollIcon;
     private Player _player;

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
        _player = player;
        _player.PlayerObject.GetComponent<PlayerMovement>().OnEndMovePlayerMove += DisableAnimation;
        diceRollIcon.SetActive(true);
        animator.SetTrigger(ResultsDiceAnimation[diceRollResult - 1]);
        
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
