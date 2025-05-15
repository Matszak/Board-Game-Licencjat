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
    Material[] materials;
    private static readonly int OutLineWidth = Shader.PropertyToID("_OutLineWidth");
    private static readonly int OutLineColor = Shader.PropertyToID("_OutLineColor");
    
    private void OnEnable()
    {
        GameManager.Instance.OnInvokeSelection += TurnOnSelection;
        GameManager.Instance.OnCardPlayerSelected += TurnSelectionOff;
     
   
    }

    private void Start()
    {
        _renderer = GetComponentInChildren<Renderer>();
        materials = _renderer.materials;
    }

    private void TurnSelectionOff(Player  selectedPlayer)
    {
        _selectedPlayer = selectedPlayer;
        isActive = false;
        materials[1].SetFloat(OutLineWidth, 0.188f);
        materials[1].SetColor(OutLineColor, Color.white);
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
        Debug.Log($"Clicked on: {player.CurrentPlayer.Name}");
        GameManager.Instance.PlayerIsSelected(player.CurrentPlayer);
        _renderer.material.color = _tempColor;
    }

    private void OnMouseOver()
    {
        if(!isActive) return;   
        materials[1].SetFloat(OutLineWidth, 0.188f);
        materials[1].SetColor(OutLineColor, Color.red);
    }

    private void OnMouseExit()
    {
        if(!isActive) return;   
        materials[1].SetFloat(OutLineWidth, 0.0f);
        materials[1].SetColor(OutLineColor, Color.white);
    }
}
 