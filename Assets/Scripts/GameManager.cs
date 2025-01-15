using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] playersArray;
    public enum GamePhase
    {
        Move = 0,
        PickUpCard = 1,
        Fight = 2,
    }

    public GamePhase CurrentGamePhase;
    
    public Dictionary<GameObject, int> playersID = new Dictionary<GameObject, int>();

    public void Start()
    {
        CurrentGamePhase = GamePhase.Move;
        for (int i = 0; i < playersArray.Length; i++)
        {
            playersID.Add(playersArray[i], i);
        }
    }

    public void Update()
    {
        foreach (var player in playersID)
        {
            switch (CurrentGamePhase)
            {
                case GamePhase.Move:
                    break;
                case GamePhase.PickUpCard:
                    break;
                case GamePhase.Fight:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }
    }
}
