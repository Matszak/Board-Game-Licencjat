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
    private Vector2 startPosition;

    public TextMeshProUGUI playerWinText;

    public RectTransform buttonRectTransform;
    private void Start()
    {
        GameManager.Instance.OnFightStarted += EnableBattleScreen;
        FightSystem.EndEnemyFight += DisableBattleScreen;
        startPosition = transform.position;
    }
    private void EnableBattleScreen (Player player, EnemyCard enemyCard)
    {
        BattleScreenCanvas.gameObject.SetActive(true);
        enemyImage.GetComponent<RawImage>().texture = enemyCard.cardImage.texture;
        playerWinText.text = enemyCard.enemyDefeatedBehaviour.winText;
        buttonRectTransform.anchoredPosition = new Vector2(-820, 230);
        playerImage.GetComponent<RawImage>().texture = player.PlayerObject.GetComponent<Image>().sprite.texture;
    }
    private void DisableBattleScreen (bool end, Player player, EnemyCard enemyCard)
    {
        BattleScreenCanvas.SetActive(false);
        buttonRectTransform.anchoredPosition = startPosition;

    }



    private void OnDisable()
    {
        GameManager.Instance.OnFightStarted -= EnableBattleScreen;
        FightSystem.EndEnemyFight -= DisableBattleScreen;
    }

}
