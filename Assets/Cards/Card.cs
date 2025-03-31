using System;
using System.Collections;
using System.Collections.Generic;
using Cards;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Card", menuName = "Card", order = 1)]
public abstract class Card :ScriptableObject 
{
     
    public GameObject cardPrefab;
    [FormerlySerializedAs("CardImage")] public Sprite cardImage;
    [FormerlySerializedAs("NameText")] public string nameText;
    [FormerlySerializedAs("DescriptionText")] public string descriptionText;
    
    public event Action<Player> OnCardCompleted;

    protected void CompleteCard(Player player)
    {
        OnCardCompleted?.Invoke(player);
    }

    public abstract void TriggerCard(Player currentPlayer);
    
 
}
