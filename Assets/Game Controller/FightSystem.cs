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
   
    public static event Action<FightResult> EndEnemyFight;
    public static event Action<Player> fightStarted;
    private Player _player;
    private Player _currentPlayer;

    public int _playerAttackValue { get; private set; }
    public int _enemyAttackValue { get; private set; }
    
    public void OnEnable()
    {
       GameManager.Instance.OnFightStarted += StartFight;
    }
    
    private void StartFight()
    {        
        if (Player.CurrentPlayer.currentEnemyCard is BossCard)
        {
            PlayerBossAttack(Player.CurrentPlayer);
        }
        else
        {
            PlayerAttack(Player.CurrentPlayer);
        }
    }

    private void PlayerAttack(Player player)
    {
        GameManager.Instance.diceRoll.RequestDiceRoll(false, i =>
        {
            _playerAttackValue = player.Controller.Attack(i);
            EnemyAttack(player.currentEnemyCard);
            DebugConsole.Log($"{Player.CurrentPlayer.Name} rolled = {_playerAttackValue}");
        });
        
    }

    private void PlayerBossAttack(Player player)
    {
        GameManager.Instance.diceRoll.RequestDiceRoll(false, i =>
        {
            DebugConsole.Log($"{player.Name} rolled = {i}");
            GameManager.Instance.diceRoll.RequestDiceRoll(false, j =>
            {
                DebugConsole.Log($"{player.Name} rolled = {j}");
                _playerAttackValue = i + j;
                DebugConsole.Log($"{player.Name} total value = {_playerAttackValue}");
                EnemyAttack(player.currentEnemyCard);
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
        Debug.Log($"endFight {EndEnemyFight} for {Player.CurrentPlayer.Name}, {Player.CurrentPlayer.Name}");
        
        FightResult fightResult;
        if (_playerAttackValue > _enemyAttackValue)
        {
            fightResult = FightResult.Win;
        }
        else if (_playerAttackValue == _enemyAttackValue)
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
        EndEnemyFight?.Invoke(fightState);        
    }
    
    
 
}
