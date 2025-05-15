using System;
using System.Collections;
using System.Collections.Generic;
using Cards.EnemyCards;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BattleScreenUI : MonoBehaviour
{
    public GameObject BattleScreenCanvas;
    public GameObject enemyImage;
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
        enemyImage.GetComponent<RawImage>().texture = enemyCard.cardImage.texture;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnFightStarted -= EnableBattleScreen;
        FightSystem.EndEnemyFight -= DisableBattleScreen;
    }

}
