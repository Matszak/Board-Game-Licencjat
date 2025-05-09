using System;
using System.Collections;
using System.Collections.Generic;
using Cards.EnemyCards;
using UnityEngine;

public class FightSystem : MonoBehaviour
{
    
 
    public static event Action<bool, Player, EnemyCard> EndEnemyFight;
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
        DebugConsole.LogCentered($"= {_currentPlayer.Name} is attacked =");
    }

    private void PlayerAttack(Player player)
    {
        GameManager.Instance.diceRoll.RequestDiceRoll(false, i =>
        {
            _playerAttackValue = player.PlayerObject.GetComponent<PlayerController>().Attack(i);
            EnemyAttack(_enemy);
            DebugConsole.Log($"{_currentPlayer.Name} rolled = {_playerAttackValue}");
        });
        
    }

    private void EnemyAttack(EnemyCard enemy)
    {
        
         enemy.enemyAttackAttackBehaviour.EnemyAttack(attackValue =>
        { 
            _enemyAttackValue = attackValue;
            DebugConsole.Log($"{enemy.name} rolled = {_enemyAttackValue}");
            EndFight();
        });
        
    }

    private void EndFight()
    {
        Debug.Log($"playerRolled {_playerAttackValue}, enemyRolled {_enemyAttackValue}");
        Debug.Log($"endFight {EndEnemyFight} for {_currentPlayer}, {_player}");
        bool endFightState = _playerAttackValue > _enemyAttackValue;
        
        StartCoroutine(DelayedEndFight(0.5f, endFightState));
        
        if (_playerAttackValue > _enemyAttackValue)
        {
            DebugConsole.Log($"{_currentPlayer.Name} Wins");
            {
                DebugConsole.Log($"{_currentPlayer.Name} is moving {(_playerAttackValue - _enemyAttackValue)}");
            }
        }
        else if (_enemyAttackValue > _playerAttackValue)
        {
            DebugConsole.Log($"{_currentPlayer.Name} Loses");
        }
        else if (_playerAttackValue == _enemyAttackValue)
        {
            DebugConsole.Log("Draw");
        }
    }

    private IEnumerator DelayedEndFight(float delay, bool win)
    {
        yield return new WaitForSeconds(delay);
        EndEnemyFight?.Invoke(win,_currentPlayer, _enemy);
        
    }
 
}
