using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AdventuresCards : MonoBehaviour
{
    [SerializeField] private GameObject cardsUI;
    
    [SerializeField] private Image cardImage;
    [SerializeField] private TextMeshProUGUI cardText;
    
    [SerializeField] private Card[] cards;
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

    private void TriggerCard(Player player)
    {
        // check if current player triggered this card
         if (player != _player) return;
         if(!checkForCard) return;
         // get random card from cards list
         _selectedCard = cards[Random.Range(0, cards.Length)];
         
         // assing stuff from card to ui, name of card and image 
         cardImage.sprite = _selectedCard.CardImage;
         cardText.text = _selectedCard.NameText;
         
         // when everything set show UI
         cardsUI.SetActive(true);
    }

    public void OnButtonClick()
    {
         // trigger card now, and send which player triggered it.
         _selectedCard.TriggerCard(_player);
        checkForCard = false;
         _selectedCard.OnCardCompleted += EndTurn;
        cardsUI.SetActive(false);
   
    }

    private void EndTurn(Player obj)
    {
        GameManager.Instance.TurnEnded(obj);
        _selectedCard.OnCardCompleted -= EndTurn;
    }
}
