using System;
using System.Collections;
using System.Collections.Generic;
using Cards.EnemyCards;
using UnityEngine;
using TMPro;

public class BattleScreenUI : MonoBehaviour
{
    public GameObject BattleScreenCanvas;

    private void Start()
    {
        GameManager.Instance.OnFightStarted += EnableBattleScreen;
        FightSystem.EndEnemyFight += DisableBattleScreen;
        GameManager.Instance.TurnStarted += TurnStartedData ;
    }

    private void TurnStartedData (GameManager.TurnStatedData playerState)
    {
        
    }

    private void DisableBattleScreen (bool end, Player player, EnemyCard enemyCard)
    {
        BattleScreenCanvas.SetActive(false);
    }


    private void EnableBattleScreen (Player player, EnemyCard enemyCard)
    {
        BattleScreenCanvas.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnFightStarted -= EnableBattleScreen;
        FightSystem.EndEnemyFight -= DisableBattleScreen;
    }

}
