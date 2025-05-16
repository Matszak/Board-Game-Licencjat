using System;
using System.Collections;
using Cards;
using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    public bool isActive;
    private Player _selectedPlayer;
    private Player _selectingPlayer;
    private Color _tempColor;
    private Renderer _renderer;
    
    private Material currentPlayerMaterial;
    private Material mouseOverMaterial;
    
    private static readonly int OutLineBoolean = Shader.PropertyToID("_TurnOn");
    private static readonly int OutLineColor = Shader.PropertyToID("_OutLineColor");
    
    private void OnEnable()
    {
        GameManager.Instance.OnInvokeSelection += TurnOnSelection;
        GameManager.Instance.OnCardPlayerSelected += TurnSelectionOff;
     
   
    }

    private void Start()
    {
        _renderer = GetComponentInChildren<Renderer>();
        currentPlayerMaterial = _renderer.materials[1];
    }

    private void TurnSelectionOff(Player  selectedPlayer)
    {
        _selectedPlayer = selectedPlayer;
        isActive = false;
        
        if (gameObject.GetComponent<PlayerController>().CurrentPlayer == _selectingPlayer)
        {
            SetOutLine(currentPlayerMaterial,1,Color.white);
            return;
        }
        
        SetOutLine(currentPlayerMaterial,0,Color.white);
   
    }
    
    private void TurnOnSelection(Player player)
    {
        _selectingPlayer = player;
        isActive = true;
        
        if (gameObject.GetComponent<PlayerController>().CurrentPlayer == _selectingPlayer)
        {
            SetOutLine(currentPlayerMaterial,1,Color.white);
            return;
        }
        
        SetOutLine(currentPlayerMaterial,1,Color.red);
    }

 

    private void OnMouseDown()
    {
        if (!gameObject.TryGetComponent(out PlayerController player) || !isActive) return;

        if (gameObject.GetComponent<PlayerController>().CurrentPlayer == _selectingPlayer)
        {
            return;
        }
        Debug.Log($"Clicked on: {player.CurrentPlayer.Name}");
        GameManager.Instance.PlayerIsSelected(player.CurrentPlayer);
         
    }

    private void OnMouseOver()
    {
        if(!isActive) return;   
        if(gameObject == _selectingPlayer.PlayerObject) return;
        
        SetOutLine(currentPlayerMaterial,1,Color.green);
    }

    private void OnMouseExit()
    {
        if(!isActive) return;   
        if (gameObject.GetComponent<PlayerController>().CurrentPlayer == _selectingPlayer)
        {
            return;
        }
        SetOutLine(currentPlayerMaterial,1,Color.red);
    }

    private void SetOutLine(Material material,int boolean, Color color)
    {
        material.SetColor(OutLineColor, color);
        material.SetFloat(OutLineBoolean, boolean);
    }
    
    private void OnDisable()
    {
        if (GameManager.Instance == null) return;
        GameManager.Instance.OnInvokeSelection -= TurnOnSelection;
        GameManager.Instance.OnCardPlayerSelected -= TurnSelectionOff;
    }
}
 