    using System;
using System.Collections;
using System.Collections.Generic;
using Cards;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using TMPro;

public class ControllerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentTurnText;
    [SerializeField] private TextMeshProUGUI nextTurnText;
    [SerializeField] private TextMeshProUGUI currentPlayerText;
    
    [SerializeField] private GameObject nextTurnButton;
    [SerializeField] private GameObject playerCards;

    public List<GameObject> _cards;
    private Player _player;

 

    private void OnEnable()
    {
        GameManager.Instance.TurnStarted += UpdateUI;
        GameManager.Instance.OnTurnEnded += EndTurn;
        GameManager.Instance.OnWinGame += WinnerUi;
        nextTurnButton.SetActive(false);
    }

    private void OnPickupTile(Card obj)
    {
        throw new NotImplementedException();
    }

    private void WinnerUi(Player obj)
    {
        Debug.Log($"winner: {obj.Name}");
        currentTurnText.text = $"Winner: {obj}";
        
    }
 
    public void UpdateUI(GameManager.TurnStatedData obj)
    {
        if (_player == obj.Player) return; 
        
        currentTurnText.text = $"Turn: {obj.Turn}";
        currentPlayerText.text = obj.Player.Name;
        _player = obj.Player;
        LoadCards(obj.Player);
    }
 
    void OnDestroy()
    {
        GameManager.Instance.TurnStarted -= UpdateUI;
    }

    public void EndTurn(Player player)
    {
        if (player != _player) return;
        LoadCards(_player);
        var state = player.PlayerObject.GetComponent<PlayerController>().playerState;
        switch (state)
        {
            case PlayerState.FightStarted:
                nextTurnButton.SetActive(false);
                break;
            case PlayerState.FightLose:
                nextTurnButton.SetActive(true); 
                break;
            case PlayerState.FightWin:
                 
                break;
            case PlayerState.CardPickedUp:
                nextTurnButton.SetActive(true);
                break;
            case PlayerState.None:
                nextTurnButton.SetActive(true);
                break;
        }
       
      
    }

    public void OnButtonClicked()
    {
        
        DebugConsole.LogCentered("== Turn Ended ==");
        GameManager.Instance.NextTurn(false);
        nextTurnButton.SetActive(false);
    }

    public void NextTurnButtonNameChange(string message)
    { 
        nextTurnText.text = message;
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
                 cardUsage.SetCard(player.playerCards[i], player);
                 
             }
        }
      
    }
}
