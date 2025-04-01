using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class FightSystem : MonoBehaviour
{
    [SerializeField] private DiceRoll diceRoll;
    private int playerValue;
    private int enemyValue;
    public static event Action endFight;
    public static event Action<Player> fightStarted;
    private Enemy _enemy;
    private Player _player;
    private Player _currentPlayer;
    
    public void OnEnable()
    {
      
       DiceRoll.OnPlayerRolled += OnPlayerRolled;
       DiceRoll.OnEnemyRolled += OnEnemyRolled;
       GameManager.Instance.TurnStarted += InstanceOnTurnStarted;
       GameManager.Instance.OnFightStarted += StartFight;
    }

    private void InstanceOnTurnStarted(GameManager.TurnStatedData obj)
    {
        _currentPlayer = obj.Player;
    }


    private void OnPlayerRolled(int e,Player player)
    {
        if(_currentPlayer != player) return;
        if(_currentPlayer.PlayerObject.GetComponent<PlayerController>().playerState != PlayerState.Fighting) return;
         playerValue = e;
         diceRoll.EnemyDiceRoll(_enemy);
    }

    private void OnEnemyRolled(int e, Enemy enemy)
    {
        enemyValue = e;
        EndFight();
    }
 

    private void StartFight(Player player, Enemy enemy)
    {
        _player = player;
        _enemy = enemy;
        if(_currentPlayer != _player) return;
        diceRoll.RequestDiceRoll(player);
    }

    private void EndFight()
    {
        endFight?.Invoke();
        if(_currentPlayer != _player) return;
        Debug.Log($"playerRolled {playerValue}, enemyRolled {enemyValue}");
         
        Debug.Log($"endFight {endFight} for {_currentPlayer}, {_player}");
        if (playerValue > enemyValue)
        {
            _player.PlayerObject.GetComponent<PlayerMovement>().MovePlayer(1, _player);
            GameManager.Instance.NextTurn();
        }
        else
        {
            GameManager.Instance.NextTurn();
        }
    }

 
}
