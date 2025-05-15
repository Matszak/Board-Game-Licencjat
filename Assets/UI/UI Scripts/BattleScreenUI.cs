using System;
using System.Collections;
using System.Collections.Generic;
using Cards.EnemyCards;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class BattleScreenUI : MonoBehaviour
{
    public GameObject BattleScreenCanvas;

    public GameObject playerCard;
    public GameObject enemyBattleScreen;
    public Button closeRollButton;
    public Button rollButton;

    

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
        closeRollButton.gameObject.SetActive(true);

    }


    private void EnableBattleScreen (Player player, EnemyCard enemyCard)
    {
        BattleScreenCanvas.gameObject.SetActive(true);
        enemyBattleScreen.GetComponent<RawImage>().texture = enemyCard.cardImage.texture;
        closeRollButton.gameObject.SetActive(false);

    }

    private void OnDisable()
    {
        GameManager.Instance.OnFightStarted -= EnableBattleScreen;
        FightSystem.EndEnemyFight -= DisableBattleScreen;
    }

}
