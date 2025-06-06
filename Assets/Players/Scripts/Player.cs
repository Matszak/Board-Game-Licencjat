using System;
using System.Collections.Generic;
using Cards.EnemyCards;
using UnityEngine;

[Serializable]
public class Player
{
    public static Player CurrentPlayer { get; private set; }
    public static void SetPlayer(Player player)
    {
        CurrentPlayer?.Deactivate();
        CurrentPlayer = player;
        CurrentPlayer?.Activate();
    }
    public static PlayerController CurrentPlayerController => CurrentPlayer?.PlayerObject?.GetComponent<PlayerController>();
    public string Name;
    private GameObject _gameObject;
    public GameObject PlayerObject
    {
        get => _gameObject;
        set
        {
            _gameObject = value;
            if (Controller != null)
                Controller.Player = this;
            if (Movement != null)
                Movement.Player = this;
            if (Checker != null)
                Checker.Player = this;
            if (Selector != null)
                Selector.Player = this;

        }
    }
    public int TileIndex;
    public List<Card> playerCards = new List<Card>();
    public EnemyCard currentEnemyCard;
    public long LastRollValue;
    public PlayerSelector Selector => _gameObject.TryGetComponent<PlayerSelector>(out PlayerSelector selector) ? selector : null;
    public AdventureCardsChecker Checker => _gameObject.TryGetComponent<AdventureCardsChecker>(out AdventureCardsChecker checker) ? checker : null;
    public PlayerMovement Movement => _gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement movement) ? movement : null;
    public PlayerController Controller => _gameObject.TryGetComponent<PlayerController>(out PlayerController controller) ? controller : null;
    public void Activate()
    {
        Controller?.Activate();
        Selector.Activate();
    }
    public void Deactivate()
    {
        Controller?.Deactivate();
        Selector?.Deactivate();
    }
}