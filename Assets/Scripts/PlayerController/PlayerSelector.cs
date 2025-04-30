using System;
using System.Collections;
using Cards;
using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    public bool isActive;
    private Player _selectedPlayer;
    private Color _tempColor;
    private Renderer _renderer;
    
    private void OnEnable()
    {
        GameManager.Instance.OnInvokeSelection += TurnOnSelection;
        GameManager.Instance.OnCardPlayerSelected += TurnSelectionOff;
        _renderer = GetComponentInChildren<Renderer>();
    }
    
    private void TurnSelectionOff(Player  selectedPlayer)
    {
        _selectedPlayer = selectedPlayer;
        isActive = false;
    }
    
    private void TurnOnSelection(Player player)
    {
        isActive = true;
    }

    private void OnMouseEnter()
    {
        _tempColor = _renderer.material.color;
    }

    private void OnMouseDown()
    {
        if (!gameObject.TryGetComponent(out PlayerController player) || !isActive) return;
        Debug.Log($"Clicked on: {player.Player.Name}");
        GameManager.Instance.PlayerIsSelected(player.Player);
        _renderer.material.color = _tempColor;
    }

    private void OnMouseOver()
    {
        if(!isActive) return;   
        _renderer.material.color = Color.red;
    }

    private void OnMouseExit()
    {
        if(!isActive) return;   
        _renderer.material.color = _tempColor;
    }
}
 