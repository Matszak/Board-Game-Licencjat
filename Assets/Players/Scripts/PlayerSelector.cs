using System;
using System.Collections;
using Cards;
using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    public bool IsActive;
    private Material currentPlayerMaterial => _renderer.materials[1];
    private static readonly int OutLineBoolean = Shader.PropertyToID("_TurnOn");
    private static readonly int OutLineColor = Shader.PropertyToID("_OutLineColor");

    public Player Player { get; set; }
    Renderer _renderer => GetComponentInChildren<Renderer>();
    private void OnEnable()
    {
    }

    public void Activate()
    {
        GameManager.Instance.OnInvokeSelection += TurnSelectionOn;
        GameManager.Instance.OnCardPlayerSelected += _ => TurnSelectionOff();
        TurnSelectionOn(Color.white);
    }

    public void Deactivate()
    {
        GameManager.Instance.OnInvokeSelection -= TurnSelectionOn;
        GameManager.Instance.OnCardPlayerSelected -= _ => TurnSelectionOff();
        TurnSelectionOff();
    }

    public void TurnSelectionOff()
    {
        SetOutLine(currentPlayerMaterial, 0, Color.white);

    }

    public void TurnSelectionOn(Color color)
    {
        SetOutLine(currentPlayerMaterial, 1, color);
    }



    private void OnMouseDown()
    {
        if (!gameObject.TryGetComponent(out PlayerController player) || !IsActive) return;

        if (Player.CurrentPlayer == Player)
        {
            return;
        }

        if (Player.Controller.magicShield)
        {
            return;
        }
        Debug.Log($"Clicked on: {Player.CurrentPlayer.Name}");
        GameManager.Instance.PlayerIsSelected(Player);
        IsActive = false;
    }

    private void OnMouseOver()
    {
        if (!IsActive) return;
        if (Player.CurrentPlayer == Player) return;

        SetOutLine(currentPlayerMaterial, 1, Color.green);
    }

    private void OnMouseExit()
    {
        if (!IsActive) return;
        if (Player.CurrentPlayer == Player)
        {
            return;
        }
        SetOutLine(currentPlayerMaterial, 1, Color.red);
    }

    private void SetOutLine(Material material, int boolean, Color color)
    {
        material.SetColor(OutLineColor, color);
        material.SetFloat(OutLineBoolean, boolean);
    }

    private void OnDisable()
    {
        if (GameManager.Instance == null) return;
        GameManager.Instance.OnInvokeSelection -= TurnSelectionOn;
        GameManager.Instance.OnCardPlayerSelected -= _ => TurnSelectionOff();
    }
}
