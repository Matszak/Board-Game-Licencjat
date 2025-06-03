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
    private Player _player;
 
    private Card _selectedCard;
    private bool checkForCard = false;
    
    private GameManager.TurnStatedData turnStartedData;
    
    private void OnEnable()
    {
        GameManager.Instance.TurnStarted += OnTurnStarted;
        GameManager.Instance.OnCardTriggered += TriggerCard;
        GameManager.Instance.OnTurnEnded += StopChecking;
    }

    private void StopChecking(Player obj)
    {
        checkForCard = false;
    }

    private void Awake()
    {
        cardsUI.SetActive(false);
    }
    private void OnTurnStarted(GameManager.TurnStatedData obj)
    {
        // assign current player
        checkForCard = true;
        _player = obj.Player;
        turnStartedData = obj;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnCardTriggered -= TriggerCard;
    }

    private void TriggerCard(Player player, AdventureTile adventureTile)
    {
         if (player != _player) return;
         
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
                    GameManager.Instance.TurnEnded(_player);
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
             _player.PlayerObject.GetComponent<PlayerController>().playerState = PlayerState.None;
             GameManager.Instance.TurnEnded(_player);
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
                 EndTurn(_player);
                 _player.PlayerObject.GetComponent<PlayerController>().playerState = PlayerState.CardPickedUp;
             }
             _player.playerCards.Add(_selectedCard);
             checkForCard = false;  
             cardsUI.SetActive(false);
             controllerUI.UpdateUI(turnStartedData);   
             _player.PlayerObject.GetComponent<PlayerController>().playerState = PlayerState.CardPickedUp;
             EndTurn(_player);
         }
         else if (_selectedCard is EnemyCard card)
         {
             _player.currentEnemyCard = card;
             checkForCard = false;
             cardsUI.SetActive(false);
             card.TriggerCard(_player);
         }
         else
         {
             checkForCard = false;
             _selectedCard.OnCardCompleted += EndTurn;
             _player.PlayerObject.GetComponent<PlayerController>().playerState = PlayerState.CardPickedUp;
            _selectedCard.TriggerCard(_player);
            cardsUI.SetActive(false);
         }
    }

    private void EndTurn(Player obj)
    {
        GameManager.Instance.TurnEnded(obj);
        _selectedCard.OnCardCompleted -= EndTurn;
    }
}
