using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CardsOnStart : MonoBehaviour
{
    
    public GameObject cardButton_01;
    public GameObject cardButton_02;
    public GameObject cardButton_03;
    
    public AdventuresCards adventuresCards;
    
    public PlayerController playerController;
    
    private Player _player;
    
    Card[] cards = new Card[3];
    
    public static Action<Card, Player> OnStartCardSelect;

    private void Awake()
    {
        GameManager.Instance.TurnStarted += InstanceOnTurnStarted;
    }

    private void Start()
    {
        
        
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i] = adventuresCards.magicCards[Random.Range(0, adventuresCards.magicCards.Length)];
        }

        cardButton_01.GetComponent<Image>().sprite = cards[0].cardImage;
        cardButton_02.GetComponent<Image>().sprite = cards[1].cardImage;
        cardButton_03.GetComponent<Image>().sprite = cards[2].cardImage;
    }

    private void InstanceOnTurnStarted(GameManager.TurnStatedData obj)
    {
        _player = obj.Player;
        if (GameManager.Instance.currentTurn != 0) return;
        gameObject.SetActive(true);
        
    }

    public void StartCardSelect_00()
    {
        OnStartCardSelect?.Invoke(cards[0], _player);
        gameObject.SetActive(false);
    }

    public void StartCardSelect_01()
    {
        OnStartCardSelect?.Invoke(cards[1], _player);
        gameObject.SetActive(false);

    }

    public void StartCardSelect_02()
    {
        OnStartCardSelect?.Invoke(cards[2], _player);
        gameObject.SetActive(false);

    }
    
    
    
    
}
