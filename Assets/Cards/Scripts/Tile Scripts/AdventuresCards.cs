using System;
using System.Collections;
using System.Collections.Generic;
using Cards;
using Cards.EnemyCards;
using CardsAndTilesScripts.adventureTiles;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AdventuresCards : MonoBehaviour
{
    [SerializeField] private ControllerUI controllerUI;
    
    [SerializeField] private GameObject cardsUI;
    
    private Card card;
    
    [SerializeField] private Image cardImage;
    //[SerializeField] private TextMeshProUGUI cardText;
    //[SerializeField] private TextMeshProUGUI descriptionText;
    
    [SerializeField] private Card[] cards;
    [SerializeField] private Card[] bonusCards;
    [SerializeField] private Card[] disadvantageCards; 
    public Card[] magicCards;
    [SerializeField] private EnemyCard[] enemiesCards;
 
    private Card _selectedCard;
    
    private void OnEnable()
    {
        GameManager.Instance.OnCardTriggered += TriggerCard;
    }

    private void Awake()
    {
        cardsUI.SetActive(false);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnCardTriggered -= TriggerCard;
    }

    private void TriggerCard(Player player, AdventureTile adventureTile)
    {         
         switch (adventureTile)
         {
             case BonusTile:
                 _selectedCard = bonusCards[Random.Range(0, bonusCards.Length)];
                 break;
             case DisadvantageTile:
                 _selectedCard = disadvantageCards[Random.Range(0, disadvantageCards.Length)];
                 break;
             case PickUpTile pickUpTile:
                 if (pickUpTile.pickUpCard == null)
                 {
                    GameManager.Instance.TurnEnded();
                 }
                 _selectedCard = pickUpTile.pickUpCard;
                 break;
             case BattleTile battleTile:
                 _selectedCard = battleTile.enemyCard;
                 break;
             case RandomEnemyTile randomEnemyTile:
                 _selectedCard = enemiesCards[Random.Range(0, enemiesCards.Length)];
                 break;
             case MagicTile magicTile:
                 _selectedCard = magicCards[Random.Range(0, magicCards.Length)];
                 break;
             default:
                 _selectedCard = cards[Random.Range(0, cards.Length)];
                 break;
         }

         if (_selectedCard == null)
         {
             Player.CurrentPlayer.Controller.playerState = PlayerState.None;
             GameManager.Instance.TurnEnded();
             return;
         }

         if (_selectedCard is not EnemyCard)
         {
            DebugConsole.Log($"{player.Name} picked up {_selectedCard.name}");  
         }
         cardImage.sprite = _selectedCard.cardImage;
         //cardText.text = _selectedCard.nameText;
         //descriptionText.text = _selectedCard.descriptionText;
        
         cardsUI.SetActive(true);
        }

    public void OnButtonClick()
    {
        
         if (_selectedCard is PickUpCard)
         {
             if (_selectedCard == null)
             {
                 EndTurn();
                 Player.CurrentPlayer.Controller.playerState = PlayerState.CardPickedUp;
             }
             Player.CurrentPlayer.playerCards.Add(_selectedCard);
             cardsUI.SetActive(false);
             controllerUI.UpdateUI();   
             Player.CurrentPlayer.Controller.playerState = PlayerState.CardPickedUp;
             EndTurn();
         }
         else if (_selectedCard is EnemyCard card)
         {
             Player.CurrentPlayer.currentEnemyCard = card;
             cardsUI.SetActive(false);
             card.TriggerCard(Player.CurrentPlayer);
         }
         else
         {
             _selectedCard.OnCardCompleted += EndTurn;
             Player.CurrentPlayer.Controller.playerState = PlayerState.CardPickedUp;
            _selectedCard.TriggerCard(Player.CurrentPlayer);
            cardsUI.SetActive(false);
         }
    }

    private void EndTurn()
    {
        GameManager.Instance.TurnEnded();
        _selectedCard.OnCardCompleted -= EndTurn;
    }
}
