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
    public GameObject playerImage;

    public TextMeshProUGUI playerWinText;
    public TextMeshProUGUI playerLoseText;
    public TextMeshProUGUI playerDrawText;
    public RectTransform buttonRectTransform;
    private void Start()
    {
        GameManager.Instance.OnFightStarted += EnableBattleScreen;
        FightSystem.EndEnemyFight += DisableBattleScreen;
    }
    private void EnableBattleScreen (Player player, EnemyCard enemyCard)
    {
        BattleScreenCanvas.gameObject.SetActive(true);
        enemyImage.GetComponent<RawImage>().texture = enemyCard.cardImage.texture;
        playerWinText.text = enemyCard.enemyDefeatedBehaviour.winText;
        playerDrawText.text = enemyCard.enemyDrawBehaviour.drawText;
        playerLoseText.text = enemyCard.enemyWinBehaviour.loseText;
        buttonRectTransform.anchoredPosition = new Vector2(-1122, 327);
        playerImage.GetComponent<RawImage>().texture = player.PlayerObject.GetComponent<Image>().sprite.texture;
    }
    private void DisableBattleScreen (FightSystem.FightResult fightResult, Player player, EnemyCard enemyCard)
    {
        BattleScreenCanvas.SetActive(false);
        buttonRectTransform.anchoredPosition = new Vector2(-10, 20);

    }



    private void OnDisable()
    {
        GameManager.Instance.OnFightStarted -= EnableBattleScreen;
        FightSystem.EndEnemyFight -= DisableBattleScreen;
    }

}
