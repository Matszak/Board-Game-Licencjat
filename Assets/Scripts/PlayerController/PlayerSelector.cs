using System;
using System.Collections;
using Cards;
using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    public bool isActive;
    private Player _selectedPlayer;
    
    private void OnEnable()
    {
        GameManager.Instance.OnInvokeSelection += TurnOnSelection;
        GameManager.Instance.OnCardPlayerSelected += TurnSelectionOff;
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

    private void OnMouseDown()
    {
        if (!gameObject.TryGetComponent(out PlayerController player) || !isActive) return;
        
        Debug.Log($"Clicked on: {player.Player.Name}");
        GameManager.Instance.PlayerIsSelected(player.Player);
    }

    private void OnMouseOver()
    {
        if(!isActive) return;   
        
        Renderer renderer = GetComponentInChildren<Renderer>();
        renderer.material.color = Color.red;
    }

    private void OnMouseExit()
    {
        if(!isActive) return;   
        Renderer renderer = GetComponentInChildren<Renderer>();
        renderer.material.color = Color.yellow;
    }
}
 