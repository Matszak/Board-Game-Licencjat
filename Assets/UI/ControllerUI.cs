using System;
using System.Collections;
using System.Collections.Generic;
using Cards;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class ControllerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentTurnText;
    
    [SerializeField] private TextMeshProUGUI currentPlayerText;
    
    [SerializeField] private GameObject nextTurnButton;
    [SerializeField] private GameObject playerCards;

    public List<GameObject> _cards;
    private Player _player;

 

    private void OnEnable()
    {
        GameManager.Instance.TurnStarted += OnUIUpdated;
        GameManager.Instance.OnTurnEnded += EndTurn;
        
        nextTurnButton.SetActive(false);
    }

    private void OnUIUpdated(GameManager.TurnStatedData obj)
    {
        if (_player == obj.Player) return; 
        
        currentTurnText.text = $"Turn: {obj.Turn}";
        currentPlayerText.text = obj.Player.Name;
        _player = obj.Player;
        LoadCards(obj.Player);
    }
 
    void OnDestroy()
    {
        GameManager.Instance.TurnStarted -= OnUIUpdated;
    }

    void EndTurn(Player player)
    {
        if (player != _player) return;
        nextTurnButton.SetActive(true);
    }

    public void OnButtonClicked()
    {
        GameManager.Instance.NextTurn();
        nextTurnButton.SetActive(false);
    }

    private void LoadCards(Player player)
    {
        foreach (var card in _cards)
        {
            Destroy(card);
        }
        _cards.Clear();

        if(player.playerCards.Count == 0) return;
        
        for (int i = 0; i < player.playerCards.Count; i++)
        {
            _cards.Add(Instantiate(player.playerCards[i].cardPrefab,
                new Vector3(playerCards.transform.position.x + i * 450,
                    playerCards.transform.position.y,
                    playerCards.transform.position.z)
                , Quaternion.identity, playerCards.transform));
             _cards[i].GameObject().name = player.playerCards[i].nameText;
             if (_cards[i].TryGetComponent(out UICardUsage cardUsage))
             {
                 cardUsage.SetCard(player.playerCards[i]);
             }
        }
      
    }
}
