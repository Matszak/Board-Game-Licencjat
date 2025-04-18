using System;
using System.Collections;
using System.Collections.Generic;
using Cards.EnemyCards;
using UnityEngine;

public class FightSystem : MonoBehaviour
{
    private int playerValue;
    private int enemyValue;
    public static event Action<bool, Player> endFight;
    public static event Action<Player> fightStarted;
    private EnemyCard _enemy;
    private Player _player;
    private Player _currentPlayer;
    
    public void OnEnable()
    {
      
       DiceRoll.OnDiceRolled += OnPlayerRolled;
       GameManager.Instance.TurnStarted += InstanceOnTurnStarted;
       GameManager.Instance.OnFightStarted += StartFight;
       GameManager.Instance.OnEnemyAttacksEnded += EndFight;
    }

    private void InstanceOnTurnStarted(GameManager.TurnStatedData obj)
    {
        _currentPlayer = obj.Player;
    }


    private void OnPlayerRolled(int e)
    {
        if(_currentPlayer != GameManager.Instance.currentPlayerObj) return;
        if(_currentPlayer.PlayerObject.GetComponent<PlayerController>().playerState != PlayerState.Fighting) return;
         playerValue = e;
         _enemy.enemyBehaviour.EnemyAttack();
         
    }

  

    private void StartFight(Player player, EnemyCard enemy)
    {
        _player = player;
        _enemy = enemy;
        if(_currentPlayer != _player) return;
        GameManager.Instance.diceRoll.RequestDiceRoll(false); 
    }

    private void EndFight(Player player, EnemyCard enemyCard)
    {
         if(_currentPlayer != player || enemyCard != _enemy) return;
        if(_currentPlayer != _player) return;
        Debug.Log($"playerRolled {playerValue}, enemyRolled {enemyValue}");
        Debug.Log($"endFight {endFight} for {_currentPlayer}, {_player}");
        
        if (playerValue > enemyValue)
        {
            var playerMovement = _player.PlayerObject.GetComponent<PlayerMovement>();
            playerMovement.MovePlayer(playerValue - enemyValue, _player);
            playerMovement.OnEndMovePlayerMove += OnOnEndMovePlayerMove;

            void OnOnEndMovePlayerMove(Player obj)
            {
                StartCoroutine(DelayedEndFight(0.5f,true));
            }
        }
        else
        {
            StartCoroutine(DelayedEndFight(0.5f,false));
        }
        
    }

    private IEnumerator DelayedEndFight(float delay, bool win)
    {
        yield return new WaitForSeconds(delay);
        endFight?.Invoke(win,_player);
        
    }
 
}
