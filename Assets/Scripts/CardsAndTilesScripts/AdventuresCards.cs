using System;
using System.Collections;
using System.Collections.Generic;
using Cards;
using CardsAndTilesScripts.adventureTiles;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AdventuresCards : MonoBehaviour
{
    [SerializeField] private GameObject cardsUI;
    
    [SerializeField] private Image cardImage;
    [SerializeField] private TextMeshProUGUI cardText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    
    [SerializeField] private Card[] cards;
    [SerializeField] private Card[] bonusCards;
    [SerializeField] private Card[] disadvantageCards;
    private Player _player;
   
    private Card _selectedCard;
    private bool checkForCard = false;
    
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
    }

    private void OnDisable()
    {
        GameManager.Instance.OnCardTriggered -= TriggerCard;
    }

    private void TriggerCard(Player player, AdventureTile adventureTile)
    {
   
        // check if current player triggered this card
         if (player != _player) return;
         if(!checkForCard) return;
   
         switch (adventureTile)
         {
             case BonusTile:
                 _selectedCard = bonusCards[Random.Range(0, bonusCards.Length)];
                 break;
             case DisadvantageTile:
                 _selectedCard = disadvantageCards[Random.Range(0, disadvantageCards.Length)];
                 break;
             case PickUpTile pickUpTile:
                 _selectedCard = pickUpTile.pickUpCard;
                 break;
             default:
                 _selectedCard = cards[Random.Range(0, cards.Length)];
                 break;
         }
         
         // assing stuff from card to ui, name of card and image 
         cardImage.sprite = _selectedCard.CardImage;
         cardText.text = _selectedCard.NameText;
         descriptionText.text = _selectedCard.DescriptionText;
         
         // when everything set show UI
         cardsUI.SetActive(true);
        }

    public void OnButtonClick()
    {
 
         if (_selectedCard is PickUpCard)
         {
             _player.playerCards.Add(_selectedCard);
             checkForCard = false;
             cardsUI.SetActive(false);
             EndTurn(_player);
         }
         else
         {
            _selectedCard.TriggerCard(_player);
             checkForCard = false;
             _selectedCard.OnCardCompleted += EndTurn;
            cardsUI.SetActive(false);
         }
   
    }

    private void EndTurn(Player obj)
    {
        GameManager.Instance.TurnEnded(obj);
        _selectedCard.OnCardCompleted -= EndTurn;
    }
}
