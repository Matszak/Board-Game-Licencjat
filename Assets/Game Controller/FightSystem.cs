using System;
using System.Collections;
using System.Collections.Generic;
using Cards.EnemyCards;
using UnityEngine;

public class FightSystem : MonoBehaviour
{
    public GameObject BattleScreenCanvas;
    public enum FightResult
    {
        Win,
        Lose,
        Draw
    }
   
    public static event Action<FightResult, Player, EnemyCard> EndEnemyFight;
    public static event Action<Player> fightStarted;
    [SerializeField] EnemyCard _enemy;
    private Player _player;
    private Player _currentPlayer;

    public int _playerAttackValue { get; private set; }
    public int _enemyAttackValue { get; private set; }
    
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
        
        if (enemy is BossCard)
        {
            PlayerBossAttack(player);
        }
        else
        {
            PlayerAttack(player);
        }
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

    private void PlayerBossAttack(Player player)
    {
        GameManager.Instance.diceRoll.RequestDiceRoll(false, i =>
        {
            DebugConsole.Log($"{_currentPlayer.Name} rolled = {i}");
            GameManager.Instance.diceRoll.RequestDiceRoll(false, j =>
            {
                DebugConsole.Log($"{_currentPlayer.Name} rolled = {j}");
                _playerAttackValue = player.PlayerObject.GetComponent<PlayerController>().Attack(i + j);
                DebugConsole.Log($"{_currentPlayer.Name} total value = {_playerAttackValue}");
                EnemyAttack(_enemy);
            });
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
        
        FightResult fightResult;
        if (_playerAttackValue > _enemyAttackValue)
        {
            fightResult = FightResult.Win;
        }
        else if (_playerAttackValue == _playerAttackValue)
        {
            fightResult = FightResult.Draw;
        }
        else
        {
            fightResult = FightResult.Lose;
        }
        StartCoroutine(DelayedEndFight(0.5f, fightResult));
        
 
        
    }

    private IEnumerator DelayedEndFight(float delay, FightResult fightState)
    {
        yield return new WaitForSeconds(delay);
        EndEnemyFight?.Invoke(fightState, _currentPlayer, _enemy);
        
    }
    
    
 
}
