using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class ControllerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentTurnText;
    
    [SerializeField] private TextMeshProUGUI currentPlayerText;
    
    [SerializeField] private GameObject nextTurnButton;
    
    private Player _player;
    
    
    private void Start()
    {
        GameManager.Instance.TurnStarted += OnUIUpdated;
        GameManager.Instance.OnTurnEnded += EndTurn;
        nextTurnButton.SetActive(false);
    }

    private void OnUIUpdated(GameManager.TurnStatedData obj)
    {
        currentTurnText.text = $"Turn: {obj.Turn}";
        currentPlayerText.text = obj.Player.Name;
        _player = obj.Player;
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
}
