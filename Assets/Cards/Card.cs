using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Card", menuName = "Card", order = 1)]
public class Card :ScriptableObject 
{
    public GameObject cardPrefab;
    public Sprite CardImage;
    public string NameText;
    public string DescriptionText;
    
    public event Action<Player> OnCardCompleted;

    protected void CompleteCard(Player player)
    {
        OnCardCompleted?.Invoke(player);
    }

    public virtual void TriggerCard(Player player)
    {
        
    }
    
}
