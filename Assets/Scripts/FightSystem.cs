using System;
using System.Collections;
using System.Collections.Generic;
using Cards.EnemyCards;
using UnityEngine;

public class FightSystem : MonoBehaviour
{
 
    public static event Action<bool, Player> endFight;
    public static event Action<Player> fightStarted;
    [SerializeField] EnemyCard _enemy;
    private Player _player;
    private Player _currentPlayer;

    private int _playerAttackValue;
    private int _enemyAttackValue;
    
    public void OnEnable()
    {
       GameManager.Instance.TurnStarted += InstanceOnTurnStarted;
       GameManager.Instance.OnFightStarted += StartFight;
       
    }

    private void InstanceOnTurnStarted(GameManager.TurnStatedData obj)
    {
        _currentPlayer = obj.Player;
    }
    
    private void StartFight(Player player, EnemyCard enemy)
    {
        if(_currentPlayer != player ) return;
        _enemy = enemy;
        PlayerAttack(player);
    }

    private void PlayerAttack(Player player)
    {
        GameManager.Instance.diceRoll.RequestDiceRoll(false, i =>
        {
            _playerAttackValue = player.PlayerObject.GetComponent<PlayerController>().Attack(i);
            Debug.Log(_playerAttackValue);
            EnemyAttack(_enemy);
        });

    }

    private void EnemyAttack(EnemyCard enemy)
    {
        
         enemy.enemyBehaviour.EnemyAttack(attackValue =>
        {
            _enemyAttackValue = attackValue;
            Debug.Log(attackValue);
            EndFight();
        });
    }

    private void EndFight()
    {
    
        Debug.Log($"playerRolled {_playerAttackValue}, enemyRolled {_enemyAttackValue}");
        Debug.Log($"endFight {endFight} for {_currentPlayer}, {_player}");
        
        if (_playerAttackValue > _enemyAttackValue)
        {
            var playerMovement = _currentPlayer.PlayerObject.GetComponent<PlayerMovement>();
            playerMovement.MovePlayer(_playerAttackValue - _enemyAttackValue, _currentPlayer);
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
        endFight?.Invoke(win,_currentPlayer);
        
    }
 
}
