    using System;
using System.Collections;
using System.Collections.Generic;
using Cards;
using Cards.EnemyCards;
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

    [SerializeField] private GameObject winScreen;
    
    public List<GameObject> _cards;
    private Player _player;
    
    

 

    private void OnEnable()
    {
        GameManager.Instance.TurnStarted += UpdateUI;
        GameManager.Instance.OnTurnEnded += EndTurn;
        GameManager.Instance.OnWinGame += WinnerUi;
        GameManager.Instance.OnFightStarted += TurnOffNextTurnButton;
        nextTurnButton.SetActive(false);
        winScreen.SetActive(false);
    }

    private void TurnOffNextTurnButton()
    {
        nextTurnButton.SetActive(false);
    }

    private void OnPickupTile(Card obj)
    {
        throw new NotImplementedException();
    }

    private void WinnerUi()
    {
        Debug.Log($"winner: {Player.CurrentPlayer.Name}");
        currentTurnText.text = $"Winner: {Player.CurrentPlayer}";
        winScreen.SetActive(true);
        var playerText = playerCards.GetComponent<TextMeshProUGUI>();
        playerText.text = $"Winner: {Player.CurrentPlayer.Name}!";
        
    }
 
    public void UpdateUI()
    {        
        currentTurnText.text = $"Turn: {GameManager.Instance.currentTurn}";
        currentPlayerText.text = Player.CurrentPlayer.Name;
        LoadCards(Player.CurrentPlayer);
    }
 
    void OnDestroy()
    {
        GameManager.Instance.TurnStarted -= UpdateUI;
    }

    public void EndTurn()
    {
        LoadCards(Player.CurrentPlayer);
        var state = Player.CurrentPlayer.PlayerObject.GetComponent<PlayerController>().playerState;
        var recentState = Player.CurrentPlayer.PlayerObject.GetComponent<PlayerController>().recentPlayerState;
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
            case PlayerState.Stunned:
                nextTurnButton.SetActive(true);
                break;
            case PlayerState.Walking:
                //nextTurnButton.SetActive(true);
                break;
        }

        /*switch (recentState)
        {
            case PlayerState.Stunned:
                nextTurnButton.SetActive(true);
                break;
        }*/
       
      
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

        float spacing = 75f;
        int cardCount = player.playerCards.Count;
        
        float totalWidth = (cardCount -1) * spacing;
        float startX = playerCards.transform.position.x - totalWidth / 2;
        
        for (int i = 0; i < player.playerCards.Count; i++)
        {
            _cards.Add(Instantiate(player.playerCards[i].cardPrefab,
                new Vector3(startX + i * spacing,
                    playerCards.transform.position.y,
                    playerCards.transform.position.z
                    )
                , Quaternion.identity, playerCards.transform)
            );
            
            _cards[i].GameObject().name = player.playerCards[i].nameText;
            
            if (_cards[i].TryGetComponent(out UICardUsage cardUsage))
            {
                cardUsage.SetCard(player.playerCards[i], player);
            }
        }
      
    }
    
 
}
