using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager GameManager;

    private void Start()
    {
        GameManager = GameManager.Instance;
        GameManager.TurnStarted += OnTurnStarted;
    }

    private void OnTurnStarted(GameManager.TurnStatedData data)
    {
        if (data.Player.PlayerObcject == gameObject)
        {
            
        }
    }

    private void OnDestroy()
    {
        GameManager.TurnStarted -= OnTurnStarted;
    }
}

