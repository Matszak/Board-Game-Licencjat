using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player  
{
    public string Name;
    public GameObject PlayerObject;
    public int TileIndex;
    public List<Card> playerCards = new List<Card>();
}